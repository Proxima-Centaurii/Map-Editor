/*
 * Created by SharpDevelop.
 * User: Mirela
 * Date: 3/23/2017
 * Time: 10:29 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Map_Editor
{
	
	public partial class MainForm : Form
	{
		
		//Visual
		private Graphics gfx;
		private SolidBrush sBrush;
		private Pen pen;

		private Color defaultColor = Color.Blue;
		
		//System
		private Core core;
		
		//Input
		private bool mouseDown = false;
		private Point lastMouseLocation;
		
		//File
		private String fileName = "";
		private String filePath = "";
		
		public MainForm()
		{
			InitializeComponent();
		
			gfx = canvas.CreateGraphics();
			sBrush = new SolidBrush(defaultColor);
			pen = new Pen(defaultColor);
		
			lastMouseLocation = new Point(0,0);
		}
		
		private void Dispose(){
			if(core != null)
				core.Dispose();
			
			sBrush.Dispose();
			pen.Dispose();
			gfx.Dispose();
			
			MessageBox.Show("Resources disposed!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		//Other
		private String findName(String s){
			String[] arr = s.Split('\\','.');
			return arr[arr.Length-2];
		}
		private String getExtension(String s){
			String[] arr = s.Split('.');
			return arr[arr.Length-1];
		}

		//Events
		
		//[FORM]
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			Dispose();
		}
		
		void MainFormShown(object sender, EventArgs e)
		{
			//core = new CoreFIXED(ref gfx, ref sBrush, ref pen, ref statusLabel, ref listBox);
		}
		
		void ListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			core.changedSelection(listBox.SelectedIndex);
		}

		
		//[INPUT]
		void CanvasMouseDown(object sender, MouseEventArgs e)
		{
			mouseDown = true;
			core.mouseDown(e.Location,e.Button);
		}
		void CanvasMouseMove(object sender, MouseEventArgs e)
		{
			core.requestDraw();
			
			if(mouseDown)
				core.mouseDragged(e.Location,lastMouseLocation,e.Button);
			core.mouseMoved(e.Location,lastMouseLocation);
			
			lastMouseLocation = e.Location;
		}
		void CanvasMouseUp(object sender, MouseEventArgs e)
		{
			core.mouseUp(e.Location, e.Button);
			mouseDown = false;
		}
	
		//[MENU-FILE]
		//New map FIXED
		void FIXEDToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(core == null){
				core = new CoreFIXED(ref gfx, ref sBrush, ref pen, ref statusLabel, ref listBox);
				
				canvas.Enabled = true;
				listBox.Enabled = true;
				saveMap.Enabled = true;
				saveTo.Enabled = true;
			}
				
			using(NewMapFIXED newMapDialog = new NewMapFIXED()){
				DialogResult result = newMapDialog.ShowDialog();
				
				if(result == DialogResult.OK)
					core.newMap();
				
				fileName = newMapDialog.fileName;
				fileName = fileName == "" ? "NewMap" : fileName;
				this.Text = fileName + " - Map Editor";
			}			
		}
		
		//Save map
		void SaveMapToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(filePath != ""){
				canvas.Enabled = false;
				core.saveMap(filePath);
				canvas.Enabled = true;
			}
			else	
				using(var sfd = new SaveFileDialog()){
					sfd.Filter = core.fileFilter;
					sfd.FileName = fileName;
					
					DialogResult result = sfd.ShowDialog();
				
					if(result == DialogResult.OK){
						canvas.Enabled = false;
						filePath = sfd.FileName;
						fileName = findName(filePath);
						core.saveMap(filePath);
						canvas.Enabled = true;
						
						this.Text = fileName+" - Map Editor";
					}
				}
			
			statusLabel.Text = "Map saved!";
		}
		//Save to..
		void SaveToClick(object sender, EventArgs e)
		{
			using(var sfd = new SaveFileDialog()){
				sfd.Filter = core.fileFilter;
				sfd.FileName = fileName;
				
				DialogResult result = sfd.ShowDialog();
			
				if(result == DialogResult.OK){
					canvas.Enabled = false;
					filePath = sfd.FileName;
					fileName = findName(filePath);
					core.saveMap(filePath);
					canvas.Enabled = true;
					
					this.Text = fileName+" - Map Editor";
				}
			}
			
			statusLabel.Text = fileName+" - MapEditor";
		}
		
		//Load map
		void LoadMapClick(object sender, EventArgs e)
		{
			canvas.Enabled = false;
			using(var ofd = new OpenFileDialog()){
				ofd.Filter = "FIXED map|*.lvl";
				
				DialogResult result = ofd.ShowDialog();
				
				if(result == DialogResult.OK){
					filePath = ofd.FileName;
					fileName = findName(filePath);
					
					string ext = getExtension(filePath);
					if(ext == "lvl"){
						core = new CoreFIXED(ref gfx, ref sBrush, ref pen, ref statusLabel, ref listBox);
						core.loadMap(filePath);
						this.Text = fileName+" - Map Editor";
						
						canvas.Enabled = true;
						saveMap.Enabled = true;
						saveTo.Enabled = true;
						listBox.Enabled = true;
					}
				}
			}
			
			canvas.Enabled = true;
			statusLabel.Text = "Map loaded!";
		}	
		
	}//End of class
}//End of namespace
