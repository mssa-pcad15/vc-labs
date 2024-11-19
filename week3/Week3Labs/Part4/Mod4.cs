

using System.Text;

namespace Part4
{
    internal class Mod4
    {
        internal static void Formating()
        {
            int invoiceNumber = 1201;
            decimal productShares = 25.4568m;
            decimal subtotal = 2750.00m;
            decimal taxPercentage = .15825m;
            decimal total = 3185.19m;

            Console.WriteLine($"Invoice Number: {invoiceNumber}");
            Console.WriteLine($"   Shares: {productShares:N3} Product");// IT Does rounding!
            Console.WriteLine($"     Sub Total: {subtotal:C}");
            Console.WriteLine($"           Tax: {taxPercentage:P2}");
        }

        internal static void LetterChallenge()
        {

            string customerName = "Ms. Barros";

            string currentProduct = "Magic Yield";
            int currentShares = 2975000;
            decimal currentReturn = 0.1275m;
            decimal currentProfit = 55000000.0m;

            string newProduct = "Glorious Future";
            decimal newReturn = 0.13125m;
            decimal newProfit = 63000000.0m;

            Console.OutputEncoding = UTF8Encoding.UTF8;
            string letter = $"""
Dear {customerName},
As a customer of our {currentProduct} offering we are excited to tell you about a new financial product that would dramatically increase your return.

Currently, you own {currentShares:N} shares at a return of {currentReturn:P2}.

Our new product, {newProduct} offers a return of {newReturn:P2}.  Given your current volume, your potential profit would be ¤{newProfit:N2}.

Here's a quick comparison:

{currentProduct.PadRight(20)} {currentReturn:P2}    {currentProfit:C}      
{newProduct.PadRight(20)} {newReturn:P2}    {newProfit:C}
""";
            Console.WriteLine(letter);
        }
    }
}