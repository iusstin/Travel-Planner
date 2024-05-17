namespace Domain.Exceptions;

public class DuplicateEntityException(string message) : Exception(message)
{ }
