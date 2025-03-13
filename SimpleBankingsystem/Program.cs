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
            AccountRepository repository = new AccountRepository(context);

            if (!AuthenticateUser())
            {
                Console.WriteLine("Authentication failed. Exiting application.");
                return;
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("\nSimple Banking System");
                    Console.WriteLine("1. Create New Account");
                    Console.WriteLine("2. Deposit Money");
                    Console.WriteLine("3. Withdraw Money");
                    Console.WriteLine("4. Check Balance");
                    Console.WriteLine("5. Exit");
                    Console.Write("Choose an option: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            CreateAccount(repository);
                            break;
                        case "2":
                            DepositMoney(repository);
                            break;
                        case "3":
                            WithdrawMoney(repository);
                            break;
                        case "4":
                            CheckBalance(repository);
                            break;
                        case "5":
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

    
        static bool AuthenticateUser()
        {
            Console.WriteLine("Welcome to the Banking System!");
            Console.Write("Enter Username: ");
            var username = Console.ReadLine();
            Console.Write("Enter Password: ");
            var password = Console.ReadLine();

          
            if (username == "srithan" && password == "praneeth")
            {
                Console.WriteLine("Authentication successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
                return false;
            }
        }

        static void CreateAccount(AccountRepository repository)
        {
            try
            {
                Console.Write("Enter Account Number: ");
                var accountNumber = Console.ReadLine();
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

        static void DepositMoney(AccountRepository repository)
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

                Console.Write("Enter Deposit Amount: ");
                var amount = decimal.Parse(Console.ReadLine());

                account.Deposit(amount);
                repository.UpdateBalance(account);
                Console.WriteLine("Deposit successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error depositing money: {ex.Message}");
            }
        }

        static void WithdrawMoney(AccountRepository repository)
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

                Console.Write("Enter Withdrawal Amount: ");
                var amount = decimal.Parse(Console.ReadLine());

                account.Withdraw(amount);
                repository.UpdateBalance(account);
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
    }
}
