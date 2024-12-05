using System.Buffers;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SaveToDisk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // instantial 3 customers
            // save each customer to its own file in
            // JSON format with [firstname]-[lastname].json
            Customer[] customers = [
                new Customer() { FirstName="Bob",LastName= "Smith", Accounts = new List<BankAccount>()},
                new Customer() { FirstName="Alice",LastName= "Archer", Accounts = new List<BankAccount>()},
                new Customer() { FirstName="Charlie",LastName= "Baker", Accounts = new List<BankAccount>()},
                ];

            customers[0].Accounts.Add(new SavingsAccount(customers[0]) { AccountNumber = "BS10001", Balance = 10000, APR = 3.1m });
            customers[0].Accounts.Add(new CheckingAccount(customers[0]) { AccountNumber = "BS10002", Balance = 11000, MinimumBalance = 300 });
            customers[0].Accounts.Add(new CheckingAccount(customers[0]) { AccountNumber = "BS10003", Balance = 12000, MinimumBalance = 200 });
            customers[0].Accounts.Add(new SavingsAccount(customers[0]) { AccountNumber = "BS10004", Balance = 5000, APR = 2.1m });

            customers[1].Accounts.Add(new SavingsAccount(customers[1]) { AccountNumber = "AA10001", Balance = 8000, APR = 3.2m });
            customers[1].Accounts.Add(new CheckingAccount(customers[1]) { AccountNumber = "AA10002", Balance = 7000, MinimumBalance = 400 });

            customers[2].Accounts.Add(new SavingsAccount(customers[2]) { AccountNumber = "CB10001", Balance = 500, APR = 3.3m });
            customers[2].Accounts.Add(new CheckingAccount(customers[2]) { AccountNumber = "CB10002", Balance = 60, MinimumBalance = 500 });

            foreach (var customer in customers)
            {
                string fileName = $"{customer.FirstName}-{customer.LastName}.json";
                if (File.Exists(fileName)) { File.Delete(fileName); }
                var sw = new StreamWriter(File.Create(fileName));

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    WriteIndented = true
                };

                sw.WriteLine(JsonSerializer.Serialize(customer, options)); // this doesn't work
                sw.Close();
            }
            //load the customers from files
            
            string folderPath = Directory.GetCurrentDirectory();
            string[] jsonFiles = Directory.GetFiles(folderPath, "*-*.json");
            // load 3 customers from files
            List<Customer> loadedCustomers = new List<Customer>();
            foreach (string jsonFile in jsonFiles)
            {
                FileInfo file = new FileInfo(jsonFile);

                var aCustomer = JsonSerializer.Deserialize<Customer>(file.OpenRead());
                loadedCustomers.Add(aCustomer);
            }
          

            foreach (var customer in loadedCustomers) {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}:");
                foreach (var account in customer.Accounts) {
                    Console.WriteLine($"\t{account.AccountNumber} - {account.Balance,-8:c0}, " +
                        $"{
                            (
                                (account is SavingsAccount s) ? $"Saving with APR {s.APR}" :
                                    (account is CheckingAccount c) ? $"Checking with minimum {c.MinimumBalance:C0} " : ""
                            )
                          }");
                }
            
            }
            
        }
    }

    public class Customer
    {
        // public property FirstName, LastName

        // public property List<BankAccount>
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required List<BankAccount> Accounts { get; set; }
        public Customer()
        {
            
        }
    }

   
    [JsonDerivedType(typeof(SavingsAccount), typeDiscriminator: "savings")]
    [JsonDerivedType(typeof(CheckingAccount), typeDiscriminator: "checking")]
    public abstract class BankAccount
    {
        public required decimal Balance { get; set; }
        public required string AccountNumber { get; set; }
        public Customer Owner { get; set; }

    }
    // SavingsAccount class inherits BankAccount
    // has Interest in APR % e.g. 3.1 float
    public class SavingsAccount : BankAccount
    {
        public required decimal APR { get; set; }
        public SavingsAccount(Customer owner)
        {
            this.Owner = owner;
        }
    }
    // CheckingAccount class inherits BankAccount
    // has MinimumBalance
    public class CheckingAccount : BankAccount
    {
        public required decimal MinimumBalance { get; set; }

        public CheckingAccount(Customer owner)
        {
            this.Owner = owner;
        }
    }
}
