using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using PhoneBookApplication.Models;
using PhoneBookApplication.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;


namespace PhoneBookApplication.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ContactDetailViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private Contact _contact;

        public Contact Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }

        public void SetContact(Contact contact)
        {
            Contact = contact;
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand ChooseCommand { get; set; }

        public ICommand TakeCommand { get; set; }

        public ContactDetailViewModel()
        {

            UpdateCommand = new Command(OnUpdateCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            //ChooseCommand = new Command(OnChooseCommand);
            //TakeCommand = new Command(OnTakeCommand);

        }

        private async void OnUpdateCommand()
        {
            if (Contact == null)
            {
                //await App.Database.SaveContactAsync
                var newContact = new Models.Contact
                {
                    Id = Guid.NewGuid(),
                    ProfilePicture = "https://tse2.mm.bing.net/th?id=OIP.sqXwVpmQneP_AsTdxn9khgHaEo&pid=Api&P=0&h=180",
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Email = string.Empty,
                    Address = string.Empty
                };

                if (!string.IsNullOrWhiteSpace(newContact.FirstName)
                && !string.IsNullOrWhiteSpace(newContact.LastName)
                && !string.IsNullOrWhiteSpace(newContact.Email)
                && !string.IsNullOrWhiteSpace(newContact.Address))
                {
                    

                    newContact.FirstName = Contact.FirstName;
                    newContact.LastName = Contact.LastName;
                    newContact.Email = Contact.Email;
                    newContact.Address = Contact.Address;
                }

                await App.Database.SaveContactAsync(newContact);

                Contact = newContact;
            }
            else
            {
                await App.Database.UpdateContactAsync(Contact);
            }

            await App.Current.MainPage.Navigation.PopAsync() ;
        }

        private async void OnDeleteCommand()
        {
            if (Contact != null)
            {
                App.Database.DeleteContactAsync(Contact);
            }

            await App.Current.MainPage.Navigation.PopAsync();
        }

        //private async void OnChooseCommand()
        //{
        //    try
        //    {
        //        var result = await Xamarin.Essentials.MediaPicker.PickPhotoAsync(new Xamarin.Essentials.MediaPickerOptions
        //        {
        //            Title = "Choose photo from your gallery"
        //        });

        //        var stream = await result.OpenReadAsync();

        //        if (stream != null)
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                await stream.CopyToAsync(ms);
        //                byte[] imageBytes = ms.ToArray();
        //                string base64String = Convert.ToBase64String(imageBytes);

        //                // Set the base64 string as the ProfilePicture property
        //                Contact.ProfilePicture = base64String;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception, e.g., display an error message
        //        Console.WriteLine($"Error selecting photo: {ex.Message}");
        //    }
        //}


        //public async void OnTakeCommand()
        //{
        //    try
        //    {
        //        var result = await Xamarin.Essentials.MediaPicker.CapturePhotoAsync();
        //        var stream = await result.OpenReadAsync();

        //        if (stream != null)
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                stream.CopyTo(ms);
        //                byte[] imageBytes = ms.ToArray();
        //                string base64String = Convert.ToBase64String(imageBytes);

        //                // Set the base64 string as the ProfilePicture property
        //                Contact.ProfilePicture = base64String;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that may occur during image capture
        //        Console.WriteLine($"Error capturing photo: {ex.Message}");
        //    }
        //}



    }
}