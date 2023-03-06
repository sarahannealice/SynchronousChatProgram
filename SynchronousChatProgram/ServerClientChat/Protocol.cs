using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ServerClientChat
{
    public class Protocol
    {
        //protected so that the server class extending client can access them
        protected TcpClient Customer { get; set; }//https://stackoverflow.com/a/5096967
        protected NetworkStream Stream;
        
        //default constructor needed for the server/client class to extend protocol
        protected Protocol(){}
        
        //checks if stream is open then translates incoming msg to string if so
        //also checking for user input if they want to disconnect or send a msg
        protected void Listening()
        {
            //enter listening loop
            while (true)
            {
                //checks if the connection is still open
                //if an error is thrown it will deem it disconnected
                //breaks listening loop if disconnected
                try
                {
                    SendMsg(" ");
                }
                catch (IOException)
                {
                    break;
                }

                //looks for sent messages
                ReceiveMsg();

                if (!Console.KeyAvailable)
                {
                    continue;
                }
                
                //if statement for if input mode/quit/esc is selected
                //using .Key and ConsoleKey functions to help distinguish which key has been pressed
                var input = Console.ReadKey(true);
                
                if (input.Key == ConsoleKey.I)
                {
                    Console.Write("send >> ");
                    string msg = Console.ReadLine();

                    if (msg.ToLower() == "quit")
                    {
                        Console.WriteLine("you wish to be disconnected");
                        Disconnect();
                        break;
                    }

                    SendMsg(msg);
                } 
                else if (input.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("you wish to be disconnected");
                    Disconnect();
                    break;
                }            
            }
        }

        //checks if there is anything on the stream and then prints the msg if so
        private void ReceiveMsg()
        {
            //checks for msg from connected server (in stream)
            if (!Stream.DataAvailable) 
            {
                return;
            }
            
            //buffer for reading data
            var bytes = new byte[256];
            var i = Stream.Read(bytes, 0, bytes.Length);
            string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

            //prints only if there are characters to account for
            if (!string.IsNullOrWhiteSpace(data))
            {
                Console.WriteLine(data);
            }
            
        }
        
        //translates string to ascii and stores as bytes then sends to connected server
        private void SendMsg(string msg)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(msg);
            Stream.Write(data, 0, msg.Length);
        }

        public void Disconnect()
        {
            Stream.Close();
            Environment.Exit(1);
        }
        
    }
}