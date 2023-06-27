using System;
using System.Collections.Generic;
using System.Text;
using PhoneBookApplication.ViewModels;
using PhoneBookApplication.Views;

namespace PhoneBookApplication.Utilities
{
    class ViewModelLocator
    {
        public static ContactOverviewViewModel ContactOverviewViewModel { get; set; }
        = new ContactOverviewViewModel();
        public static ContactDetailViewModel ContactDetailViewModel{ get; set; } 
        = new ContactDetailViewModel();

    }
}
