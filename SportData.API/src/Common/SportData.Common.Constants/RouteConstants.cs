namespace SportData.Common.Constants;

public static class RouteConstants
{
    public const string API_DEFAULT_ROUTE = "/api/[controller]";

    public const string AUTHENTICATE_REGISTER = "register";
    public const string AUTHENTICATE_LOGIN = "login";
    public const string AUTHENTICATE_REFRESH_TOKEN = "refresh-token";
    public const string AUTHENTICATE_REVOKE = "revoke/{username}";
    public const string AUTHENTICATE_REVOKE_ALL = "revoke-all";

    public const string CONVERTER_START = "Start";

    public const string CRAWLER_START = "start/{id}";
}