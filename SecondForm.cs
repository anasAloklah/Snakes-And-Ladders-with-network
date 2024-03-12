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
using System.Collections;

namespace SnakesAndLadders
{
    public partial class SecondForm : Form
    {
        IPAddress host;
        IPEndPoint hostEndpoint;
        Socket clientSocket;
        static List<Socket> clientSockets;
        public static Socket currentSoket;
        public static Socket myserverSocket;
        public static Socket clientSocketTCP;
        public static int numberOfPlayer;


        Hashtable h_bord;
        Hashtable h_bord2;
        Hashtable h_snakes;
        Hashtable h_Ladders;
        Hashtable h_Location;
        Hashtable check_L_S;
        PictureBox[] smpal_pic;
        PictureBox[] snakesAndLadders;
        Label[] playerText;
        string[,] bord = new string[10, 10];
        int[,] check = new int[10, 10];

        public string snakesMessage;
        public string LadderMessage;
        public SecondForm(string mode,Socket sock)
        {
            InitializeComponent();
            
            if(mode=="server")
            {
                senderMessage.Enabled = true;
                playingButton.Enabled = true;
                IPAddress myip = Dns.GetHostAddresses(Dns.GetHostName())[1];
                string my = myip.ToString();
                hostEndpoint = new IPEndPoint(IPAddress.Broadcast, 8000);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                listIP.Items.Add(my);
                //Accptor.Enabled = true;
                 myserverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, 8001);
                 
                clientSockets = new List<Socket>();
                myserverSocket.Bind(ip);
                myserverSocket.Listen(10);
                checkPlay.Enabled = false;
                Thread t = new Thread(AcceptFun);
                t.Start();
                iniBoard();
            }
            else if (mode == "client")
            {
                string serverAdd = sock.RemoteEndPoint.ToString().Split(':')[0];
                listIP.Items.Add(serverAdd);
                reseverTCP.Enabled = true;
                checkPlay.Enabled = true;
                clientSocketTCP = sock;
                iniBoard();
                //sock
                /*
                 byte[] arr2 = new byte[1024];
                sock.Receive(arr2);
                serverMessage = ASCIIEncoding.ASCII.GetString(arr2);
                 */
            }
        }
        public static bool openGame = false;
        public void readFromServer()
        {
            
            byte[] arr2 = new byte[1024];
            clientSocketTCP.Receive(arr2);
            string serverMessage = ASCIIEncoding.ASCII.GetString(arr2);
            if (serverMessage.Substring(0,9) == "goplaying")
            {
                serverMessage = serverMessage.Split('\0')[0];
                string serverMessage1 = serverMessage.Split(':')[0];
                 snakesMessage = serverMessage.Split(':')[1];
                 LadderMessage = serverMessage.Split(':')[2];
                numberOfPlayer = int.Parse(serverMessage1.Substring(0, 11).Split(',')[1]);

                reseverTCP.Enabled = false;
                openGame = true;
                
            }
            else
            {
                //listIP.Items.Clear();
                this.Invoke((MethodInvoker)(() => listIP.Items.Clear()));
                string[] listitme = serverMessage.Split(',');
                for (int i = 0; i < listitme.Length; i++)
                {
                    //listIP.Items.Add(listitme[i]);
                    this.Invoke((MethodInvoker)(() => listIP.Items.Add(listitme[i])));
                }
            }
        }
        List<string> GetGroups()
        {
            if (listIP.InvokeRequired)
            {
                //var dlg = new GetItemsDlg(GetGroups);
                //return listIP.Invoke(dlg) as List<string>;
            }

            List<string> prodGroups = (from object item in listIP.SelectedItems select item.ToString()).ToList();



            return prodGroups;

        }
        public void AcceptFun()
        {
            while (true)
            {
                currentSoket = myserverSocket.Accept();
                string ipcurrentsocket= currentSoket.RemoteEndPoint.ToString().Split(':')[0];
                this.Invoke((MethodInvoker)(() => listIP.Items.Add(ipcurrentsocket)));
                //listIP.Items.Add(ipcurrentsocket);
                
                clientSockets.Add(currentSoket);
                List<string> values = new List<string>();
                //this.Invoke((MethodInvoker)(() => listIP.SelectedItems));
                //foreach (object o in  listIP.SelectedItems)
                //   values.Add(o.ToString());
                values=GetGroups();
                // listIP.InvokeRequired
                //string selectedItems = String.Join(",", values);
                string selectedItems = "192.168.2.5192.168.2.5,192.168.2.5,192.168.2.5";
                Byte[] recievedMsg = new byte[selectedItems.Length+1];
                recievedMsg = ASCIIEncoding.ASCII.GetBytes(selectedItems);
                for (int i = 0; i < clientSockets.Count; i++)
                {
                    clientSockets[i].Send(recievedMsg);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string selectedItems = "go to";
            //Byte[] recievedMsg = new byte[selectedItems.Length + 1];
            string snakesMessage = getSnakesPostion();
            string LadderMessage = getLaddersPostion();
            Byte[] recievedMsg = ASCIIEncoding.ASCII.GetBytes("goplaying," +( clientSockets.Count+1).ToString()+":"+ snakesMessage+":"+ LadderMessage);
            for (int i = 0; i < clientSockets.Count; i++)
            {
                clientSockets[i].Send(recievedMsg);
            }

            Form1 gsc = new Form1("server", clientSockets,null, clientSockets.Count+1, snakesMessage, LadderMessage, h_bord, h_bord2);
            gsc.Show();
            this.Visible = false;
        }

        private void senderMessage_Tick(object sender, EventArgs e)
        {
            string msgToServer = "Creating";
            byte[] msgToServerArr = Encoding.ASCII.GetBytes(msgToServer);
            clientSocket.EnableBroadcast = true;
            int msgLength = clientSocket.SendTo(msgToServerArr, hostEndpoint);

        }


        public static Thread t2;
        private void reseverTCP_Tick(object sender, EventArgs e)
        {
            if (!openGame)
            {
                t2 = new Thread(readFromServer);
                t2.Start();
            }
        }

        private void checkPlay_Tick(object sender, EventArgs e)
        {
            if(openGame)
            {
                Form1 gsc = new Form1("client", null, clientSocketTCP, numberOfPlayer, snakesMessage, LadderMessage, h_bord, h_bord2);
                gsc.Show();
                this.Visible = false;
                checkPlay.Enabled = false;
                reseverTCP.Enabled = false;
                t2.Abort();
                t2.Suspend();
                this.Close();

                
            }
        }
        void iniCheckmatrix()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    check[i, j] = 0;
                }
            }
        }
        private bool checkLOrSinColom(int x, int y, int level)
        {

            /*for (int i=x;i>= x+(level + 1); i++)
             {
                 if (check[i,y] == 1)
                     return false;
             }
             
             
             int lastval = x - (level );
             if (lastval < 0)
                 lastval = 0;
             for (int i = x; i >= lastval; i--)
             {
                 if (check[i,y] == 1)
                     return false;
             }
             
             
            for (int i = 0; i >10; i++)
            {
                if (check[i,y] == 1)
                    return false;
            }
            */
            if (check_L_S.ContainsKey(y))
            {
                return false;
            }
            return true;
        }
        private int getPostion(int level)
        {
            Random r = new Random();
            int x1 = r.Next(10 + (level * 10), 98) + 1;
            string loc = (string)h_bord[x1];
            int xp = int.Parse(loc.Split(',')[0]);
            int yp = int.Parse(loc.Split(',')[1]);
            while (!checkLOrSinColom(xp, yp, level))
            {
                x1 = r.Next(10 + (level * 10), 98) + 1;
                loc = (string)h_bord[x1];
                xp = int.Parse(loc.Split(',')[0]);
                yp = int.Parse(loc.Split(',')[1]);
            }
            loc = (string)h_bord[x1];
            xp = int.Parse(loc.Split(',')[0]);
            yp = int.Parse(loc.Split(',')[1]);
            check_L_S.Add(yp, 1);
            /// check[xp, yp] = 1;
            return x1;
        }
        private void iniBoard()
        {
            
            iniCheckmatrix();
            h_bord = new Hashtable();
            h_bord2 = new Hashtable();
            check_L_S = new Hashtable();
            int num_cile = 101;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i % 2 == 0)
                    {
                        num_cile--;
                    }
                    else
                    {
                        num_cile++;
                    }
                    bord[i, j] = "c:0";
                    string po = i.ToString() + ',' + j.ToString();
                    string loc = (i * 60).ToString() + ',' + (j * 60).ToString();
                    h_bord.Add(num_cile, po);
                    h_bord2.Add(po, num_cile);
                }
                if (i % 2 == 0)
                    num_cile = num_cile - 11;
                else
                    num_cile = num_cile - 9;
            }


        }

        private string getSnakesPostion()
        {
            string s = "";
            for (int i = 0; i < 4; i++)
            {
                int x = getPostion(i);
                if (i != 4 - 1)
                    s += "S," + x.ToString() + "," + i.ToString() + ";";
                else
                    s += "S," + x.ToString() + "," + i.ToString();
            }
            return s;
        }

        private string getLaddersPostion()
        {

            string l = "";
            for (int i = 0; i < 4; i++)
            {
                int x = getPostion(i);
                if (i != 4 - 1)
                    l += "L," + x.ToString() + "," + i.ToString() + ";";
                else
                    l += "L," + x.ToString() + "," + i.ToString();
            }
            return l;
        }
        

    }
}
