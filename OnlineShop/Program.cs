using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OnlineShop
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            using(var context = new DbContextShop())
            {
                Console.WriteLine("Welcome to Online Shop!");
                while (true)
                {
                    Console.WriteLine("Enter option:");
                    Console.WriteLine("1. Sign in");
                    Console.WriteLine("2. Sign up");
                    Console.Write("Enter option: ");
                    var option = Int32.Parse(Console.ReadLine());
                    switch(option)
                    {
                    case 1:
                            {
                                Console.Write("Enter email: ");
                                var email = Console.ReadLine();
                                Console.Write("Enter password: ");
                                var password = Console.ReadLine();
                                var customer = context.Customers.FirstOrDefault(c => c.Email == email);
                                if(customer == null)
                                {
                                    customer = new Admin();
                                    if(customer.SignIn(email, password))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Welcome to admin panel!");
                                        while (true)
                                        {
                                            Console.WriteLine("1. View products");
                                            Console.WriteLine("2. Add product");
                                            Console.WriteLine("3. Delete product");
                                            Console.WriteLine("4. Sign out");
                                            Console.Write("Enter option: ");

                                            var adminOption = Int32.Parse(Console.ReadLine());
                                            if (adminOption == 1)
                                            {
                                                Console.Clear();
                                                var products = context.Products.ToList();
                                                foreach (var product in products)
                                                {
                                                    Console.WriteLine($"{product.Id}. Name: {product.Name}\n Price: ${product.Price}\n Quatity: {product.Quantity}");
                                                }
                                            }
                                            else if (adminOption == 2)
                                            {
                                                Console.Write("Enter product name: ");
                                                var name = Console.ReadLine();
                                                var product = context.Products.FirstOrDefault(c => c.Name == name);
                                                Console.Write("Enter product quantity: ");
                                                var quantity = Int32.Parse(Console.ReadLine());
                                                if(quantity <= 0)
                                                {
                                                    Console.WriteLine("Quantity must be positive!");
                                                    continue;
                                                }
                                                if (product != null)
                                                {
                                                    product.Quantity += quantity;
                                                    context.SaveChanges();
                                                    Console.WriteLine("Product added successfully!");
                                                }
                                                else
                                                {
                                                    Console.Write("Enter product price: ");
                                                    var priceString = Console.ReadLine();
                                                    if (decimal.TryParse(priceString, out var price))
                                                    {
                                                        if(price < 0)
                                                        {
                                                            Console.WriteLine("Price must be positive!");
                                                            continue;
                                                        }
                                                        var newProduct = new Product
                                                        {
                                                            Name = name,
                                                            Price = price,
                                                            Quantity = quantity
                                                        };
                                                        context.Products.Add(newProduct);
                                                        context.SaveChanges();
                                                        Console.WriteLine("Product added successfully!");
                                                    }
                                                }
                                            }
                                            else if (adminOption == 3)
                                            {
                                                Console.Write("Enter product id: ");
                                                var id = Int32.Parse(Console.ReadLine());
                                                Console.Write("Enter product quantity to delete: ");
                                                var quantity = Int32.Parse(Console.ReadLine());
                                                var product = context.Products.Find(id);
                                                if (quantity <= 0 || quantity > product.Quantity)
                                                {
                                                    Console.WriteLine("Bad quantity amount!");
                                                    continue;
                                                }
                                                if (product != null)
                                                {
                                                    product.Quantity -= quantity;
                                                    if (product.Quantity <= 0)
                                                    {
                                                        context.Products.Remove(product);
                                                    }
                                                    context.SaveChanges();
                                                    Console.Clear();
                                                    Console.WriteLine("Product delete successfully!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Product not found");
                                                }
                                            }
                                            else if (adminOption == 4)
                                            {
                                                Console.WriteLine("Signing out...");
                                                System.Threading.Thread.Sleep(500);
                                                Console.Clear();
                                                break;
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Invalid option");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Customer not found.");
                                    }
                                    
                                }
                                else if (customer.SignIn(email, password))
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Sign in successful! Welcome, {customer.Name}");
                                    customer.Basket = context.Baskets.FirstOrDefault(b => b.Id == customer.Id);
                                    customer.PurchaseHistory = context.PurchaseHistories.FirstOrDefault(ph => ph.Id == customer.Id);
                                    customer.Basket.Refill(customer);
                                    customer.PurchaseHistory.Refill(customer);
                                    while (true)
                                    {
                                        
                                        Console.WriteLine($"Your balance: {customer.Balance}");
                                        Console.WriteLine("1. View products");
                                        Console.WriteLine("2. Add product to basket");
                                        Console.WriteLine("3. View basket");
                                        Console.WriteLine("4. Checkout");
                                        Console.WriteLine("5. Show purchased history");
                                        Console.WriteLine("6. Top up the balance");
                                        Console.WriteLine("7. Sign out");
                                        Console.Write("Enter option: ");

                                        var customerOption = Int32.Parse(Console.ReadLine());
                                        if (customerOption == 1)
                                        {
                                            Console.Clear();
                                            var products = context.Products.ToList();
                                            Console.WriteLine("ID  Name\t\t\tPrice  Quantity");
                                            Console.WriteLine("-------------------------------------------");
                                            foreach (var product in products)
                                            {
                                                Console.WriteLine($"{product.Id}.  {product.Name}\t\t\t{product.Price}$  {product.Quantity}");
                                            }
                                        }
                                        else if (customerOption == 2)
                                        {
                                            //Console.Clear();
                                            Console.Write("Enter the product id: ");
                                            var productId = int.Parse(Console.ReadLine());
                                            var product = context.Products.SingleOrDefault(p => p.Id == productId);
                                            if (product == null)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Invalid product id");
                                                continue;
                                            }

                                            Console.Write("Enter the quantity: ");
                                            var quantity = int.Parse(Console.ReadLine());
                                            if (quantity > product.Quantity || quantity <= 0)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Invalid quantity");
                                                continue;
                                            }
                                            customer.Basket.AddItem(product, quantity);
                                            context.SaveChanges();

                                            Console.WriteLine($"{quantity}x {product.Name} added to basket");


                                        }
                                        else if (customerOption == 3)
                                        {
                                            Console.Clear();
                                            if(customer.Basket.Items.Count == 0)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Your basket is empty!");
                                                continue;
                                            }
                                            var totalPrice = 0m;
                                            Console.WriteLine("Items in your basket:");
                                            Console.WriteLine("Name                                  Price");
                                            Console.WriteLine("-------------------------------------------");
                                            foreach (var item in customer.Basket.Items)
                                            {
                                                Console.WriteLine($"{item.Quantity}x {item.Product.Name}                                {item.Quantity * item.Product.Price}$");
                                                totalPrice += item.Quantity * item.Product.Price;

                                            }
                                            Console.WriteLine("--------------------------------------------");
                                            Console.WriteLine($"Total price: {totalPrice}$");
                                        }
                                        else if (customerOption == 4)
                                        {
                                            Console.Clear();
                                            var totalPrice = 0m;
                                            foreach (var item in customer.Basket.Items)
                                            {
                                                totalPrice += item.Quantity * item.Product.Price;
                                            }
                                            if (totalPrice > customer.Balance)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Insufficient balance");
                                                continue;
                                            }
                                            foreach (var item in customer.Basket.Items)
                                            {
                                                var product = context.Products.Find(item.ProductId);
                                                if (item.Quantity > product.Quantity)
                                                {
                                                    Console.WriteLine($"Changed {product.Name} quantity to {product.Quantity} because it was more that in our shop.");
                                                    Console.Write("Do you agree with changes? [Y|N]");
                                                    var chooser = Console.ReadLine();
                                                    if (chooser == "Y" || chooser == "y")
                                                    {
                                                        item.Quantity = product.Quantity;
                                                    }
                                                    else if (chooser == "N" || chooser == "n")
                                                    {
                                                        Console.WriteLine("Returning to menu...");
                                                        customer.Basket.Items.Clear();
                                                        break;
                                                    }
                                                }
                                            }
                                            if (customer.Basket.Items.Count == 0)
                                            {
                                                Console.Clear();
                                                continue;
                                            }

                                            foreach (var item in customer.Basket.Items)
                                            {
                                                var product = context.Products.FirstOrDefault(c => c.Name == item.Product.Name);
                                                if (product != null)
                                                {
                                                    product.Quantity -= item.Quantity;
                                                    if (product.Quantity == 0)
                                                    {
                                                        context.Products.Remove(product);
                                                    }
                                                   // context.SaveChanges();

                                                }
                                            }
                                            customer.Balance -= totalPrice;

                                            customer.PurchaseHistory.AddItem(customer);
                                            context.BasketItems.RemoveRange(customer.Basket.Items);
                                            customer.Basket.Items.Clear();
                                            context.SaveChanges();

                                            Console.WriteLine("Checkout successful");

                                        }
                                        else if (customerOption == 5)
                                        {
                                            Console.Clear();
                                            var totalPrice = 0m;
                                            Console.WriteLine("Your purchase history:");
                                            Console.WriteLine("ID  Name                   Price");
                                            Console.WriteLine("--------------------------------");
                                            foreach (var item in customer.PurchaseHistory.Items)
                                            {
                                                Console.WriteLine($"{item.Id}.  {item.Quantity}x {item.Product.Name}\t\t{item.Quantity * item.Product.Price}$");
                                                totalPrice += item.Quantity * item.Product.Price;

                                            }
                                            Console.WriteLine("-------------------------------=");
                                            Console.WriteLine($"Total spent: {totalPrice}$");
                                        }
                                        else if (customerOption == 6)
                                        {
                                            
                                            Console.Write("Enter amount of money you want to top up: ");
                                            try
                                            {
                                                var amount = decimal.Parse(Console.ReadLine());
                                                if(amount <= 0)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Amount gonna be less than 0!");
                                                    continue;
                                                }
                                                customer.Balance += amount;
                                                Console.Clear();
                                                Console.WriteLine($"Successfully added {amount}$");
                                            } catch (ArgumentOutOfRangeException ex)
                                            {
                                                Console.WriteLine("Amount gonna be less than 0!");
                                                continue;
                                            }
                                           // context.SaveChanges();
                                        }
                                        else if (customerOption == 7)
                                        {
                                            Console.WriteLine("Signing out...");
                                            System.Threading.Thread.Sleep(500);
                                            Console.Clear();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Wrong Input.");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Something wrong... Try again.");
                                }
                                break;
                            }
                    case 2:
                            {
                                Console.Write("Enter name: ");
                                var name = Console.ReadLine();
                                Console.Write("Enter email: ");
                                var email = Console.ReadLine();
                                Console.Write("Enter password: ");
                                var password = Console.ReadLine();

                                var existingCustomer = context.Customers.FirstOrDefault(c => c.Email == email);
                                if (existingCustomer != null)
                                {
                                    Console.WriteLine("A customer with this email address already exists.");
                                    System.Threading.Thread.Sleep(1000);
                                    Console.Clear();
                                    continue;
                                }

                                var customer = new User
                                {
                                    Name = name,
                                    Email = email,
                                    Basket = new Basket(),
                                    PurchaseHistory = new PurchaseHistory()
                                };
                                customer.HashPassword(password);
                                context.Customers.Add(customer);
                                context.SaveChanges();
                                Console.WriteLine("Customer added successfully!");
                                System.Threading.Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Wrong input...");
                                break;
                            }
                    }
                }
                
            }
        }
    }
}