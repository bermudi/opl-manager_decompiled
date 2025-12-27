using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class BatchCfgUpdate : BaseForm
{
	private List<string> CFGS;

	private IContainer components;

	internal Button B_Start;

	internal ProgressBar ProgressBar1;

	internal Label L_Progress;

	internal TextBox TB_Log;

	internal SaveFileDialog SaveFileDialog1;

	public BatchCfgUpdate()
	{
		InitializeComponent();
	}

	private void BatchUpdateCFG_Shown(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.BatchCfgUpdate_Title;
		((Control)L_Progress).Text = "0% (0/0)";
		((Control)B_Start).Text = Translated.BatchCfgUpdate_BtnStart;
		CFGS = new List<string>();
		if (!Directory.Exists(OplFolders.CFG))
		{
			return;
		}
		string[] files = Directory.GetFiles(OplFolders.CFG);
		foreach (string text in files)
		{
			if (Path.GetExtension(text).ToLower() == ".cfg")
			{
				CFGS.Add(text);
			}
		}
	}

	private void B_Start_Click(object sender, EventArgs e)
	{
		((Control)B_Start).Enabled = false;
		((Control)TB_Log).Text = "";
		ProgressBar1.Maximum = CFGS.Count;
		ProgressBar1.Value = 0;
		WriteTextBox(Translated.BatchCfgUpdate_StartingUpgradeProcess);
		foreach (string cFG in CFGS)
		{
			if (File.Exists(cFG))
			{
				TextBox textboxxx = null;
				ConfigClass configClass = new ConfigClass(cFG, skipCheck: false, ref textboxxx);
				WriteTextBox("****");
				WriteTextBox(Translated.BatchCfgUpdate_ProcessingFile + " " + Path.GetFileName(cFG));
				WriteTextBox(configClass.CheckVersion() + Environment.NewLine + Environment.NewLine);
				configClass.WriteCFG();
			}
			ProgressBar progressBar = ProgressBar1;
			progressBar.Value += 1;
			((Control)L_Progress).Text = Math.Round((double)(100 * ProgressBar1.Value) / (double)ProgressBar1.Maximum, 2) + "% (" + ProgressBar1.Value + "/" + ProgressBar1.Maximum + ")";
			Application.DoEvents();
		}
		WriteTextBox("*** " + Translated.GlobalString_OperationComplete + " ***");
	}

	private void WriteTextBox(string text)
	{
		((TextBoxBase)TB_Log).AppendText(text + Environment.NewLine);
		((TextBoxBase)TB_Log).Select(((TextBoxBase)TB_Log).TextLength, 0);
		((TextBoxBase)TB_Log).ScrollToCaret();
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
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Expected O, but got Unknown
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BatchCfgUpdate));
		B_Start = new Button();
		ProgressBar1 = new ProgressBar();
		L_Progress = new Label();
		TB_Log = new TextBox();
		SaveFileDialog1 = new SaveFileDialog();
		((Control)this).SuspendLayout();
		((Control)B_Start).Location = new Point(457, 287);
		((Control)B_Start).Name = "B_Start";
		((Control)B_Start).Size = new Size(75, 23);
		((Control)B_Start).TabIndex = 2;
		((Control)B_Start).Text = "Start";
		((ButtonBase)B_Start).UseVisualStyleBackColor = true;
		((Control)B_Start).Click += B_Start_Click;
		((Control)ProgressBar1).Location = new Point(129, 287);
		((Control)ProgressBar1).Name = "ProgressBar1";
		((Control)ProgressBar1).Size = new Size(322, 23);
		((Control)ProgressBar1).TabIndex = 3;
		((Control)L_Progress).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Progress).Location = new Point(9, 287);
		((Control)L_Progress).Name = "L_Progress";
		((Control)L_Progress).Size = new Size(114, 23);
		((Control)L_Progress).TabIndex = 9;
		((Control)L_Progress).Text = "000% (00/00)";
		L_Progress.TextAlign = (ContentAlignment)32;
		((Control)TB_Log).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)0, (GraphicsUnit)3);
		((Control)TB_Log).Location = new Point(12, 12);
		((TextBoxBase)TB_Log).Multiline = true;
		((Control)TB_Log).Name = "TB_Log";
		((TextBoxBase)TB_Log).ReadOnly = true;
		TB_Log.ScrollBars = (ScrollBars)3;
		((Control)TB_Log).Size = new Size(520, 269);
		((Control)TB_Log).TabIndex = 10;
		((FileDialog)SaveFileDialog1).DefaultExt = "txt";
		((FileDialog)SaveFileDialog1).FileName = "Log.txt";
		((FileDialog)SaveFileDialog1).Filter = "Txt File|*.txt";
		((FileDialog)SaveFileDialog1).Title = "Save log file...";
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(544, 322);
		((Control)this).Controls.Add((Control)(object)TB_Log);
		((Control)this).Controls.Add((Control)(object)L_Progress);
		((Control)this).Controls.Add((Control)(object)ProgressBar1);
		((Control)this).Controls.Add((Control)(object)B_Start);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).Name = "BatchCfgUpdate";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Update Old CFGs";
		((Form)this).Shown += BatchUpdateCFG_Shown;
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
