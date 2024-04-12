using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Messanger.Model
{
    internal class Logger
    {
        public string Message { get; }


        public static string Info(string message)
        { return Log("Info", message); }
        public static string Warning(string message)
        { return Log("Warning", message); }
        public static string Error(string message)
        { return Log("Error", message); }
        private static string Log(string Type, string message) 
        {

            return $"[ {Type} ] - {message}";
        }

    }
}
