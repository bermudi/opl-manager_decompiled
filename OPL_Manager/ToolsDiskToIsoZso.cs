using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;
using ZSO;

namespace OPL_Manager;

public class ToolsDiskToIsoZso : BaseForm
{
	private DriveToZso convDriveToZso = new DriveToZso();

	private DriveToIso convDriveToIso = new DriveToIso();

	public bool flagShouldReload;

	private IContainer components;

	internal ComboBox CbDrive;

	internal Button BRefresh;

	internal GroupBox GbOutput;

	internal TextBox TbGameId;

	internal TextBox TbTitle;

	internal Label LGameId;

	internal Label LTitle;

	internal GroupBox GbInput;

	internal Label LFile;

	internal TextBox TbFileName;

	internal Button BStart;

	internal CheckBox CbOldFormat;

	internal ProgressBar PbProgress;

	internal Label LProgress;

	internal FolderBrowserDialog FolderBrowserDialog1;

	internal Label LDiskDrive;

	internal RadioButton RbIso;

	internal RadioButton RbZso;

	internal Label LOutPath;

	internal Button BOutBrowse;

	internal TextBox TbOutPath;

	public ToolsDiskToIsoZso()
	{
		InitializeComponent();
	}

	private void ToolsDiskToIsoZso_Load(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.ToolsDiskToIsoZso_Title;
		((Control)LDiskDrive).Text = Translated.ToolsDiskToIsoZso_DiskDrive;
		((Control)CbOldFormat).Text = Translated.ToolsDiskToIsoZso_UseOldFormat;
		((Control)LTitle).Text = Translated.GLOBAL_TITLE;
		((Control)LFile).Text = Translated.GLOBAL_FILENAME;
		((Control)LOutPath).Text = Translated.Global_OutputDirectory;
		((Control)BOutBrowse).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)BStart).Text = Translated.GLOBAL_BUTTON_START;
		convDriveToIso.ProgressChanged += DriveToIsoProgressChanged;
		convDriveToIso.ProgressCompleted += DriveToIsoProgressCompleted;
		convDriveToIso.ProgressCanceled += DriveToIsoProgressCanceled;
		convDriveToIso.ProgressException += DriveToIsoProgressException;
		convDriveToZso.ProgressChanged += DriveToZsoProgressChanged;
		convDriveToZso.ProgressCompleted += DriveToZsoProgressCompleted;
		convDriveToZso.ProgressCanceled += DriveToZsoProgressCanceled;
		convDriveToZso.ProgressException += DriveToZsoProgressException;
		RefreshDrives();
		CbOldFormat.Checked = OplmSettings.ReadBool("ISO_USE_OLD_NAMING_FORMAT");
		((Control)TbOutPath).Text = OplFolders.DVD;
		LockOrUnlockUI(enabled: true);
		flagShouldReload = false;
	}

	private void RefreshDrives()
	{
		CbDrive.Items.Clear();
		DriveInfo[] drives = DriveInfo.GetDrives();
		int num = -1;
		int i = 0;
		for (int num2 = drives.Length - 1; i <= num2; i++)
		{
			if (drives[i].DriveType == DriveType.CDRom)
			{
				CbDrive.Items.Add((object)drives[i].Name);
				if (drives[i].IsReady && num == -1)
				{
					num = CbDrive.Items.Count - 1;
				}
			}
		}
		if (CbDrive.Items.Count > 0 && num >= 0)
		{
			((ListControl)CbDrive).SelectedIndex = num;
		}
	}

	private void BRefresh_Click(object sender, EventArgs e)
	{
		RefreshDrives();
	}

	private void CbDrive_SelectedIndexChanged(object sender, EventArgs e)
	{
		HandleDiskChanged();
	}

	private void TbGameId_TextChanged(object sender, EventArgs e)
	{
		HandleGameIdChanged();
	}

	private void HandleDiskChanged()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		((Control)TbFileName).Text = "";
		((Control)TbTitle).Text = "";
		((Control)TbGameId).Text = "";
		string text = CbDrive.SelectedItem.ToString();
		if (!new DriveInfo(text).IsReady)
		{
			MessageBox.Show(Translated.ToolsIsoCreator_DriveNotReady, Translated.Global_Error);
			return;
		}
		string path = Path.Combine(text, "SYSTEM.CNF");
		if (File.Exists(path))
		{
			Match match = new Regex("[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]").Match(File.ReadAllText(path));
			if (match.Success)
			{
				((Control)TbGameId).Text = match.Value;
			}
		}
	}

	private void HandleGameIdChanged()
	{
		if (string.IsNullOrEmpty(((Control)TbGameId).Text))
		{
			return;
		}
		string gameNameById = CommonFuncs.SoapAPI.GetGameNameById(GameType.PS2, ((Control)TbGameId).Text);
		if (!string.IsNullOrEmpty(gameNameById))
		{
			((Control)TbTitle).Text = gameNameById;
			return;
		}
		DriveInfo driveInfo = new DriveInfo(CbDrive.SelectedItem.ToString());
		if (!string.IsNullOrEmpty(driveInfo.VolumeLabel))
		{
			((Control)TbTitle).Text = driveInfo.VolumeLabel;
		}
	}

	private void TbTitle_TextChanged(object sender, EventArgs e)
	{
		GenerateFileName();
	}

	private void RbIso_CheckedChanged(object sender, EventArgs e)
	{
		GenerateFileName();
	}

	private void RbZso_CheckedChanged(object sender, EventArgs e)
	{
		GenerateFileName();
	}

	private void CbOldFormat_CheckedChanged(object sender, EventArgs e)
	{
		GenerateFileName();
	}

	private void GenerateFileName()
	{
		if (!string.IsNullOrEmpty(((Control)TbGameId).Text) && !string.IsNullOrEmpty(((Control)TbTitle).Text))
		{
			string text = (CbOldFormat.Checked ? (((Control)TbGameId).Text + ".") : "");
			text += CommonFuncs.SanitizeGameTitle(((Control)TbTitle).Text);
			text += (RbIso.Checked ? ".iso" : ".zso");
			((Control)TbFileName).Text = text;
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
		((Control)GbInput).Enabled = enabled;
		((Control)GbOutput).Enabled = enabled;
		((Control)BStart).Text = (enabled ? Translated.GLOBAL_BUTTON_START : Translated.GLOBAL_BUTTON_CANCEL);
		if (enabled)
		{
			PbProgress.Value = 0;
			((Control)LProgress).Text = "-";
		}
	}

	private void BStart_Click(object sender, EventArgs e)
	{
		if (convDriveToIso.IsRunning)
		{
			convDriveToIso.Abort();
		}
		else if (convDriveToZso.IsRunning)
		{
			convDriveToZso.Abort();
		}
		else if (((Control)GbInput).Enabled)
		{
			StartProcess();
		}
	}

	private void StartProcess()
	{
		LockOrUnlockUI(enabled: false);
		string discPath = CbDrive.SelectedItem.ToString();
		string output = Path.Combine(((Control)TbOutPath).Text, ((Control)TbFileName).Text);
		if (RbZso.Checked)
		{
			convDriveToZso.CreateZso(discPath, output, 100, 0);
		}
		else
		{
			convDriveToIso.CreateIso(discPath, output);
		}
	}

	private void DriveToIsoProgressChanged(object sender, DriveToIsoEventArgs e)
	{
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				DriveToIsoProgressChanged(sender, e);
			});
		}
		else
		{
			PbProgress.Value = e.Percent;
			((Control)LProgress).Text = $"{e.Percent}%";
		}
	}

	private void DriveToIsoProgressCompleted(object sender, EventArgs e)
	{
		ShowMsgCompleted();
		LockOrUnlockUI(enabled: true);
		flagShouldReload = true;
	}

	private void DriveToIsoProgressCanceled(object sender, EventArgs e)
	{
		ShowMsgCanceled();
		LockOrUnlockUI(enabled: true);
	}

	private void DriveToIsoProgressException(object sender, DriveToIsoExceptionEventArgs e)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				DriveToIsoProgressException(sender, e);
			});
			return;
		}
		if (e.ErrorType == DriveToIsoExceptions.IsoFileAlreadyExists)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_IsoFileAlreadyExists, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == DriveToIsoExceptions.DriveNotCdRom)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_DriveNotCdRom, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == DriveToIsoExceptions.DriveEmptyOrNotReady)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_DriveEmptyOrNotReady, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == DriveToIsoExceptions.NoFreeSpace)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_NoFreeSpace, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else
		{
			MessageBox.Show(e.Msg, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		LockOrUnlockUI(enabled: true);
	}

	private void DriveToZsoProgressChanged(object sender, ZsoCompressionProgressEventArgs args)
	{
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				DriveToZsoProgressChanged(sender, args);
			});
			return;
		}
		PbProgress.Value = args.Percent;
		((Control)LProgress).Text = $"{Translated.Global_String_Progress} {args.Percent}% | {Translated.Global_RatioZso}: {args.Ratio}%";
	}

	private void DriveToZsoProgressCompleted(object sender, EventArgs e)
	{
		ShowMsgCompleted();
		LockOrUnlockUI(enabled: true);
		flagShouldReload = true;
	}

	private void DriveToZsoProgressCanceled(object sender, EventArgs e)
	{
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				DriveToZsoProgressCanceled(sender, e);
			});
		}
		else
		{
			ShowMsgCanceled();
			LockOrUnlockUI(enabled: true);
		}
	}

	private void DriveToZsoProgressException(object sender, DriveToZsoExceptionEventArgs e)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (((Control)this).InvokeRequired)
		{
			((Control)this).Invoke((Action)delegate
			{
				DriveToZsoProgressException(sender, e);
			});
			return;
		}
		if (e.ErrorType == DriveToZsoExceptions.ZsoFileAlreadyExists)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_ZsoFileAlreadyExists, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == DriveToZsoExceptions.DriveNotCdRom)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_DriveNotCdRom, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == DriveToZsoExceptions.DriveEmptyOrNotReady)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_DriveEmptyOrNotReady, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else if (e.ErrorType == DriveToZsoExceptions.NoFreeSpace)
		{
			MessageBox.Show(Translated.ToolsDiskToIsoZso_NoFreeSpace, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		else
		{
			MessageBox.Show(e.Msg, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
		LockOrUnlockUI(enabled: true);
	}

	private void BOutBrowse_Click(object sender, EventArgs e)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)((CommonDialog)FolderBrowserDialog1).ShowDialog() == 1 && Directory.Exists(FolderBrowserDialog1.SelectedPath))
		{
			((Control)TbOutPath).Text = FolderBrowserDialog1.SelectedPath;
		}
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
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0928: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb5: Expected O, but got Unknown
		//IL_0bc8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd2: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsDiskToIsoZso));
		CbDrive = new ComboBox();
		BRefresh = new Button();
		GbOutput = new GroupBox();
		RbIso = new RadioButton();
		RbZso = new RadioButton();
		LOutPath = new Label();
		BOutBrowse = new Button();
		TbOutPath = new TextBox();
		BStart = new Button();
		TbGameId = new TextBox();
		TbTitle = new TextBox();
		LGameId = new Label();
		LTitle = new Label();
		GbInput = new GroupBox();
		LDiskDrive = new Label();
		CbOldFormat = new CheckBox();
		LFile = new Label();
		TbFileName = new TextBox();
		PbProgress = new ProgressBar();
		LProgress = new Label();
		FolderBrowserDialog1 = new FolderBrowserDialog();
		((Control)GbOutput).SuspendLayout();
		((Control)GbInput).SuspendLayout();
		((Control)this).SuspendLayout();
		CbDrive.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)CbDrive).FormattingEnabled = true;
		((Control)CbDrive).Location = new Point(7, 40);
		((Control)CbDrive).Name = "CbDrive";
		((Control)CbDrive).Size = new Size(121, 21);
		((Control)CbDrive).TabIndex = 0;
		CbDrive.SelectedIndexChanged += CbDrive_SelectedIndexChanged;
		((Control)BRefresh).BackgroundImage = (Image)componentResourceManager.GetObject("BRefresh.BackgroundImage");
		((Control)BRefresh).BackgroundImageLayout = (ImageLayout)4;
		((Control)BRefresh).Location = new Point(134, 40);
		((Control)BRefresh).Margin = new Padding(0);
		((Control)BRefresh).Name = "BRefresh";
		((Control)BRefresh).Size = new Size(21, 21);
		((Control)BRefresh).TabIndex = 1;
		((ButtonBase)BRefresh).UseVisualStyleBackColor = true;
		((Control)BRefresh).Click += BRefresh_Click;
		((Control)GbOutput).Controls.Add((Control)(object)RbIso);
		((Control)GbOutput).Controls.Add((Control)(object)RbZso);
		((Control)GbOutput).Controls.Add((Control)(object)LOutPath);
		((Control)GbOutput).Controls.Add((Control)(object)BOutBrowse);
		((Control)GbOutput).Controls.Add((Control)(object)TbOutPath);
		((Control)GbOutput).Location = new Point(5, 227);
		((Control)GbOutput).Name = "GbOutput";
		((Control)GbOutput).Size = new Size(439, 111);
		((Control)GbOutput).TabIndex = 2;
		GbOutput.TabStop = false;
		RbIso.Checked = true;
		((Control)RbIso).Location = new Point(149, 71);
		((Control)RbIso).Name = "RbIso";
		((Control)RbIso).Size = new Size(50, 24);
		((Control)RbIso).TabIndex = 12;
		RbIso.TabStop = true;
		((Control)RbIso).Text = "ISO";
		((ButtonBase)RbIso).UseVisualStyleBackColor = true;
		RbIso.CheckedChanged += RbIso_CheckedChanged;
		((Control)RbZso).Location = new Point(242, 71);
		((Control)RbZso).Name = "RbZso";
		((Control)RbZso).Size = new Size(50, 24);
		((Control)RbZso).TabIndex = 13;
		((Control)RbZso).Text = "ZSO";
		((ButtonBase)RbZso).UseVisualStyleBackColor = true;
		RbZso.CheckedChanged += RbZso_CheckedChanged;
		((Control)LOutPath).Location = new Point(14, 9);
		((Control)LOutPath).Margin = new Padding(0);
		((Control)LOutPath).Name = "LOutPath";
		((Control)LOutPath).Size = new Size(341, 20);
		((Control)LOutPath).TabIndex = 11;
		((Control)LOutPath).Text = "Output folder";
		LOutPath.TextAlign = (ContentAlignment)256;
		((Control)BOutBrowse).Location = new Point(358, 11);
		((Control)BOutBrowse).Name = "BOutBrowse";
		((Control)BOutBrowse).Size = new Size(75, 20);
		((Control)BOutBrowse).TabIndex = 10;
		((Control)BOutBrowse).Text = "Browse";
		((ButtonBase)BOutBrowse).UseVisualStyleBackColor = true;
		((Control)BOutBrowse).Click += BOutBrowse_Click;
		((Control)TbOutPath).Location = new Point(14, 32);
		((Control)TbOutPath).Name = "TbOutPath";
		((Control)TbOutPath).Size = new Size(419, 20);
		((Control)TbOutPath).TabIndex = 9;
		((Control)BStart).Location = new Point(369, 344);
		((Control)BStart).Name = "BStart";
		((Control)BStart).Size = new Size(75, 23);
		((Control)BStart).TabIndex = 10;
		((Control)BStart).Text = "Start";
		((ButtonBase)BStart).UseVisualStyleBackColor = true;
		((Control)BStart).Click += BStart_Click;
		((Control)TbGameId).Location = new Point(7, 87);
		((Control)TbGameId).Name = "TbGameId";
		((TextBoxBase)TbGameId).ReadOnly = true;
		((Control)TbGameId).Size = new Size(121, 20);
		((Control)TbGameId).TabIndex = 3;
		((Control)TbGameId).TextChanged += TbGameId_TextChanged;
		((Control)TbTitle).Location = new Point(7, 133);
		((Control)TbTitle).Name = "TbTitle";
		((Control)TbTitle).Size = new Size(420, 20);
		((Control)TbTitle).TabIndex = 4;
		((Control)TbTitle).TextChanged += TbTitle_TextChanged;
		((Control)LGameId).Location = new Point(7, 64);
		((Control)LGameId).Margin = new Padding(0);
		((Control)LGameId).Name = "LGameId";
		((Control)LGameId).Size = new Size(120, 20);
		((Control)LGameId).TabIndex = 5;
		((Control)LGameId).Text = "ID";
		LGameId.TextAlign = (ContentAlignment)256;
		((Control)LTitle).Location = new Point(7, 110);
		((Control)LTitle).Margin = new Padding(0);
		((Control)LTitle).Name = "LTitle";
		((Control)LTitle).Size = new Size(420, 20);
		((Control)LTitle).TabIndex = 6;
		((Control)LTitle).Text = "Title";
		LTitle.TextAlign = (ContentAlignment)256;
		((Control)GbInput).Controls.Add((Control)(object)LDiskDrive);
		((Control)GbInput).Controls.Add((Control)(object)CbOldFormat);
		((Control)GbInput).Controls.Add((Control)(object)LFile);
		((Control)GbInput).Controls.Add((Control)(object)TbFileName);
		((Control)GbInput).Controls.Add((Control)(object)CbDrive);
		((Control)GbInput).Controls.Add((Control)(object)LTitle);
		((Control)GbInput).Controls.Add((Control)(object)BRefresh);
		((Control)GbInput).Controls.Add((Control)(object)LGameId);
		((Control)GbInput).Controls.Add((Control)(object)TbTitle);
		((Control)GbInput).Controls.Add((Control)(object)TbGameId);
		((Control)GbInput).Location = new Point(12, 12);
		((Control)GbInput).Name = "GbInput";
		((Control)GbInput).Size = new Size(432, 209);
		((Control)GbInput).TabIndex = 7;
		GbInput.TabStop = false;
		((Control)LDiskDrive).Location = new Point(7, 16);
		((Control)LDiskDrive).Margin = new Padding(0);
		((Control)LDiskDrive).Name = "LDiskDrive";
		((Control)LDiskDrive).Size = new Size(120, 20);
		((Control)LDiskDrive).TabIndex = 10;
		((Control)LDiskDrive).Text = "Disk drive";
		LDiskDrive.TextAlign = (ContentAlignment)256;
		((Control)CbOldFormat).Location = new Point(135, 87);
		((Control)CbOldFormat).Margin = new Padding(0);
		((Control)CbOldFormat).Name = "CbOldFormat";
		((Control)CbOldFormat).Size = new Size(292, 20);
		((Control)CbOldFormat).TabIndex = 9;
		((Control)CbOldFormat).Text = "Use old filename format";
		((ButtonBase)CbOldFormat).UseVisualStyleBackColor = true;
		CbOldFormat.CheckedChanged += CbOldFormat_CheckedChanged;
		((Control)LFile).Location = new Point(7, 156);
		((Control)LFile).Margin = new Padding(0);
		((Control)LFile).Name = "LFile";
		((Control)LFile).Size = new Size(420, 20);
		((Control)LFile).TabIndex = 8;
		((Control)LFile).Text = "File";
		LFile.TextAlign = (ContentAlignment)256;
		((Control)TbFileName).Location = new Point(7, 179);
		((Control)TbFileName).Name = "TbFileName";
		((TextBoxBase)TbFileName).ReadOnly = true;
		((Control)TbFileName).Size = new Size(420, 20);
		((Control)TbFileName).TabIndex = 7;
		((Control)PbProgress).Location = new Point(5, 373);
		((Control)PbProgress).Name = "PbProgress";
		((Control)PbProgress).Size = new Size(439, 23);
		((Control)PbProgress).TabIndex = 8;
		((Control)LProgress).Location = new Point(5, 399);
		((Control)LProgress).Name = "LProgress";
		((Control)LProgress).Size = new Size(439, 40);
		((Control)LProgress).TabIndex = 9;
		((Control)LProgress).Text = "-";
		LProgress.TextAlign = (ContentAlignment)32;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(454, 441);
		((Control)this).Controls.Add((Control)(object)BStart);
		((Control)this).Controls.Add((Control)(object)LProgress);
		((Control)this).Controls.Add((Control)(object)PbProgress);
		((Control)this).Controls.Add((Control)(object)GbInput);
		((Control)this).Controls.Add((Control)(object)GbOutput);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(470, 480);
		((Control)this).MinimumSize = new Size(470, 480);
		((Control)this).Name = "ToolsDiskToIsoZso";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "ToolsDiskToIsoZso";
		((Form)this).Load += ToolsDiskToIsoZso_Load;
		((Control)GbOutput).ResumeLayout(false);
		((Control)GbOutput).PerformLayout();
		((Control)GbInput).ResumeLayout(false);
		((Control)GbInput).PerformLayout();
		((Control)this).ResumeLayout(false);
	}
}
