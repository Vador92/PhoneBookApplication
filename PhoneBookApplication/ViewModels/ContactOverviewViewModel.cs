using PhoneBookApplication.Models;
using PhoneBookApplication.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Contact = PhoneBookApplication.Models.Contact;

namespace PhoneBookApplication.ViewModels
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class ContactOverviewViewModel : BaseViewModel
    {
        public ICommand ContactSelectedCommand { get; set; }
        public ICommand AddCommand { get; set; }

        //handles displaying the contacts on the homepage
        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts != value)
                {
                    _contacts = value;
                    OnPropertyChanged(nameof(Contacts));
                }
            }
        }

        //sets contact to the one that the user clicked on the overviewpage
        private Contact _SelectedContact;
        public Contact SelectedContact
        {
            get => _SelectedContact;
            set
            {
                _SelectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }

        //intialzies the commands needed to complete the task of adding and editing an existing contact
        public ContactOverviewViewModel()
        {
            AddCommand = new Command(OnAddCommand);
            ContactSelectedCommand = new Command((data) =>
            {
                OnContactSelectedCommand(data);
            });
        }
        
        //navigates to contactdetailviewpage with existing contact
        private void OnContactSelectedCommand(Object contact)
        {
            Contact selectedContact = contact as Contact;
            App.Current.MainPage.Navigation.PushAsync(new ContactDetailView(selectedContact));
        }

        //navigates to the contact detail view page, without an existing contact
        private void OnAddCommand()
        {
            App.Current.MainPage.Navigation.PushAsync(new ContactDetailView());
        }

        //creates new observable collection on the homepage, where user can click to access detail page
        public async void OnGetContactsCommand()
        {
            var contacts = await App.Database.GetContactsAsync();
            Contacts = new ObservableCollection<Contact>(contacts);
        }

    }
}