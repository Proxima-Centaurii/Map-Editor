/*
 * Created by SharpDevelop.
 * User: Alexandru
 * Date: 3/26/2017
 * Time: 7:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Map_Editor
{
	/// <summary>
	/// Gets user data about the settings of the map that is about to be created.
	/// </summary>
	public partial class NewMapFIXED : Form
	{
		public String fileName = "";
		
		public NewMapFIXED()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		//Events
		void BOkClick(object sender, EventArgs e)
		{
			fileName = tbMapName.Text;
		}
	}
}
