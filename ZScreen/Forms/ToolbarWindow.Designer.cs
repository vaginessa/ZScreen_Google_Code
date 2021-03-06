﻿using ZScreenGUI.Properties;

namespace ZScreenGUI
{
    partial class ToolbarWindow
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
            this.tsQuickActions = new System.Windows.Forms.ToolStrip();
            this.tsbEntireScreen = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectedWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbLastCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoCapture = new System.Windows.Forms.ToolStripButton();
            this.tsbClipboardUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDragDropWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbLanguageTranslator = new System.Windows.Forms.ToolStripButton();
            this.tsbScreenColorPicker = new System.Windows.Forms.ToolStripButton();
            this.tsQuickActions.SuspendLayout();
            this.SuspendLayout();
            //
            // tsQuickActions
            //
            this.tsQuickActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsQuickActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEntireScreen,
            this.tsbSelectedWindow,
            this.tsbCropShot,
            this.tsbLastCropShot,
            this.tsbAutoCapture,
            this.tsbClipboardUpload,
            this.tsbDragDropWindow,
            this.tsbLanguageTranslator,
            this.tsbScreenColorPicker});
            this.tsQuickActions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tsQuickActions.Location = new System.Drawing.Point(0, 0);
            this.tsQuickActions.Name = "tsQuickActions";
            this.tsQuickActions.Size = new System.Drawing.Size(208, 24);
            this.tsQuickActions.TabIndex = 0;
            this.tsQuickActions.Text = "toolStrip1";
            this.tsQuickActions.MouseEnter += new System.EventHandler(this.tsQuickActions_MouseEnter);
            //
            // tsbEntireScreen
            //
            this.tsbEntireScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEntireScreen.Image = Resources.monitor;
            this.tsbEntireScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEntireScreen.Name = "tsbEntireScreen";
            this.tsbEntireScreen.Size = new System.Drawing.Size(23, 20);
            this.tsbEntireScreen.Text = "Entire Screen";
            this.tsbEntireScreen.Click += new System.EventHandler(this.tsbEntireScreen_Click);
            //
            // tsbSelectedWindow
            //
            this.tsbSelectedWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelectedWindow.Image = Resources.application_double;
            this.tsbSelectedWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectedWindow.Name = "tsbSelectedWindow";
            this.tsbSelectedWindow.Size = new System.Drawing.Size(23, 20);
            this.tsbSelectedWindow.Text = "Selected Window";
            this.tsbSelectedWindow.Click += new System.EventHandler(this.tsbSelectedWindow_Click);
            //
            // tsbCropShot
            //
            this.tsbCropShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCropShot.Image = Resources.shape_square;
            this.tsbCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCropShot.Name = "tsbCropShot";
            this.tsbCropShot.Size = new System.Drawing.Size(23, 20);
            this.tsbCropShot.Text = "Crop Shot";
            this.tsbCropShot.Click += new System.EventHandler(this.tsbCropShot_Click);
            //
            // tsbLastCropShot
            //
            this.tsbLastCropShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLastCropShot.Image = Resources.shape_square_go;
            this.tsbLastCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastCropShot.Name = "tsbLastCropShot";
            this.tsbLastCropShot.Size = new System.Drawing.Size(23, 20);
            this.tsbLastCropShot.Text = "Last Crop Shot";
            this.tsbLastCropShot.Click += new System.EventHandler(this.tsbLastCropShot_Click);
            //
            // tsbAutoCapture
            //
            this.tsbAutoCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAutoCapture.Image = Resources.images_stack;
            this.tsbAutoCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoCapture.Name = "tsbAutoCapture";
            this.tsbAutoCapture.Size = new System.Drawing.Size(23, 20);
            this.tsbAutoCapture.Text = "Auto Capture";
            this.tsbAutoCapture.Click += new System.EventHandler(this.tsbAutoCapture_Click);
            //
            // tsbClipboardUpload
            //
            this.tsbClipboardUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClipboardUpload.Image = Resources.images;
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(23, 20);
            this.tsbClipboardUpload.Text = "Clipboard Upload";
            this.tsbClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            //
            // tsbDragDropWindow
            //
            this.tsbDragDropWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDragDropWindow.Image = Resources.shape_move_backwards;
            this.tsbDragDropWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDragDropWindow.Name = "tsbDragDropWindow";
            this.tsbDragDropWindow.Size = new System.Drawing.Size(23, 20);
            this.tsbDragDropWindow.Text = "Drag & Drop Window";
            this.tsbDragDropWindow.Click += new System.EventHandler(this.tsbDragDropWindow_Click);
            //
            // tsbLanguageTranslator
            //
            this.tsbLanguageTranslator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLanguageTranslator.Image = Resources.comments;
            this.tsbLanguageTranslator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLanguageTranslator.Name = "tsbLanguageTranslator";
            this.tsbLanguageTranslator.Size = new System.Drawing.Size(23, 20);
            this.tsbLanguageTranslator.Text = "Language Translator";
            this.tsbLanguageTranslator.Click += new System.EventHandler(this.tsbLanguageTranslator_Click);
            //
            // tsbScreenColorPicker
            //
            this.tsbScreenColorPicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScreenColorPicker.Image = Resources.color_wheel;
            this.tsbScreenColorPicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScreenColorPicker.Name = "tsbScreenColorPicker";
            this.tsbScreenColorPicker.Size = new System.Drawing.Size(23, 20);
            this.tsbScreenColorPicker.Text = "Screen Color Picker";
            this.tsbScreenColorPicker.Click += new System.EventHandler(this.tsbScreenColorPicker_Click);
            //
            // ToolbarWindow
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 24);
            this.Controls.Add(this.tsQuickActions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ToolbarWindow";
            this.Text = " Actions Toolbar";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.ToolbarWindow_Shown);
            this.Move += new System.EventHandler(this.ToolbarWindow_Move);
            this.tsQuickActions.ResumeLayout(false);
            this.tsQuickActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.ToolStrip tsQuickActions;
        private System.Windows.Forms.ToolStripButton tsbEntireScreen;
        private System.Windows.Forms.ToolStripButton tsbSelectedWindow;
        private System.Windows.Forms.ToolStripButton tsbCropShot;
        private System.Windows.Forms.ToolStripButton tsbLastCropShot;
        private System.Windows.Forms.ToolStripButton tsbClipboardUpload;
        private System.Windows.Forms.ToolStripButton tsbDragDropWindow;
        private System.Windows.Forms.ToolStripButton tsbLanguageTranslator;
        private System.Windows.Forms.ToolStripButton tsbScreenColorPicker;
        private System.Windows.Forms.ToolStripButton tsbAutoCapture;
    }
}