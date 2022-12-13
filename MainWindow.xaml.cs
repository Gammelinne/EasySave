using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace EasySaveApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SwitchLanguage("en");
            string json = File.ReadAllText("../../../Settings.json");
            Dictionary<string, string> setting = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            Application.Current.Properties["TypeOfLog"] = setting["TypeOfLog"];
            Application.Current.Properties["ExtensionToCrypt"] = ".txt .exe";
            Application.Current.Properties["CryptKey"] = "100";
            Application.Current.Properties["Software"] = "vlc notepad";

            Socket socket = Connect();
            Socket client = AllowConnection(socket);
            SendMessage(client, "Salut1");
            Thread.Sleep(40);
            SendMessage(client, "<END>");
            Disconnect(client);

        }

        private static Socket Connect()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress address = host.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, 11000);
            Socket socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);
            return socket;
        }

        private static Socket AllowConnection(Socket socket)
        {
            Socket acceptedSocket = socket.Accept();
            return acceptedSocket;
        }

        private static void SendMessage(Socket client, string message)
        {
            byte[] messageByte = Encoding.ASCII.GetBytes(message + "<STOP>");
            client.Send(messageByte);
        }

        private static void Disconnect(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private void FrenchButton_Checked(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("fr");
        }

        private void EnglishButton_Checked(object sender, RoutedEventArgs e)
        {
            SwitchLanguage("en");
        }

        private void SwitchLanguage(string languageCode)
        {
            ResourceDictionary dict = new ResourceDictionary
            {
                Source = languageCode switch
                {
                    "en" => new Uri(@"..\Languages\StringResources_en.xaml", UriKind.Relative),
                    "fr" => new Uri(@"..\Languages\StringResources_fr.xaml", UriKind.Relative),
                    _ => new Uri(@"..\Languages\StringResources_en.xaml", UriKind.Relative),
                }
            };
            Resources.MergedDictionaries.Add(dict);
        }
    }
}