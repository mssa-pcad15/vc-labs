using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDelegate
{
    public class Kata
    {
        public static string SpinWords(string input) {

            return new SpinWordSolver(input).Reversed;
        }
    }

    internal class SpinWordSolver
    {
        private string _initialString = string.Empty;
        private readonly int lengthOfWordToSpin;

        public string Reversed { 
        
            get
            {
                string[] splitted = _initialString.Split(' ');

                //complete this code
                Func<string[], string> processor =
                    (words) =>
                    {
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i].Length >= lengthOfWordToSpin)
                                words[i] = new String(words[i].Reverse().ToArray());
                        }
                        return string.Join(' ',words);
                    };
                
                return processor(splitted); 
            }
        
        }

        internal SpinWordSolver(string input,int lengthOfWordToSpin = 5) { 
            this._initialString = input;
            this.lengthOfWordToSpin = lengthOfWordToSpin;
        }
    }
}
