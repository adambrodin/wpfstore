namespace Store
{
    class Product
    {
        public string title, description, imagePath;
        public double price;

        public Product(string title, string description, string imagePath, double price)
        {
            this.title = title;
            this.description = description;
            this.imagePath = imagePath;
            this.price = price;
        }
    }
}
