using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhoneBookApplication.CustomControls
{
    public class InfiniteListView : ListView
    {
        /// <summary>
        /// Respresents the command that is fired to ask the view model to load additional data bound collection.
        /// </summary>
        [Obsolete]
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create<InfiniteListView, ICommand>(bp => bp.LoadMoreCommand, default(ICommand));



        /// <summary>
        /// Gets or sets the command binding that is called whenever the listview is getting near the bottomn of the list, and therefore requiress more data to be loaded.
        /// </summary>
        [Obsolete]
        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }



        // <summary>
        /// Respresents the command that is fired to ask the view model to load additional data bound collection.
        /// </summary>
        public static readonly BindableProperty ItemClickCommandProperty = BindableProperty.Create<InfiniteListView, ICommand>(bp => bp.ItemClickCommand, default(ICommand));




        public ICommand ItemClickCommand
        {
            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
            set { this.SetValue(ItemClickCommandProperty, value); }
        }



        /// <summary>
        /// Creates a new instance of a <see cref="InfiniteListView" />
        /// </summary>
        public InfiniteListView()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
            ItemTapped += InfiniteListView_OnItemTapped;
        }




        void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;
            if (items.Count > 0)
            {
                if (items != null && e.Item == items[items.Count - 1])
                {
                    if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                        LoadMoreCommand.Execute(null);
                }
            }
        }



        private void InfiniteListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && this.ItemClickCommand != null && this.ItemClickCommand.CanExecute(e))
            {
                this.ItemClickCommand.Execute(e.Item);
                this.SelectedItem = null;
            }
        }



        public InfiniteListView(ListViewCachingStrategy strategy) : base(strategy)
        {
            try
            {
                ItemAppearing += InfiniteListView_ItemAppearing;
                ItemTapped += InfiniteListView_OnItemTapped;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
