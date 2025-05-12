namespace PARKE_Landing_Page.Services.DTOs
{
    public class ResetPasswordRequest
    {
        public string Token { get; set; }        
        public string NewPassword { get; set; }  
        public string ConfirmPassword { get; set; }
    }
}
