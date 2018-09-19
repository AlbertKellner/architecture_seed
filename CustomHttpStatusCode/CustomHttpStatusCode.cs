namespace CustomHttpStatusCode
{
    public enum CustomHttpStatusCode
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        NotFound = 404,
        InternalServerError = 500,
        EntityValidation = 900,
        AlreadyExists = 901,
        MissingHeader = 902
    }
}