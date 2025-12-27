using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using ZSO;

namespace OPL_Manager;

public class ToolsConvertIsoZso : BaseForm
{
	private int curIdx;

	private IsoToZso convIsoToZso = new IsoToZso();

	private ZsoToIso convZsoToIso = new ZsoToIso();

	public bool flagShouldReload;

	private IContainer components;

	internal RadioButton RbIso2Zso;

	internal GroupBox Gb1;

	internal RadioButton RbZso2Iso;

	internal ListView LvFiles;

	internal ColumnHeader ColIn;

	internal ColumnHeader ColProgress;

	internal GroupBox Gb2;

	internal Button BPickFiles;

	internal GroupBox Gb3;

	internal Button BPickOutputDirectory;

	internal TextBox TbOutputDirectory;

	internal GroupBox Gb4;

	internal Label LProgress;

	internal Button BStartCancel;

	internal ProgressBar ProgressBar1;

	internal OpenFileDialog OpenFileDialog1;

	internal FolderBrowserDialog FolderBrowserDialog1;

	internal Button BClearList;

	internal ColumnHeader ColRate;

	public ToolsConvertIsoZso()
	{
		InitializeComponent();
	}

	private void ToolsConvertIsoZso_Load(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.ToolsConvertIsoZso_Title;
		((Control)Gb1).Text = Translated.ToolsConvertIsoZso_SelectMode;
		((Control)Gb2).Text = Translated.ToolsConvertIsoZso_SelectFiles;
		((Control)BClearList).Text = Translated.Global_ButtonClear;
		((Control)BPickFiles).Text = Translated.GLOBAL_BUTTON_BROWSE;
		ColIn.Text = Translated.GLOBAL_FILENAME;
		ColRate.Text = Translated.Global_RatioZso + " %";
		ColProgress.Text = Translated.Global_Progress + " %";
		((Control)Gb3).Text = Translated.Global_OutputDirectory;
		((Control)BPickOutputDirectory).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)BStartCancel).Text = Translated.GLOBAL_BUTTON_START;
		((Control)TbOutputDirectory).Text = OplFolders.DVD;
		convIsoToZso.ProgressChanged += IsoToZsoProgressChanged;
		convIsoToZso.ProgressCompleted += IsoToZsoProgressCompleted;
		convIsoToZso.ProgressCanceled += IsoToZsoProgressCanceled;
		convIsoToZso.ProgressException += IsoToZsoProgressException;
		convZsoToIso.ProgressChanged += ZsoToIsoProgressChanged;
		convZsoToIso.ProgressCompleted += ZsoToIsoProgressCompleted;
		convZsoToIso.ProgressCanceled += ZsoToIsoProgressCanceled;
		convZsoToIso.ProgressException += ZsoToIsoProgressException;
		flagShouldReload = false;
	}

	private void BPickFiles_Click(object sender, EventArgs e)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Invalid comparison between Unknown and I4
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		((FileDialog)OpenFileDialog1).InitialDirectory = OplFolders.Main;
		((FileDialog)OpenFileDialog1).FileName = "";
		((FileDialog)OpenFileDialog1).Filter = (RbIso2Zso.Checked ? "iso games (*.iso)|*.iso" : "zso games (*.zso)|*.zso");
		if ((int)((CommonDialog)OpenFileDialog1).ShowDialog() == 1 && ((FileDialog)OpenFileDialog1).FileNames.Length != 0)
		{
			string[] fileNames = ((FileDialog)OpenFileDialog1).FileNames;
			foreach (string text in fileNames)
			{
				ListViewItem val = new ListViewItem(Path.GetFileName(text));
				val.Tag = text;
				val.SubItems.Add("-");
				val.SubItems.Add("-");
				LvFiles.Items.Add(val);
			}
		}
		HandleFileListChanged();
		CheckConditionsToStart();
	}

	private void HandleFileListChanged()
	{
		((Control)BClearList).Enabled = LvFiles.Items.Count > 0;
	}

	private void ConversionModeChanged(bool modeIsoToZso)
	{
		LvFiles.Items.Clear();
	}

	private void RbIso2Zso_CheckedChanged(object sender, EventArgs e)
	{
		if (RbIso2Zso.Checked)
		{
			ConversionModeChanged(modeIsoToZso: true);
		}
	}

	private void RbZso2Iso_CheckedChanged(object sender, EventArgs e)
	{
		if (RbZso2Iso.Checked)
		{
			ConversionModeChanged(modeIsoToZso: false);
		}
	}

	private void BPickOutputDirectory_Click(object sender, EventArgs e)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Invalid comparison between Unknown and I4
		FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
		FolderBrowserDialog1.ShowNewFolderButton = true;
		if ((int)((CommonDialog)FolderBrowserDialog1).ShowDialog() == 1 && Directory.Exists(FolderBrowserDialog1.SelectedPath))
		{
			((Control)TbOutputDirectory).Text = FolderBrowserDialog1.SelectedPath;
		}
		CheckConditionsToStart();
	}

	private void CheckConditionsToStart()
	{
		((Control)Gb4).Enabled = !string.IsNullOrEmpty(((Control)TbOutputDirectory).Text) && LvFiles.Items.Count > 0;
	}

	private void BClearList_Click(object sender, EventArgs e)
	{
		LvFiles.Items.Clear();
	}

	private void BStartCancel_Click(object sender, EventArgs e)
	{
		if (convIsoToZso.IsRunning)
		{
			((Control)BStartCancel).Text = Translated.Global_Canceling;
			convIsoToZso.Abort();
			return;
		}
		if (convZsoToIso.IsRunning)
		{
			((Control)BStartCancel).Text = Translated.Global_Canceling;
			convZsoToIso.Abort();
			return;
		}
		curIdx = 0;
		LockOrUnlockUI(enabled: false);
		if (RbIso2Zso.Checked)
		{
			IsoToZsoStart();
		}
		else
		{
			ZsoToIsoStart();
		}
	}

	private void IsoToZsoStart()
	{
		string text = LvFiles.Items[curIdx].Tag.ToString();
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
		string zsoFilePath = Path.Combine(((Control)TbOutputDirectory).Text, fileNameWithoutExtension + ".zso");
		convIsoToZso.Run(text, zsoFilePath, 100, 0);
	}

	private void IsoToZsoProgressChanged(object sender, ZsoCompressionProgressEventArgs args)
	{
		UpdateProgress(args.Percent, args.Ratio);
	}

	private void IsoToZsoProgressCompleted(object sender, EventArgs e)
	{
		StartNextWorkIfAny();
		flagShouldReload = true;
	}

	private void IsoToZsoProgressCanceled(object sender, EventArgs e)
	{
		ShowMsgCanceled();
		LockOrUnlockUI(enabled: true);
	}

	private void IsoToZsoProgressException(object sender, IsoToZsoExceptionEventArgs e)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (e.ErrorType == IsoToZsoExceptions.IsoFileIsInvalid)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_IsoFileIsInvalid, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == IsoToZsoExceptions.IsoFileNotFound)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_IsoFileNotFound, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == IsoToZsoExceptions.ZsoFileAlreadyExists)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_ZsoFileAlreadyExists, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else
		{
			MessageBox.Show(e.Msg, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		LockOrUnlockUI(enabled: true);
	}

	private void ZsoToIsoStart()
	{
		string text = LvFiles.Items[curIdx].Tag.ToString();
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
		string isoFilePath = Path.Combine(((Control)TbOutputDirectory).Text, fileNameWithoutExtension + ".iso");
		convZsoToIso.Run(text, isoFilePath);
	}

	private void ZsoToIsoProgressChanged(object sender, ZsoDecompressionProgressEventArgs args)
	{
		UpdateProgress(args.Percent);
	}

	private void ZsoToIsoProgressCompleted(object sender, EventArgs e)
	{
		StartNextWorkIfAny();
		flagShouldReload = true;
	}

	private void ZsoToIsoProgressCanceled(object sender, EventArgs e)
	{
		ShowMsgCanceled();
		LockOrUnlockUI(enabled: true);
	}

	private void ZsoToIsoProgressException(object sender, ZsoToIsoExceptionEventArgs e)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (e.ErrorType == ZsoToIsoExceptions.ZsoFileNotFound)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_ZsoFileNotFound, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == ZsoToIsoExceptions.IsoFileAlreadyExists)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_IsoFileAlreadyExists, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else
		{
			MessageBox.Show(e.Msg, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		LockOrUnlockUI(enabled: true);
	}

	private void StartNextWorkIfAny()
	{
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				StartNextWorkIfAny();
			});
		}
		else if (curIdx < LvFiles.Items.Count - 1)
		{
			curIdx++;
			if (RbIso2Zso.Checked)
			{
				IsoToZsoStart();
			}
			else
			{
				ZsoToIsoStart();
			}
		}
		else
		{
			ShowMsgCompleted();
			LockOrUnlockUI(enabled: true);
		}
	}

	private void UpdateProgress(int progress, int ratio = -1)
	{
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				UpdateProgress(progress, ratio);
			});
			return;
		}
		LvFiles.Items[curIdx].SubItems[1].Text = progress.ToString() ?? "";
		if (ratio > -1)
		{
			LvFiles.Items[curIdx].SubItems[2].Text = ratio.ToString() ?? "";
		}
		int num = (int)Math.Round(Math.Ceiling(100.0 / (double)LvFiles.Items.Count));
		int num2 = num * curIdx;
		int num3 = (int)Math.Round(Math.Ceiling((double)progress * ((double)num / 100.0)));
		int num4 = num2 + num3;
		if (num4 > 100)
		{
			num4 = 100;
		}
		ProgressBar1.Value = num4;
		((Control)LProgress).Text = num4 + "%";
	}

	private void ShowMsgCanceled()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				ShowMsgCanceled();
			});
		}
		else
		{
			MessageBox.Show(Translated.Global_String_OperationCanceled, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
	}

	private void ShowMsgCompleted()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				ShowMsgCompleted();
			});
		}
		else
		{
			MessageBox.Show(Translated.GlobalString_OperationComplete, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
	}

	private void LockOrUnlockUI(bool enabled)
	{
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				LockOrUnlockUI(enabled);
			});
			return;
		}
		((Control)BStartCancel).Text = (enabled ? Translated.BatchCfgUpdate_BtnStart : Translated.GLOBAL_BUTTON_CANCEL);
		((Control)Gb1).Enabled = enabled;
		((Control)Gb2).Enabled = enabled;
		((Control)Gb3).Enabled = enabled;
		if (enabled)
		{
			ProgressBar1.Value = 0;
			((Control)LProgress).Text = "-";
		}
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((Form)this).Dispose(disposing);
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
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fc: Expected O, but got Unknown
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0919: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsConvertIsoZso));
		RbIso2Zso = new RadioButton();
		Gb1 = new GroupBox();
		RbZso2Iso = new RadioButton();
		LvFiles = new ListView();
		ColIn = new ColumnHeader();
		ColProgress = new ColumnHeader();
		ColRate = new ColumnHeader();
		Gb2 = new GroupBox();
		BClearList = new Button();
		BPickFiles = new Button();
		Gb3 = new GroupBox();
		BPickOutputDirectory = new Button();
		TbOutputDirectory = new TextBox();
		Gb4 = new GroupBox();
		LProgress = new Label();
		BStartCancel = new Button();
		ProgressBar1 = new ProgressBar();
		OpenFileDialog1 = new OpenFileDialog();
		FolderBrowserDialog1 = new FolderBrowserDialog();
		((Control)Gb1).SuspendLayout();
		((Control)Gb2).SuspendLayout();
		((Control)Gb3).SuspendLayout();
		((Control)Gb4).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)RbIso2Zso).Location = new Point(10, 19);
		((Control)RbIso2Zso).Name = "RbIso2Zso";
		((Control)RbIso2Zso).Size = new Size(110, 25);
		((Control)RbIso2Zso).TabIndex = 0;
		RbIso2Zso.TabStop = true;
		((Control)RbIso2Zso).Text = "ISO -> ZSO";
		((ButtonBase)RbIso2Zso).TextAlign = (ContentAlignment)32;
		((ButtonBase)RbIso2Zso).UseVisualStyleBackColor = true;
		RbIso2Zso.CheckedChanged += RbIso2Zso_CheckedChanged;
		((Control)Gb1).Controls.Add((Control)(object)RbZso2Iso);
		((Control)Gb1).Controls.Add((Control)(object)RbIso2Zso);
		((Control)Gb1).Location = new Point(12, 12);
		((Control)Gb1).Name = "Gb1";
		((Control)Gb1).Size = new Size(527, 57);
		((Control)Gb1).TabIndex = 2;
		Gb1.TabStop = false;
		((Control)Gb1).Text = "Select mode";
		((Control)RbZso2Iso).Location = new Point(167, 19);
		((Control)RbZso2Iso).Name = "RbZso2Iso";
		((Control)RbZso2Iso).Size = new Size(110, 25);
		((Control)RbZso2Iso).TabIndex = 1;
		RbZso2Iso.TabStop = true;
		((Control)RbZso2Iso).Text = "ZSO -> ISO";
		((ButtonBase)RbZso2Iso).TextAlign = (ContentAlignment)32;
		((ButtonBase)RbZso2Iso).UseVisualStyleBackColor = true;
		RbZso2Iso.CheckedChanged += RbZso2Iso_CheckedChanged;
		LvFiles.Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[3] { ColIn, ColProgress, ColRate });
		((Control)LvFiles).Location = new Point(7, 49);
		LvFiles.MultiSelect = false;
		((Control)LvFiles).Name = "LvFiles";
		((Control)LvFiles).Size = new Size(505, 286);
		((Control)LvFiles).TabIndex = 3;
		LvFiles.UseCompatibleStateImageBehavior = false;
		LvFiles.View = (View)1;
		ColIn.Text = "Input";
		ColIn.Width = 312;
		ColProgress.Text = "Progress %";
		ColProgress.Width = 91;
		ColRate.Text = "Rate %";
		((Control)Gb2).Controls.Add((Control)(object)BClearList);
		((Control)Gb2).Controls.Add((Control)(object)BPickFiles);
		((Control)Gb2).Controls.Add((Control)(object)LvFiles);
		((Control)Gb2).Location = new Point(12, 75);
		((Control)Gb2).Name = "Gb2";
		((Control)Gb2).Size = new Size(527, 346);
		((Control)Gb2).TabIndex = 5;
		Gb2.TabStop = false;
		((Control)Gb2).Text = "Select files";
		((Control)BClearList).Enabled = false;
		((Control)BClearList).Location = new Point(262, 19);
		((Control)BClearList).Name = "BClearList";
		((Control)BClearList).Size = new Size(96, 23);
		((Control)BClearList).TabIndex = 4;
		((Control)BClearList).Text = "Clear";
		((ButtonBase)BClearList).UseVisualStyleBackColor = true;
		((Control)BClearList).Click += BClearList_Click;
		((Control)BPickFiles).Location = new Point(364, 19);
		((Control)BPickFiles).Name = "BPickFiles";
		((Control)BPickFiles).Size = new Size(148, 23);
		((Control)BPickFiles).TabIndex = 0;
		((Control)BPickFiles).Text = "Select files";
		((ButtonBase)BPickFiles).UseVisualStyleBackColor = true;
		((Control)BPickFiles).Click += BPickFiles_Click;
		((Control)Gb3).Controls.Add((Control)(object)BPickOutputDirectory);
		((Control)Gb3).Controls.Add((Control)(object)TbOutputDirectory);
		((Control)Gb3).Location = new Point(12, 427);
		((Control)Gb3).Name = "Gb3";
		((Control)Gb3).Size = new Size(527, 55);
		((Control)Gb3).TabIndex = 6;
		Gb3.TabStop = false;
		((Control)Gb3).Text = "Output directory";
		((Control)BPickOutputDirectory).Location = new Point(437, 19);
		((Control)BPickOutputDirectory).Name = "BPickOutputDirectory";
		((Control)BPickOutputDirectory).Size = new Size(75, 23);
		((Control)BPickOutputDirectory).TabIndex = 0;
		((Control)BPickOutputDirectory).Text = "Browse";
		((ButtonBase)BPickOutputDirectory).UseVisualStyleBackColor = true;
		((Control)BPickOutputDirectory).Click += BPickOutputDirectory_Click;
		((Control)TbOutputDirectory).Location = new Point(7, 19);
		((Control)TbOutputDirectory).Name = "TbOutputDirectory";
		((Control)TbOutputDirectory).Size = new Size(413, 20);
		((Control)TbOutputDirectory).TabIndex = 0;
		((Control)Gb4).Controls.Add((Control)(object)LProgress);
		((Control)Gb4).Controls.Add((Control)(object)BStartCancel);
		((Control)Gb4).Controls.Add((Control)(object)ProgressBar1);
		((Control)Gb4).Enabled = false;
		((Control)Gb4).Location = new Point(12, 489);
		((Control)Gb4).Name = "Gb4";
		((Control)Gb4).Size = new Size(527, 74);
		((Control)Gb4).TabIndex = 7;
		Gb4.TabStop = false;
		((Control)LProgress).Location = new Point(7, 50);
		((Control)LProgress).Name = "LProgress";
		((Control)LProgress).Size = new Size(413, 17);
		((Control)LProgress).TabIndex = 2;
		((Control)LProgress).Text = "-";
		LProgress.TextAlign = (ContentAlignment)32;
		((Control)BStartCancel).Location = new Point(437, 19);
		((Control)BStartCancel).Name = "BStartCancel";
		((Control)BStartCancel).Size = new Size(75, 23);
		((Control)BStartCancel).TabIndex = 1;
		((Control)BStartCancel).Text = "Start";
		((ButtonBase)BStartCancel).UseVisualStyleBackColor = true;
		((Control)BStartCancel).Click += BStartCancel_Click;
		((Control)ProgressBar1).Location = new Point(7, 20);
		((Control)ProgressBar1).Name = "ProgressBar1";
		((Control)ProgressBar1).Size = new Size(413, 23);
		((Control)ProgressBar1).TabIndex = 0;
		OpenFileDialog1.Multiselect = true;
		OpenFileDialog1.ReadOnlyChecked = true;
		((Form)this).ClientSize = new Size(555, 574);
		((Control)this).Controls.Add((Control)(object)Gb4);
		((Control)this).Controls.Add((Control)(object)Gb3);
		((Control)this).Controls.Add((Control)(object)Gb2);
		((Control)this).Controls.Add((Control)(object)Gb1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).Name = "ToolsConvertIsoZso";
		((Form)this).SizeGripStyle = (SizeGripStyle)2;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Convert between ISO and ZSO";
		((Form)this).Load += ToolsConvertIsoZso_Load;
		((Control)Gb1).ResumeLayout(false);
		((Control)Gb2).ResumeLayout(false);
		((Control)Gb3).ResumeLayout(false);
		((Control)Gb3).PerformLayout();
		((Control)Gb4).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
	}
}
