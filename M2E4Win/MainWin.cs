using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using M2E4Win.Properties;
namespace M2E4Win {
	public partial class MainWin : Form {
		public MainWin() {
			InitializeComponent();
			const int EM_SETTABSTOPS = 0x00CB;
			SendMessage(_preview.Handle, EM_SETTABSTOPS, 1, new int[] { 16 });
		}
		private string _connectionstring { get; set; }

		[System.Runtime.InteropServices.DllImport("User32.dll")]
		static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int[] lParam);

		private void _open_Click(object sender, EventArgs e) {
			DialogResult res = _openDialog.ShowDialog();
			if (res == DialogResult.OK) {
				_mwb.Text = _openDialog.FileName;
				string wk = _openDialog.FileName;
				wk = wk.Substring(wk.LastIndexOf(Resources.DirSp) + 1, wk.LastIndexOf(".") - wk.LastIndexOf(Resources.DirSp) - 1);
				_name.Text = wk;
			}
		}

		private void _ok_Click(object sender, EventArgs e) {
			Mwb fil = new Mwb();
			fil.Filename = _mwb.Text;
			fil.Server = _server.Text;
			fil.Name = _name.Text;
			fil.UserID = _user.Text;
			fil.Password = _pass.Text;
			fil.Database = _data.Text;
			fil.Load();
			//Pango.FontDescription font = Pango.FontDescription.FromString ("MigMix1P Normal 11");
			//Pango.FontDescription font = Pango.FontDescription.FromString("TakaoGothic Normal 10");
			//_preview.ModifyFont(font);
			_preview.AcceptsTab = true;
			//Tabs.SetTab(1, Pango.TabAlign.Left, 28);
			//_preview.Tabs = this.Tabs;
			_preview.Text = fil.GetSourceCode();

		}

		private void _test_Click(object sender, EventArgs e) {
			MySql.Data.MySqlClient.MySqlConnectionStringBuilder bld
				= new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
			bld.Server = _server.Text;
			bld.UserID = _user.Text;
			bld.Password = _pass.Text;
			_connectionstring = bld.ConnectionString;
			MySql.Data.MySqlClient.MySqlConnection con
				= new MySql.Data.MySqlClient.MySqlConnection();
			con.ConnectionString = _connectionstring;
			DialogResult dlg ;
			try {
				con.Open();
				if (con.State != System.Data.ConnectionState.Open) {
					dlg = MessageBox.Show("オープンできません。" + con.State.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else {
					dlg = MessageBox.Show("オープンできました。" , "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
					con.Close();
				}

			} catch (MySql.Data.MySqlClient.MySqlException ex) {
				dlg = MessageBox.Show("オープンできません。" + con.State.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
