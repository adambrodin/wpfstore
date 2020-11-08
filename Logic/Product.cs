namespace Store
{
    class Product
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }
        public double price { get; set; }

        public Product(string title, string description, string imagePath, double price)
        {
            this.title = title;
            this.description = description;
            this.imagePath = imagePath;
            this.price = price;
        }

    }
}
