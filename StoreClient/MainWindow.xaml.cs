﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Logic;

namespace StoreClient
{
    public partial class MainWindow : Window
    {
        private double windowWidth = 1280, windowHeight = 720;
        private Product[] productsForSale = new Product[] { new Product("Ethereum", "A solid cryptocurrency", "-", 473.53), new Product("Ethereum", "A solid cryptocurrency", "-", 473.53) };
        private ListBox productsBox, cartBox;
        private Button cartBtn, backToMainBtn;
        private Border mainBorder;
        private StackPanel currentPanel, mainPanel, cartPanel;

        private enum View
        {
            Main,
            Cart,
            Receipt
        }

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void FetchProducts()
        {
            // TODO implement product service
        }

        private void Start()
        {
            FetchProducts();

            // Window settings
            Title = "Store";
            Width = windowWidth;
            Height = windowHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.SizeChanged += OnWindowSizeChanged;

            // Element setup
            mainBorder = new Border();
            mainBorder.Background = Brushes.LightBlue;
            mainBorder.BorderBrush = Brushes.Black;
            mainBorder.Padding = new Thickness(15);
            mainBorder.BorderThickness = new Thickness(1);

            mainPanel = MainLayout();
            cartPanel = CartLayout();
            SetLayout(mainPanel);
        }

        private void SetLayout(StackPanel panel)
        {
            currentPanel = panel;
            mainBorder.Child = panel;
            Content = mainBorder;
        }

        private StackPanel MainLayout()
        {
            StackPanel p = new StackPanel()
            {
                Background = Brushes.Azure,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            TextBlock storeText = new TextBlock
            {
                Margin = new Thickness(10),
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = "Cryptocurrency Store"
            };

            TextBlock productListText = new TextBlock
            {
                Margin = new Thickness(40, 0, 0, 0),
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = "Available cryptocurrencies"
            };

            productsBox = new ListBox
            {
                FontSize = 24,
                BorderBrush = Brushes.Red,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(30, -150, 0, -300),
                Padding = new Thickness(5)
            };
            productsBox.MouseDoubleClick += ProductDoubleClick;
            SetupProductList();

            cartBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(50),
                Padding = new Thickness(20),
                Content = "Show Cart"
            };
            cartBtn.Click += CartBtnClick;

            p.Width = windowWidth;
            p.Height = windowHeight;
            p.Children.Add(storeText);
            p.Children.Add(productListText);
            p.Children.Add(cartBtn);
            p.Children.Add(productsBox);

            return p;
        }

        private StackPanel CartLayout()
        {
            StackPanel p = new StackPanel()
            {
                Background = Brushes.Beige,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            TextBlock storeText = new TextBlock
            {
                Margin = new Thickness(10),
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = "Cart"
            };

            TextBlock productListText = new TextBlock
            {
                Margin = new Thickness(40, 0, 0, 0),
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = "Currencies added to cart"
            };

            cartBox = new ListBox
            {
                FontSize = 24,
                BorderBrush = Brushes.Red,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(30, -150, 0, -300),
                Padding = new Thickness(5)
            };

            cartBox.Items.Add("Example Cart Item 1");
            cartBox.Items.Add("Example Cart Item 2");
            cartBox.Items.Add("Example Cart Item 3");


            backToMainBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(50),
                Padding = new Thickness(20),
                Content = "Back"
            };
            backToMainBtn.Click += BackToMainBtnClick;

            p.Width = windowWidth;
            p.Height = windowHeight;
            p.Children.Add(storeText);
            p.Children.Add(productListText);
            p.Children.Add(backToMainBtn);
            p.Children.Add(cartBox);

            return p;
        }

        private void SetupProductList()
        {
            foreach (Product p in productsForSale)
            {
                productsBox.Items.Add($"{p.title} - {p.price} USD");
            }
        }

        private void AddItems()
        {

        }

        private void CartBtnClick(object sender, RoutedEventArgs e)
        {
            SetLayout(cartPanel);
        }

        private void BackToMainBtnClick(object sender, RoutedEventArgs e)
        {
            SetLayout(mainPanel);
        }
        private void ProductDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                Product product = productsForSale[productsBox.SelectedIndex];
                MessageBox.Show($"You purchased 1x {product.title} for a total of ${product.price}");
            }
            catch
            {
                return;
            }
        }

        protected void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            windowWidth = e.NewSize.Width;
            windowHeight = e.NewSize.Height;

            if (currentPanel != null)
            {
                currentPanel.Width = windowWidth;
                currentPanel.Height = windowHeight;
            }
        }
    }
}
