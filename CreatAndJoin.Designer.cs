namespace SnakesAndLadders
{
    partial class CreatAndJoin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.creatButton = new System.Windows.Forms.Button();
            this.joinButton = new System.Windows.Forms.Button();
            this.reseverUDP = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // creatButton
            // 
            this.creatButton.Location = new System.Drawing.Point(101, 48);
            this.creatButton.Name = "creatButton";
            this.creatButton.Size = new System.Drawing.Size(75, 23);
            this.creatButton.TabIndex = 0;
            this.creatButton.Text = "creat";
            this.creatButton.UseVisualStyleBackColor = true;
            this.creatButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // joinButton
            // 
            this.joinButton.Enabled = false;
            this.joinButton.Location = new System.Drawing.Point(101, 88);
            this.joinButton.Name = "joinButton";
            this.joinButton.Size = new System.Drawing.Size(75, 23);
            this.joinButton.TabIndex = 1;
            this.joinButton.Text = "join";
            this.joinButton.UseVisualStyleBackColor = true;
            this.joinButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // reseverUDP
            // 
            this.reseverUDP.Enabled = true;
            this.reseverUDP.Tick += new System.EventHandler(this.reseverUDP_Tick);
            // 
            // CreatAndJoin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.joinButton);
            this.Controls.Add(this.creatButton);
            this.Name = "CreatAndJoin";
            this.Text = "CreatAndJoin";
            this.Load += new System.EventHandler(this.CreatAndJoin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button creatButton;
        private System.Windows.Forms.Button joinButton;
        private System.Windows.Forms.Timer reseverUDP;
    }
}