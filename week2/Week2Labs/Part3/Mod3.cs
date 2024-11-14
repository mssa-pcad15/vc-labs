

namespace Part3
{
    internal class Mod3
    {
        internal static void Challenge()
        {
            // SKU = Stock Keeping Unit. 
            // SKU value format: <product #>-<2-letter color code>-<size code>
            string sku = "01-MN-L";

            string[] product = sku.Split('-');

            string type = "";
            string color = "";
            string size = "";

            switch (product[0]) {
                case "01": type = "Sweat shirt"; break;
                case "02": type = "T-Shirt"; break;
                case "03": type = "Sweat pants"; break;
                default: type = "Other";break;  
            }

            #region replaced with switch
            //if (product[0] == "01")
            //{
            //    type = "Sweat shirt";
            //}
            //else if (product[0] == "02")
            //{
            //    type = "T-Shirt";
            //}
            //else if (product[0] == "03")
            //{
            //    type = "Sweat pants";
            //}
            //else
            //{
            //    type = "Other";
            //} 
            #endregion

            switch (product[1]) {
                case "BL": color = "Black"; break;
                case "MN": color = "Maroon"; break;
                default: color = "White"; break;
            }

            #region replaced by switch

            //if (product[1] == "BL")
            //{
            //    color = "Black";
            //}
            //else if (product[1] == "MN")
            //{
            //    color = "Maroon";
            //}
            //else
            //{
            //    color = "White";
            //}

            #endregion

            //highlight all the if/else, use Ctrl+. -> select "convert to switch
            switch (product[2])
            {
                case "S":
                    size = "Small";
                    break;
                case "M":
                    size = "Medium";
                    break;
                case "L":
                    size = "Large";
                    break;
                default:
                    size = "One Size Fits All";
                    break;
            }

            Console.WriteLine($"Product: {size} {color} {type}");
        }

        internal static void SwitchCase()
        {
            int employeeLevel = 201;
            string name = "John Smith";
            string title = "";
            switch (employeeLevel)
            {
                case 100:
                case 200: title = "Senior Associate"; break;
                case 300: title = "Manager"; break;
                case 400: title = "Senior Manager"; break;
                case 500:
                case 600:
                case 700:
                    title = "Super Manager"; break; // this means 500,600,700 will all be
                    //super manager
                default : title = "Associate";break;
            }
            Console.WriteLine($"{name}: {title}");

            switch (employeeLevel)
            {
                case <= 100:
                case <= 200: title = "Senior Associate"; break;
                case <= 300: title = "Manager"; break;
                case <= 400: title = "Senior Manager"; break;
                case <= 500:
                case <= 600:
                case <= 700:
                    title = "Super Manager"; break; // this means 500,600,700 will all be
                                                    //super manager
               
            }
            Console.WriteLine($"{name}: {title}");
        }
    }
}