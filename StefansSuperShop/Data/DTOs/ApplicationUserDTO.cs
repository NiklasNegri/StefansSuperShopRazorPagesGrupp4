namespace StefansSuperShop.Data.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
        public bool NewsletterIsActive { get; set; }
    }
}
