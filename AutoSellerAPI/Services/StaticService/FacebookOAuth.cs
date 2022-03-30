namespace Services.StaticService;

public class FacebookOAuth
{
    public const string TokenDebug = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
    public const string UserInfo = "https://graph.facebook.com/me?fields=first_name,last_name,email&access_token={0}";
}