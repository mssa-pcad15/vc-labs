using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals.LearnTypes //notice the generated namespace matched folder name
{
    public class LearnClass
    {
        //fields, should use camelCasing with leading _
        public int _instanceMember;
        public static int StaticMember;

        private DateTime _dob;

        private string _password; //private field
        
        //properties
        //property allow code to validate value
        public int Age
        {
            get
            {
                return  DateTime.Now.Year - _dob.Year ;
            }
        }
        public int ChineseAge
        {
            get
            {
                return DateTime.Now.Year - _dob.Year + 1;
            }
        }
        public string Password {  //public property

            set {
                if (value.Length > 8 && value.Length < 15)
                {
                    _password = value;
                }
            }
        }

        //methods
        public bool VerifyPassword(string passwordToVerify)
        {
            return this._password == passwordToVerify;
        }

        //constructor
        public LearnClass(DateTime dob,string password=""){ //constructor
            this._password = password;
            this._dob = dob;
        }
    }
}
