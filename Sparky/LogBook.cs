using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public interface ILogBook
    {
        public int Logseverity { get; set; } 
        public string LogType { get; set; }
        void Message(string message); 
        bool LogToDb(string message);
        bool LogBalanceAfterWWithdrawal(int balanceAfterWithdrawal); 
        string MessageWithReturnStr(string message);
        bool LogWithOutputResult(string str, out string outputStr); 
        bool LogWithRefObj(ref Customer customer);
    }

    public class LogBook :ILogBook 
    {
        public int Logseverity { get; set ; }
        public string LogType { get ; set ; }

        public bool LogBalanceAfterWWithdrawal(int balanceAfterWithdrawal)
        {
            Console.WriteLine("Success");
            if (balanceAfterWithdrawal >= 0)
            {
                return true;
            }
            Console.WriteLine("Fail");
            return false;
        }

        public bool LogToDb(string message)
        {
            Console.WriteLine(message);
            return true;
        }

        public bool LogWithOutputResult(string str, out string outputStr)
        {
            outputStr = "Hello " + str;
            return true; 
        }

        public bool LogWithRefObj(ref Customer customer)
        {
            return true; 
        }

        public void Message(string message)
        {
            Console.WriteLine(message); 
        }

        public string MessageWithReturnStr(string message)
        {
            Console.WriteLine(message); 
            return message.ToLower();
        }
    }
    

    //public class LogFaker : ILogBook
    //{
    //    public void Message(string message)
    //    {

    //    }
    //}
}
