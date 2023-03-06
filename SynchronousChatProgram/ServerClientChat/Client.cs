//sarah newman -- prog2200 assignment 1

using System;
using System.Net.Sockets;

namespace ServerClientChat
{
    public class Client : Protocol
    {
        
        protected Client() {}

        public Client(int port, string ipAddr)
        {
            //try-catch to help prevent any errors while connecting client-server
            try
            {
                //connect to server
                Customer = new TcpClient(ipAddr, port);
                
                //get stream object for reading and writing
                Stream = Customer.GetStream();
                
                Console.WriteLine("connection with server has been established");

                //start listening loop
                Listening();
            }
            catch (Exception)
            {
                //do nothing as it will catch upon closing
            }
            finally
            {
                //explicit close is not necessary since Customer.Dispose() will be called automatically -- Microsoft comment
                Console.WriteLine("connection closed");
            }
        }
        
        
    }
}