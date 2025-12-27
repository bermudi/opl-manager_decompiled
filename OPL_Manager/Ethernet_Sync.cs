using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class Ethernet_Sync : BaseForm
{
	private List<string> filesPC = new List<string>();

	private string hdlFolder = Program.MainFormInst.app_folder + "\\hdl\\";

	private string[] FoldersToSync = new string[3] { "ART", "CFG", "CHT" };

	private IContainer components;

	internal ListView LV_PC;

	internal ColumnHeader ColumnHeader1;

	internal ColumnHeader ColumnHeader2;

	internal PictureBox PictureBox1;

	internal ListView LV_PS2;

	internal ColumnHeader ColumnHeader4;

	internal ColumnHeader ColumnHeader5;

	internal Button Button1;

	internal Label Label1;

	internal Label Label2;

	internal Label Label3;

	internal Button B_PC_TO_PS2;

	internal ProgressBar ProgressBar1;

	internal Label Label4;

	internal TextBox TB_Partition;

	internal Label Label5;

	internal Label L_UpStatus;

	internal Label L_ToPs2_Files;

	internal Label Label6;

	internal TextBox TB_IP;

	internal Button B_Delete_PS2;

	internal Label Label7;

	internal GroupBox GroupBox1;

	internal GroupBox GroupBox2;

	public Ethernet_Sync()
	{
		InitializeComponent();
	}

	private void Ethernet_Sync_Shown(object sender, EventArgs e)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		if (string.IsNullOrEmpty(OplmSettings.Read("MODE_NET_IP", "")))
		{
			MessageBox.Show("Please set the ip in the settings tab!!", Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
			((Form)this).Close();
			return;
		}
		((Control)TB_IP).Text = OplmSettings.Read("MODE_NET_IP", "");
		string[] foldersToSync = FoldersToSync;
		foreach (string path in foldersToSync)
		{
			filesPC.AddRange(RelativeGetFiles(path));
		}
		foreach (string item in filesPC)
		{
			ListViewItem val = new ListViewItem(item);
			val.SubItems.Add(new FileInfo(hdlFolder + item).Length.ToString());
			LV_PC.Items.Add(val);
		}
		try
		{
			LV_PC.Columns[0].Width = -1;
			LV_PC.Columns[1].Width = -1;
		}
		catch (Exception)
		{
		}
	}

	private List<string> RelativeGetFiles(object path)
	{
		string path2 = Program.MainFormInst.app_folder + "\\hdl\\" + path;
		if (!Directory.Exists(path2))
		{
			Directory.CreateDirectory(path2);
		}
		string[] files = Directory.GetFiles(path2);
		List<string> list = new List<string>();
		string[] array = files;
		foreach (string text in array)
		{
			if (!text.ToLower().EndsWith(".db"))
			{
				list.Add(path?.ToString() + "\\" + Path.GetFileName(text));
			}
		}
		return list;
	}

	private void FTPmountHDD()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create("ftp://" + ((Control)TB_IP).Text + "/hdd/0/" + ((Control)TB_Partition).Text + "/");
		ftpWebRequest.Method = "LIST";
		ftpWebRequest.UsePassive = false;
		ftpWebRequest.KeepAlive = true;
		ftpWebRequest.ConnectionGroupName = "OPL";
		try
		{
			_ = (FtpWebResponse)ftpWebRequest.GetResponse();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "FTP ERROR!", (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
	}

	private List<KeyValuePair<string, string>> GetFtpFiles(string folder)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
		string text = "ftp://" + ((Control)TB_IP).Text + "/pfs/0/" + folder + "/";
		FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(text);
		ftpWebRequest.Method = "LIST";
		ftpWebRequest.UsePassive = false;
		ftpWebRequest.KeepAlive = true;
		ftpWebRequest.ConnectionGroupName = "OPL";
		FtpWebResponse ftpWebResponse = null;
		try
		{
			ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
		}
		catch (WebException ex)
		{
			MessageBox.Show(((FtpWebResponse)ex.Response).StatusDescription + Environment.NewLine + text, "FTP ERROR!", (MessageBoxButtons)0, (MessageBoxIcon)16);
			return list;
		}
		StreamReader streamReader = new StreamReader(ftpWebResponse.GetResponseStream());
		string[] array = streamReader.ReadToEnd().Split(Environment.NewLine);
		foreach (string input in array)
		{
			Match match = new Regex("^([d-])([rwxt-]{3}){3}\\s+\\d{1,}\\s+.*?(\\d{1,})\\s+(\\w+\\s+\\d{1,2}\\s+(?:\\d{4})?)(\\d{1,2}:\\d{2})?\\s+(.+?)\\s?$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace).Match(input);
			if (match.Success & (match.Groups[1].Value == "-"))
			{
				list.Add(new KeyValuePair<string, string>(folder + "/" + match.Groups[6].Value, match.Groups[3].Value));
			}
		}
		streamReader.Close();
		ftpWebResponse.Close();
		return list;
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		FTPmountHDD();
		LV_PS2.Items.Clear();
		string[] foldersToSync = FoldersToSync;
		foreach (string folder in foldersToSync)
		{
			foreach (KeyValuePair<string, string> ftpFile in GetFtpFiles(folder))
			{
				ListViewItem val = new ListViewItem(ftpFile.Key);
				val.SubItems.Add(ftpFile.Value);
				LV_PS2.Items.Add(val);
			}
		}
		try
		{
			LV_PS2.Columns[0].Width = -1;
			LV_PS2.Columns[1].Width = -1;
		}
		catch (Exception)
		{
		}
		((Control)B_PC_TO_PS2).Enabled = true;
		((Control)L_ToPs2_Files).Text = "Files to up: " + ComputeFilesToUpToPs2().Count;
	}

	private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		if (LV_PS2.SelectedItems.Count == 1)
		{
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create("ftp://" + ((Control)TB_IP).Text + "/pfs/0/" + LV_PS2.SelectedItems[0].Text);
			ftpWebRequest.Method = "RETR";
			ftpWebRequest.UsePassive = false;
			ftpWebRequest.KeepAlive = true;
			ftpWebRequest.ConnectionGroupName = "OPL";
			FtpWebResponse ftpWebResponse;
			try
			{
				ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "FTP ERROR!", (MessageBoxButtons)0, (MessageBoxIcon)16);
				return;
			}
			Stream responseStream = ftpWebResponse.GetResponseStream();
			try
			{
				PictureBox1.Image = Image.FromStream(responseStream);
			}
			catch (Exception)
			{
			}
		}
	}

	private void B_PC_TO_PS2_Click(object sender, EventArgs e)
	{
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		ProgressBar1.Value = 0;
		List<string> list = ComputeFilesToUpToPs2();
		ProgressBar1.Maximum = list.Count;
		foreach (string item in list)
		{
			((Control)Label4).Text = "Uploading: " + (ProgressBar1.Value + 1) + "/" + ProgressBar1.Maximum;
			string text = "ftp://" + ((Control)TB_IP).Text + "/pfs/0/" + item.Replace("\\", "/");
			((Control)L_UpStatus).Text = text;
			Application.DoEvents();
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(text);
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.UsePassive = false;
			ftpWebRequest.KeepAlive = true;
			ftpWebRequest.ConnectionGroupName = "OPL";
			try
			{
				byte[] array = File.ReadAllBytes(Program.MainFormInst.app_folder + "\\hdl\\" + item);
				Stream requestStream = ftpWebRequest.GetRequestStream();
				int offset = 0;
				requestStream.Write(array, offset, array.Length);
				Application.DoEvents();
				requestStream.Close();
				Application.DoEvents();
				requestStream.Dispose();
				Application.DoEvents();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + Environment.NewLine + "ERROR UPLOADING " + item);
			}
			ProgressBar progressBar = ProgressBar1;
			progressBar.Value += 1;
			Application.DoEvents();
		}
		((Control)Label4).Text = "All done!";
		MessageBox.Show("Upload complete!", Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
	}

	private List<string> ComputeFilesToUpToPs2()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string>();
		foreach (ListViewItem item in LV_PC.Items)
		{
			ListViewItem val = item;
			string text = val.Text;
			string text2 = val.SubItems[1].Text;
			bool flag = true;
			foreach (ListViewItem item2 in LV_PS2.Items)
			{
				string text3 = item2.Text.Replace("/", "\\");
				string text4 = item2.SubItems[1].Text;
				if ((text3 ?? "") == (text ?? "") && (text4 ?? "") == (text2 ?? ""))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				list.Add(text);
				val.BackColor = Color.Red;
			}
			else
			{
				val.BackColor = Color.Green;
			}
		}
		return list;
	}

	private void B_Delete_PS2_Click(object sender, EventArgs e)
	{
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
		}
		finally
		{
			((Form)this).Dispose(disposing);
		}
	}

	[DebuggerStepThrough]
	private void InitializeComponent()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0bb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbb: Expected O, but got Unknown
		//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd1: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Ethernet_Sync));
		LV_PC = new ListView();
		ColumnHeader1 = new ColumnHeader();
		ColumnHeader2 = new ColumnHeader();
		PictureBox1 = new PictureBox();
		LV_PS2 = new ListView();
		ColumnHeader4 = new ColumnHeader();
		ColumnHeader5 = new ColumnHeader();
		Button1 = new Button();
		Label1 = new Label();
		Label2 = new Label();
		Label3 = new Label();
		B_PC_TO_PS2 = new Button();
		ProgressBar1 = new ProgressBar();
		Label4 = new Label();
		TB_Partition = new TextBox();
		Label5 = new Label();
		L_UpStatus = new Label();
		L_ToPs2_Files = new Label();
		Label6 = new Label();
		TB_IP = new TextBox();
		B_Delete_PS2 = new Button();
		Label7 = new Label();
		GroupBox1 = new GroupBox();
		GroupBox2 = new GroupBox();
		((ISupportInitialize)PictureBox1).BeginInit();
		((Control)GroupBox1).SuspendLayout();
		((Control)GroupBox2).SuspendLayout();
		((Control)this).SuspendLayout();
		LV_PC.Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[2] { ColumnHeader1, ColumnHeader2 });
		((Control)LV_PC).Location = new Point(21, 99);
		((Control)LV_PC).Name = "LV_PC";
		((Control)LV_PC).Size = new Size(313, 358);
		((Control)LV_PC).TabIndex = 0;
		LV_PC.UseCompatibleStateImageBehavior = false;
		LV_PC.View = (View)1;
		ColumnHeader1.Text = "Path";
		ColumnHeader1.Width = 130;
		ColumnHeader2.Text = "Size";
		ColumnHeader2.Width = 122;
		((Control)PictureBox1).Location = new Point(820, 99);
		((Control)PictureBox1).Name = "PictureBox1";
		((Control)PictureBox1).Size = new Size(159, 134);
		PictureBox1.SizeMode = (PictureBoxSizeMode)4;
		PictureBox1.TabIndex = 10;
		PictureBox1.TabStop = false;
		LV_PS2.Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[2] { ColumnHeader4, ColumnHeader5 });
		LV_PS2.FullRowSelect = true;
		((Control)LV_PS2).Location = new Point(449, 99);
		LV_PS2.MultiSelect = false;
		((Control)LV_PS2).Name = "LV_PS2";
		((Control)LV_PS2).Size = new Size(355, 358);
		((Control)LV_PS2).TabIndex = 9;
		LV_PS2.UseCompatibleStateImageBehavior = false;
		LV_PS2.View = (View)1;
		LV_PS2.SelectedIndexChanged += ListView2_SelectedIndexChanged;
		ColumnHeader4.Text = "Path";
		ColumnHeader5.Text = "Size";
		((Control)Button1).Location = new Point(342, 99);
		((Control)Button1).Name = "Button1";
		((Control)Button1).Size = new Size(101, 49);
		((Control)Button1).TabIndex = 11;
		((Control)Button1).Text = "CONNECT FTP";
		((ButtonBase)Button1).UseVisualStyleBackColor = true;
		((Control)Button1).Click += Button1_Click;
		((Control)Label1).AutoSize = true;
		((Control)Label1).Location = new Point(18, 79);
		((Control)Label1).Name = "Label1";
		((Control)Label1).Size = new Size(41, 13);
		((Control)Label1).TabIndex = 12;
		((Control)Label1).Text = "LOCAL";
		((Control)Label2).AutoSize = true;
		((Control)Label2).Location = new Point(446, 79);
		((Control)Label2).Name = "Label2";
		((Control)Label2).Size = new Size(27, 13);
		((Control)Label2).TabIndex = 13;
		((Control)Label2).Text = "PS2";
		((Control)Label3).Location = new Point(18, 9);
		((Control)Label3).Name = "Label3";
		((Control)Label3).Size = new Size(415, 70);
		((Control)Label3).TabIndex = 14;
		((Control)B_PC_TO_PS2).Enabled = false;
		((Control)B_PC_TO_PS2).Location = new Point(342, 169);
		((Control)B_PC_TO_PS2).Name = "B_PC_TO_PS2";
		((Control)B_PC_TO_PS2).Size = new Size(101, 60);
		((Control)B_PC_TO_PS2).TabIndex = 15;
		((Control)B_PC_TO_PS2).Text = "SYNC\r\nPC-->PS2";
		((ButtonBase)B_PC_TO_PS2).UseVisualStyleBackColor = true;
		((Control)B_PC_TO_PS2).Click += B_PC_TO_PS2_Click;
		((Control)ProgressBar1).Location = new Point(6, 58);
		((Control)ProgressBar1).Name = "ProgressBar1";
		((Control)ProgressBar1).Size = new Size(313, 23);
		((Control)ProgressBar1).TabIndex = 16;
		((Control)Label4).AutoSize = true;
		((Control)Label4).Location = new Point(6, 42);
		((Control)Label4).Name = "Label4";
		((Control)Label4).Size = new Size(78, 13);
		((Control)Label4).TabIndex = 17;
		((Control)Label4).Text = "Uploading: 0/0";
		((Control)TB_Partition).Location = new Point(6, 37);
		((Control)TB_Partition).Name = "TB_Partition";
		((Control)TB_Partition).Size = new Size(159, 20);
		((Control)TB_Partition).TabIndex = 18;
		((Control)TB_Partition).Text = "+OPL";
		((Control)Label5).AutoSize = true;
		((Control)Label5).Location = new Point(6, 21);
		((Control)Label5).Name = "Label5";
		((Control)Label5).Size = new Size(95, 13);
		((Control)Label5).TabIndex = 19;
		((Control)Label5).Text = "HDD PARTITION:";
		((Control)L_UpStatus).AutoSize = true;
		((Control)L_UpStatus).Location = new Point(6, 19);
		((Control)L_UpStatus).Name = "L_UpStatus";
		((Control)L_UpStatus).Size = new Size(22, 13);
		((Control)L_UpStatus).TabIndex = 20;
		((Control)L_UpStatus).Text = "NA";
		((Control)L_ToPs2_Files).AutoSize = true;
		((Control)L_ToPs2_Files).Location = new Point(340, 232);
		((Control)L_ToPs2_Files).Name = "L_ToPs2_Files";
		((Control)L_ToPs2_Files).Size = new Size(67, 13);
		((Control)L_ToPs2_Files).TabIndex = 21;
		((Control)L_ToPs2_Files).Text = "Files to up: 0";
		((Control)Label6).AutoSize = true;
		((Control)Label6).Location = new Point(6, 71);
		((Control)Label6).Name = "Label6";
		((Control)Label6).Size = new Size(20, 13);
		((Control)Label6).TabIndex = 22;
		((Control)Label6).Text = "IP:";
		((Control)TB_IP).Location = new Point(9, 87);
		((Control)TB_IP).Name = "TB_IP";
		((TextBoxBase)TB_IP).ReadOnly = true;
		((Control)TB_IP).Size = new Size(159, 20);
		((Control)TB_IP).TabIndex = 23;
		((Control)TB_IP).Text = "192.168.0.0";
		((Control)B_Delete_PS2).Enabled = false;
		((Control)B_Delete_PS2).Location = new Point(340, 383);
		((Control)B_Delete_PS2).Name = "B_Delete_PS2";
		((Control)B_Delete_PS2).Size = new Size(101, 74);
		((Control)B_Delete_PS2).TabIndex = 24;
		((Control)B_Delete_PS2).Text = "Delete files that exist on PS2 but not on PC";
		((ButtonBase)B_Delete_PS2).UseVisualStyleBackColor = true;
		((Control)B_Delete_PS2).Click += B_Delete_PS2_Click;
		((Control)Label7).Location = new Point(810, 405);
		((Control)Label7).Name = "Label7";
		((Control)Label7).Size = new Size(159, 52);
		((Control)Label7).TabIndex = 25;
		((Control)Label7).Text = "You can also click on any image file (png,jpg) in  the PS2 list,  to show them above";
		((Control)GroupBox1).Controls.Add((Control)(object)TB_Partition);
		((Control)GroupBox1).Controls.Add((Control)(object)Label5);
		((Control)GroupBox1).Controls.Add((Control)(object)Label6);
		((Control)GroupBox1).Controls.Add((Control)(object)TB_IP);
		((Control)GroupBox1).Location = new Point(810, 252);
		((Control)GroupBox1).Name = "GroupBox1";
		((Control)GroupBox1).Size = new Size(178, 123);
		((Control)GroupBox1).TabIndex = 26;
		GroupBox1.TabStop = false;
		((Control)GroupBox1).Text = "Settings";
		((Control)GroupBox2).Controls.Add((Control)(object)ProgressBar1);
		((Control)GroupBox2).Controls.Add((Control)(object)Label4);
		((Control)GroupBox2).Controls.Add((Control)(object)L_UpStatus);
		((Control)GroupBox2).Location = new Point(479, 6);
		((Control)GroupBox2).Name = "GroupBox2";
		((Control)GroupBox2).Size = new Size(325, 87);
		((Control)GroupBox2).TabIndex = 27;
		GroupBox2.TabStop = false;
		((Control)GroupBox2).Text = "File transfer status:";
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(991, 469);
		((Control)this).Controls.Add((Control)(object)GroupBox2);
		((Control)this).Controls.Add((Control)(object)GroupBox1);
		((Control)this).Controls.Add((Control)(object)Label7);
		((Control)this).Controls.Add((Control)(object)B_Delete_PS2);
		((Control)this).Controls.Add((Control)(object)L_ToPs2_Files);
		((Control)this).Controls.Add((Control)(object)B_PC_TO_PS2);
		((Control)this).Controls.Add((Control)(object)Label3);
		((Control)this).Controls.Add((Control)(object)Label2);
		((Control)this).Controls.Add((Control)(object)Label1);
		((Control)this).Controls.Add((Control)(object)Button1);
		((Control)this).Controls.Add((Control)(object)PictureBox1);
		((Control)this).Controls.Add((Control)(object)LV_PS2);
		((Control)this).Controls.Add((Control)(object)LV_PC);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "Ethernet_Sync";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Ethernet Sync";
		((Form)this).Shown += Ethernet_Sync_Shown;
		((ISupportInitialize)PictureBox1).EndInit();
		((Control)GroupBox1).ResumeLayout(false);
		((Control)GroupBox1).PerformLayout();
		((Control)GroupBox2).ResumeLayout(false);
		((Control)GroupBox2).PerformLayout();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
