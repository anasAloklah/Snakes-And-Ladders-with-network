namespace SnakesAndLadders
{
    partial class SecondForm
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
            this.listIP = new System.Windows.Forms.ListBox();
            this.playingButton = new System.Windows.Forms.Button();
            this.senderMessage = new System.Windows.Forms.Timer(this.components);
            this.reseverTCP = new System.Windows.Forms.Timer(this.components);
            this.checkPlay = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // listIP
            // 
            this.listIP.FormattingEnabled = true;
            this.listIP.Location = new System.Drawing.Point(45, 22);
            this.listIP.Name = "listIP";
            this.listIP.Size = new System.Drawing.Size(229, 264);
            this.listIP.TabIndex = 0;
            // 
            // playingButton
            // 
            this.playingButton.Enabled = false;
            this.playingButton.Location = new System.Drawing.Point(184, 311);
            this.playingButton.Name = "playingButton";
            this.playingButton.Size = new System.Drawing.Size(90, 23);
            this.playingButton.TabIndex = 1;
            this.playingButton.Text = "go to play";
            this.playingButton.UseVisualStyleBackColor = true;
            this.playingButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // senderMessage
            // 
            this.senderMessage.Tick += new System.EventHandler(this.senderMessage_Tick);
            // 
            // reseverTCP
            // 
            this.reseverTCP.Tick += new System.EventHandler(this.reseverTCP_Tick);
            // 
            // checkPlay
            // 
            this.checkPlay.Tick += new System.EventHandler(this.checkPlay_Tick);
            // 
            // SecondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 480);
            this.Controls.Add(this.playingButton);
            this.Controls.Add(this.listIP);
            this.Name = "SecondForm";
            this.Text = "SecondForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listIP;
        private System.Windows.Forms.Button playingButton;
        private System.Windows.Forms.Timer senderMessage;
        private System.Windows.Forms.Timer reseverTCP;
        private System.Windows.Forms.Timer checkPlay;
    }
}