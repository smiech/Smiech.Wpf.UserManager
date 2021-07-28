using System.Collections.Generic;

namespace Smiech.Wpf.UserManager.Services.Interfaces.Models
{
    public class UserResponse
    {
        public int Code { get; set; }
        public Metadata Meta { get; set; }
        public IEnumerable<User> Data { get; set; }
    }
}
