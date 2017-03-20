namespace M2E4Win {
	partial class MainWin {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._preview = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this._ok = new System.Windows.Forms.Button();
			this._cancel = new System.Windows.Forms.Button();
			this._test = new System.Windows.Forms.Button();
			this._name = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this._data = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this._pass = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this._user = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this._server = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this._mwb = new System.Windows.Forms.TextBox();
			this._open = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this._openDialog = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tableLayoutPanel1.Controls.Add(this._preview, 2, 7);
			this.tableLayoutPanel1.Controls.Add(this.label7, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this._ok, 4, 6);
			this.tableLayoutPanel1.Controls.Add(this._cancel, 3, 6);
			this.tableLayoutPanel1.Controls.Add(this._test, 2, 6);
			this.tableLayoutPanel1.Controls.Add(this._name, 2, 5);
			this.tableLayoutPanel1.Controls.Add(this.label6, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this._data, 2, 4);
			this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this._pass, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.label4, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this._user, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this._server, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this._mwb, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this._open, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(620, 437);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _preview
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._preview, 3);
			this._preview.Dock = System.Windows.Forms.DockStyle.Fill;
			this._preview.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._preview.Location = new System.Drawing.Point(85, 188);
			this._preview.Multiline = true;
			this._preview.Name = "_preview";
			this._preview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._preview.Size = new System.Drawing.Size(519, 246);
			this._preview.TabIndex = 17;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(5, 185);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(74, 252);
			this.label7.TabIndex = 16;
			this.label7.Text = "Preview";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// _ok
			// 
			this._ok.Dock = System.Windows.Forms.DockStyle.Fill;
			this._ok.Location = new System.Drawing.Point(435, 153);
			this._ok.Name = "_ok";
			this._ok.Size = new System.Drawing.Size(169, 29);
			this._ok.TabIndex = 15;
			this._ok.Text = "OK";
			this._ok.UseVisualStyleBackColor = true;
			this._ok.Click += new System.EventHandler(this._ok_Click);
			// 
			// _cancel
			// 
			this._cancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._cancel.Location = new System.Drawing.Point(260, 153);
			this._cancel.Name = "_cancel";
			this._cancel.Size = new System.Drawing.Size(169, 29);
			this._cancel.TabIndex = 14;
			this._cancel.Text = "CANCEL";
			this._cancel.UseVisualStyleBackColor = true;
			// 
			// _test
			// 
			this._test.Dock = System.Windows.Forms.DockStyle.Fill;
			this._test.Location = new System.Drawing.Point(85, 153);
			this._test.Name = "_test";
			this._test.Size = new System.Drawing.Size(169, 29);
			this._test.TabIndex = 13;
			this._test.Text = "TEST";
			this._test.UseVisualStyleBackColor = true;
			this._test.Click += new System.EventHandler(this._test_Click);
			// 
			// _name
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._name, 3);
			this._name.Dock = System.Windows.Forms.DockStyle.Fill;
			this._name.Location = new System.Drawing.Point(85, 128);
			this._name.Name = "_name";
			this._name.Size = new System.Drawing.Size(519, 19);
			this._name.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(5, 125);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(74, 25);
			this.label6.TabIndex = 11;
			this.label6.Text = "Namespace";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _data
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._data, 3);
			this._data.Dock = System.Windows.Forms.DockStyle.Fill;
			this._data.Location = new System.Drawing.Point(85, 103);
			this._data.Name = "_data";
			this._data.Size = new System.Drawing.Size(519, 19);
			this._data.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(5, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(74, 25);
			this.label5.TabIndex = 9;
			this.label5.Text = "Database";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _pass
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._pass, 3);
			this._pass.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pass.Location = new System.Drawing.Point(85, 78);
			this._pass.Name = "_pass";
			this._pass.PasswordChar = '･';
			this._pass.Size = new System.Drawing.Size(519, 19);
			this._pass.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(5, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 25);
			this.label4.TabIndex = 7;
			this.label4.Text = "Password";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _user
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._user, 3);
			this._user.Dock = System.Windows.Forms.DockStyle.Fill;
			this._user.Location = new System.Drawing.Point(85, 53);
			this._user.Name = "_user";
			this._user.Size = new System.Drawing.Size(519, 19);
			this._user.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(5, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 25);
			this.label3.TabIndex = 5;
			this.label3.Text = "User ID";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _server
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._server, 3);
			this._server.Dock = System.Windows.Forms.DockStyle.Fill;
			this._server.Location = new System.Drawing.Point(85, 28);
			this._server.Name = "_server";
			this._server.Size = new System.Drawing.Size(519, 19);
			this._server.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(5, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _mwb
			// 
			this.tableLayoutPanel1.SetColumnSpan(this._mwb, 2);
			this._mwb.Dock = System.Windows.Forms.DockStyle.Fill;
			this._mwb.Location = new System.Drawing.Point(85, 3);
			this._mwb.Name = "_mwb";
			this._mwb.Size = new System.Drawing.Size(344, 19);
			this._mwb.TabIndex = 1;
			// 
			// _open
			// 
			this._open.Dock = System.Windows.Forms.DockStyle.Left;
			this._open.Location = new System.Drawing.Point(435, 3);
			this._open.Name = "_open";
			this._open.Size = new System.Drawing.Size(75, 19);
			this._open.TabIndex = 2;
			this._open.Text = "File";
			this._open.UseVisualStyleBackColor = true;
			this._open.Click += new System.EventHandler(this._open_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(5, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "MySQL Models";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _openDialog
			// 
			this._openDialog.Filter = "MySQL Workbench Models (*.mwb)|*.mwb|すべてのファイル (*.*)|*.*";
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(620, 437);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "MainWin";
			this.Text = "MySQL Database Model To Entity.net";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _mwb;
		private System.Windows.Forms.OpenFileDialog _openDialog;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button _open;
		private System.Windows.Forms.TextBox _server;
		private System.Windows.Forms.TextBox _user;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox _pass;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox _data;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button _ok;
		private System.Windows.Forms.Button _cancel;
		private System.Windows.Forms.Button _test;
		private System.Windows.Forms.TextBox _name;
		private System.Windows.Forms.TextBox _preview;
		private System.Windows.Forms.Label label7;
	}
}

