namespace SportData.Common.Constants;

public static class RouteConstants
{
    public const string API_DEFAULT_ROUTE = "/api/v{v:apiVersion}/[controller]";

    public const string AUTHENTICATE_REGISTER = "register";
    public const string AUTHENTICATE_LOGIN = "login";
    public const string AUTHENTICATE_REFRESH_TOKEN = "refresh-token";
    public const string AUTHENTICATE_REVOKE = "revoke/{username}";
    public const string AUTHENTICATE_REVOKE_ALL = "revoke-all";

    //public const string USERS_CREATE = "create";

    public const string TOKENS_CREATE_REFRESH = "create-refresh";

    public const string ADMINS_DELETE_REFRESH_TOKEN = "delete-refresh-token";
    public const string ADMINS_DELETE_REFRESH_TOKENS = "delete-refresh-tokens";

    public const string CONVERTER_START = "Start";

    public const string CRAWLER_START = "start/{id}";
}