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
        //initalizes the binding context and connecting the viewmodel
		public ContactDetailView(Contact contact = null)
		{
			InitializeComponent ();
			this.BindingContext = ViewModelLocator.ContactDetailViewModel;
            ViewModelLocator.ContactDetailViewModel.SetContact(contact);

        }
		

        //in the works
		public void Handle_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(FirstNameEntry.Text))
			{
				VisualStateManager.GoToState(FirstNameEntry, "Invalid");
			}
			else
			{
				VisualStateManager.GoToState(FirstNameEntry, "Normal");
			}

            if (string.IsNullOrWhiteSpace(LastNameEntry.Text))
            {
                VisualStateManager.GoToState(LastNameEntry, "Invalid");
            }
            else
            {
                VisualStateManager.GoToState(LastNameEntry, "Normal");
            }

            if (string.IsNullOrWhiteSpace(EmailEntry.Text))
            {
                VisualStateManager.GoToState(EmailEntry, "Invalid");
            }
            else
            {
                VisualStateManager.GoToState(EmailEntry, "Normal");
            }

            if (string.IsNullOrWhiteSpace(AddressEntry.Text))
            {
                VisualStateManager.GoToState(AddressEntry, "Invalid");
            }
            else
            {
                VisualStateManager.GoToState(AddressEntry, "Normal");
            }
        }
    }
}