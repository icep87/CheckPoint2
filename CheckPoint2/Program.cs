using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CheckPoint2
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create a list to hold categories. 
            List<Category> categories = new List<Category>();

            Console.WriteLine("Welcome");
            Console.WriteLine("Please choose what you would like to do:");

            bool runProgram = true;

            while (runProgram)
            {
                Console.WriteLine("");
                Console.WriteLine("1) Add Categories");
                Console.WriteLine("2) List Everything");
                Console.WriteLine("3) Search Products");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Write \"q\" to quit");
                Console.ResetColor();
                Console.WriteLine("");

                string actionMethod = Console.ReadLine();

                if (actionMethod == "1")
                {
                    AddCategoryAndProduct(categories);

                }
                else if (actionMethod == "2")
                {
                    ShowList(categories);

                }
                else if (actionMethod == "3")
                {
                    Search(categories);
                }
                else if (actionMethod.ToLower() == "q")
                {
                    ShowList(categories);
                    break;
                }
                else
                {
                    InvalidInputMessage();

                }
            }
        }

        private static void ShowList(List<Category> categories, string categoryName = null)
        {
            IOrderedEnumerable<Category> categoryQuery = null;


            if (categoryName == null)
            {
                categoryQuery = from Category in categories
                                orderby Category.Name ascending
                                select Category;
            }
            else
            {
                categoryQuery = from Category in categories
                                where Category.Name.Equals(categoryName)
                                orderby Category.Name ascending
                                select Category;
            }
            if (categoryQuery.Any())
            {
                foreach (Category category in categoryQuery)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Category:");
                    Console.WriteLine("{0}", category.Name);

                    IEnumerable<Product> productQuery = from Product in category.Products
                                                        select Product;

                    Console.WriteLine("");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Name".PadRight(10) + "Price");
                    Console.ResetColor();
                    foreach (Product product in productQuery)
                    {
                        Console.WriteLine("{0} {1}", product.Name.PadRight(10), product.Price.ToString().PadRight(10));
                    }

                    //Show total sum of product price
                    Console.WriteLine("");
                    Console.WriteLine("Total price: {0}", category.Products.Sum(item => item.Price));


                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("No results found");
                Console.ResetColor();
            }
        }

        private static void AddCategoryAndProduct(List<Category> categories)
        {
            Console.WriteLine("Name of the category:");

            var category = new Category();
            category.Name = Console.ReadLine();

            categories.Add(category);

            //Notify the user that a category has been added 
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("New category added: {0}", category.Name);
            Console.ResetColor();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("");
                Console.WriteLine("1) Add product");
                Console.WriteLine("2) List products");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Write \"q\" to quit");
                Console.ResetColor();
                Console.WriteLine("");

                string action = Console.ReadLine();

                if (action == "1")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Add product choosen");
                    category.AddProduct();

                }

                //Option two and q has similar actions they need both to output the list sorted desceding by price.
                //Adding a break if "q" is used.

                else if (action == "2" || action.ToLower() == "q")
                {
                    ShowList(categories, category.Name);

                    if (action.ToLower() == "q")
                    {
                        break;
                    }
                }
                else
                {
                    InvalidInputMessage();

                }
            }
        }

        private static void Search(List<Category> categories)
        {
            Console.WriteLine("What would you like to search?");
            Console.WriteLine("");
            Console.WriteLine("1) Category");
            Console.WriteLine("2) Products");
            Console.WriteLine("");

            var searchCategory = Console.ReadLine();

            if (searchCategory == "1")
            {
                Console.WriteLine("");
                Console.WriteLine("Please provide the name?");
                Console.WriteLine("Search is case sensitive");

                string name = Console.ReadLine();
                SearchCategory(categories, name);

            }
            else if (searchCategory == "2")
            {
                Console.WriteLine("");
                Console.WriteLine("Please provide the name?");
                Console.WriteLine("Search is case sensitive");

                string name = Console.ReadLine();
                SearchProduct(categories, name);

            }
            else
            {
                InvalidInputMessage();
            }
        }

        private static void InvalidInputMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input");
            Console.ResetColor();
        }

        private static void SearchCategory(List<Category> categories, string name)
        {
            var query = from Category in categories
                        where Category.Name.Contains(name)
                        select Category;

            Console.WriteLine("Search results:");
            Console.WriteLine("");

            //Check if any results are found
            if (query.Any())
            {
                //Show search results and hightlight search word
                foreach (Category category in query)
                {
                    ColoredSearchResults(name, category.Name);
                    Console.Write("\r\n");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("No results found");
                Console.ResetColor();
            }
        }
        private static void SearchProduct(List<Category> categories, string name)
        {
            var query = categories.SelectMany(x => x.Products).Where(product => product.Name.Contains(name));

            Console.WriteLine("Search results:");
            Console.WriteLine("");

            //Check if any results are found
            if (query.Any())
            {
                //Show search results and hightlight search word
                foreach (Product product in query)
                {
                    ColoredSearchResults(name, product.Name);
                    Console.Write("{0}\r\n", product.Price);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("No results found");
                Console.ResetColor();
            }

        }

        private static void ColoredSearchResults(string searchString, string searchResult)
        {
            string pattern = $"({searchString})";
            string[] words = Regex.Split(searchResult, pattern);

            foreach (string word in words)
            {
                if (word == searchString)
                {
                    var originalColor = Console.BackgroundColor;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(word);
                    Console.BackgroundColor = originalColor;
                }
                else
                {
                    Console.Write(word);
                }
            }
        }
    }
}
