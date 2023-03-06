//sarah newman -- prog2200 assignment 1

using System;
using System.Linq;

using ServerClientChat;

namespace SynchronousChatProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            Int32 port = 1300;
            const string ipAddr = "127.0.0.1";

            if (args.Length == 0)
            {
                Client client = new Client(port, ipAddr);
            } 
            else if (args.Contains("-server"))
            {
                Server server = new Server(port, ipAddr);
            }
            else
            {
                Console.WriteLine("error, leave blank or type '-server' when starting up the program");
            }
        }
    }
}