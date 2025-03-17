using System;
using SimpleBankingSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Data;

namespace SimpleBankingSystem.Repositories
{
    public class TransactionRepository
    {
        private readonly BankingDbContext _context;

        public TransactionRepository(BankingDbContext context)
        {
            _context = context;
        }

        public void CreateTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public List<Transaction> GetTransactionsByAccountId(string accountNumber)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                return null;
            }

            return _context.Transactions.Where(t => t.AccountId == account.AccountId).ToList();
        }
    }
}
