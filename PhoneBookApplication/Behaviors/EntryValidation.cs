using System.Collections.Generic;
using Xamarin.Forms;
namespace PhoneBookApplication.Behaviors
{
    public class EntryValidation : Behavior<Entry>
    {

        public static readonly BindableProperty EntriesProperty =
            BindableProperty.Create(nameof(Entries), typeof(ICollection<Entry>), typeof(EntryValidation), null);

        public ICollection<Entry> Entries
        {
            get => (ICollection<Entry>)GetValue(EntriesProperty);
            set => SetValue(EntriesProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            bool isValid = !string.IsNullOrWhiteSpace(entry.Text);
            entry.BackgroundColor = isValid ? Color.Default : Color.OrangeRed;
        }

        public void AttachEntries()
        {
            if (Entries != null)
            {
                foreach (var entry in Entries)
                {
                    OnAttachedTo(entry);
                    OnEntryTextChanged(entry, null); 
                }
            }

        }


        public void DetachEntries()
        {
            if (Entries != null)
            {
                foreach (var entry in Entries)
                {
                    OnDetachingFrom(entry);
                }
            }
        }
    }
}

