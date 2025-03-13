using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string HolderName { get; set; }
        [Required]
        public decimal Balance { get; set; }


        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.");
            }
            if (Balance < amount)
            {
                throw new InvalidOperationException("Insufficient balance.");
            }
            Balance -= amount;
        }

        public decimal CheckBalance()
        {
            return Balance;
        }
    }
}
