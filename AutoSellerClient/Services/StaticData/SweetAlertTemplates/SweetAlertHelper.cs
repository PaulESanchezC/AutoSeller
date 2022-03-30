

namespace Services.StaticData.SweetAlertTemplates;

public static class SweetAlertHelper
{
    public static class Types
    {
        public static string Success = "success";
        public static string Error = "error";
        public static string Warning = "warning";
        public static string Info = "info";
    }

    public static class Titles
    {
        public static string RegisterSuccess = "Registered Successfully";
        public static string UpdateSuccess = "Updated Successfully";
        public static string EmailSent = "Email Sent";
        public static string InvalidOperation = "Invalid Operation";
        public static string TutorialCrudBlock = "Tutorial Accounts";
    }
    public static class Messages
    {
        public static string VehicleRegister = "Vehicle was registered successfully";
        public static string VehicleUpdate = "Vehicle was updated successfully";
        public static string EmailSent = "A confirmation email has been sent to the account {0}";
        public static string ProfileRegister = $"You have successfully created an account with us. {EmailSent}";
        public static string ProfileUpdate = "You have successfully updated your profile information";
        public static string PasswordUpdate = "You have successfully updated your password";
        public static string InvalidLogin = "Login unsuccessful";
        public static string ValidLogin = "Login successful";
        

        public static string InvalidEmailConfirmation = "There seems to be a problem with the email validation, it is expired";
        public static string EmailConfirmationRequired = "This account is not yet confirmed, please search your email for subject: AutoSeller Registration Confirmation";
        public static string InvalidResetPasswordToken = "There seems to be a problem with the email validation, it is expired, it could have been used already, and you could request another one. Or the account has only external login rights, try to log in with your external login provider. (eg. Facebook)";

        public static string TutorialCrudBlock = "Tutorial Accounts are protected from operations, the intentions is to keep the Portfolio in a healthy state. For more information, click on Tutorial on the side navigation menu. Thank you!";
    }
}