namespace dotnet_mongodb.Application.Shared
{
    public enum EDomainCode
    {
        Success = 0,
        InternalError = 1,
        NotFound = 2,
        InvalidInput = 3,
        AlreadyExists = 4,
        Unauthorized = 5
    }
}