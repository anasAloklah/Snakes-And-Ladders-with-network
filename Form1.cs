using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SnakesAndLadders
{
    public partial class Form1 : Form
    {
        Random r;
        int p1=1;
        int p2 = 1;
        
        int i = 0;
        int numOfPlayer;
        int[] players;
        int i_roll = 0;
        Hashtable h_bord;
        Hashtable h_bord2;
        Hashtable h_snakes;
        Hashtable h_Ladders;
        Hashtable h_Location;
        Hashtable check_L_S;
        PictureBox[] smpal_pic;
        PictureBox[] snakesAndLadders;
        Label[] playerText;
        string[,] bord=new string [10,10];
        int[,] check = new int[10, 10];

        IPAddress host;
        IPEndPoint hostEndpoint;
        Socket clientSocket;
        static List<Socket> clientSockets;
        public static Socket clientSocketTCP;
        public static Socket myserverSocket;
        public string mode;
        public  string snakesMessage;
        public  string LadderMessage;

        public Form1(string mode, List<Socket> Sockets, Socket clientSocketTCP1,int numberOfPlayer,string snakesMessage, string LadderMessage, Hashtable h_bord, Hashtable h_bord2)
        {
            this.mode = mode;
            this.snakesMessage = snakesMessage;
            this.LadderMessage = LadderMessage;
            this.h_bord = h_bord;
            this.h_bord2 = h_bord2;
            numOfPlayer = numberOfPlayer;
            smpal_pic = new PictureBox[numOfPlayer];
            snakesAndLadders = new PictureBox[8];
            players = new int[numOfPlayer];
            playerText = new Label[numOfPlayer];
            foreach (var box in smpal_pic)
                this.Controls.Add(box);
            foreach (var box in snakesAndLadders)
                this.Controls.Add(box);
            foreach (var box2 in playerText)
                this.Controls.Add(box2);
            for (int i = 0; i < numOfPlayer; i++)
            {
                playerText[i] = new Label();
                playerText[i].Text = "player" + (i + 1).ToString() + " :";
                playerText[i].Location = new Point(725, 90 + (i * 30));
                playerText[i].Enabled = true;
                playerText[i].Visible = true;
                this.Controls.Add(playerText[i]);
            }


            

            clientSockets = Sockets;
            clientSocketTCP = clientSocketTCP1;
            InitializeComponent();
          
           
        }
        /*
        private void goToladderAndSnake(int x, int y, int numPlayer)
        {
            string loc_S = (string)h_bord[x];
            string loc_D = (string)h_bord[y];
            int xps = int.Parse(loc_S.Split(',')[0]);
            int yps = int.Parse(loc_S.Split(',')[1]);
            int xpd = int.Parse(loc_D.Split(',')[0]);
            int ypd = int.Parse(loc_D.Split(',')[1]);
            double dis = Math.Sqrt(Math.Pow(xps - xpd, 2) + Math.Pow(yps - ypd, 2));
            for (int i = yps*60; i <= (int)dis*60; i+=60)
            {
                
                System.Threading.Thread.Sleep(500);
                if (numPlayer == 1)
                    pl1.Location = new Point(i * 60, xps * 60);
                else if (numPlayer == 2)
                    pl2.Location = new Point(i * 60, xps * 60);
            }
        }*/

        private void restGame()
        {
            for (int i = 0; i < numOfPlayer; i++)
            {
                players[i] = 1;
                
                smpal_pic[i].Location = new Point(0 * 60, 9 * 60);
                
            }
        }
        private void checkWinner()
        {
            for (int i = 0; i < numOfPlayer; i++)
            {
                if (players[i] >= 100)
                {
                    MessageBox.Show("the player"+(i+1)+" is win");
                    restGame();
                }
            }
        }
        private void goToPlyer(int x, int y, int numPlayer)
        {
            if (y > 100)
                y = 100;
            for (int i = x; i <= y; i++)
            {
                string loc = (string)h_bord[i];
                int xp = int.Parse(loc.Split(',')[0]);
                int yp = int.Parse(loc.Split(',')[1]);
                System.Threading.Thread.Sleep(100);
                smpal_pic[numPlayer].Location = new Point(yp * 60, xp * 60);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int x=r.Next(0, 6)+1;
            label1.Text = x.ToString();
            Image image = Image.FromFile("r"+x.ToString()+".jpg");
            randomPic.Image = image;
            button1.Text = "roll player :" + (((i+1) % numOfPlayer) + 1).ToString();
            i_roll = (i % numOfPlayer);
            
            goToPlyer(players[i_roll], players[i_roll] + x, i_roll);
            players[i_roll] += x;

            if (h_Ladders.ContainsKey(players[i_roll]))
            {
                // goToladderAndSnake(p1, (int)h_Ladders[p1], 1);
                playerText[i_roll].Text = "player" + (i_roll + 1).ToString() + " : "+ players[i_roll].ToString();
                if (h_bord.ContainsKey(players[i_roll]))
                {
                    string loc = (string)h_bord[players[i_roll]];
                    int xp = int.Parse(loc.Split(',')[0]);
                    int yp = int.Parse(loc.Split(',')[1]);
                    smpal_pic[i_roll].Location = new Point(yp * 60, xp * 60);
                }
                players[i_roll] = (int)h_Ladders[players[i_roll]];
                MessageBox.Show("the player"+ (i_roll+1)+" in ladder");
            }
            if (h_snakes.ContainsKey(players[i_roll]))
            {
                //goToladderAndSnake(p1, (int)h_snakes[p1], 1);
                playerText[i_roll].Text = "player" + (i_roll + 1).ToString() + " : " + players[i_roll].ToString();
                if (h_bord.ContainsKey(players[i_roll]))
                {
                    string loc = (string)h_bord[players[i_roll]];
                    int xp = int.Parse(loc.Split(',')[0]);
                    int yp = int.Parse(loc.Split(',')[1]);
                    smpal_pic[i_roll].Location = new Point(yp * 60, xp * 60);
                }
                players[i_roll] = (int)h_snakes[players[i_roll]];
                MessageBox.Show("the player" + (i_roll+1) + " pit from snakes");
            }
            if (h_bord.ContainsKey(players[i_roll]))
            {
                string loc = (string)h_bord[players[i_roll]];
                int xp = int.Parse(loc.Split(',')[0]);
                int yp = int.Parse(loc.Split(',')[1]);
                smpal_pic[i_roll].Location = new Point(yp * 60, xp * 60);
                playerText[i_roll].Text = "player" + (i_roll + 1).ToString() + " : " + players[i_roll].ToString();
            }
            i++;
            checkWinner();
        }
 
        
        public void readBorder()
        {
            byte[] arr2 = new byte[1024];
           
            clientSocketTCP.Receive(arr2);
            string serverMessage = ASCIIEncoding.ASCII.GetString(arr2);
            serverMessage = serverMessage.Split('\0')[0];
            snakesMessage = serverMessage.Split(':')[0];
            LadderMessage = serverMessage.Split(':')[1];
            puttingSnakes(snakesMessage);
            puttingLadders(LadderMessage);
        }

        private void iniBoard()
        {
            r = new Random();
            
            h_snakes = new Hashtable();
            h_Ladders = new Hashtable();
            h_Location = new Hashtable();
            check_L_S = new Hashtable();
            button1.Text = "roll player :1";
            //iniCheckmatrix();
            for (int i = 0; i < numOfPlayer; i++)
            {
                //this.Controls.Add(smpal_pic[i]);
                players[i] = 1;
                smpal_pic[i] = new PictureBox();
                Image image22 = Image.FromFile("player" + (i + 1).ToString() + ".png");
                smpal_pic[i].Image = image22;
                smpal_pic[i].Location = new Point(0, 540);
                smpal_pic[i].Show();
                smpal_pic[i].Enabled = true;
                smpal_pic[i].Visible = true;
                smpal_pic[i].Size = new Size(60, 60);
                pictureBox1.Controls.Add(smpal_pic[i]);
                smpal_pic[i].BackColor = Color.Transparent;

            }
            


        }
        private void puttingSnakes(string snakes)
        {
            string[] snakesArray = snakes.Split(';');
            for (int i = 0; i < snakesArray.Length; i++)
            {
                snakesAndLadders[i] = new PictureBox();
                Image image22 = Image.FromFile("L" + (i + 1).ToString() + ".png");
                snakesAndLadders[i].Image = image22;
                int x = int.Parse(snakesArray[i].Split(',')[1]);
                //int x = getPostion(i);

                string loc = (string)h_bord[x];
                int xp = int.Parse(loc.Split(',')[0]);
                int yp = int.Parse(loc.Split(',')[1]);
                //string po = (xp+(i+1)).ToString() + ',' + yp.ToString();
                string i_j = (xp + (i + 1)).ToString() + "," + yp.ToString();
                int loc2 = (int)h_bord2[i_j];
                h_Ladders.Add(loc2, x);
                //int loc2=(int) h_bord2.ContainsKey(i_j);
                //h_Ladders.Add(7, 14);
                //h_Ladders.Add(20, 40); h_Ladders.Add(37, 57); h_Ladders.Add(47, 87);
                snakesAndLadders[i].Location = new Point(yp * 60, xp * 60);
                snakesAndLadders[i].Show();
                snakesAndLadders[i].Enabled = true;
                snakesAndLadders[i].Visible = true;
                snakesAndLadders[i].Size = new Size(60, 60 * (i + 2));
                pictureBox1.Controls.Add(snakesAndLadders[i]);
                snakesAndLadders[i].BackColor = Color.Transparent;
            }
        }
        private void puttingLadders(string Ladder)
        {
            string[] ladderArray = Ladder.Split(';');
            for (int i = 4, j = 0; i < 8; i++, j++)
            {
                snakesAndLadders[i] = new PictureBox();
                Image image22 = Image.FromFile("S" + (j + 1).ToString() + ".png");
                snakesAndLadders[i].Image = image22;
                int x = int.Parse(ladderArray[j].Split(',')[1]);
                //int x = getPostion(j);

                string loc = (string)h_bord[x];
                int xp = int.Parse(loc.Split(',')[0]);
                int yp = int.Parse(loc.Split(',')[1]);
                string i_j = (xp + (j + 1)).ToString() + "," + yp.ToString();
                int loc2 = (int)h_bord2[i_j];
                h_snakes.Add(x, loc2);
                //h_snakes.Add(17, 4); h_snakes.Add(68, 28); h_snakes.Add(78, 43); h_snakes.Add(95, 66);
                snakesAndLadders[i].Location = new Point(yp * 60, xp * 60);
                snakesAndLadders[i].Show();
                snakesAndLadders[i].Enabled = true;
                snakesAndLadders[i].Visible = true;
                snakesAndLadders[i].Size = new Size(60, 60 * (j + 2));
                pictureBox1.Controls.Add(snakesAndLadders[i]);
                snakesAndLadders[i].BackColor = Color.Transparent;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //h_bord.Clear();
            if (mode == "server")
            {
                Thread.Sleep(1000);
                senderMessage.Enabled = true;
                //playingButton.Enabled = true;
                hostEndpoint = new IPEndPoint(IPAddress.Broadcast, 8000);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                iniBoard();
                //string snakesMessage = getSnakesPostion();
                //string LadderMessage = getLaddersPostion();
                puttingSnakes(snakesMessage);
                puttingLadders(LadderMessage);
                /*
                string dataBorder = snakesMessage + ":" + LadderMessage;
                Byte[] recievedMsg = ASCIIEncoding.ASCII.GetBytes(dataBorder);
                
                for (int i = 0; i < clientSockets.Count; i++)
                {
                    clientSockets[i].Send(recievedMsg);
                }
                Thread.Sleep(1000);
                for (int i = 0; i < clientSockets.Count; i++)
                {
                    clientSockets[i].Send(recievedMsg);
                }
                */

            }
            else if (mode == "client")
            {
                iniBoard();
                puttingSnakes(snakesMessage);
                puttingLadders(LadderMessage);
                Thread t = new Thread(readBorder);
                t.Start();
                
                
            }
        }

        private void senderMessage_Tick(object sender, EventArgs e)
        {
            string msgToServer = "Playing";
            byte[] msgToServerArr = Encoding.ASCII.GetBytes(msgToServer);
            clientSocket.EnableBroadcast = true;
            int msgLength = clientSocket.SendTo(msgToServerArr, hostEndpoint);
        }
    }
}
