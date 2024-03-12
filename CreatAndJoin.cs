using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace SnakesAndLadders
{
    public partial class CreatAndJoin : Form
    {
        IPAddress host;
        IPEndPoint hostEndpoint;
        Socket clientSocket;
        public static Socket serverSocket;
        public static EndPoint endPoint;
        public static string ipServer; 
        public CreatAndJoin()
        {
            InitializeComponent();
             endPoint = new IPEndPoint(IPAddress.Any, 8000);
            // Or use 127.0.0.1 address that refers to local host
            //8000 is the number of any free port
            //use command netstat to find all used ports.

             serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            serverSocket.Bind(endPoint);
           // ThreadPool.QueueUserWorkItem(HandleClient, serverSocket);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SecondForm gsc = new SecondForm("server",null);
            gsc.Show();
            this.Visible = false;
            //creatButton.Enabled = true;
            //this.Close();



            // clientSocket.Shutdown(SocketShutdown.Both);
            //clientSocket.Close();
            //clientSocket.Shutdown(SocketShutdown g);
        }

        
        private void CreatAndJoin_Load(object sender, EventArgs e)
        {
            
        }
        public void HandleClient(Object serverSocket1)
        {
            Socket serverSocket = (Socket)serverSocket1;
            string msg;
            byte[] msgFromClient = new byte[1024];
            try
            {
                int length = serverSocket.ReceiveFrom(msgFromClient, ref endPoint);
                string sss= endPoint.ToString();
                msg = Encoding.ASCII.GetString(msgFromClient, 0, length);
                creatButton.Enabled = false;
            }
            catch (Exception e)
            {

            }
            //Console.WriteLine(msg);

        }
        public static bool firstfind = false;
        public void HandleClient2()
        {
            
            string msg;
            byte[] msgFromClient = new byte[1024];
            try
            {
                int length = serverSocket.ReceiveFrom(msgFromClient, ref endPoint);
                 msg = Encoding.ASCII.GetString(msgFromClient, 0, length);
                string sss = endPoint.ToString();
                 ipServer = sss.Split(':')[0];
                if (msg == "Creating"&&!firstfind)
                {
                    //creatButton.Enabled = false;
                    //joinButton.Enabled = true;
                    this.Invoke((MethodInvoker)(() => creatButton.Enabled=false));
                    this.Invoke((MethodInvoker)(() => joinButton.Enabled = true));
                }
                else if (msg == "Playing")
                {
                    //creatButton.Enabled = false;
                    //joinButton.Enabled = false;
                    this.Invoke((MethodInvoker)(() => creatButton.Enabled = false));
                    this.Invoke((MethodInvoker)(() => joinButton.Enabled = false));
                    firstfind = true;
                }
                
            } catch(Exception e)
            {

            }
            //Console.WriteLine(msg);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ipServer
            //string ipaddress = Console.ReadLine();
            IPEndPoint ipend = new IPEndPoint(IPAddress.Parse(ipServer), 8001);
            //Socket =new SocketAddress(Sockets.AddressFamily)
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(ipend);
            SecondForm gsc = new SecondForm("client", sock);
            gsc.Show();
            this.Visible = false;
        }

        private void reseverUDP_Tick(object sender, EventArgs e)
        {
            Thread t = new Thread(HandleClient2);
            t.Start();
        }
    }
}
