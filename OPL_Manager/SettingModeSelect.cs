using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class SettingModeSelect : BaseForm
{
	private bool canClose;

	private IContainer components;

	internal RadioButton RB_Net;

	internal RadioButton RB_Normal;

	internal GroupBox GB_NetSettings;

	internal TextBox TB_IP;

	internal Label GB_NetSettings_L_IP;

	internal Button B_Save;

	internal GroupBox GB_NormalSettings;

	internal Label L_NormalHint;

	internal Button B_Normal_Browse;

	internal Label GB_NormalSettings_L_Path;

	internal TextBox TB_Path;

	internal GroupBox GB_Mode;

	internal Button GB_NetSettings_ClearCache;

	internal Button GB_NetSettings_Test;

	internal RadioButton RB_HddLocal;

	internal ComboBox hdl_version;

	internal GroupBox GB_HdlVersion;

	internal Label L_HdlRunAdmin;

	public SettingModeSelect()
	{
		InitializeComponent();
	}

	private void B_Save_Click(object sender, EventArgs e)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (RB_Net.Checked)
		{
			if (!CommonFuncs.IsAddressValid(((Control)TB_IP).Text.Trim()))
			{
				MessageBox.Show("The IP address is INVALID!", Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
				return;
			}
			OplmSettings.Write("MODE", 1.ToString());
			OplmSettings.Write("MODE_NET_IP", ((Control)TB_IP).Text.Trim());
			OplmSettings.Write("HDL_VERSION", ((Control)hdl_version).Text);
		}
		else if (RB_Normal.Checked)
		{
			OplmSettings.Write("MODE", 0.ToString());
			OplmSettings.Write("OPL_FOLDER", ((Control)TB_Path).Text.Trim());
		}
		else if (RB_HddLocal.Checked)
		{
			OplmSettings.Write("MODE", 3.ToString());
			OplmSettings.Write("HDL_VERSION", ((Control)hdl_version).Text);
		}
		Program.MainFormInst.setMode();
		canClose = true;
		((Form)this).Close();
	}

	private void Setting_ModeSelect_FormClosing(object sender, FormClosingEventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (!canClose)
		{
			((CancelEventArgs)(object)e).Cancel = true;
			MessageBox.Show(Translated.Global_PleaseUseTheSaveBtn, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
	}

	private void Setting_ModeSelect_Shown(object sender, EventArgs e)
	{
		int num = OplmSettings.Read("MODE", 0);
		((Control)TB_Path).Text = OplmSettings.Read("OPL_FOLDER", "");
		((Control)TB_IP).Text = OplmSettings.Read("MODE_NET_IP", "");
		RB_Normal.Checked = num == 0;
		RB_Net.Checked = num == 1;
		RB_HddLocal.Checked = num == 3;
		RadioButtons_CheckedChanged(null, null);
		((Control)this).Text = Translated.MAIN_TOOLBAR_SETTINGS_MODE;
		((Control)GB_Mode).Text = Translated.GLOBAL_STRING_MODE;
		((Control)RB_Normal).Text = Translated.SettingModeSelect_ModeNormal;
		((Control)RB_Net).Text = Translated.SettingModeSelect_ModeNetwork;
		((Control)RB_HddLocal).Text = Translated.SettingModeSelect_ModeLocalHDD;
		((Control)GB_NetSettings).Text = Translated.SettingModeSelect_SetNet;
		((Control)GB_NetSettings_Test).Text = Translated.SettingModeSelect_SetNetTest;
		((Control)GB_NetSettings_ClearCache).Text = Translated.SettingModeSelect_SetNetClearCache;
		((Control)GB_HdlVersion).Text = Translated.SettingModeSelect_HdlVersion;
		((Control)L_HdlRunAdmin).Text = Translated.SettingModeSelect_HddRunAsAdministrator;
		((ListControl)hdl_version).SelectedIndex = hdl_version.FindStringExact(OplmSettings.ReadString("HDL_VERSION"));
		((Control)GB_NormalSettings).Text = Translated.SettingModeSelect_SetNormal;
		((Control)B_Normal_Browse).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)GB_NormalSettings_L_Path).Text = Translated.SettingModeSelect_SetNormalPath;
		((Control)B_Save).Text = Translated.GLOBAL_BUTTON_SAVE;
		((Control)L_NormalHint).Text = Translated.SettingModeSelect_SetNormalHint;
	}

	private void B_Normal_Browse_Click(object sender, EventArgs e)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Invalid comparison between Unknown and I4
		FolderBrowserDialog val = new FolderBrowserDialog();
		try
		{
			val.RootFolder = Environment.SpecialFolder.Desktop;
			if ((int)((CommonDialog)val).ShowDialog() == 1)
			{
				((Control)TB_Path).Text = val.SelectedPath;
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		((Control)GB_NormalSettings).Enabled = RB_Normal.Checked;
		((Control)GB_NetSettings).Enabled = RB_Net.Checked;
		((Control)GB_HdlVersion).Enabled = RB_Net.Checked | RB_HddLocal.Checked;
	}

	private void B_Network_ClearCache_Click(object sender, EventArgs e)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Invalid comparison between Unknown and I4
		if ((int)MessageBox.Show("You sure you want to clear all HDL Dump cache on this pc?", "Question", (MessageBoxButtons)4) == 6)
		{
			Directory.Delete(Program.MainFormInst.app_folder + "\\hdl", recursive: true);
		}
	}

	private void B_NetworkTest_Click(object sender, EventArgs e)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Invalid comparison between Unknown and I4
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if ((int)MessageBox.Show("You really want to test connection to ps2 HDL Server?", "", (MessageBoxButtons)4) == 6)
		{
			MessageBox.Show("Make sure HDL Server is running on the ps2 at " + ((Control)TB_IP).Text + " and press OK!", "", (MessageBoxButtons)0);
			if (CommonFuncs.HDL_Dump_GetList(((Control)TB_IP).Text).Contains("total"))
			{
				MessageBox.Show("Test successful!", "Result");
			}
			else
			{
				MessageBox.Show("Test Failed!", "Result");
			}
		}
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
		//IL_0a71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7b: Expected O, but got Unknown
		//IL_0a8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a98: Expected O, but got Unknown
		//IL_0ae0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aea: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SettingModeSelect));
		RB_Net = new RadioButton();
		RB_Normal = new RadioButton();
		GB_NetSettings = new GroupBox();
		GB_NetSettings_Test = new Button();
		GB_NetSettings_ClearCache = new Button();
		TB_IP = new TextBox();
		GB_NetSettings_L_IP = new Label();
		B_Save = new Button();
		GB_NormalSettings = new GroupBox();
		L_NormalHint = new Label();
		B_Normal_Browse = new Button();
		GB_NormalSettings_L_Path = new Label();
		TB_Path = new TextBox();
		GB_Mode = new GroupBox();
		RB_HddLocal = new RadioButton();
		hdl_version = new ComboBox();
		GB_HdlVersion = new GroupBox();
		L_HdlRunAdmin = new Label();
		((Control)GB_NetSettings).SuspendLayout();
		((Control)GB_NormalSettings).SuspendLayout();
		((Control)GB_Mode).SuspendLayout();
		((Control)GB_HdlVersion).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)RB_Net).Location = new Point(137, 20);
		((Control)RB_Net).Name = "RB_Net";
		((Control)RB_Net).Size = new Size(138, 50);
		((Control)RB_Net).TabIndex = 2;
		RB_Net.TabStop = true;
		((Control)RB_Net).Text = "Network (hdlserver + ftp)";
		((ButtonBase)RB_Net).UseVisualStyleBackColor = true;
		RB_Net.CheckedChanged += RadioButtons_CheckedChanged;
		((Control)RB_Normal).Location = new Point(11, 20);
		((Control)RB_Normal).Name = "RB_Normal";
		((Control)RB_Normal).Size = new Size(120, 50);
		((Control)RB_Normal).TabIndex = 3;
		RB_Normal.TabStop = true;
		((Control)RB_Normal).Text = "Normal (iso + ul.cfg)";
		((ButtonBase)RB_Normal).UseVisualStyleBackColor = true;
		RB_Normal.CheckedChanged += RadioButtons_CheckedChanged;
		((Control)GB_NetSettings).Controls.Add((Control)(object)GB_NetSettings_Test);
		((Control)GB_NetSettings).Controls.Add((Control)(object)GB_NetSettings_ClearCache);
		((Control)GB_NetSettings).Controls.Add((Control)(object)TB_IP);
		((Control)GB_NetSettings).Controls.Add((Control)(object)GB_NetSettings_L_IP);
		((Control)GB_NetSettings).Location = new Point(12, 96);
		((Control)GB_NetSettings).Name = "GB_NetSettings";
		((Control)GB_NetSettings).Size = new Size(217, 75);
		((Control)GB_NetSettings).TabIndex = 4;
		GB_NetSettings.TabStop = false;
		((Control)GB_NetSettings).Text = "Nework Settings";
		((Control)GB_NetSettings_Test).Location = new Point(137, 42);
		((Control)GB_NetSettings_Test).Name = "GB_NetSettings_Test";
		((Control)GB_NetSettings_Test).Size = new Size(46, 23);
		((Control)GB_NetSettings_Test).TabIndex = 10;
		((Control)GB_NetSettings_Test).Text = "Test";
		((ButtonBase)GB_NetSettings_Test).UseVisualStyleBackColor = true;
		((Control)GB_NetSettings_Test).Click += B_NetworkTest_Click;
		((Control)GB_NetSettings_ClearCache).Location = new Point(56, 42);
		((Control)GB_NetSettings_ClearCache).Name = "GB_NetSettings_ClearCache";
		((Control)GB_NetSettings_ClearCache).Size = new Size(75, 23);
		((Control)GB_NetSettings_ClearCache).TabIndex = 2;
		((Control)GB_NetSettings_ClearCache).Text = "Clear Cache";
		((ButtonBase)GB_NetSettings_ClearCache).UseVisualStyleBackColor = true;
		((Control)GB_NetSettings_ClearCache).Click += B_Network_ClearCache_Click;
		((Control)TB_IP).Location = new Point(34, 19);
		((Control)TB_IP).Name = "TB_IP";
		((Control)TB_IP).Size = new Size(172, 20);
		((Control)TB_IP).TabIndex = 1;
		((Control)GB_NetSettings_L_IP).AutoSize = true;
		((Control)GB_NetSettings_L_IP).Location = new Point(8, 22);
		((Control)GB_NetSettings_L_IP).Name = "GB_NetSettings_L_IP";
		((Control)GB_NetSettings_L_IP).Size = new Size(20, 13);
		((Control)GB_NetSettings_L_IP).TabIndex = 0;
		((Control)GB_NetSettings_L_IP).Text = "IP:";
		((Control)B_Save).Location = new Point(168, 267);
		((Control)B_Save).Name = "B_Save";
		((Control)B_Save).Size = new Size(75, 23);
		((Control)B_Save).TabIndex = 5;
		((Control)B_Save).Text = "Save";
		((ButtonBase)B_Save).UseVisualStyleBackColor = true;
		((Control)B_Save).Click += B_Save_Click;
		((Control)GB_NormalSettings).Controls.Add((Control)(object)L_NormalHint);
		((Control)GB_NormalSettings).Controls.Add((Control)(object)B_Normal_Browse);
		((Control)GB_NormalSettings).Controls.Add((Control)(object)GB_NormalSettings_L_Path);
		((Control)GB_NormalSettings).Controls.Add((Control)(object)TB_Path);
		((Control)GB_NormalSettings).Location = new Point(12, 177);
		((Control)GB_NormalSettings).Name = "GB_NormalSettings";
		((Control)GB_NormalSettings).Size = new Size(384, 84);
		((Control)GB_NormalSettings).TabIndex = 6;
		GB_NormalSettings.TabStop = false;
		((Control)GB_NormalSettings).Text = "Normal Settings";
		((Control)L_NormalHint).Location = new Point(9, 61);
		((Control)L_NormalHint).Name = "L_NormalHint";
		((Control)L_NormalHint).Size = new Size(367, 13);
		((Control)L_NormalHint).TabIndex = 6;
		((Control)L_NormalHint).Text = "Type path to OPL folder or search with \"Browse...\"";
		L_NormalHint.TextAlign = (ContentAlignment)32;
		((Control)B_Normal_Browse).Location = new Point(303, 9);
		((Control)B_Normal_Browse).Name = "B_Normal_Browse";
		((Control)B_Normal_Browse).Size = new Size(75, 23);
		((Control)B_Normal_Browse).TabIndex = 5;
		((Control)B_Normal_Browse).Text = "Browse...";
		((ButtonBase)B_Normal_Browse).UseVisualStyleBackColor = true;
		((Control)B_Normal_Browse).Click += B_Normal_Browse_Click;
		((Control)GB_NormalSettings_L_Path).Location = new Point(6, 38);
		((Control)GB_NormalSettings_L_Path).Name = "GB_NormalSettings_L_Path";
		((Control)GB_NormalSettings_L_Path).Size = new Size(59, 20);
		((Control)GB_NormalSettings_L_Path).TabIndex = 3;
		((Control)GB_NormalSettings_L_Path).Text = "Path:";
		GB_NormalSettings_L_Path.TextAlign = (ContentAlignment)64;
		((Control)TB_Path).Location = new Point(71, 38);
		((Control)TB_Path).Name = "TB_Path";
		((Control)TB_Path).Size = new Size(307, 20);
		((Control)TB_Path).TabIndex = 4;
		((Control)GB_Mode).Controls.Add((Control)(object)RB_HddLocal);
		((Control)GB_Mode).Controls.Add((Control)(object)RB_Net);
		((Control)GB_Mode).Controls.Add((Control)(object)RB_Normal);
		((Control)GB_Mode).Location = new Point(12, 12);
		((Control)GB_Mode).Name = "GB_Mode";
		((Control)GB_Mode).Size = new Size(384, 78);
		((Control)GB_Mode).TabIndex = 7;
		GB_Mode.TabStop = false;
		((Control)GB_Mode).Text = "Mode:";
		((Control)RB_HddLocal).Location = new Point(281, 20);
		((Control)RB_HddLocal).Name = "RB_HddLocal";
		((Control)RB_HddLocal).Size = new Size(98, 50);
		((Control)RB_HddLocal).TabIndex = 4;
		RB_HddLocal.TabStop = true;
		((Control)RB_HddLocal).Text = "Local PS2 HDD";
		((ButtonBase)RB_HddLocal).UseVisualStyleBackColor = true;
		hdl_version.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)hdl_version).FormattingEnabled = true;
		hdl_version.Items.AddRange(new object[3] { "hdl_dump_092.exe", "hdl_dump_090.exe", "hdl_dump_086.exe" });
		((Control)hdl_version).Location = new Point(6, 18);
		((Control)hdl_version).Name = "hdl_version";
		((Control)hdl_version).Size = new Size(150, 21);
		((Control)hdl_version).TabIndex = 8;
		((Control)GB_HdlVersion).Controls.Add((Control)(object)L_HdlRunAdmin);
		((Control)GB_HdlVersion).Controls.Add((Control)(object)hdl_version);
		((Control)GB_HdlVersion).Location = new Point(235, 96);
		((Control)GB_HdlVersion).Name = "GB_HdlVersion";
		((Control)GB_HdlVersion).Size = new Size(162, 75);
		((Control)GB_HdlVersion).TabIndex = 9;
		GB_HdlVersion.TabStop = false;
		((Control)GB_HdlVersion).Text = "HDL Dump Version";
		((Control)L_HdlRunAdmin).BackColor = SystemColors.Control;
		((Control)L_HdlRunAdmin).ForeColor = Color.Red;
		((Control)L_HdlRunAdmin).Location = new Point(6, 42);
		((Control)L_HdlRunAdmin).Name = "L_HdlRunAdmin";
		((Control)L_HdlRunAdmin).Size = new Size(150, 30);
		((Control)L_HdlRunAdmin).TabIndex = 9;
		((Control)L_HdlRunAdmin).Text = "Make sure to run OPLM as administrator!";
		L_HdlRunAdmin.TextAlign = (ContentAlignment)32;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(409, 292);
		((Control)this).Controls.Add((Control)(object)GB_HdlVersion);
		((Control)this).Controls.Add((Control)(object)GB_Mode);
		((Control)this).Controls.Add((Control)(object)GB_NormalSettings);
		((Control)this).Controls.Add((Control)(object)B_Save);
		((Control)this).Controls.Add((Control)(object)GB_NetSettings);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(425, 330);
		((Control)this).Name = "SettingModeSelect";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Mode Select";
		((Form)this).FormClosing += new FormClosingEventHandler(Setting_ModeSelect_FormClosing);
		((Form)this).Shown += Setting_ModeSelect_Shown;
		((Control)GB_NetSettings).ResumeLayout(false);
		((Control)GB_NetSettings).PerformLayout();
		((Control)GB_NormalSettings).ResumeLayout(false);
		((Control)GB_NormalSettings).PerformLayout();
		((Control)GB_Mode).ResumeLayout(false);
		((Control)GB_HdlVersion).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
	}
}
