using Smiech.Wpf.UserManager.Services.Interfaces;

namespace Smiech.Wpf.UserManager.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
