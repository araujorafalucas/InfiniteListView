using InfiniteListView.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfiniteListView.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoadMoreCommand { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public bool CanLoadMore { get; set; }
        private bool isBusy { get; set; }
        public bool IsBusy { get { return isBusy; } set { isBusy = value; OnPropertyChanged(); } }
        

        public ProductViewModel()
        {
            LoadMoreCommand = new Command(async () => await LoadMore());            
            InitializeProducts();
            CanLoadMore = true;
        }

        private void InitializeProducts()
        {
            Products = new ObservableCollection<Product>();
            for (int index = 1; index <= 25; index++)
            {
                Products.Add(new Product { Name = $"Product {index}" });
            }

        }

        private async Task LoadMore()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            CanLoadMore = false;

            int nextPage = Products.Count + 5;

            for (int index = Products.Count + 1; index < nextPage; index++)
            {
                Products.Add(new Product { Name = $"Product {index}" });
                await Task.Delay(1500);
            }

            IsBusy = false;
            CanLoadMore = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
