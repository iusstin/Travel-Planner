namespace Domain;

public enum Currency : byte
{
    Euro = 0,
    USD = 1,
    GBP = 2,
}

public enum DestinationType: byte
{
    City = 0,
    Country = 1,
    Island = 2,
    Region = 3,
}

public enum CapabilityType : byte
{
    Read = 0, Write = 1, Denied = 2,
}

