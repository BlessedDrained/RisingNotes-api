namespace MainLib.Api.Auth.Constant;

public static class RoleConstants
{
    public const string Admin = "admin";
    public const string Author = "author";
    public const string User = "user";

    public static bool IsRoleCorrect(string roleName)
    {
        return roleName is Admin or Author or User;
    }
}