//sarah newman -- prog2200 assignment 1

using System;
using System.Net;
using System.Net.Sockets;

namespace ServerClientChat
{
    public class Server : Protocol
    {
        private TcpListener _host;

        public Server(int port, string ipAddr)
        {
            try
            {
                IPAddress localAddr = IPAddress.Parse(ipAddr);

                _host = new TcpListener(localAddr, port);
                _host.Start();

                Console.WriteLine("waiting for a client connection...");

                //perform a blocking call to accept requests
                Customer = _host.AcceptTcpClient();

                //get a stream object for reading and writing
                Stream = Customer.GetStream();
                
                Console.WriteLine("connection with client has been established");

                Listening();
            }
            catch (SocketException e)
            {
                Console.WriteLine("socket exception: {0}", e);
            }
            finally
            {
                _host.Stop();
                Console.WriteLine("connection closed");
            }
        }
    }
}