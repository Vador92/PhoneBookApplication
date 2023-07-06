using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhoneBookApplication.Behaviors
{
    
        public class EntryValidationBehavior : Behavior<Entry>
        {
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

            void OnEntryTextChanged(object sender, TextChangedEventArgs args)
            {
                string newTextValue = args.NewTextValue;
                bool isValid = !string.IsNullOrWhiteSpace(newTextValue);
                ((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.OrangeRed;
            }
        }
}
