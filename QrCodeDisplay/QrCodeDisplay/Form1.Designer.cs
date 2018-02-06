namespace QrCodeDisplay
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
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.btnGenerateQRcode = new System.Windows.Forms.Button();
            this.txtASCII = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(71, 42);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxQRCode.TabIndex = 0;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // btnGenerateQRcode
            // 
            this.btnGenerateQRcode.Location = new System.Drawing.Point(394, 364);
            this.btnGenerateQRcode.Name = "btnGenerateQRcode";
            this.btnGenerateQRcode.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateQRcode.TabIndex = 1;
            this.btnGenerateQRcode.Text = "Generate";
            this.btnGenerateQRcode.UseVisualStyleBackColor = true;
            this.btnGenerateQRcode.Click += new System.EventHandler(this.btnGenerateQRcode_Click);
            // 
            // txtASCII
            // 
            this.txtASCII.Location = new System.Drawing.Point(71, 366);
            this.txtASCII.Name = "txtASCII";
            this.txtASCII.Size = new System.Drawing.Size(300, 20);
            this.txtASCII.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 417);
            this.Controls.Add(this.txtASCII);
            this.Controls.Add(this.btnGenerateQRcode);
            this.Controls.Add(this.pictureBoxQRCode);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.Button btnGenerateQRcode;
        private System.Windows.Forms.TextBox txtASCII;
    }
}

