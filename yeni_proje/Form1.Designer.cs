namespace yeni_proje
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
            this.img_list1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // img_list1
            // 
            this.img_list1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img_list1.ImageStream")));
            this.img_list1.TransparentColor = System.Drawing.Color.Transparent;
            this.img_list1.Images.SetKeyName(0, "tao-smile.gif");
            this.img_list1.Images.SetKeyName(1, "tao-dead.gif");
            this.img_list1.Images.SetKeyName(2, "bomb.png");
            this.img_list1.Images.SetKeyName(3, "mine_flag.png");
            this.img_list1.Images.SetKeyName(4, "indir.jpg");
            this.img_list1.Images.SetKeyName(5, "images.jpg");
            this.img_list1.Images.SetKeyName(6, "polis.jpg");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(257, 276);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList img_list1;


    }
}

