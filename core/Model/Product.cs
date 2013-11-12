// -----------------------------------------------------------------------
// <copyright file="Product.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    public class Product
    {
        public Product(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Price: {1}", this.Name, this.Price);
        }
    }
}