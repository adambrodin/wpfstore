using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Logic.Models;
using Logic.Services;
using Logic.IO;
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
        private TextBox couponNameBox, couponDiscountBox, productNameBox, productDescriptionBox, productPriceBox, productImagePathBox;
        private Border border;
        private Button addCouponBtn, addImageBtn, addProductBtn, saveBtn, clearBtn, updateProductBtn;
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

            border = new Border
            {
                Background = Brushes.LightBlue,
                BorderBrush = Brushes.Black,
                Padding = new Thickness(5),
                BorderThickness = new Thickness(1),
                Child = stackPanel
            };
            Content = border;

            FetchStoreInformation();
            AddBoxItems();
        }


        private void AddBoxItems()
        {
            productsBox.Items.Clear();
            couponBox.Items.Clear();

            foreach (Product p in products)
            {
                productsBox.Items.Add($"{p.title} - ${p.price}");
            }

            foreach (Coupon c in coupons)
            {
                couponBox.Items.Add($"{c.code} - {c.discount * 100}%");
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
            productsBox.SelectionChanged += ProductSelected;

            ContextMenu productMenu = new ContextMenu();
            MenuItem removeProduct = new MenuItem
            {
                Header = "Remove Product",
            };

            removeProduct.Click += RemoveProductClick;
            productMenu.Items.Add(removeProduct);
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

            productImagePathBox = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                Height = 25,
                FontSize = 18,
                Margin = new Thickness(125, -155, 0, 0),
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

            saveBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Height = 50,
                Margin = new Thickness(225, -50, 0, 0),
                Content = "Save store"
            };
            saveBtn.Click += SaveBtnClick;

            clearBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Height = 50,
                Margin = new Thickness(325, -50, 0, 0),
                Content = "Clear fields"
            };
            clearBtn.Click += ClearBtnClick;

            updateProductBtn = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                Height = 50,
                Margin = new Thickness(425, -50, 0, 0),
                Content = "Update product"
            };
            updateProductBtn.Click += UpdateProductBtnClick;

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
            p.Children.Add(saveBtn);
            p.Children.Add(productImagePathBox);
            p.Children.Add(clearBtn);
            p.Children.Add(updateProductBtn);
            return p;
        }

        private void UpdateProductBtnClick(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
            SaveStore();
        }

        private void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            productNameBox.Text = "";
            productDescriptionBox.Text = "";
            productPriceBox.Text = "";
            productImagePathBox.Text = "";
        }

        private string OpenImageDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png)|*.png";

            if (dialog.ShowDialog() == true) // If the dialog wasn't cancelled
            {
                return dialog.InitialDirectory + dialog.FileName;
            }

            return "";
        }

        private void UpdateProduct()
        {
            if (productsBox.SelectedItem != null)
            {
                products[productsBox.SelectedIndex].title = productNameBox.Text;
                products[productsBox.SelectedIndex].description = productDescriptionBox.Text;
                products[productsBox.SelectedIndex].price = double.Parse(productPriceBox.Text);
                products[productsBox.SelectedIndex].imagePath = productImagePathBox.Text;
            }

            AddBoxItems();
            MessageBox.Show("Product updated");
        }

        private void ProductSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Product selectedProduct = products[productsBox.SelectedIndex];
                productNameBox.Text = selectedProduct.title;
                productDescriptionBox.Text = selectedProduct.description;
                productPriceBox.Text = selectedProduct.price.ToString();
                productImagePathBox.Text = selectedProduct.imagePath;
            }
            catch
            { }
        }


        private void SaveStore()
        {
            Writer writer = new Writer();
            FileHelper.DeleteTempFile("products.csv");
            FileHelper.DeleteFile("coupons.csv");
            writer.WriteDataToCsvTemp(products, "products.csv");
            writer.WriteDataToCsv(coupons, "coupons.csv");

            FetchStoreInformation();
            AddBoxItems();
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            SaveStore();
            MessageBox.Show("Store saved!");
        }

        private void AddImageBtnClick(object sender, RoutedEventArgs e)
        {
            productImagePathBox.Text = OpenImageDialog();
        }

        private void AddProductBtnClick(object sender, RoutedEventArgs e)
        {
            if (productImagePathBox.Text == null)
            {
                MessageBox.Show("Please add an image first!");
                return;
            }

            Product p = new Product(productNameBox.Text, productDescriptionBox.Text, productImagePathBox.Text, double.Parse(productPriceBox.Text));
            products.Add(p);
            productsBox.Items.Add($"{p.title} - ${p.price}");
        }

        private void AddCouponBtnClick(object sender, RoutedEventArgs e)
        {
            Coupon c = new Coupon(couponNameBox.Text, double.Parse(couponDiscountBox.Text) / 100);
            coupons.Add(c);
            couponBox.Items.Add($"{c.code} - {c.discount * 100}%");
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

        private void RemoveProductClick(object sender, RoutedEventArgs e)
        {
            products.RemoveAt(productsBox.SelectedIndex);
            productsBox.Items.Remove(productsBox.SelectedItem);
        }

        private void RemoveCouponItem(object sender, RoutedEventArgs e)
        {
            coupons.RemoveAt(couponBox.SelectedIndex);
            couponBox.Items.Remove(couponBox.SelectedItem);
        }

    }
}
