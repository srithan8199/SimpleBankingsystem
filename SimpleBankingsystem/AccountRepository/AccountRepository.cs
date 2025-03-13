using SimpleBankingSystem.Models;
using SimpleBankingSystem.Data;

namespace SimpleBankingSystem.Repositories
{
    public class AccountRepository
    {
        private readonly BankingDbContext _context;

        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }

        public void CreateAccount(Account account)
        {
            try
            {
                if (_context.Accounts.Any(a => a.AccountNumber == account.AccountNumber))
                {
                    throw new Exception("Account with this account number already exists.");
                }
                _context.Accounts.Add(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create account: " + ex.Message);
            }
        }

        public Account GetAccount(string accountNumber)
        {
            try
            {
                return _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve account: " + ex.Message);
            }
        }

        public void UpdateBalance(Account account)
        {
            try
            {
                _context.Accounts.Update(account);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update balance: " + ex.Message);
            }
        }
    }
}
