using ApplicationCore.Data;
using MediatR;

namespace ApplicationCore.Behaviours;

public sealed class UnitOfWorkBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;
    public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (typeof(TRequest).Name.EndsWith("Cmd"))
            return await next();

        var response = await next();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return response;
    }
}
