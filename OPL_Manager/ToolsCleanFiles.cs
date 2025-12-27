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

namespace OPL_Manager;

public class ToolsCleanFiles : BaseForm
{
	private readonly string MSG_SCANNING = Translated.ToolsCleanFiles_LogScanning;

	private readonly string MSG_INVALID_TARGET_FOLDER = Translated.ToolsCleanFiles_LogInvalidTargetFolder;

	private readonly string MSG_FOUND_DRY = Translated.ToolsCleanFiles_LogFoundDry;

	private readonly string MSG_FOUND_DELETE = Translated.ToolsCleanFiles_LogFoundDelete;

	private readonly string MSG_FOUND_MOVE = Translated.ToolsCleanFiles_LogFoundMove;

	private readonly string MSG_NO_FILES_FOUND = Translated.ToolsCleanFiles_LogFoundNoFiles;

	private IContainer components;

	internal TextBox TB_Log;

	internal Button B_Start;

	internal Label L_Notice;

	internal GroupBox GB_Action;

	internal RadioButton RadioMoveFiles;

	internal RadioButton RadioDeleteFiles;

	internal RadioButton RadioDryRun;

	internal GroupBox GB_FileTypes;

	internal CheckedListBox FileTypesInput;

	internal FolderBrowserDialog FolderBrowserDialog1;

	public ToolsCleanFiles()
	{
		InitializeComponent();
	}

	private void ToolsCleanFiles_Shown(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.ToolsCleanFiles_Title;
		((Control)B_Start).Text = Translated.GLOBAL_BUTTON_START;
		((Control)L_Notice).Text = Translated.ToolsCleanFiles_Explanation;
		((Control)GB_FileTypes).Text = Translated.ToolsCleanFiles_GroupTypes;
		((Control)GB_Action).Text = Translated.ToolsCleanFiles_GroupAction;
		((Control)RadioDryRun).Text = Translated.ToolsCleanFiles_GroupActionDryRun;
		((Control)RadioMoveFiles).Text = Translated.ToolsCleanFiles_GroupActionMoveFiles;
		((Control)RadioDeleteFiles).Text = Translated.ToolsCleanFiles_GroupActionDelete;
	}

	private void WriteTextBox(string text, bool newLine = true)
	{
		((TextBoxBase)TB_Log).AppendText(text + (newLine ? Environment.NewLine : ""));
		((TextBoxBase)TB_Log).Select(((TextBoxBase)TB_Log).TextLength, 0);
		((TextBoxBase)TB_Log).ScrollToCaret();
	}

	private void B_Start_Click(object sender, EventArgs e)
	{
		((Control)B_Start).Enabled = false;
		DoCleanFiles();
		((Control)B_Start).Enabled = true;
	}

	private void DoCleanFiles()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		((Control)TB_Log).Text = "";
		string text = null;
		if (RadioMoveFiles.Checked)
		{
			if ((int)((CommonDialog)FolderBrowserDialog1).ShowDialog() == 1)
			{
				text = FolderBrowserDialog1.SelectedPath;
			}
			if (string.IsNullOrEmpty(text) | !Directory.Exists(text))
			{
				WriteTextBox(MSG_INVALID_TARGET_FOLDER);
				return;
			}
		}
		Regex regex = new Regex("^[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]");
		List<string> list = new List<string>();
		List<string> list2 = ((IEnumerable)FileTypesInput.CheckedItems).OfType<string>().ToList();
		List<string> list3 = GameProvider.GameList.Select((GameInfo x) => x.ID).ToList();
		WriteTextBox(MSG_SCANNING);
		foreach (string item in list2)
		{
			WriteTextBox("\t" + item + " ", newLine: false);
			string path = Path.Combine(OplFolders.Main, item);
			int num = 0;
			if (Directory.Exists(path))
			{
				string[] files = Directory.GetFiles(path);
				foreach (string path2 in files)
				{
					Application.DoEvents();
					string fileName = Path.GetFileName(path2);
					Match match = regex.Match(fileName);
					if (match.Success && !list3.Contains(match.Value))
					{
						list.Add(Path.Combine(item, fileName));
						num++;
					}
				}
			}
			WriteTextBox("(" + num + ")");
		}
		WriteTextBox("");
		if (list.Count == 0)
		{
			WriteTextBox(MSG_NO_FILES_FOUND);
			return;
		}
		if (RadioDryRun.Checked)
		{
			WriteTextBox(MSG_FOUND_DRY);
		}
		else if (RadioDeleteFiles.Checked)
		{
			WriteTextBox(MSG_FOUND_DELETE);
		}
		else if (RadioMoveFiles.Checked)
		{
			WriteTextBox(MSG_FOUND_MOVE);
		}
		foreach (string item2 in list)
		{
			Application.DoEvents();
			string text2 = Path.Combine(OplFolders.Main, item2);
			WriteTextBox("\t" + item2);
			if (RadioDeleteFiles.Checked)
			{
				File.Delete(text2);
			}
			else if (RadioMoveFiles.Checked)
			{
				string text3 = Path.Combine(text, item2);
				Directory.CreateDirectory(Path.GetDirectoryName(text3));
				File.Move(text2, text3);
			}
		}
		WriteTextBox(Environment.NewLine + Translated.GlobalString_OperationComplete);
	}

	private void FileTypesInput_SelectedIndexChanged(object sender, EventArgs e)
	{
		((Control)B_Start).Enabled = FileTypesInput.CheckedItems.Count > 0;
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
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Expected O, but got Unknown
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsCleanFiles));
		TB_Log = new TextBox();
		B_Start = new Button();
		L_Notice = new Label();
		GB_Action = new GroupBox();
		RadioMoveFiles = new RadioButton();
		RadioDeleteFiles = new RadioButton();
		RadioDryRun = new RadioButton();
		GB_FileTypes = new GroupBox();
		FileTypesInput = new CheckedListBox();
		FolderBrowserDialog1 = new FolderBrowserDialog();
		((Control)GB_Action).SuspendLayout();
		((Control)GB_FileTypes).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)TB_Log).Location = new Point(15, 221);
		((TextBoxBase)TB_Log).Multiline = true;
		((Control)TB_Log).Name = "TB_Log";
		((TextBoxBase)TB_Log).ReadOnly = true;
		TB_Log.ScrollBars = (ScrollBars)3;
		((Control)TB_Log).Size = new Size(433, 227);
		((Control)TB_Log).TabIndex = 1;
		((Control)B_Start).Enabled = false;
		((Control)B_Start).Location = new Point(338, 184);
		((Control)B_Start).Name = "B_Start";
		((Control)B_Start).Size = new Size(110, 34);
		((Control)B_Start).TabIndex = 2;
		((Control)B_Start).Text = "Start!";
		((ButtonBase)B_Start).UseVisualStyleBackColor = true;
		((Control)B_Start).Click += B_Start_Click;
		((Control)L_Notice).Location = new Point(12, 9);
		((Control)L_Notice).Name = "L_Notice";
		((Control)L_Notice).Size = new Size(436, 102);
		((Control)L_Notice).TabIndex = 8;
		((Control)L_Notice).Text = componentResourceManager.GetString("L_Notice.Text");
		L_Notice.TextAlign = (ContentAlignment)16;
		((Control)GB_Action).Controls.Add((Control)(object)RadioMoveFiles);
		((Control)GB_Action).Controls.Add((Control)(object)RadioDeleteFiles);
		((Control)GB_Action).Controls.Add((Control)(object)RadioDryRun);
		((Control)GB_Action).Location = new Point(121, 114);
		((Control)GB_Action).Name = "GB_Action";
		((Control)GB_Action).Size = new Size(211, 104);
		((Control)GB_Action).TabIndex = 10;
		GB_Action.TabStop = false;
		((Control)GB_Action).Text = "Action";
		((Control)RadioMoveFiles).Location = new Point(5, 45);
		((Control)RadioMoveFiles).Name = "RadioMoveFiles";
		((Control)RadioMoveFiles).Size = new Size(200, 17);
		((Control)RadioMoveFiles).TabIndex = 2;
		((Control)RadioMoveFiles).Text = "Move files to other folder";
		((ButtonBase)RadioMoveFiles).UseVisualStyleBackColor = true;
		((Control)RadioDeleteFiles).Location = new Point(5, 70);
		((Control)RadioDeleteFiles).Name = "RadioDeleteFiles";
		((Control)RadioDeleteFiles).Size = new Size(200, 17);
		((Control)RadioDeleteFiles).TabIndex = 1;
		((Control)RadioDeleteFiles).Text = "Delete files";
		((ButtonBase)RadioDeleteFiles).UseVisualStyleBackColor = true;
		RadioDryRun.Checked = true;
		((Control)RadioDryRun).Location = new Point(5, 20);
		((Control)RadioDryRun).Name = "RadioDryRun";
		((Control)RadioDryRun).Size = new Size(200, 17);
		((Control)RadioDryRun).TabIndex = 0;
		RadioDryRun.TabStop = true;
		((Control)RadioDryRun).Text = "Dry run";
		((ButtonBase)RadioDryRun).UseVisualStyleBackColor = true;
		((Control)GB_FileTypes).Controls.Add((Control)(object)FileTypesInput);
		((Control)GB_FileTypes).Location = new Point(12, 114);
		((Control)GB_FileTypes).Name = "GB_FileTypes";
		((Control)GB_FileTypes).Size = new Size(103, 104);
		((Control)GB_FileTypes).TabIndex = 11;
		GB_FileTypes.TabStop = false;
		((Control)GB_FileTypes).Text = "Types";
		FileTypesInput.CheckOnClick = true;
		((Control)FileTypesInput).Dock = (DockStyle)5;
		((ListControl)FileTypesInput).FormattingEnabled = true;
		((ObjectCollection)FileTypesInput.Items).AddRange(new object[4] { "ART", "CFG", "CHT", "VMC" });
		((Control)FileTypesInput).Location = new Point(3, 16);
		((Control)FileTypesInput).Name = "FileTypesInput";
		((Control)FileTypesInput).Size = new Size(97, 85);
		((Control)FileTypesInput).TabIndex = 0;
		((ListBox)FileTypesInput).SelectedIndexChanged += FileTypesInput_SelectedIndexChanged;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(464, 458);
		((Control)this).Controls.Add((Control)(object)GB_FileTypes);
		((Control)this).Controls.Add((Control)(object)GB_Action);
		((Control)this).Controls.Add((Control)(object)L_Notice);
		((Control)this).Controls.Add((Control)(object)B_Start);
		((Control)this).Controls.Add((Control)(object)TB_Log);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "ToolsCleanFiles";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Clean files";
		((Form)this).Shown += ToolsCleanFiles_Shown;
		((Control)GB_Action).ResumeLayout(false);
		((Control)GB_FileTypes).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
