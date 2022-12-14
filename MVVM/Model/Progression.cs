using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Timers;
using System.Diagnostics;

namespace EasySaveApp.MVVM.Model
{
    internal class Progression
    {
        private int progressionValue;
        private int fileLeftToTransfert;
        private int fileTotal;

        public int ProgressionValue { get => progressionValue; set => progressionValue = value; }
        public int FileLeftToTransfert { get => fileLeftToTransfert; set => fileLeftToTransfert = value; }
        public int FileTotal { get => fileTotal; set => fileTotal = value; }

        public Progression()
        {
            ProgressionValue = 0;
            FileLeftToTransfert = 0;
            FileTotal = 0;
        }
        public static Socket Connect()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress address = host.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, 11000);
            Socket socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);
            return socket;
        }

        public static Socket AllowConnection(Socket socket)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            MessageBox.Show("Please waiting for connection");
            while (timer.ElapsedMilliseconds < 3000) //test during 3 second if there is distant console.
            {
                if (socket.Poll(0, SelectMode.SelectRead))
                {
                    MessageBox.Show("Connection accepted");
                    Socket client = socket.Accept();
                    Application.Current.Properties["Socket"] = client;
                    return client;
                }
            }
            MessageBox.Show("Any distant console connected");
            return null;

        }

        public static void SendMessage(Socket client, string message)
        {
                byte[] messageByte = Encoding.ASCII.GetBytes(message + "<STOP>");
                client.Send(messageByte);
           
        }

        public static void Disconnect(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
