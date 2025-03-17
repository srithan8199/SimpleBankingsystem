
namespace SimpleBankingSystem
{
    using System;
    using SimpleBankingSystem.Repositories;
    using SimpleBankingSystem.Models;
    using Microsoft.Extensions.Configuration;
    using SimpleBankingSystem.Data;
   

    class Program
    {
        static void Main(string[] args)
        {
            BankingDbContext context = new BankingDbContext();
            AccountRepository accountRepository = new AccountRepository(context);
            TransactionRepository transactionRepository = new TransactionRepository(context);

           
            while (true)
            {
                try
                {
                    Console.WriteLine("\nSimple Banking System");
                    Console.WriteLine("1. Create New Account");
                    Console.WriteLine("2. Deposit Money");
                    Console.WriteLine("3. Withdraw Money");
                    Console.WriteLine("4. Check Balance");
                    Console.WriteLine("5. View Transactions");
                    Console.WriteLine("6. Exit");
                    Console.Write("Choose an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            CreateAccount(accountRepository);
                            break;
                        case "2":
                            DepositMoney(accountRepository, transactionRepository);
                            break;
                        case "3":
                            WithdrawMoney(accountRepository, transactionRepository);
                            break;
                        case "4":
                            CheckBalance(accountRepository);
                            break;
                        case "5":
                            ViewTransactions(transactionRepository);
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

       

        static void CreateAccount(AccountRepository repository)
        {
            try
            {
                Console.Write("Enter Account Number: ");
                var accountNumber = Console.ReadLine();
                if (repository.GetAccount(accountNumber) != null)
                {
                    Console.WriteLine("Account with this account number already exists.");
                    return;
                }

                Console.Write("Enter Holder Name: ");
                var holderName = Console.ReadLine();
                Console.Write("Enter Initial Balance: ");
                var balance = decimal.Parse(Console.ReadLine());

                var account = new Account
                {
                    AccountNumber = accountNumber,
                    HolderName = holderName,
                    Balance = balance
                };

                repository.CreateAccount(account);
                Console.WriteLine("Account created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account: {ex.Message}");
            }
        }

        static void DepositMoney(AccountRepository accountRepository, TransactionRepository transactionRepository)
        {
            try
            {
                Console.Write("Enter Account Number: ");
                var accountNumber = Console.ReadLine();
                var account = accountRepository.GetAccount(accountNumber);

                if (account == null)
                {
                    Console.WriteLine("Account not found.");
                    return;
                }

                Console.Write("Enter Deposit Amount: ");
                var amount = decimal.Parse(Console.ReadLine());

                account.Deposit(amount);
                accountRepository.UpdateBalance(account);

                var transaction = new Transaction
                {
                    AccountId = account.AccountId,
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Deposit"
                };
                transactionRepository.CreateTransaction(transaction);

                Console.WriteLine("Deposit successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error depositing money: {ex.Message}");
            }
        }

        static void WithdrawMoney(AccountRepository accountRepository, TransactionRepository transactionRepository)
        {
            try
            {
                Console.Write("Enter Account Number: ");
                var accountNumber = Console.ReadLine();
                var account = accountRepository.GetAccount(accountNumber);

                if (account == null)
                {
                    Console.WriteLine("Account not found.");
                    return;
                }

                Console.Write("Enter Withdrawal Amount: ");
                var amount = decimal.Parse(Console.ReadLine());

                account.Withdraw(amount);
                accountRepository.UpdateBalance(account);

                var transaction = new Transaction
                {
                    AccountId = account.AccountId,
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionType = "Withdrawal"
                };
                transactionRepository.CreateTransaction(transaction);

                Console.WriteLine("Withdrawal successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error withdrawing money: {ex.Message}");
            }
        }

        static void CheckBalance(AccountRepository repository)
        {
            try
            {
                Console.Write("Enter Account Number: ");
                var accountNumber = Console.ReadLine();
                var account = repository.GetAccount(accountNumber);

                if (account == null)
                {
                    Console.WriteLine("Account not found.");
                    return;
                }

                Console.WriteLine($"Current Balance: {account.CheckBalance()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking balance: {ex.Message}");
            }
        }

        static void ViewTransactions(TransactionRepository transactionRepository)
        {
            try
            {
                Console.Write("Enter Account Number: ");
                var accountNumber = Console.ReadLine();
                var transactions = transactionRepository.GetTransactionsByAccountId(accountNumber);

                if (transactions == null || !transactions.Any())
                {
                    Console.WriteLine("No transactions found for this account.");
                    return;
                }

                Console.WriteLine("Transactions:");
                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"ID: {transaction.TransactionId}, Date: {transaction.TransactionDate}, Type: {transaction.TransactionType}, Amount: {transaction.Amount}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving transactions: {ex.Message}");
            }
        }
    }
}