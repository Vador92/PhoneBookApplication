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

        public ContactOverviewViewModel()
        {
            AddCommand = new Command(OnAddCommand);
            ContactSelectedCommand = new Command((data) =>
            {
                OnContactSelectedCommand(data);
            });
        }
        
        
        private void OnContactSelectedCommand(Object contact)
        {
            Contact selectedContact = contact as Contact;
            App.Current.MainPage.Navigation.PushAsync(new ContactDetailView(selectedContact));
        }

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