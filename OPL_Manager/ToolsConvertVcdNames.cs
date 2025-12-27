using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class ToolsConvertVcdNames : BaseForm
{
	private string PREFIX = "";

	private List<string> fakeIDsList = new List<string>();

	private IContainer components;

	internal GroupBox GroupBox2;

	internal Button B_Start;

	internal Label L_Info;

	internal ComboBox CB_Mode;

	internal Label L_Mode;

	internal CheckBox CB_Dry;

	internal ListBox ListBox1;

	internal Button B_CopyLog;

	internal CheckBox CB_DeleteElfs;

	public ToolsConvertVcdNames()
	{
		InitializeComponent();
	}

	private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (((ListControl)CB_Mode).SelectedIndex == 1)
		{
			PREFIX = "SB.";
		}
		else if (((ListControl)CB_Mode).SelectedIndex == 2)
		{
			PREFIX = "XX.";
		}
		else if (((ListControl)CB_Mode).SelectedIndex == 3)
		{
			PREFIX = "";
		}
		((Control)B_Start).Enabled = ((ListControl)CB_Mode).SelectedIndex > 0;
	}

	private void WriteLine(string line)
	{
		ListBox1.Items.Add((object)line);
		ListBox1.TopIndex = ListBox1.Items.Count - 1;
	}

	private void B_Go_Click(object sender, EventArgs e)
	{
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		((Control)CB_Mode).Enabled = false;
		((Control)B_Start).Enabled = false;
		((Control)CB_Dry).Enabled = false;
		ListBox1.Items.Clear();
		IEnumerable<GameInfo> enumerable = GameProvider.GameList.Where((GameInfo x) => x.Type == GameType.POPS && x.IsBad);
		WriteLine(string.Format(Translated.ToolsConvertVcdNames_FoundXincorretVCDs, enumerable.Count().ToString()));
		WriteLine("");
		foreach (GameInfo item in enumerable)
		{
			string text = item.ID;
			string path = Path.GetDirectoryName(item.FileDiscFullPath) + Path.DirectorySeparatorChar;
			if (text == null)
			{
				text = CommonFuncs.GenFakeGameID(ref fakeIDsList);
				WriteLine(string.Format(Translated.ToolsConvertVcdNames_GameHasInvalidID1, Path.GetFileName(item.FileDiscOnly)));
				WriteLine("\t" + string.Format(Translated.ToolsConvertVcdNames_GameHasInvalidID2, text));
				WriteLine("");
			}
			WriteLine(string.Format(Translated.ToolsConvertVcdNames_RenamingGame, Path.GetFileName(item.FileDiscOnly)) + "'  ID: " + text);
			string fileName = Path.GetFileName(item.FileDiscFullPath);
			string text2 = Regex.Replace(fileName, "[a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]_[0-9][0-9][0-9]\\.[0-9][0-9]\\.", "");
			string text3 = text + "." + text2;
			string text4 = PREFIX + Path.GetFileNameWithoutExtension(fileName) + ".ELF";
			string[] files = Directory.GetFiles(OplFolders.ART, text4 + "*");
			foreach (string text5 in files)
			{
				string text6 = Path.GetFileName(text5).Replace(text4, "");
				string text7 = text + text6;
				WriteLine("\t" + Translated.ToolsConvertVcdNames_RenamingArt + " " + Path.GetFileName(text5) + " ==> " + text7);
				RenameFile(text5, text7);
			}
			string text8 = Path.Combine(OplFolders.CFG, text4 + ".cfg");
			if (File.Exists(text8))
			{
				string text9 = text + ".cfg";
				WriteLine("\t" + Translated.ToolsConvertVcdNames_RenamingCfg + " " + Path.GetFileName(text8) + " ==> " + text9);
				RenameFile(text8, text9);
			}
			string text10 = Path.Combine(OplFolders.CFG, text4 + ".cfg.bak");
			if (File.Exists(text10))
			{
				string text11 = text + ".cfg.bak";
				WriteLine("\t" + Translated.ToolsConvertVcdNames_RenamingCfgBak + " " + Path.GetFileName(text10) + " ==> " + text11);
				RenameFile(text10, text11);
			}
			string text12 = Path.Combine(path, Path.GetFileNameWithoutExtension(item.FileDiscFullPath));
			if (Directory.Exists(text12))
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text3);
				WriteLine("\t" + Translated.ToolsConvertVcdNames_RenamingVmc + " " + Path.GetFileName(text12) + " ==> " + fileNameWithoutExtension);
				RenameFolder(text12, fileNameWithoutExtension);
			}
			if (CB_DeleteElfs.Checked)
			{
				string text13 = Path.Combine(path, text4);
				if (File.Exists(text13))
				{
					WriteLine("\t" + Translated.ToolsConvertVcdNames_DeleteELF + " " + Path.GetFileName(text13));
					DeleteFile(text13);
				}
			}
			if ((fileName ?? "") != (text3 ?? ""))
			{
				WriteLine("\t" + Translated.ToolsConvertVcdNames_RenamingVCD + " " + fileName + " ==> " + text3);
				RenameFile(Path.Combine(path, fileName), text3);
			}
			WriteLine("");
		}
		if (CB_Dry.Checked)
		{
			WriteLine(string.Format(Translated.ToolsConvertVcdNames_TestRunComplete, Translated.ToolsConvertVcdNames_CbTestRun));
			MessageBox.Show(string.Format(Translated.ToolsConvertVcdNames_TestRunComplete, Translated.ToolsConvertVcdNames_CbTestRun));
		}
		else
		{
			WriteLine(Translated.GlobalString_OperationComplete);
			MessageBox.Show(Translated.ToolsConvertVcdNames_RememberElf, Translated.GlobalString_OperationComplete);
		}
		((Control)B_Start).Enabled = CB_Dry.Checked;
		((Control)CB_Dry).Enabled = true;
	}

	private void DeleteFile(string filePath)
	{
		if (CB_Dry.Checked)
		{
			return;
		}
		try
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
		catch (Exception)
		{
			WriteLine("\t\t" + Translated.ToolsConvertVcdNames_ErrorDeletingFile);
		}
	}

	private void RenameFile(string filePath, string newName)
	{
		if (CB_Dry.Checked)
		{
			return;
		}
		try
		{
			string text = Path.Combine(Path.GetDirectoryName(filePath), newName);
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			File.Move(filePath, text);
		}
		catch (Exception)
		{
			WriteLine("\t\t" + Translated.ToolsConvertVcdNames_ErrorRenamingFile);
		}
	}

	private void RenameFolder(string dirPath, string newName)
	{
		if (CB_Dry.Checked)
		{
			return;
		}
		try
		{
			Directory.Move(dirPath, newName);
		}
		catch (Exception)
		{
			WriteLine("\t\t" + Translated.ToolsConvertVcdNames_ErrorRenamingDirectory);
		}
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		string[] array = (from object o in (IEnumerable)ListBox1.Items
			select ((ListControl)ListBox1).GetItemText(o)).ToArray();
		if (array.Length != 0)
		{
			Clipboard.SetText(string.Join(Environment.NewLine, array));
			MessageBox.Show(Translated.Global_LogCopied, Translated.Global_Information);
		}
	}

	private void ToolsConvertVcdNames_Shown(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.ToolsConvertVcdNames_Title;
		((Control)B_Start).Text = Translated.GLOBAL_BUTTON_START;
		((Control)CB_Dry).Text = Translated.ToolsConvertVcdNames_CbTestRun;
		((Control)L_Mode).Text = Translated.GLOBAL_STRING_MODE;
		((Control)B_CopyLog).Text = Translated.Global_CopyLog;
		((Control)CB_DeleteElfs).Text = Translated.ToolsConvertVcdNames_CbDeleteElfs;
		((Control)L_Info).Text = Translated.ToolsConvertVcdNames_Explain;
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
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Expected O, but got Unknown
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsConvertVcdNames));
		GroupBox2 = new GroupBox();
		CB_DeleteElfs = new CheckBox();
		B_CopyLog = new Button();
		L_Mode = new Label();
		CB_Mode = new ComboBox();
		CB_Dry = new CheckBox();
		B_Start = new Button();
		L_Info = new Label();
		ListBox1 = new ListBox();
		((Control)GroupBox2).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)GroupBox2).Controls.Add((Control)(object)CB_DeleteElfs);
		((Control)GroupBox2).Controls.Add((Control)(object)B_CopyLog);
		((Control)GroupBox2).Controls.Add((Control)(object)L_Mode);
		((Control)GroupBox2).Controls.Add((Control)(object)CB_Mode);
		((Control)GroupBox2).Controls.Add((Control)(object)CB_Dry);
		((Control)GroupBox2).Controls.Add((Control)(object)B_Start);
		((Control)GroupBox2).Location = new Point(12, 12);
		((Control)GroupBox2).Name = "GroupBox2";
		((Control)GroupBox2).Size = new Size(266, 107);
		((Control)GroupBox2).TabIndex = 9;
		GroupBox2.TabStop = false;
		CB_DeleteElfs.Checked = true;
		CB_DeleteElfs.CheckState = (CheckState)1;
		((Control)CB_DeleteElfs).Location = new Point(6, 82);
		((Control)CB_DeleteElfs).Name = "CB_DeleteElfs";
		((Control)CB_DeleteElfs).Size = new Size(120, 17);
		((Control)CB_DeleteElfs).TabIndex = 13;
		((Control)CB_DeleteElfs).Text = "Delete ELFs";
		((ButtonBase)CB_DeleteElfs).UseVisualStyleBackColor = true;
		((Control)B_CopyLog).Location = new Point(132, 78);
		((Control)B_CopyLog).Name = "B_CopyLog";
		((Control)B_CopyLog).Size = new Size(128, 23);
		((Control)B_CopyLog).TabIndex = 12;
		((Control)B_CopyLog).Text = "Copy LOG";
		((ButtonBase)B_CopyLog).UseVisualStyleBackColor = true;
		((Control)B_CopyLog).Click += Button1_Click;
		((Control)L_Mode).BackColor = SystemColors.Control;
		((Control)L_Mode).Location = new Point(6, 16);
		((Control)L_Mode).Name = "L_Mode";
		((Control)L_Mode).Size = new Size(63, 21);
		((Control)L_Mode).TabIndex = 11;
		((Control)L_Mode).Text = "Mode:";
		L_Mode.TextAlign = (ContentAlignment)64;
		CB_Mode.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)CB_Mode).FormattingEnabled = true;
		CB_Mode.Items.AddRange(new object[4] { "<select>", "Network", "USB", "HDD" });
		((Control)CB_Mode).Location = new Point(75, 17);
		((Control)CB_Mode).Name = "CB_Mode";
		((Control)CB_Mode).Size = new Size(185, 21);
		((Control)CB_Mode).TabIndex = 10;
		CB_Mode.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
		((Control)CB_Dry).Location = new Point(6, 50);
		((Control)CB_Dry).Name = "CB_Dry";
		((Control)CB_Dry).Size = new Size(120, 17);
		((Control)CB_Dry).TabIndex = 7;
		((Control)CB_Dry).Text = "Test Run";
		((ButtonBase)CB_Dry).UseVisualStyleBackColor = true;
		((Control)B_Start).Enabled = false;
		((Control)B_Start).Location = new Point(132, 44);
		((Control)B_Start).Name = "B_Start";
		((Control)B_Start).Size = new Size(128, 23);
		((Control)B_Start).TabIndex = 6;
		((Control)B_Start).Text = "Start";
		((ButtonBase)B_Start).UseVisualStyleBackColor = true;
		((Control)B_Start).Click += B_Go_Click;
		L_Info.BorderStyle = (BorderStyle)1;
		((Control)L_Info).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Info).Location = new Point(12, 122);
		((Control)L_Info).Name = "L_Info";
		((Control)L_Info).Size = new Size(266, 320);
		((Control)L_Info).TabIndex = 8;
		((Control)ListBox1).Anchor = (AnchorStyles)15;
		((Control)ListBox1).BackColor = SystemColors.ControlDark;
		((ListControl)ListBox1).FormattingEnabled = true;
		((Control)ListBox1).Location = new Point(284, 9);
		((Control)ListBox1).Name = "ListBox1";
		ListBox1.SelectionMode = (SelectionMode)0;
		((Control)ListBox1).Size = new Size(641, 433);
		((Control)ListBox1).TabIndex = 11;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(937, 456);
		((Control)this).Controls.Add((Control)(object)ListBox1);
		((Control)this).Controls.Add((Control)(object)GroupBox2);
		((Control)this).Controls.Add((Control)(object)L_Info);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "ToolsConvertVcdNames";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "ToolsConvertVcdNames";
		((Form)this).Shown += ToolsConvertVcdNames_Shown;
		((Control)GroupBox2).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
	}
}
