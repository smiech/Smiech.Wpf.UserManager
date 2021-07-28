namespace Smiech.Wpf.UserManager.Services.Interfaces.Models
{
    public class SingleUserResponse
    {
        public int Code { get; set; }
        public Metadata Meta { get; set; }
        public User Data { get; set; }
    }
}