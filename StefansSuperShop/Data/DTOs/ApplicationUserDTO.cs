namespace StefansSuperShop.Data.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool NewsletterIsActive { get; set; }
    }
}
