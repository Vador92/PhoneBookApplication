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
        //initializing the setter properties for the view
        private Contact _contact;
        public Contact Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }


        //If we pass in a nonnull, we make that as the set contact that we are going to be updating based on the user inputs
        //else we create a new contact in which we tell the program to make the new contact based on user inputs, but also making sure that they fill in the required fields
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

        //used as a means of determining whether we save or create using the database commands
        private bool _isNew = false;

        public bool _readOnly = true;
        public bool _isEditable = false;



        //icommands are implemented for the different operations
        //update -> used to update an existing contact with altered details of what the user changes
        //delete -> used to delete that setContact from the database, if the contact exists.
        //choose -> allows user to access gallery of phone and choose an existing image from there
        //toggle -> allows user to edit the detail page
        //take -> allows user to use phone camera to take a profile picture
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChooseCommand { get; set; }
        public ICommand ToggledCommand { get; set; }
        public ICommand TakeCommand { get; set; }


        //initalizes the commands, providing functionality between the front and backend
        public ContactDetailViewModel()
        {

            UpdateCommand = new Command(OnUpdateCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            ChooseCommand = new Command(OnChooseCommand);
            TakeCommand = new Command(OnTakeCommand);
            ToggledCommand = new Command(OnToggledCommand);
        }

      
        //if contact is new, we save (create it) in the database, else we update the existing contact in the database, and then we pop the page (move back to overview)
        private async void OnUpdateCommand()
        {
            if (_isNew)
            {
                //required to create new contact
                if(!string.IsNullOrWhiteSpace(Contact.FirstName) 
                    && !string.IsNullOrWhiteSpace(Contact.LastName)
                    && !string.IsNullOrWhiteSpace(Contact.Email)
                    && !string.IsNullOrWhiteSpace(Contact.Address))
                {
                    Contact.Id = Guid.NewGuid();
                    if (Contact.ProfilePicture == null)
                    {
                        Contact.ProfilePicture = "https://www.pngarts.com/files/10/Default-Profile-Picture-Transparent-Image.png";
                    }
                    // Save the new contact
                    await App.Database.SaveContactAsync(Contact);
                }
            }
            else
            {
                await App.Database.UpdateContactAsync(Contact);
            }            

            await App.Current.MainPage.Navigation.PopAsync();
        }


        //deletes the contact if it exists in the database already
        public async void OnDeleteCommand()
        {
            if (Contact != null)
            {
                App.Database.DeleteContactAsync(Contact);
            }

            await App.Current.MainPage.Navigation.PopAsync();
        }


        //this checks the permissions for accessing photos and sends that image file info as the profile picture for the contact that the user is creating or updating
        public async void OnChooseCommand()
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


        //checks camera permissions and then allows that picture that was taken to be used as the contact profile picture
        public async void OnTakeCommand()
        {
            try
            {
                var status = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.Camera>();
                if(status != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    var allowedCamera = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.Camera>();
                    if (allowedCamera == Xamarin.Essentials.PermissionStatus.Granted)
                    {
                        status = Xamarin.Essentials.PermissionStatus.Granted;
                    }
                }
                if(status == Xamarin.Essentials.PermissionStatus.Granted)
                {
                    var result = await Xamarin.Essentials.MediaPicker.CapturePhotoAsync();
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
                // Handle any exceptions that may occur during image capture
                Console.WriteLine($"Error capturing photo: {ex.Message}");
            }
        }



        //enables the ability to edit the details of the page
        public void OnToggledCommand()
        {
            _readOnly = !_readOnly;
            _isEditable = !_isEditable;
        }



    }
}