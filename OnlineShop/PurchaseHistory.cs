using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class PurchaseHistory
    {
        public int Id { get; set; }
        public List<PurchaseHistoryItem> Items { get; set; }

        public PurchaseHistory()
        {
            Items = new List<PurchaseHistoryItem>();
        }

        public void AddItem(Customer customer)
        {
            foreach (var it in customer.Basket.Items)
            {
                var item = Items.SingleOrDefault(i => i.Product.Id == it.Product.Id);
                if (item == null)
                {
                    item = new PurchaseHistoryItem
                    {
                        Product = it.Product,
                        Quantity = 0,
                        ProductId = it.Product.Id,
                        CustomerId = customer.Id
                    };
                    Items.Add(item);
                }
                item.Quantity += it.Quantity;
            }
        }

        public void Refill(Customer customer)
        {
            using (var context = new DbContextShop())
            {
                customer.PurchaseHistory.Items = context.PurchaseHistoryItems.Where(i => i.CustomerId == customer.Id).ToList();
                foreach (var item in customer.PurchaseHistory.Items)
                {
                    item.Product = context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                }
            }
        }
    }

    public class PurchaseHistoryItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
    }
}
