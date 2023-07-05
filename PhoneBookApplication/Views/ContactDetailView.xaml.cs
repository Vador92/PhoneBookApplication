using PhoneBookApplication.Models;
using PhoneBookApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneBookApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailView : ContentPage
	{
		public ContactDetailView(Contact contact = null)
		{
			InitializeComponent ();
			this.BindingContext = ViewModelLocator.ContactDetailViewModel;
            ViewModelLocator.ContactDetailViewModel.SetContact(contact);

        }

    }
}