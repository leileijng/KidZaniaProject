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
            this.wb_existingcodes = new System.Windows.Forms.WebBrowser();
            this.btn_searchcodes = new System.Windows.Forms.Button();
            this.lbl_existingcode = new System.Windows.Forms.Label();
            this.lbl_printer = new System.Windows.Forms.Label();
            this.cmb_printer = new System.Windows.Forms.ComboBox();
            this.btn_print = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.wb_photos = new System.Windows.Forms.WebBrowser();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_date = new System.Windows.Forms.TextBox();
            this.pic_holder = new System.Windows.Forms.PictureBox();
            this.lbl_photo = new System.Windows.Forms.Label();
            this.btn_next = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_loadimages = new System.Windows.Forms.Button();
            this.txtcopies = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_photodetails = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_holder)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-45, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1329, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // wb_existingcodes
            // 
            this.wb_existingcodes.Location = new System.Drawing.Point(511, 178);
            this.wb_existingcodes.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_existingcodes.Name = "wb_existingcodes";
            this.wb_existingcodes.Size = new System.Drawing.Size(237, 388);
            this.wb_existingcodes.TabIndex = 17;
            // 
            // btn_searchcodes
            // 
            this.btn_searchcodes.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_searchcodes.FlatAppearance.BorderSize = 2;
            this.btn_searchcodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_searchcodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_searchcodes.Location = new System.Drawing.Point(627, 146);
            this.btn_searchcodes.Name = "btn_searchcodes";
            this.btn_searchcodes.Size = new System.Drawing.Size(125, 27);
            this.btn_searchcodes.TabIndex = 16;
            this.btn_searchcodes.Text = "Refresh";
            this.btn_searchcodes.UseVisualStyleBackColor = false;
            this.btn_searchcodes.Click += new System.EventHandler(this.btn_searchcodes_Click);
            // 
            // lbl_existingcode
            // 
            this.lbl_existingcode.AutoSize = true;
            this.lbl_existingcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_existingcode.Location = new System.Drawing.Point(507, 150);
            this.lbl_existingcode.Name = "lbl_existingcode";
            this.lbl_existingcode.Size = new System.Drawing.Size(114, 20);
            this.lbl_existingcode.TabIndex = 15;
            this.lbl_existingcode.Text = "Existing Codes";
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
            // btn_print
            // 
            this.btn_print.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_print.Location = new System.Drawing.Point(37, 553);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(192, 58);
            this.btn_print.TabIndex = 20;
            this.btn_print.Text = "Print All";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label1.Location = new System.Drawing.Point(32, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 22;
            this.label1.Text = "Code";
            // 
            // txt_code
            // 
            this.txt_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.txt_code.Location = new System.Drawing.Point(37, 238);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(165, 31);
            this.txt_code.TabIndex = 21;
            // 
            // wb_photos
            // 
            this.wb_photos.Location = new System.Drawing.Point(37, 370);
            this.wb_photos.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb_photos.Name = "wb_photos";
            this.wb_photos.Size = new System.Drawing.Size(447, 155);
            this.wb_photos.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label2.Location = new System.Drawing.Point(32, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "Photos";
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_submit.FlatAppearance.BorderSize = 2;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.Location = new System.Drawing.Point(221, 258);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(191, 49);
            this.btn_submit.TabIndex = 25;
            this.btn_submit.Text = "Retrieve Photos";
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 25);
            this.label3.TabIndex = 27;
            this.label3.Text = "Date (ddmmyy)";
            // 
            // txt_date
            // 
            this.txt_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_date.Location = new System.Drawing.Point(37, 298);
            this.txt_date.Name = "txt_date";
            this.txt_date.Size = new System.Drawing.Size(150, 31);
            this.txt_date.TabIndex = 26;
            // 
            // pic_holder
            // 
            this.pic_holder.Cursor = System.Windows.Forms.Cursors.No;
            this.pic_holder.Location = new System.Drawing.Point(800, 178);
            this.pic_holder.Name = "pic_holder";
            this.pic_holder.Size = new System.Drawing.Size(459, 347);
            this.pic_holder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_holder.TabIndex = 28;
            this.pic_holder.TabStop = false;
            // 
            // lbl_photo
            // 
            this.lbl_photo.AutoSize = true;
            this.lbl_photo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_photo.Location = new System.Drawing.Point(796, 153);
            this.lbl_photo.Name = "lbl_photo";
            this.lbl_photo.Size = new System.Drawing.Size(68, 20);
            this.lbl_photo.TabIndex = 29;
            this.lbl_photo.Text = "Photo #:";
            // 
            // btn_next
            // 
            this.btn_next.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_next.Location = new System.Drawing.Point(801, 544);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(204, 58);
            this.btn_next.TabIndex = 30;
            this.btn_next.Text = "Preview Next";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1056, 544);
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
            this.btn_loadimages.Location = new System.Drawing.Point(800, 617);
            this.btn_loadimages.Name = "btn_loadimages";
            this.btn_loadimages.Size = new System.Drawing.Size(205, 77);
            this.btn_loadimages.TabIndex = 32;
            this.btn_loadimages.Text = "Load photos from local drive";
            this.btn_loadimages.UseVisualStyleBackColor = true;
            this.btn_loadimages.Click += new System.EventHandler(this.btn_loadimages_Click);
            // 
            // txtcopies
            // 
            this.txtcopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.txtcopies.Location = new System.Drawing.Point(1056, 649);
            this.txtcopies.Name = "txtcopies";
            this.txtcopies.Size = new System.Drawing.Size(165, 31);
            this.txtcopies.TabIndex = 33;
            this.txtcopies.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label4.Location = new System.Drawing.Point(1051, 617);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 34;
            this.label4.Text = "Copies";
            // 
            // txt_photodetails
            // 
            this.txt_photodetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_photodetails.Location = new System.Drawing.Point(941, 125);
            this.txt_photodetails.Multiline = true;
            this.txt_photodetails.Name = "txt_photodetails";
            this.txt_photodetails.ReadOnly = true;
            this.txt_photodetails.Size = new System.Drawing.Size(318, 45);
            this.txt_photodetails.TabIndex = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 706);
            this.Controls.Add(this.txt_photodetails);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtcopies);
            this.Controls.Add(this.btn_loadimages);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.lbl_photo);
            this.Controls.Add(this.pic_holder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_date);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.wb_photos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.btn_print);
            this.Controls.Add(this.lbl_printer);
            this.Controls.Add(this.cmb_printer);
            this.Controls.Add(this.wb_existingcodes);
            this.Controls.Add(this.btn_searchcodes);
            this.Controls.Add(this.lbl_existingcode);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Kidzania Station Console - Printer (Version 1.003) ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_holder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.WebBrowser wb_existingcodes;
        private System.Windows.Forms.Button btn_searchcodes;
        private System.Windows.Forms.Label lbl_existingcode;
        private System.Windows.Forms.Label lbl_printer;
        private System.Windows.Forms.ComboBox cmb_printer;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.WebBrowser wb_photos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_date;
        private System.Windows.Forms.PictureBox pic_holder;
        private System.Windows.Forms.Label lbl_photo;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_loadimages;
        private System.Windows.Forms.TextBox txtcopies;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_photodetails;
    }
}

