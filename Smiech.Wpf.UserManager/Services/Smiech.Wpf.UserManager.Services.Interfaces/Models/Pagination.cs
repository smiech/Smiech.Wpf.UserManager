﻿namespace Smiech.Wpf.UserManager.Services.Interfaces.Models
{
    public class Pagination
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}