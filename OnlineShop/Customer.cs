using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Linq;

namespace OnlineShop
{
    public abstract class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Basket Basket { get; set; }

        public PurchaseHistory PurchaseHistory { get; set; }

        private decimal myBalance = 0;
        public decimal Balance { get
            {
                return myBalance;
            }
            set
            {
                if (value >= 0) myBalance = value;
                else
                {
                    throw new System.ArgumentOutOfRangeException("Error", "Value less 0");
                }
            }
        }

        public virtual void HashPassword(string password)
        { }
        public abstract bool SignIn(string email, string password);

        public virtual void Refill() { }


    }

    public class User : Customer
    {
        public override void HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                PasswordHash = Convert.ToBase64String(hashedBytes);
            }
        }

        public override bool SignIn(string email, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = Convert.ToBase64String(hashedBytes);
                return Email == email && hashedPassword == PasswordHash;
            }
        }


    }


    public class Admin : Customer
    {
        private bool isAdmin(string email, string password)
        {
            return email == "admin" && password == "admin";
        }
        public override bool SignIn(string email, string password)
        {
            return isAdmin(email, password);
        }

    }
}