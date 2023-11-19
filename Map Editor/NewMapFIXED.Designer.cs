/*
 * Created by SharpDevelop.
 * User: Alexandru
 * Date: 3/26/2017
 * Time: 7:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Map_Editor
{
	partial class NewMapFIXED
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Label lName;
		private System.Windows.Forms.TextBox tbMapName;
		
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
			this.label1 = new System.Windows.Forms.Label();
			this.bOk = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.lName = new System.Windows.Forms.Label();
			this.tbMapName = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(335, 47);
			this.label1.TabIndex = 0;
			this.label1.Text = "New FIXED map";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// bOk
			// 
			this.bOk.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOk.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.bOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bOk.Location = new System.Drawing.Point(19, 92);
			this.bOk.Margin = new System.Windows.Forms.Padding(4);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(164, 49);
			this.bOk.TabIndex = 2;
			this.bOk.Text = "Ok";
			this.bOk.UseVisualStyleBackColor = false;
			this.bOk.Click += new System.EventHandler(this.BOkClick);
			// 
			// bCancel
			// 
			this.bCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bCancel.Location = new System.Drawing.Point(191, 92);
			this.bCancel.Margin = new System.Windows.Forms.Padding(4);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(163, 49);
			this.bCancel.TabIndex = 3;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = false;
			// 
			// lName
			// 
			this.lName.Location = new System.Drawing.Point(16, 58);
			this.lName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lName.Name = "lName";
			this.lName.Size = new System.Drawing.Size(64, 28);
			this.lName.TabIndex = 4;
			this.lName.Text = "Name:";
			this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbMapName
			// 
			this.tbMapName.Location = new System.Drawing.Point(91, 62);
			this.tbMapName.Margin = new System.Windows.Forms.Padding(4);
			this.tbMapName.Name = "tbMapName";
			this.tbMapName.Size = new System.Drawing.Size(259, 22);
			this.tbMapName.TabIndex = 5;
			// 
			// NewMapFIXED
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(367, 155);
			this.Controls.Add(this.tbMapName);
			this.Controls.Add(this.lName);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOk);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewMapFIXED";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "New map";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
