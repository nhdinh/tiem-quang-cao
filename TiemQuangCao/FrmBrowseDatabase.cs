using System;
using System.ComponentModel;
using System.Windows.Forms;
using TiemQuangCao.Properties;

namespace TiemQuangCao
{
	public partial class FrmBrowseDatabase : Form, INotifyPropertyChanged
	{
		private bool bConnectionValidated = false;
		private string sConnectionString = String.Empty;

		public FrmBrowseDatabase()
		{
			InitializeComponent();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void setConnectionStatus(bool bStatus)
		{
			this.bConnectionValidated = bStatus;
			if (PropertyChanged != null)
				PropertyChanged(this.bConnectionValidated, new PropertyChangedEventArgs("ConnectionValidated"));
		}

		/// <summary>
		/// Test connection with specified sConnectionString and turn on the flag bConnectionValidated
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnTest_Click(object sender, EventArgs e)
		{
			this.setConnectionStatus(Helpers.TestConnectionString(this.sConnectionString));
		}



		private void FrmBrowseDatabase_Load(object sender, EventArgs e)
		{
			this.PropertyChanged += FrmBrowseDatabase_PropertyChanged;
			this.btnSave.Enabled = bConnectionValidated;

			this.sConnectionString = "Data Source=PRE6;Initial Catalog=imap_vn_db;User ID=sa;Password=local;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
		}

		private void FrmBrowseDatabase_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if ((bool)sender == true)
			{
				this.lbConnectionTestResult.Text = "Connect success";
				this.btnSave.Enabled = true;
			}
			else
			{
				this.lbConnectionTestResult.Text = "Connect failed";
				this.btnSave.Enabled = false;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			var appSettings = new Settings();
			appSettings.ConnectionString = this.sConnectionString;
			appSettings.Save();

			this.Close();
		}
	}
}
