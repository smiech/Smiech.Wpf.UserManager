using System.Collections.Generic;

namespace Smiech.Wpf.UserManager.Modules.Main.ViewModels.Lists
{
    public class StatusList : List<string>
    {
        public StatusList()
        {
            this.Add("active");
            this.Add("inactive");
        }
    }
}