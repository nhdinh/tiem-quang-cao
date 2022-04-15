namespace TiemQuangCao
{
	partial class FrmBrowseDatabase
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
            this.btnTest = new System.Windows.Forms.Button();
            this.lbConnectionTestResult = new System.Windows.Forms.Label();
            this.lstServers = new System.Windows.Forms.ListView();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(226, 429);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(113, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test Connection";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lbConnectionTestResult
            // 
            this.lbConnectionTestResult.AutoSize = true;
            this.lbConnectionTestResult.Location = new System.Drawing.Point(12, 433);
            this.lbConnectionTestResult.Name = "lbConnectionTestResult";
            this.lbConnectionTestResult.Size = new System.Drawing.Size(69, 15);
            this.lbConnectionTestResult.TabIndex = 1;
            this.lbConnectionTestResult.Text = "Connection";
            // 
            // lstServers
            // 
            this.lstServers.HideSelection = false;
            this.lstServers.Location = new System.Drawing.Point(12, 12);
            this.lstServers.Name = "lstServers";
            this.lstServers.Size = new System.Drawing.Size(391, 411);
            this.lstServers.TabIndex = 2;
            this.lstServers.UseCompatibleStateImageBehavior = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(345, 429);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(58, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmBrowseDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 462);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lstServers);
            this.Controls.Add(this.lbConnectionTestResult);
            this.Controls.Add(this.btnTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmBrowseDatabase";
            this.Text = "FrmBrowseDatabase";
            this.Load += new System.EventHandler(this.FrmBrowseDatabase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Label lbConnectionTestResult;
		private System.Windows.Forms.ListView lstServers;
		private System.Windows.Forms.Button btnSave;
	}
}
