namespace MotionDeblur
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBoxResize = new System.Windows.Forms.PictureBox();
            this.pictureBoxDeblur = new System.Windows.Forms.PictureBox();
            this.labelResize = new System.Windows.Forms.Label();
            this.labelDeblur = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.labelOpenFile = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonSaveResize = new System.Windows.Forms.Button();
            this.buttonSaveDeblur = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeblur)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 256);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(554, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(193, 17);
            this.toolStripStatusLabel.Text = "A kezdéshez válasszon ki egy képet!";
            // 
            // pictureBoxResize
            // 
            this.pictureBoxResize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxResize.Location = new System.Drawing.Point(12, 48);
            this.pictureBoxResize.Name = "pictureBoxResize";
            this.pictureBoxResize.Size = new System.Drawing.Size(256, 144);
            this.pictureBoxResize.TabIndex = 1;
            this.pictureBoxResize.TabStop = false;
            // 
            // pictureBoxDeblur
            // 
            this.pictureBoxDeblur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxDeblur.Location = new System.Drawing.Point(288, 48);
            this.pictureBoxDeblur.Name = "pictureBoxDeblur";
            this.pictureBoxDeblur.Size = new System.Drawing.Size(256, 144);
            this.pictureBoxDeblur.TabIndex = 2;
            this.pictureBoxDeblur.TabStop = false;
            // 
            // labelResize
            // 
            this.labelResize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelResize.Location = new System.Drawing.Point(11, 199);
            this.labelResize.Name = "labelResize";
            this.labelResize.Size = new System.Drawing.Size(256, 16);
            this.labelResize.TabIndex = 6;
            this.labelResize.Text = "Eredeti (átméretezett) kép";
            this.labelResize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDeblur
            // 
            this.labelDeblur.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDeblur.Location = new System.Drawing.Point(287, 199);
            this.labelDeblur.Name = "labelDeblur";
            this.labelDeblur.Size = new System.Drawing.Size(256, 16);
            this.labelDeblur.TabIndex = 7;
            this.labelDeblur.Text = "Javított kép";
            this.labelDeblur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.buttonHelp.Location = new System.Drawing.Point(512, 12);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(30, 30);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.Text = "?";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(12, 12);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(110, 25);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Kép kiválasztása...";
            this.buttonOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // labelOpenFile
            // 
            this.labelOpenFile.AutoEllipsis = true;
            this.labelOpenFile.Location = new System.Drawing.Point(128, 12);
            this.labelOpenFile.Name = "labelOpenFile";
            this.labelOpenFile.Size = new System.Drawing.Size(110, 25);
            this.labelOpenFile.TabIndex = 5;
            this.labelOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelOpenFile.UseCompatibleTextRendering = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(248, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(60, 25);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Indítás";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "BMP Files(*.bmp)|*.bmp|GIF Files(*.gif)|*.gif|JPEG Files(*.jpg;*.jpeg)|*.jpg;*.jp" +
    "eg|PNG Files(*.png)|*.png|All files(*.*)|*.*";
            this.openFileDialog.FilterIndex = 4;
            this.openFileDialog.Title = "Kép kiválasztása...";
            // 
            // buttonSaveResize
            // 
            this.buttonSaveResize.Enabled = false;
            this.buttonSaveResize.Location = new System.Drawing.Point(90, 220);
            this.buttonSaveResize.Name = "buttonSaveResize";
            this.buttonSaveResize.Size = new System.Drawing.Size(100, 25);
            this.buttonSaveResize.TabIndex = 2;
            this.buttonSaveResize.Text = "Kép mentése...";
            this.buttonSaveResize.UseVisualStyleBackColor = true;
            this.buttonSaveResize.Click += new System.EventHandler(this.buttonSaveResize_Click);
            // 
            // buttonSaveDeblur
            // 
            this.buttonSaveDeblur.Enabled = false;
            this.buttonSaveDeblur.Location = new System.Drawing.Point(366, 220);
            this.buttonSaveDeblur.Name = "buttonSaveDeblur";
            this.buttonSaveDeblur.Size = new System.Drawing.Size(100, 25);
            this.buttonSaveDeblur.TabIndex = 3;
            this.buttonSaveDeblur.Text = "Kép mentése...";
            this.buttonSaveDeblur.UseVisualStyleBackColor = true;
            this.buttonSaveDeblur.Click += new System.EventHandler(this.buttonSaveDeblur_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 278);
            this.Controls.Add(this.buttonSaveDeblur);
            this.Controls.Add(this.buttonSaveResize);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelOpenFile);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.labelDeblur);
            this.Controls.Add(this.labelResize);
            this.Controls.Add(this.pictureBoxDeblur);
            this.Controls.Add(this.pictureBoxResize);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Motion Deblur Client";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeblur)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.PictureBox pictureBoxResize;
        private System.Windows.Forms.PictureBox pictureBoxDeblur;
        private System.Windows.Forms.Label labelResize;
        private System.Windows.Forms.Label labelDeblur;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Label labelOpenFile;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonSaveResize;
        private System.Windows.Forms.Button buttonSaveDeblur;
    }
}

