using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnDelegate
{
    public class Teacher
    {
        private int _counter = 0; 

        public event Action FlipsTable; // adding the event event keyword turns field into event, so delegate
        // can only be appended to instead of being overwritten.
        public void AnswerQuestion(Student who,string question) 
        {
            if (++_counter > 10)
            { 
                GetMad();
                return;
            } 
        
            who.Listen($"Good Question, {who.Name}!");
        }
        public void GetMad()
        {
            //if (FlipsTable != null) { FlipsTable.Invoke(); } //only flips table when being observed.
            FlipsTable?.Invoke();//shorthand "Elvis" express, same as above
        }

    }
    public class Student(string _name)
    {
        public event Action<Student,String> AskQuestion;// adding the event event keyword turns field into event, so delegate
        // can only be appended to instead of being overwritten.

        public string Name => _name;

        public void Listen(string Answer)
        {
            Debug.WriteLine(Answer);
        }

        public void RaiseHand() {
            AskQuestion.Invoke(this, "What is delegate?");
        }

        public void HandleTableFlip() {
            if (DateTime.Now.Ticks % 2 == 0)
            {
                Debug.WriteLine($" {Name} : ┬─┬ノ( º _ ºノ)");
            }
            else
            {
                Debug.WriteLine($"{Name}: (ノಠ益ಠ)ノ彡┻━┻");
            }
        }
    }
    //public delegate void delAskQuestion(Student who,string question); //retired in favor of Action Delegate
    //public delegate void delFlipTable();//retired in favor of Action Delegate
}
