using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Logic.Models;
using Logic.Services;
using Microsoft.Win32;

namespace StoreManager
{
    public partial class MainWindow : Window
    {
        private List<Product> products;
        private List<Coupon> coupons;
        private ProductService productService;
        private CouponService couponService;
        private StackPanel stackPanel;
        private ListBox productsBox, couponBox;
        private TextBlock productsText, couponsText;
        private TextBox couponNameBox, couponDiscountBox, productNameBox, productDescriptionBox, productPriceBox;
        private Border border;
        private Button addCouponBtn, addImageBtn, addProductBtn;
        private string newImagePath;
        private double windowWidth = 1280, windowHeight = 720;
        private static readonly Regex allowedDiscountFilter = new Regex("[^0-9.-]+");
        private static readonly Regex allowedCouponNameFilter = new Regex("[^a-zA-Z0-9]");

        public MainWindow()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            InitializeComponent();
            Title = "Store Manager";
            Width = 1280;
            Height = 720;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SizeChanged += OnWindowSizeChanged;
            stackPanel = GetStackPanel();


            productService = new ProductService { };
            couponService = new CouponService { };

            border = new Border();
            border.Background = Brushes.LightBlue;
            border.BorderBrush = Brushes.Black;
            border.Padding = new Thickness(5);
            border.BorderThickness = new Thickness(1);
            border.Child = stackPanel;
            Content = border;

            FetchStoreInformation();
            AddBoxItems();
        }


        private void AddBoxItems()
        {
            foreach (Product p in products)
            {
                productsBox.Items.Add($"{p.title} - ${p.price}");
            }

            foreach (Coupon c in coupons)
            {
                couponBox.Items.Add($"{c.code} - {c.discount}%");
            }
        }

        private void FetchStoreInformation()
        {
            // Fetches the current store product list
            products = productService.FetchProducts();
            coupons = couponService.FetchCoupons();
        }

        private StackPanel GetStackPanel()
        {
            StackPanel p = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            productsBox = new ListBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 20,
                Width = 300,
                Height = 400,
                Margin = new Thickness(25, 100, 0, 0)
            };
            ContextMenu productMenu = new ContextMenu();
            productMenu.Items.Add("Remove product");
            productMenu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(RemoveProductItem));
            productsBox.ContextMenu = productMenu;

            couponBox = new ListBox
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                FontSize = 20,
                Width = 300,
                Height = 400,
                Margin = new Thickness(600, -375, 0, 0)
            };

            ContextMenu couponMenu = new ContextMenu();
            couponMenu.Items.Add("Remove coupon");
            couponMenu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(RemoveCouponItem));
            couponBox.ContextMenu = couponMenu;

            productsText = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 30,
                Text = "Products",
                Margin = new Thickness(25, -300, 0, 0)
            };

            couponsText = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                FontSize = 30,
                Text = "Coupons",
                Margin = new Thickness(400, -300, 0, 0)
            };

            couponNameBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 200,
                Height = 25,
                FontSize = 18,
                Margin = new Thickness(0, 0, 100, 0)
            };
            couponNameBox.PreviewTextInput += CouponNameBoxVerifier;

            couponDiscountBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 50,
                Height = 25,
                FontSize = 18,
                Margin = new Thickness(0, 0, 250, 0),
            };

            couponDiscountBox.PreviewTextInput += DiscountBoxVerifier;

            addCouponBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 100,
                Height = 50,
                Margin = new Thickness(0, 0, 200, 0),
                Content = "Add Coupon"
            };
            addCouponBtn.Click += AddCouponBtnClick;

            productNameBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                Height = 25,
                FontSize = 18,
                Margin = new Thickness(25, -225, 0, 0),
            };

            productDescriptionBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 300,
                Height = 25,
                FontSize = 18,
                Margin = new Thickness(25, -175, 0, 0),
            };

            productPriceBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Height = 25,
                FontSize = 18,
                Margin = new Thickness(25, -125, 0, 0),
            };

            addImageBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Height = 50,
                Margin = new Thickness(25, -35, 0, 0),
                Content = "Add Image"
            };
            addImageBtn.Click += AddImageBtnClick;

            addProductBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Height = 50,
                Margin = new Thickness(125, -50, 0, 0),
                Content = "Add Product"
            };
            addProductBtn.Click += AddProductBtnClick;

            p.Children.Add(productsBox);
            p.Children.Add(couponBox);
            p.Children.Add(couponNameBox);
            p.Children.Add(couponDiscountBox);
            p.Children.Add(addCouponBtn);
            p.Children.Add(productNameBox);
            p.Children.Add(productDescriptionBox);
            p.Children.Add(productPriceBox);
            p.Children.Add(addImageBtn);
            p.Children.Add(addProductBtn);
            //p.Children.Add(productsText);
            //p.Children.Add(couponsText);
            return p;
        }

        private void AddImageBtnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png)|*.png";

            if (dialog.ShowDialog() == true) // If the dialog wasn't cancelled
            {
                newImagePath = dialog.InitialDirectory + dialog.FileName;
                MessageBox.Show("NewImagePath: " + newImagePath);
            }
        }

        private void AddProductBtnClick(object sender, RoutedEventArgs e)
        {
            if (newImagePath == null)
            {
                MessageBox.Show("Please add an image first!");
                return;
            }

            Product p = new Product(productNameBox.Text, productDescriptionBox.Text, newImagePath, double.Parse(productPriceBox.Text));
            products.Add(p);
            productsBox.Items.Add($"{p.title} - ${p.price}");
        }

        private void AddCouponBtnClick(object sender, RoutedEventArgs e)
        {
            Coupon c = new Coupon(couponNameBox.Text, double.Parse(couponDiscountBox.Text));
            coupons.Add(c);
            couponBox.Items.Add($"{c.code} - {c.discount}%");
        }

        private void DiscountBoxVerifier(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(allowedDiscountFilter, e.Text);
        }

        private void CouponNameBoxVerifier(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(allowedCouponNameFilter, e.Text);
        }

        private static bool IsTextAllowed(Regex reg, string text)
        {
            return !reg.IsMatch(text);
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            windowWidth = e.NewSize.Width;
            windowHeight = e.NewSize.Height;
            stackPanel.Width = windowWidth;
            stackPanel.Height = windowHeight;
        }

        private void RemoveProductItem(object sender, RoutedEventArgs e)
        {
            productsBox.Items.Remove(productsBox.SelectedItem);
        }
        private void RemoveCouponItem(object sender, RoutedEventArgs e)
        {
            couponBox.Items.Remove(couponBox.SelectedItem);
        }


    }
}
