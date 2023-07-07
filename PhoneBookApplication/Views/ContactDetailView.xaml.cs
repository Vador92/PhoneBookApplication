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
using PhoneBookApplication.Behaviors;
namespace PhoneBookApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailView : ContentPage
	{
        //initalizes the binding context and connecting the viewmodel
		public ContactDetailView(Contact contact = null)
		{
			InitializeComponent ();
			this.BindingContext = ViewModelLocator.ContactDetailViewModel;
            ViewModelLocator.ContactDetailViewModel.SetContact(contact);

        }

        private void Validate(object sender, EventArgs e)
        {
            
            EntryValidation behavior = new EntryValidation();
            behavior.Entries = new List<Entry> { FirstNameEntry, LastNameEntry, EmailEntry, AddressEntry };
            behavior.AttachEntries();

        }


    }
}