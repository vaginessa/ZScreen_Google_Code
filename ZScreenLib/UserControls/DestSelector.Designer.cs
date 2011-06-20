﻿namespace ZScreenLib
{
    partial class DestSelector
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DestSelector));
            this.gbMainOptions = new System.Windows.Forms.GroupBox();
            this.cboURLShorteners = new System.Windows.Forms.ComboBox();
            this.tsDest = new System.Windows.Forms.ToolStrip();
            this.tsddDestImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestText = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddLinkShorteners = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDestConfig = new System.Windows.Forms.ToolStripButton();
            this.gbMainOptions.SuspendLayout();
            this.tsDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMainOptions
            // 
            this.gbMainOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbMainOptions.Controls.Add(this.cboURLShorteners);
            this.gbMainOptions.Controls.Add(this.tsDest);
            this.gbMainOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMainOptions.Location = new System.Drawing.Point(0, 0);
            this.gbMainOptions.Name = "gbMainOptions";
            this.gbMainOptions.Size = new System.Drawing.Size(352, 144);
            this.gbMainOptions.TabIndex = 80;
            this.gbMainOptions.TabStop = false;
            this.gbMainOptions.Text = "Active Destinations";
            // 
            // cboURLShorteners
            // 
            this.cboURLShorteners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURLShorteners.FormattingEnabled = true;
            this.cboURLShorteners.Location = new System.Drawing.Point(128, 88);
            this.cboURLShorteners.Name = "cboURLShorteners";
            this.cboURLShorteners.Size = new System.Drawing.Size(208, 21);
            this.cboURLShorteners.TabIndex = 124;
            // 
            // tsDest
            // 
            this.tsDest.AutoSize = false;
            this.tsDest.BackColor = System.Drawing.Color.White;
            this.tsDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsDest.CanOverflow = false;
            this.tsDest.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDest.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddDestImage,
            this.tsddDestText,
            this.tsddDestFile,
            this.tsddLinkShorteners,
            this.toolStripSeparator1,
            this.tsbDestConfig});
            this.tsDest.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsDest.Location = new System.Drawing.Point(8, 16);
            this.tsDest.Name = "tsDest";
            this.tsDest.Size = new System.Drawing.Size(336, 120);
            this.tsDest.TabIndex = 128;
            this.tsDest.Text = "Destinations";
            // 
            // tsddDestImage
            // 
            this.tsddDestImage.Image = ((System.Drawing.Image)(resources.GetObject("tsddDestImage.Image")));
            this.tsddDestImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestImage.Name = "tsddDestImage";
            this.tsddDestImage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestImage.Size = new System.Drawing.Size(334, 20);
            this.tsddDestImage.Text = "Image output:";
            // 
            // tsddDestText
            // 
            this.tsddDestText.Image = global::ZScreenLib.Properties.Resources.pencil_go;
            this.tsddDestText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestText.Name = "tsddDestText";
            this.tsddDestText.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestText.Size = new System.Drawing.Size(334, 20);
            this.tsddDestText.Text = "Text output:";
            // 
            // tsddDestFiles
            // 
            this.tsddDestFile.Image = global::ZScreenLib.Properties.Resources.page_go;
            this.tsddDestFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestFile.Name = "tsddDestFiles";
            this.tsddDestFile.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestFile.Size = new System.Drawing.Size(334, 20);
            this.tsddDestFile.Text = "File output:";
            // 
            // tsddLinkShorteners
            // 
            this.tsddLinkShorteners.Image = global::ZScreenLib.Properties.Resources.link_go;
            this.tsddLinkShorteners.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddLinkShorteners.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddLinkShorteners.Name = "tsddLinkShorteners";
            this.tsddLinkShorteners.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddLinkShorteners.Size = new System.Drawing.Size(334, 20);
            this.tsddLinkShorteners.Text = "URL Shorteners";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(334, 6);
            // 
            // tsbDestConfig
            // 
            this.tsbDestConfig.Image = global::ZScreenLib.Properties.Resources.server_edit;
            this.tsbDestConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDestConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDestConfig.Name = "tsbDestConfig";
            this.tsbDestConfig.Size = new System.Drawing.Size(334, 20);
            this.tsbDestConfig.Text = "Open destinations configuration..";
            this.tsbDestConfig.Click += new System.EventHandler(this.tsbDestConfig_Click);
            // 
            // DestSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMainOptions);
            this.Name = "DestSelector";
            this.Size = new System.Drawing.Size(352, 144);
            this.gbMainOptions.ResumeLayout(false);
            this.tsDest.ResumeLayout(false);
            this.tsDest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        internal System.Windows.Forms.GroupBox gbMainOptions;
        public System.Windows.Forms.ComboBox cboURLShorteners;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestImage;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestText;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestFile;
        private System.Windows.Forms.ToolStrip tsDest;
        private System.Windows.Forms.ToolStripDropDownButton tsddLinkShorteners;
        private System.Windows.Forms.ToolStripButton tsbDestConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}