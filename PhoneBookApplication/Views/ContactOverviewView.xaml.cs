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
	public partial class ContactOverviewView : ContentPage
	{
        ContactOverviewViewModel viewModel;
		public ContactOverviewView ()
		{
			InitializeComponent ();
            this.BindingContext = viewModel = new ContactOverviewViewModel ();
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await App.Database.GetContactsAsync();
        }
    }
}


        ////rest of the code below is for viewmodel for contactdetails
        ////code for saving a new contact (but first navigate to detail page
        //async void OnAddClicked(Object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(FirstName.Text)
        //        && !string.IsNullOrWhiteSpace(LastName.Text)
        //        && !string.IsNullOrWhiteSpace(Email.Text)
        //        && !string.IsNullOrWhiteSpace(Address.Text))
        //    {
        //        await App.Database.SaveContactAsync(new Models.Contact
        //        {
        //            Id = Guid.NewGuid(),
        //            ProfilePicture = "https://tse2.mm.bing.net/th?id=OIP.sqXwVpmQneP_AsTdxn9khgHaEo&pid=Api&P=0&h=180",
        //            FirstName = FirstName.Text,
        //            LastName = LastName.Text,
        //            Email = Email.Text,
        //            Address = Address.Text,
        //        });

        //        FirstName.Text = string.Empty;
        //        LastName.Text = string.Empty;
        //        Email.Text = string.Empty;
        //        Address.Text = string.Empty;

        //        collectionView.ItemsSource = await
        //            App.Database.GetContactsAsync();

        //    }
        //}

        ////async void OnChooseClicked(Object sender, EventArgs e)
        ////{
        ////    var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
        ////    {
        ////        Title = "Please pick a profile photo"
        ////    });

        ////    var stream = await result.OpenReadAsync();


        ////     = ImageSource.FromStream(() => stream);

        ////}

        ////void OnTakeClicked(Object sender, EventArgs e)
        ////{

        ////}