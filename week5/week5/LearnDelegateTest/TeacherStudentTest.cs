using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnDelegate;
namespace LearnDelegateTest
{
    [TestClass]
    public class TeacherStudentTest
    {
        [TestMethod]
        public void TeacherHandlesStudentAskQuestion() {

            Teacher t = new();
            Student tom = new("Tom");
            Student alice = new("Alice");
            Student bob = new("Bob");

            tom.AskQuestion += t.AnswerQuestion;
            bob.AskQuestion += t.AnswerQuestion;
            alice.AskQuestion += t.AnswerQuestion;

            tom.RaiseHand();
            bob.RaiseHand();
            alice.RaiseHand();
        }
        [TestMethod]
        public void StudentHandleTeacherFlippingTable()
        {

            Teacher t = new();
            Student tom = new("Tom");
            Student alice = new("Alice");
            Student bob = new("Bob");

            t.FlipsTable += tom.HandleTableFlip;//attach

            t.FlipsTable += bob.HandleTableFlip;
            t.FlipsTable += alice.HandleTableFlip;
            t.FlipsTable -= tom.HandleTableFlip;//detach

            t.GetMad();
        }

        //Coding prompt: when asked 10 questions, teacher gets mad
        // 1 - create a counter in Teacher Class that increments when student ask question
        // 2 - When the counter exceeds 10, Teacher flips table

        [TestMethod]
        public void TeacherGetsMadAfter10Questions() { 
        
            Teacher t = new();
            Student tom = new("tom");

            tom.AskQuestion += t.AnswerQuestion;
            
            t.FlipsTable += ()=> Debug.Print("Teacher flipped table"); //anonymous method to handle the event

            t.FlipsTable += tom.HandleTableFlip;

            for (int i = 0; i <= 10; i++) {
                tom.RaiseHand();
            }
        
        }
    }
}
