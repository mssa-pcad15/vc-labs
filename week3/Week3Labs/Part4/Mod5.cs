
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Part4
{
    internal class Mod5
    {
        internal static void Challenge()
        {
            //minimally viable product
            const string input = "<div><h2>Widgets &trade;</h2><span>5000</span></div>";

           
            string startTag= "<span>";
            string endTag = startTag.Replace("<","</");

            int startPosition = input.IndexOf(startTag, StringComparison.OrdinalIgnoreCase) + startTag.Length ;
            int endPosition = input.IndexOf(endTag, StringComparison.OrdinalIgnoreCase);

            string quantity = input.Substring(startPosition, endPosition - startPosition);

            Console.WriteLine(quantity);

            string tradeMark = "&trade;";
            string registered = "&reg;";
            
            string output = input.Replace(tradeMark,registered);

            // Your work here

            Console.WriteLine(output);

            var element = XElement.Parse(input.Replace(tradeMark, ""));
            foreach (XNode node in element.Nodes()) {
                Console.WriteLine($"Node Type:{node.NodeType}, Node Value{node.ToString()}");
            }
        }

        internal static void StringMethods1()
        {
            string stringToSearch = "Mary had a little lamb";
            int result = stringToSearch.IndexOf('a');//takes char
            Console.WriteLine($"a is found on the {result} position.");

            int result2 = stringToSearch.IndexOf("had");//takes string
            Console.WriteLine($"\"had\" is found on the {result2} position.");

            int result3 = stringToSearch.IndexOf('a',3,4);//try another overload
            Console.WriteLine($"a is found on the {result3} position.");

            int howManyA= stringToSearch.Where(c => c == 'a').Count();
            Console.WriteLine($"there are {howManyA} a in the string");

            string stringToChop = "Mary had a little lamb, it was delicious.";

            string sub1 = stringToChop.Substring(5, 8);
            Console.WriteLine($"{sub1}");

            string sub2 = stringToChop[5..13];
            Console.WriteLine($"{sub2}");

           string choppedString =  stringToChop.Remove(5, 3);
            Console.WriteLine(choppedString);

            Console.WriteLine(stringToChop);

            Console.WriteLine(stringToChop.Replace("mary","Bob",StringComparison.OrdinalIgnoreCase));

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    Console.WriteLine("Its Weekend!");
                    break;
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    Console.WriteLine("Its Not Weekend!");
                    break;
                default:
                    break;
            }
        }

    }
}