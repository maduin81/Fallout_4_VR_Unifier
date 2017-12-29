namespace Fallout_4_VR_Unifier
{
    partial class Fallout4UnifierForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fallout4UnifierForm));
            this.FO4 = new System.Windows.Forms.Button();
            this.FO4VR = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Uninstall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FO4
            // 
            this.FO4.ImageIndex = 0;
            this.FO4.ImageList = this.imageList1;
            this.FO4.Location = new System.Drawing.Point(0, 0);
            this.FO4.Name = "FO4";
            this.FO4.Size = new System.Drawing.Size(184, 86);
            this.FO4.TabIndex = 0;
            this.FO4.UseVisualStyleBackColor = true;
            this.FO4.Click += new System.EventHandler(this.FO4_Click);
            // 
            // FO4VR
            // 
            this.FO4VR.ImageIndex = 1;
            this.FO4VR.ImageList = this.imageList1;
            this.FO4VR.Location = new System.Drawing.Point(0, 87);
            this.FO4VR.Name = "FO4VR";
            this.FO4VR.Size = new System.Drawing.Size(184, 86);
            this.FO4VR.TabIndex = 1;
            this.FO4VR.UseVisualStyleBackColor = true;
            this.FO4VR.Click += new System.EventHandler(this.FO4VR_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "fo4.jpg");
            this.imageList1.Images.SetKeyName(1, "fo4vr.jpg");
            this.imageList1.Images.SetKeyName(2, "uninstall.ico");
            // 
            // Uninstall
            // 
            this.Uninstall.Font = new System.Drawing.Font("Papyrus", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Uninstall.Location = new System.Drawing.Point(0, 172);
            this.Uninstall.Name = "Uninstall";
            this.Uninstall.Size = new System.Drawing.Size(184, 86);
            this.Uninstall.TabIndex = 2;
            this.Uninstall.Text = "Uninstall :(";
            this.Uninstall.UseVisualStyleBackColor = true;
            this.Uninstall.Click += new System.EventHandler(this.Uninstall_Click);
            // 
            // Fallout4UnifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 258);
            this.Controls.Add(this.Uninstall);
            this.Controls.Add(this.FO4VR);
            this.Controls.Add(this.FO4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Fallout4UnifierForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Fallout 4 VR Unifier";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FO4;
        private System.Windows.Forms.Button FO4VR;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button Uninstall;
    }
}