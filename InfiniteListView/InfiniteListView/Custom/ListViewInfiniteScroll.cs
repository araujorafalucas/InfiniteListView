using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteListView.Custom
{
    public class ListViewInfiniteScroll : ListView
    {
        public static BindableProperty CanLoadMoreProperty = BindableProperty.Create(
                       "CanLoadMore",
                       typeof(bool),
                       typeof(ListView), false);

        public bool CanLoadMore
        {
            get
            {
                return (bool)GetValue(CanLoadMoreProperty);
            }
            set
            {
                if (this.CanLoadMore != value)
                {
                    base.SetValue(CanLoadMoreProperty, value);

                }

            }
        }

        public static BindableProperty LoadMoreCommandProperty = BindableProperty.Create(
                       "LoadMoreCommand",
                       typeof(ICommand),
                       typeof(ListView));
        public ICommand LoadMoreCommand
        {
            get
            {
                return (ICommand)GetValue(LoadMoreCommandProperty);
            }
            set
            {
                if (LoadMoreCommand != value)
                {
                    SetValue(LoadMoreCommandProperty, value);
                }
            }
        }

        public ListViewInfiniteScroll() : base(GetCachingStrategy())
        {
            Initialize();

        }

        public ListViewInfiniteScroll(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            Initialize();
        }

        private void Initialize()
        {
            ItemAppearing += ListviewInfinite_ItemAppearing;
        }

        static ListViewCachingStrategy GetCachingStrategy()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return ListViewCachingStrategy.RecycleElement;
                default:
                    return ListViewCachingStrategy.RetainElement;
            }
        }

        private void ListviewInfinite_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (!CanLoadMore)
                {
                    return;
                }

                var items = ItemsSource as IList;

                if (items != null && e.Item == items[items.Count - 1])
                {   

                    if (LoadMoreCommand != null)
                    {
                        LoadMoreCommand.Execute(null);                        
                    }

                 
                }
            }
            catch (Exception ex)
            {                
                throw;
            }

        }
    }
}
