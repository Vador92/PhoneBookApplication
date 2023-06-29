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
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.IO;

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
            if (contact != null)
            {
                Contact = contact;
                _isNew = false;
            }
            else 
            { 
               Contact = new Contact();
                _isNew = true;
            }
    
        }

        private bool _isNew = false;

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand ChooseCommand { get; set; }

        public ICommand TakeCommand { get; set; }

        public ContactDetailViewModel()
        {

            UpdateCommand = new Command(OnUpdateCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            ChooseCommand = new Command(OnChooseCommand);
            //TakeCommand = new Command(OnTakeCommand);

        }

      
        
        private async void OnUpdateCommand()
        {
            if (_isNew)
            {
                Contact.Id = Guid.NewGuid();
                if(Contact.ProfilePicture == null)
                {
                    Contact.ProfilePicture = "https://www.pngarts.com/files/10/Default-Profile-Picture-Transparent-Image.png";
                }
                // Save the new contact
                await App.Database.SaveContactAsync(Contact);

                // Assign the new contact to the Contact property
            }
            else
            {
                await App.Database.UpdateContactAsync(Contact);
            }            

            await App.Current.MainPage.Navigation.PopAsync();
        }

        public async void OnDeleteCommand()
        {
            if (Contact != null)
            {
                App.Database.DeleteContactAsync(Contact);
            }

            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnChooseCommand()
        {
            try
            {
                var status = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.Photos>();
                if(status != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    var allowPic = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.Photos>();
                    if(allowPic == Xamarin.Essentials.PermissionStatus.Granted)
                    {
                        status = Xamarin.Essentials.PermissionStatus.Granted;
                    }
                }
                if(status == Xamarin.Essentials.PermissionStatus.Granted)
                {
                    var result = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

                    var stream = await result.OpenReadAsync();

                    if (stream != null)
                    {
                        Contact.ProfilePicture = result.FullPath;
                        OnPropertyChanged(nameof(Contact));
                    }
                }  
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., display an error message
                Console.WriteLine($"Error selecting photo: {ex.Message}");
            }
        }


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