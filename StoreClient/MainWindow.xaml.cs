using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String[] productsForSale = new string[] { "Volvo V70 - 50000 SEK", "Ferrari 458 - 7 500 000 SEK" };
        private ListBox productsBox;
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Title = "Store";
            Width = 700;
            Height = 700;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ScrollViewer root = new ScrollViewer();
            root.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = root;

            Grid grid = new Grid();
            root.Content = grid;
            grid.Margin = new Thickness(5);
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            productsBox = new ListBox();
            foreach (string s in productsForSale)
            {
                productsBox.Items.Add(s);
            }
            grid.Children.Add(productsBox);
            productsBox.MouseDoubleClick += AddToCart;

        }

        private void AddItems()
        {

        }

        private void AddToCart(object sender, MouseEventArgs e)
        {
            MessageBox.Show("You purchased: " + productsForSale[productsBox.SelectedIndex]);
        }
    }
}
