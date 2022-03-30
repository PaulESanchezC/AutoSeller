namespace Services.EmailServices.EmailTemplates;

public static class EmailTemplate
{
    public const string MessageCallbackAnchorValue = @"
<head>
<style>
    @import url('https://fonts.googleapis.com/css2?family=Comfortaa:wght@300&display=swap');
    
 table,tbody,tr,td,span,a{0}font-family: 'Comfortaa', cursive ;{1}

</style>
</head>
<table width='100%' height='100%' style='padding:0px;margin:50px auto; min-width:400px;'>
    <tbody>
        <tr>
            <td width = '100%' align='center' style='margin:0px auto;'>
                <table width = '100%' align='center' style='margin:auto'>
                    <tbody>
                        <tr>
                            <td>
                                <table align = 'center' width='400px' style='margin: auto;
                                border-top: 2px solid #11a8ab; border-bottom: 2px solid #11a8ab;'>
                                    <tbody>
                                        <tr>
                                            <td align='left' style='margin: auto; font-size:18px;'>
                                                <a href = 'https://localhost:44304/Home' > AutoSeller </ a >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align='left' style='margin: auto; border-bottom: 2px solid #11a8ab;font-size: 24px'>
                                                Developed by: Paul Sanchez.
                                            </td>
                                        </tr>
                                        <tr><td style = 'margin: auto;' > &nbsp;</td></tr>
                                        <tr>
                                            <td align = 'center' style='padding: 10px 15px; margin: 15px; font-weight: bold; line-height: 36px;
                                            margin: auto; font-size: 36px;'>
                                                <span style = 'color: #11a8ab' > AutoSeller </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align='left' height='48px' style='padding: 6px 8px; margin: 10px; margin: auto; color: #292f33; 
                                                font-size: 24px;'>
                                                {2}
                                            </td>
                                        </tr>
                                        <tr><td height = '20px' style='margin: auto; '> &nbsp; </td></tr>
                                        <tr>
                                            <td align = 'center' style='margin:auto; line-height: 18px;'>
                                                <a href = '{3}' target='_blank'                                                     
                                                   style='text-decoration: none; border-radius: 50px; padding: 12px 18px;
                                                      border: 1px solid #11a8ab; display: inline-block; font-size:20px;'>
                                                    {4}
                                                </a>
                                            </td>
                                        </tr>
                                        <tr><td height = '20px' style='margin: auto;'> &nbsp; </td></tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>";
    public const string IntentSenderContactInfo = @"
            AutoSeller Someone is interested in your vehicle: 
            <br/> 
            <br/> 
            First Name: <span style='float:right; font-size: 18px;'>{0}</span> 
            <br/>
            Last Name : <span style='float:right; font-size: 18px;'>{1}</span> 
            <br>
            Phone: <span style='float:right; font-size: 18px;'>{2}</span> 
            <br>
            Email: <span style='float:right; font-size: 18px;'>{3}</span> 
            <br>";
    
    public static class Title
    {
        public const string EmailConfirmation = "AutoSeller Registration Confirmation";
        public const string IntentReceived = "AutoSeller Someone is interested in your vehicle";
        public const string ForgotPassword = "AutoSeller Forgot Password?, Reset it";
    }
    public static class Message
    {
        public const string EmailConfirmation = "Welcome to AutoSeller, thank you for using our services. \nTo activate your account, please click the link below";
        public const string ForgotPassword = @"To reset your password, please click the button below,
        this operation can only be done once per email. as soon as you click it this email will no longer be valid";
    }
    public static class Anchor
    {
        public const string EmailConfirmation = "Activate Account";
        public const string IntentReceived = "Go to AutoSeller";
        public const string ForgotPassword = "Reset Password";
    }

}