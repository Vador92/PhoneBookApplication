using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookApplication.CustomControls;
using PhoneBookApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PhoneBookApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactOverviewView : ContentPage
	{

        //creates a new contact overview page, where it signals the actions and behaviors from the viewmodel
        ContactOverviewViewModel viewModel;
		public ContactOverviewView ()
		{
			InitializeComponent ();
            this.BindingContext = viewModel = new ContactOverviewViewModel ();
		}

        //displays the database as contacts on the homepage
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnGetContactsCommand();
        }
    }
}