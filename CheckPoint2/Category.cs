using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CheckPoint2
{
    public class Category
    {
        public string Name { get; set; }
        public List<Product> Products = new List<Product>();

        public Category()
        {
        }

        public Category(string Name)
        {

        }

        public void AddProduct()
        {
            //Name is accepted in all formats
            Console.WriteLine("");
            Console.WriteLine("Please provide product name:");
            string Name = Console.ReadLine();
            //Price has a strict format that needs to be accepted. 
            Console.WriteLine("Please provide product price with decimals (9.99):");

            //To keep asking user for a correct price we will introduct a while function until correct formatting has been used. 
            bool PriceCheck = true;
            while (PriceCheck)
            {
                string PriceString = Console.ReadLine();
                if (!Regex.IsMatch(PriceString, @"^\d+(\.\d{1,2})?$"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect price format, following formats are accepted: 9.99, 9");
                    Console.ResetColor();
                }
                else
                {
                    //Console ReadLine returns a string and we want to store the price as a float. Convert to float. 
                    float Price = float.Parse(PriceString);
                    AddProduct(Name, Price);
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("New product has been added: {0} - {1}", Name, Price);
                    Console.ResetColor();

                    //Exit the PriceCheck loop.
                    PriceCheck = false;
                }
            }

        }

        public void AddProduct(string Name, float Price)
        {

            var product = new Product();
            product.Name = Name;
            product.Price = Price;
            Products.Add(product);

            Console.WriteLine("Adding to the products");
        }
    }
}
