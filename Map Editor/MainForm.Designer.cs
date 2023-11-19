/*
 * Created by SharpDevelop.
 * User: Alexandru
 * Date: 3/23/2017
 * Time: 10:29 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Map_Editor
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel canvas;
		private System.Windows.Forms.ListBox listBox;
		private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveTo;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveMap;
		private System.Windows.Forms.ToolStripMenuItem loadMap;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fIXEDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem simple2DToolStripMenuItem;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.canvas = new System.Windows.Forms.Panel();
			this.listBox = new System.Windows.Forms.ListBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fIXEDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.simple2DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadMap = new System.Windows.Forms.ToolStripMenuItem();
			this.saveTo = new System.Windows.Forms.ToolStripMenuItem();
			this.saveMap = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusLabel = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.canvas.Enabled = false;
			this.canvas.Location = new System.Drawing.Point(0, 34);
			this.canvas.Margin = new System.Windows.Forms.Padding(4);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(827, 501);
			this.canvas.TabIndex = 0;
			this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CanvasMouseDown);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CanvasMouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CanvasMouseUp);
			// 
			// listBox
			// 
			this.listBox.Enabled = false;
			this.listBox.FormattingEnabled = true;
			this.listBox.ItemHeight = 16;
			this.listBox.Location = new System.Drawing.Point(835, 34);
			this.listBox.Margin = new System.Windows.Forms.Padding(4);
			this.listBox.Name = "listBox";
			this.listBox.Size = new System.Drawing.Size(184, 500);
			this.listBox.TabIndex = 1;
			this.listBox.SelectedIndexChanged += new System.EventHandler(this.ListBoxSelectedIndexChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1026, 28);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.newMapToolStripMenuItem,
			this.loadMap,
			this.saveTo,
			this.saveMap});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newMapToolStripMenuItem
			// 
			this.newMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fIXEDToolStripMenuItem,
			this.simple2DToolStripMenuItem});
			this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
			this.newMapToolStripMenuItem.Size = new System.Drawing.Size(151, 26);
			this.newMapToolStripMenuItem.Text = "New map";
			// 
			// fIXEDToolStripMenuItem
			// 
			this.fIXEDToolStripMenuItem.Name = "fIXEDToolStripMenuItem";
			this.fIXEDToolStripMenuItem.Size = new System.Drawing.Size(151, 26);
			this.fIXEDToolStripMenuItem.Text = "FIXED";
			this.fIXEDToolStripMenuItem.Click += new System.EventHandler(this.FIXEDToolStripMenuItemClick);
			// 
			// simple2DToolStripMenuItem
			// 
			this.simple2DToolStripMenuItem.Enabled = false;
			this.simple2DToolStripMenuItem.Name = "simple2DToolStripMenuItem";
			this.simple2DToolStripMenuItem.Size = new System.Drawing.Size(151, 26);
			this.simple2DToolStripMenuItem.Text = "simple 2D";
			// 
			// loadMap
			// 
			this.loadMap.Name = "loadMap";
			this.loadMap.Size = new System.Drawing.Size(151, 26);
			this.loadMap.Text = "Load Map";
			this.loadMap.Click += new System.EventHandler(this.LoadMapClick);
			// 
			// saveTo
			// 
			this.saveTo.Enabled = false;
			this.saveTo.Name = "saveTo";
			this.saveTo.Size = new System.Drawing.Size(151, 26);
			this.saveTo.Text = "Save to...";
			this.saveTo.Click += new System.EventHandler(this.SaveToClick);
			// 
			// saveMap
			// 
			this.saveMap.Enabled = false;
			this.saveMap.Name = "saveMap";
			this.saveMap.Size = new System.Drawing.Size(151, 26);
			this.saveMap.Text = "Save map";
			this.saveMap.Click += new System.EventHandler(this.SaveMapToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// statusLabel
			// 
			this.statusLabel.Location = new System.Drawing.Point(0, 539);
			this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(1004, 30);
			this.statusLabel.TabIndex = 3;
			this.statusLabel.Text = "Map editor started!";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1026, 561);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.listBox);
			this.Controls.Add(this.canvas);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Map Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Shown += new System.EventHandler(this.MainFormShown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
