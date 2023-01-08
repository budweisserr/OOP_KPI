using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Remoting.Contexts;

namespace OnlineShop
{
    public class Basket
    {
        public int Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public Basket()
        {
            Items = new List<BasketItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            var item = Items.SingleOrDefault(i => i.Product.Id == product.Id);
            if (item == null)
            {
                item = new BasketItem
                {
                    Product = product,
                    Quantity = 0,
                    BasketId = this.Id,
                    ProductId = product.Id
                };
                Items.Add(item);
            }
            item.Quantity += quantity;
        }

        public void Refill(Customer customer)
        {
            using (var context = new DbContextShop())
            {
                customer.Basket.Items = context.BasketItems.Where(i => i.BasketId == customer.Basket.Id).ToList();
                foreach (var item in customer.Basket.Items)
                {
                    item.Product = context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                }
            }
        }

    }

    public class BasketItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int BasketId { get; set; }

        public int ProductId { get; set; }
    }
}
