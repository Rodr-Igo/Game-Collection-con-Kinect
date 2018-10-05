namespace SnakeGame
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bucle = new System.Windows.Forms.Timer(this.components);
            this.CordY = new System.Windows.Forms.Label();
            this.CordX = new System.Windows.Forms.Label();
            this.puntaje = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.game = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.game)).BeginInit();
            this.SuspendLayout();
            // 
            // bucle
            // 
            this.bucle.Enabled = true;
            this.bucle.Tick += new System.EventHandler(this.bucle_Tick);
            // 
            // CordY
            // 
            this.CordY.AutoSize = true;
            this.CordY.Location = new System.Drawing.Point(846, 502);
            this.CordY.Name = "CordY";
            this.CordY.Size = new System.Drawing.Size(46, 17);
            this.CordY.TabIndex = 12;
            this.CordY.Text = "label3";
            // 
            // CordX
            // 
            this.CordX.AutoSize = true;
            this.CordX.Location = new System.Drawing.Point(743, 502);
            this.CordX.Name = "CordX";
            this.CordX.Size = new System.Drawing.Size(46, 17);
            this.CordX.TabIndex = 11;
            this.CordX.Text = "label2";
            // 
            // puntaje
            // 
            this.puntaje.AutoSize = true;
            this.puntaje.Font = new System.Drawing.Font("Yu Gothic UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.puntaje.Location = new System.Drawing.Point(523, 470);
            this.puntaje.Name = "puntaje";
            this.puntaje.Size = new System.Drawing.Size(42, 50);
            this.puntaje.TabIndex = 10;
            this.puntaje.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(365, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 50);
            this.label1.TabIndex = 9;
            this.label1.Text = "Puntaje:";
            // 
            // game
            // 
            this.game.Location = new System.Drawing.Point(45, 36);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(1130, 420);
            this.game.TabIndex = 8;
            this.game.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1317, 660);
            this.Controls.Add(this.CordY);
            this.Controls.Add(this.CordX);
            this.Controls.Add(this.puntaje);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.game);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snake Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.game)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer bucle;
        private System.Windows.Forms.Label CordY;
        private System.Windows.Forms.Label CordX;
        private System.Windows.Forms.Label puntaje;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox game;
    }
}

