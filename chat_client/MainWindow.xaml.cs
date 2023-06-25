using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace chat_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UdpClient client = new();
        private bool isListening = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Listen()
        {
            while (isListening)
            {
                var result = await client.ReceiveAsync();
                string message = Encoding.UTF8.GetString(result.Buffer);
                msgList.Items.Add(message);
            }
        }

        private void SendBtnClick(object sender, RoutedEventArgs e)
        {
            string text = msgTxtBox.Text;
            SendMessage(text);
        }

        private void JoinBtnClick(object sender, RoutedEventArgs e)
        {
            SendMessage("<join>");
            isListening = true;
            Listen();
        }

        private void LeaveBtnClick(object sender, RoutedEventArgs e)
        {
            SendMessage("<leave>");
            isListening = false;
        }

        private void SendMessage(string text)
        {
            IPEndPoint serverIp = new(IPAddress.Parse(ipTxtBox.Text), int.Parse(portTxtBox.Text));

            byte[] data = Encoding.UTF8.GetBytes(text);
            client.Send(data, serverIp);
        }
    }
}
