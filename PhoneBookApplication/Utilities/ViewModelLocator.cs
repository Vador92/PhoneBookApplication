using System;
using System.Collections.Generic;
using System.Text;
using PhoneBookApplication.ViewModels;
using PhoneBookApplication.Views;

namespace PhoneBookApplication.Utilities
{
    class ViewModelLocator
    {
        //creates a new instance of the contactoverviewmodel which links to the actions performed on the contactoverviewpage
        public static ContactOverviewViewModel ContactOverviewViewModel { get; set; }
        = new ContactOverviewViewModel();

        //creates a new instance of the contactdetailpage which links to the actions performed on the detail page
        public static ContactDetailViewModel ContactDetailViewModel{ get; set; } 
        = new ContactDetailViewModel();

    }
}
