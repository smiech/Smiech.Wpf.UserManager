using System.Collections.Generic;

namespace Smiech.Wpf.UserManager.Services.Interfaces.Models
{
    public class UserResponse
    {
        public int Code { get; set; }
        public Metadata Metadata { get; set; }
        public IEnumerable<User> Data { get; set; }
    }
}
