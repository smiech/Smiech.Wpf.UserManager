using System.Collections.Generic;

namespace Smiech.Wpf.UserManager.Modules.Main.ViewModels.Lists
{
    public class GenderList : List<string>
    {
        public GenderList()
        {
            this.Add("male");
            this.Add("female");
        }
    }
}