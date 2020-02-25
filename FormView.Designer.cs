namespace CoverYourAssets
{
    partial class FormView
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
            this.Card1 = new System.Windows.Forms.PictureBox();
            this.Card2 = new System.Windows.Forms.PictureBox();
            this.Card3 = new System.Windows.Forms.PictureBox();
            this.Card4 = new System.Windows.Forms.PictureBox();
            this.PlayersHand = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Card1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card4)).BeginInit();
            this.SuspendLayout();
            // 
            // Card1
            // 
            this.Card1.Location = new System.Drawing.Point(12, 289);
            this.Card1.Name = "Card1";
            this.Card1.Size = new System.Drawing.Size(109, 149);
            this.Card1.TabIndex = 0;
            this.Card1.TabStop = false;
            // 
            // Card2
            // 
            this.Card2.Location = new System.Drawing.Point(127, 289);
            this.Card2.Name = "Card2";
            this.Card2.Size = new System.Drawing.Size(109, 149);
            this.Card2.TabIndex = 1;
            this.Card2.TabStop = false;
            // 
            // Card3
            // 
            this.Card3.Location = new System.Drawing.Point(242, 289);
            this.Card3.Name = "Card3";
            this.Card3.Size = new System.Drawing.Size(109, 149);
            this.Card3.TabIndex = 2;
            this.Card3.TabStop = false;
            // 
            // Card4
            // 
            this.Card4.Location = new System.Drawing.Point(357, 289);
            this.Card4.Name = "Card4";
            this.Card4.Size = new System.Drawing.Size(109, 149);
            this.Card4.TabIndex = 3;
            this.Card4.TabStop = false;
            // 
            // PlayersHand
            // 
            this.PlayersHand.Location = new System.Drawing.Point(12, 289);
            this.PlayersHand.Name = "PlayersHand";
            this.PlayersHand.Size = new System.Drawing.Size(454, 149);
            this.PlayersHand.TabIndex = 4;
            this.PlayersHand.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Card1);
            this.Controls.Add(this.Card4);
            this.Controls.Add(this.Card3);
            this.Controls.Add(this.Card2);
            this.Controls.Add(this.PlayersHand);
            this.Name = "FormView";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Card1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Card4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Card1;
        private System.Windows.Forms.PictureBox Card2;
        private System.Windows.Forms.PictureBox Card3;
        private System.Windows.Forms.PictureBox Card4;
        private System.Windows.Forms.Panel PlayersHand;
    }
}

