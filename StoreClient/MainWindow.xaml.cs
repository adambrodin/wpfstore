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
using Logic;

namespace StoreClient
{
    public partial class MainWindow : Window
    {
        private Product[] productsForSale = new Product[] { new Product("Volvo V70", "Snabb bil", "--", 15000), new Product("Volvo V70", "Snabb bil", "--", 15000) };
        private ListBox productsBox;
        private Grid mainGrid;
        private ScrollViewer root;
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

            root = new ScrollViewer();
            root.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = root;

            mainGrid = new Grid();
            root.Content = mainGrid;
            mainGrid.Margin = new Thickness(5);
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());

            productsBox = new ListBox();
            SetupProductList();
            mainGrid.Children.Add(productsBox);
            productsBox.MouseDoubleClick += ProductDoubleClick;

            Grid buttonGrid = new Grid();

            Button hideUI = new Button
            {
                Padding = new Thickness(5),
                Margin = new Thickness(5, 5, 5, 5),
                Content = "Click to hide",
                FontSize = 20
            };


            Button showUI = new Button
            {
                Padding = new Thickness(5),
                Margin = new Thickness(5, 0, 5, 100),
                Content = "Click to show",
                FontSize = 20
            };

            buttonGrid.Children.Add(hideUI);
            buttonGrid.Children.Add(showUI);

            mainGrid.Children.Add(buttonGrid);
            Grid.SetColumn(buttonGrid, 2);

            hideUI.Click += HideUIClick;
            showUI.Click += ShowUIClick;
        }

        private void SetupProductList()
        {
            foreach (Product p in productsForSale)
            {
                productsBox.Items.Add(p.title);
            }
        }

        private void AddItems()
        {

        }

        private void HideUIClick(object sender, RoutedEventArgs e)
        {
            productsBox.Visibility = Visibility.Hidden;
        }

        private void ShowUIClick(object sender, RoutedEventArgs e)
        {
            productsBox.Visibility = Visibility.Visible;
        }

        private void ProductDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string productName = productsForSale[productsBox.SelectedIndex].title;
                MessageBox.Show("You purchased: " + productName);
            }
            catch
            {
                return;
            }
        }
    }
}
