namespace Kidzania_station_console_printers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_printer = new System.Windows.Forms.Label();
            this.cmb_printer = new System.Windows.Forms.ComboBox();
            this.pic_holder = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_loadimages = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic_holder2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_holder)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_holder2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-45, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1060, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_printer
            // 
            this.lbl_printer.AutoSize = true;
            this.lbl_printer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_printer.Location = new System.Drawing.Point(37, 142);
            this.lbl_printer.Name = "lbl_printer";
            this.lbl_printer.Size = new System.Drawing.Size(64, 24);
            this.lbl_printer.TabIndex = 19;
            this.lbl_printer.Text = "Printer";
            // 
            // cmb_printer
            // 
            this.cmb_printer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_printer.FormattingEnabled = true;
            this.cmb_printer.Location = new System.Drawing.Point(37, 169);
            this.cmb_printer.Name = "cmb_printer";
            this.cmb_printer.Size = new System.Drawing.Size(292, 24);
            this.cmb_printer.TabIndex = 18;
            this.cmb_printer.SelectedIndexChanged += new System.EventHandler(this.cmb_printer_SelectedIndexChanged);
            // 
            // pic_holder
            // 
            this.pic_holder.Cursor = System.Windows.Forms.Cursors.No;
            this.pic_holder.Location = new System.Drawing.Point(60, 52);
            this.pic_holder.Name = "pic_holder";
            this.pic_holder.Size = new System.Drawing.Size(250, 347);
            this.pic_holder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_holder.TabIndex = 28;
            this.pic_holder.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(759, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 58);
            this.button1.TabIndex = 31;
            this.button1.Text = "Print Single";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_loadimages
            // 
            this.btn_loadimages.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_loadimages.Location = new System.Drawing.Point(759, 226);
            this.btn_loadimages.Name = "btn_loadimages";
            this.btn_loadimages.Size = new System.Drawing.Size(205, 77);
            this.btn_loadimages.TabIndex = 32;
            this.btn_loadimages.Text = "Load photos from local drive";
            this.btn_loadimages.UseVisualStyleBackColor = true;
            this.btn_loadimages.Click += new System.EventHandler(this.btn_loadimages_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pic_holder2);
            this.panel1.Controls.Add(this.pic_holder);
            this.panel1.Location = new System.Drawing.Point(41, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 439);
            this.panel1.TabIndex = 36;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pic_holder2
            // 
            this.pic_holder2.Cursor = System.Windows.Forms.Cursors.No;
            this.pic_holder2.Location = new System.Drawing.Point(354, 52);
            this.pic_holder2.Name = "pic_holder2";
            this.pic_holder2.Size = new System.Drawing.Size(250, 347);
            this.pic_holder2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_holder2.TabIndex = 29;
            this.pic_holder2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 706);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_loadimages);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_printer);
            this.Controls.Add(this.cmb_printer);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Kidzania Station Console - Keychain Printer (Version 1.003) ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_holder)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_holder2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_printer;
        private System.Windows.Forms.ComboBox cmb_printer;
        private System.Windows.Forms.PictureBox pic_holder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_loadimages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pic_holder2;
    }
}

