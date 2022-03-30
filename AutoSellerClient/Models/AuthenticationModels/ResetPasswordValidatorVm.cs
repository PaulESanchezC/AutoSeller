namespace Models.AuthenticationModels;

public class ResetPasswordValidatorVm
{
    public string Email { get; set; }
    public string ResetPasswordToken { get; set; }
}