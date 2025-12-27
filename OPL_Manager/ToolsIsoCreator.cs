using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using GomuLibrary.IO.DiscImage;
using GomuLibrary.IO.DiscImage.Type;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class ToolsIsoCreator : BaseForm
{
	public delegate void ProgressionEventHandler(Iso9660CreatorEventArgs e);

	private Iso9660Creator iso = new Iso9660Creator();

	private Iso9660Conv isoConv = new Iso9660Conv();

	private DateTime lastTime;

	private long lastBytes;

	private bool isoAborted;

	private IContainer components;

	internal ComboBox cmbDrive;

	internal ProgressBar prgCreate;

	internal TextBox tbxSaveas;

	internal Button b_Start;

	internal Button b_Cancel;

	internal Label Label1;

	internal Label l_Progress;

	internal Label l_speed;

	internal Label l_DiskName;

	internal RadioButton rb_CD;

	internal RadioButton rb_FromFile;

	internal GroupBox gb_FromCD;

	internal GroupBox gb_FromFile;

	internal Button b_OpenFile;

	internal TextBox tb_ImageSrc;

	internal GroupBox GB_Mode;

	internal Button b_BrowseSave;

	internal Label l_DiskSize;

	internal Button Button1;

	internal Label Label2;

	internal Label Label3;

	internal TextBox tb_ID;

	internal Label L_Title;

	internal TextBox tb_Title;

	internal Label l_TimeLeft;

	public ToolsIsoCreator()
	{
		InitializeComponent();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.ToolsIsoCreator_Title;
		((Control)b_Start).Text = Translated.GLOBAL_BUTTON_START;
		((Control)b_Cancel).Text = Translated.GLOBAL_BUTTON_CANCEL;
		((Control)b_BrowseSave).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)gb_FromFile).Text = Translated.ToolsIsoCreator_FromImageFile;
		((Control)GB_Mode).Text = Translated.GLOBAL_STRING_MODE;
		((Control)b_OpenFile).Text = Translated.ToolsIsoCreator_OpenFile;
		((Control)L_Title).Text = Translated.ToolsIsoCreator_GameTitle;
		((Control)l_Progress).BackColor = Color.Transparent;
		((Control)gb_FromCD).Enabled = rb_CD.Checked;
		((Control)gb_FromFile).Enabled = rb_FromFile.Checked;
		((Control)b_Start).Enabled = true;
		((Control)b_Cancel).Enabled = false;
		ClearInfo();
		iso.Progression += iso_Progression;
		iso.Terminate += iso_Terminate;
		iso.Aborted += iso_Aborted;
		isoConv.Progression += iso_Progression;
		isoConv.Terminate += iso_Terminate;
		refreshDiscList();
	}

	private void refreshDiscList()
	{
		cmbDrive.Items.Clear();
		DriveInfo[] drives = DriveInfo.GetDrives();
		int selectedIndex = 0;
		int i = 0;
		for (int num = drives.Length - 1; i <= num; i++)
		{
			if (drives[i].DriveType == DriveType.CDRom)
			{
				cmbDrive.Items.Add((object)drives[i].Name);
				if (drives[i].IsReady)
				{
					selectedIndex = cmbDrive.Items.Count - 1;
				}
			}
		}
		if (cmbDrive.Items.Count > 0)
		{
			((ListControl)cmbDrive).SelectedIndex = selectedIndex;
		}
	}

	private void iso_Progression(object sender, Iso9660CreatorEventArgs e)
	{
		if (((Control)this).InvokeRequired)
		{
			ProgressionEventHandler progressionEventHandler = DisplayProgress;
			((Control)this).Invoke((Delegate)progressionEventHandler, new object[1] { e });
		}
	}

	private void iso_Terminate(object sender, EventArgs e)
	{
		if (((Control)this).InvokeRequired)
		{
			EventHandler eventHandler = DisplayTerminate;
			((Control)this).Invoke((Delegate)eventHandler, new object[1] { e });
		}
	}

	private void iso_Aborted(object sender, Iso9660CreatorEventArgs e)
	{
		if (((Control)this).InvokeRequired)
		{
			EventHandler eventHandler = DisplayAbort;
			((Control)this).Invoke((Delegate)eventHandler, new object[1] { e });
		}
	}

	private void DisplayAbort(object sender, EventArgs e)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		isoAborted = true;
		MessageBox.Show(Translated.ToolsIsoCreator_IsoAborted, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		((Control)b_Start).Enabled = true;
		((Control)b_Cancel).Enabled = false;
	}

	private void DisplayTerminate(object sender, EventArgs e)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		prgCreate.Value = 0;
		((Control)l_Progress).Text = "0 / 0";
		((Control)l_speed).Text = "0 KB/s";
		((Control)l_TimeLeft).Text = "00:00";
		if (!isoAborted)
		{
			MessageBox.Show(Translated.ToolsIsoCreator_IsoSuccess, Translated.GlobalString_OperationComplete, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
		((Control)b_Start).Enabled = true;
		((Control)b_Cancel).Enabled = false;
	}

	private void DisplayProgress(Iso9660CreatorEventArgs arg)
	{
		prgCreate.Maximum = Convert.ToInt32((double)arg.DiscLength / 10.0);
		prgCreate.Value = Convert.ToInt32((double)arg.BytesWritted / 10.0);
		((Control)l_Progress).Text = Math.Round((double)arg.BytesWritted / 1024.0 / 1024.0, 0) + "MB / " + Math.Round((double)arg.DiscLength / 1024.0 / 1024.0) + "MB";
		if ((DateTime.Now - lastTime).TotalSeconds >= 2.0)
		{
			((Control)l_speed).Text = Math.Round((double)(arg.BytesWritted - lastBytes) / (DateTime.Now - lastTime).TotalSeconds / 1024.0 / 1024.0, 2) + " MB/s";
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)arg.DiscLength * (DateTime.Now - lastTime).TotalSeconds / (double)(arg.BytesWritted - lastBytes));
			((Control)l_TimeLeft).Text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
			lastBytes = arg.BytesWritted;
			lastTime = DateTime.Now;
		}
	}

	private void b_Start_Click(object sender, EventArgs e)
	{
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		isoAborted = false;
		if (rb_CD.Checked && !new DriveInfo(cmbDrive.SelectedItem.ToString()).IsReady)
		{
			MessageBox.Show(Translated.ToolsIsoCreator_DriveNotReady, Translated.Global_Error);
			return;
		}
		if (string.IsNullOrEmpty(((Control)tbxSaveas).Text))
		{
			MessageBox.Show(Translated.ToolsIsoCreator_NoSaveFileSelected, Translated.Global_Error);
			return;
		}
		if (!string.IsNullOrEmpty(((Control)tbxSaveas).Text) && File.Exists(((Control)tbxSaveas).Text))
		{
			MessageBox.Show(Translated.GlobalFileExists, Translated.Global_Error);
			return;
		}
		if (rb_FromFile.Checked)
		{
			if (string.IsNullOrEmpty(((Control)tb_ImageSrc).Text))
			{
				MessageBox.Show(Translated.ToolsIsoCreator_NoSourceFileSpecified, Translated.Global_Error);
				return;
			}
			if (!File.Exists(((Control)tb_ImageSrc).Text))
			{
				MessageBox.Show(Translated.ToolsIsoCreator_SourceFileNotExist, Translated.Global_Error);
				return;
			}
		}
		((Control)b_Start).Enabled = false;
		if (rb_FromFile.Checked)
		{
			((Control)b_Cancel).Enabled = false;
		}
		else if (rb_CD.Checked)
		{
			((Control)b_Cancel).Enabled = true;
		}
		lastTime = DateTime.Now;
		lastBytes = 0L;
		((Control)l_speed).Text = "0 KB/s";
		if (rb_CD.Checked)
		{
			try
			{
				iso.CreateImage(cmbDrive.SelectedItem.ToString(), ((Control)tbxSaveas).Text, newThread: true);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}
		if (rb_FromFile.Checked)
		{
			new Thread(ThreadConvertISO).Start();
		}
	}

	private void ThreadConvertISO()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			isoConv.Convert(((Control)tbxSaveas).Text);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		finally
		{
			isoConv.Close();
		}
	}

	private string GenerateFullIsoPath()
	{
		string text = "";
		if (!OplmSettings.ReadBool("ISO_USE_OLD_NAMING_FORMAT"))
		{
			if (!string.IsNullOrEmpty(((Control)tb_Title).Text))
			{
				text = ((Control)tb_Title).Text + ".iso";
			}
			else if (!string.IsNullOrEmpty(((Control)l_DiskName).Text))
			{
				text = CommonFuncs.SanitizeGameTitle(((Control)l_DiskName).Text) + ".iso";
			}
		}
		if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(((Control)tb_ID).Text))
		{
			text = ((Control)tb_ID).Text;
			text = ((!string.IsNullOrEmpty(((Control)tb_Title).Text)) ? (text + "." + ((Control)tb_Title).Text + ".iso") : (string.IsNullOrEmpty(((Control)l_DiskName).Text) ? (text + ".GameTitle.iso") : (text + "." + CommonFuncs.SanitizeGameTitle(((Control)l_DiskName).Text) + ".iso")));
		}
		if (!string.IsNullOrEmpty(text))
		{
			string path = OplFolders.DVD;
			if (rb_CD.Checked)
			{
				DriveInfo diskInfo = GetDiskInfo();
				if (diskInfo != null && diskInfo.IsReady && diskInfo.TotalSize < 700000000)
				{
					path = OplFolders.CD;
				}
			}
			text = Path.Combine(path, text);
		}
		return text;
	}

	private DriveInfo GetDiskInfo()
	{
		if (cmbDrive.SelectedItem == null)
		{
			return null;
		}
		try
		{
			return new DriveInfo(cmbDrive.SelectedItem.ToString());
		}
		catch (Exception)
		{
		}
		return null;
	}

	private void b_BrowseSave_Clicked(object sender, EventArgs e)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Invalid comparison between Unknown and I4
		SaveFileDialog val = new SaveFileDialog();
		try
		{
			string text = GenerateFullIsoPath();
			if (!string.IsNullOrEmpty(text))
			{
				((FileDialog)val).InitialDirectory = Path.GetDirectoryName(text);
				((FileDialog)val).FileName = Path.GetFileName(text);
			}
			else
			{
				((FileDialog)val).InitialDirectory = OplFolders.Main;
			}
			((FileDialog)val).CheckPathExists = true;
			((FileDialog)val).Filter = "Iso File (.iso)|*.iso";
			((FileDialog)val).DefaultExt = ".iso";
			((FileDialog)val).AddExtension = true;
			if ((int)((CommonDialog)val).ShowDialog() == 1)
			{
				((Control)tbxSaveas).Text = ((FileDialog)val).FileName;
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private void btnAbort_Click(object sender, EventArgs e)
	{
		if (rb_CD.Checked)
		{
			iso.Abort();
		}
	}

	private void cmbDrive_SelectedIndexChanged(object sender, EventArgs e)
	{
		ClearInfo();
		DriveInfo diskInfo = GetDiskInfo();
		if (diskInfo != null && diskInfo.IsReady)
		{
			((Control)l_DiskName).Text = diskInfo.VolumeLabel;
			((Control)l_DiskSize).Text = Math.Round((double)diskInfo.TotalSize / 1024.0 / 1024.0, 0) + " MB";
			string path = cmbDrive.SelectedItem.ToString() + "SYSTEM.CNF";
			if (File.Exists(path))
			{
				Match match = new Regex("[A-Z][A-Z][A-Z][A-Z]_[0-9][0-9][0-9]\\.[0-9][0-9]").Match(File.ReadAllText(path));
				if (match.Success)
				{
					((Control)tb_ID).Text = match.Value;
				}
			}
		}
		else
		{
			ClearInfo();
		}
	}

	private void ClearInfo()
	{
		((Control)tbxSaveas).Text = "";
		((Control)tb_Title).Text = "";
		((Control)tb_ID).Text = "";
		((Control)tb_ImageSrc).Text = "";
		((Control)l_DiskName).Text = Translated.ToolsIsoCreator_NoDisc;
		((Control)l_DiskSize).Text = Translated.ToolsIsoCreator_NoDisc;
	}

	private void rb_CD_CheckedChanged(object sender, EventArgs e)
	{
		((Control)gb_FromCD).Enabled = rb_CD.Checked;
		ClearInfo();
		cmbDrive_SelectedIndexChanged(null, null);
	}

	private void rb_ConvertFile_CheckedChanged(object sender, EventArgs e)
	{
		((Control)gb_FromFile).Enabled = rb_FromFile.Checked;
		ClearInfo();
	}

	private void b_OpenFile_Click(object sender, EventArgs e)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Invalid comparison between Unknown and I4
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		OpenFileDialog val = new OpenFileDialog();
		try
		{
			((FileDialog)val).AddExtension = true;
			((FileDialog)val).CheckFileExists = true;
			((FileDialog)val).CheckPathExists = true;
			((FileDialog)val).Filter = "Bin image file (.bin)|*.bin|Nero image file (.nrg)|*.nrg|Alcohol image file (.mdf)|*.mdf|CloneCd image file (.img)|*.img|DiscJuggler file (.cdi)|*.cdi";
			if ((int)((CommonDialog)val).ShowDialog() == 1)
			{
				((Control)tb_ImageSrc).Text = ((FileDialog)val).FileName;
				switch (((FileDialog)val).FilterIndex)
				{
				case 1:
					isoConv.DiscImage = new BinType(((FileDialog)val).FileName);
					break;
				case 2:
					isoConv.DiscImage = new NrgType(((FileDialog)val).FileName);
					break;
				case 3:
					isoConv.DiscImage = new MdfType(((FileDialog)val).FileName);
					break;
				case 4:
					isoConv.DiscImage = new CcdType(((FileDialog)val).FileName);
					break;
				case 5:
					isoConv.DiscImage = new CdiType(((FileDialog)val).FileName);
					break;
				default:
					MessageBox.Show(Translated.ToolsIsoCreator_InvalidInputFile, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
					break;
				}
				((Control)tb_ID).Text = CommonFuncs.BinGetID(((FileDialog)val).FileName, 100);
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		refreshDiscList();
	}

	private void tb_ID_TextChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(((Control)tb_ID).Text))
		{
			DoGetGameNameByID();
			((Control)tbxSaveas).Text = GenerateFullIsoPath();
		}
	}

	private async void DoGetGameNameByID()
	{
		try
		{
			GetGameNameByIdResponse getGameNameByIdResponse = await CommonFuncs.SoapAPI.GetGameNameByIdAsync(GameType.PS2, ((Control)tb_ID).Text);
			if (getGameNameByIdResponse != null)
			{
				((Control)tb_Title).Text = CommonFuncs.SanitizeGameTitle(getGameNameByIdResponse.Body.GetGameNameByIdResult);
			}
		}
		catch (Exception)
		{
			MessageBox.Show(Translated.Global_ServerFetchError, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
		}
	}

	private void tb_Title_TextChanged(object sender, EventArgs e)
	{
		((Control)tbxSaveas).Text = GenerateFullIsoPath();
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
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Expected O, but got Unknown
		//IL_0e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e10: Expected O, but got Unknown
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e26: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsIsoCreator));
		cmbDrive = new ComboBox();
		prgCreate = new ProgressBar();
		tbxSaveas = new TextBox();
		b_Start = new Button();
		b_Cancel = new Button();
		Label1 = new Label();
		l_Progress = new Label();
		l_speed = new Label();
		l_DiskName = new Label();
		rb_CD = new RadioButton();
		rb_FromFile = new RadioButton();
		gb_FromCD = new GroupBox();
		Button1 = new Button();
		l_DiskSize = new Label();
		gb_FromFile = new GroupBox();
		b_OpenFile = new Button();
		tb_ImageSrc = new TextBox();
		GB_Mode = new GroupBox();
		b_BrowseSave = new Button();
		Label2 = new Label();
		Label3 = new Label();
		tb_ID = new TextBox();
		L_Title = new Label();
		tb_Title = new TextBox();
		l_TimeLeft = new Label();
		((Control)gb_FromCD).SuspendLayout();
		((Control)gb_FromFile).SuspendLayout();
		((Control)GB_Mode).SuspendLayout();
		((Control)this).SuspendLayout();
		cmbDrive.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)cmbDrive).FormattingEnabled = true;
		((Control)cmbDrive).Location = new Point(44, 19);
		((Control)cmbDrive).Name = "cmbDrive";
		((Control)cmbDrive).Size = new Size(71, 21);
		((Control)cmbDrive).TabIndex = 0;
		cmbDrive.SelectedIndexChanged += cmbDrive_SelectedIndexChanged;
		((Control)prgCreate).Location = new Point(9, 218);
		((Control)prgCreate).Name = "prgCreate";
		((Control)prgCreate).Size = new Size(373, 23);
		((Control)prgCreate).TabIndex = 1;
		((Control)tbxSaveas).Location = new Point(12, 191);
		((Control)tbxSaveas).Name = "tbxSaveas";
		((TextBoxBase)tbxSaveas).ReadOnly = true;
		((Control)tbxSaveas).Size = new Size(286, 20);
		((Control)tbxSaveas).TabIndex = 2;
		((Control)b_Start).Location = new Point(216, 247);
		((Control)b_Start).Name = "b_Start";
		((Control)b_Start).Size = new Size(75, 23);
		((Control)b_Start).TabIndex = 3;
		((Control)b_Start).Text = "Start";
		((ButtonBase)b_Start).UseVisualStyleBackColor = true;
		((Control)b_Start).Click += b_Start_Click;
		((Control)b_Cancel).Location = new Point(307, 247);
		((Control)b_Cancel).Name = "b_Cancel";
		((Control)b_Cancel).Size = new Size(75, 23);
		((Control)b_Cancel).TabIndex = 5;
		((Control)b_Cancel).Text = "Cancel";
		((ButtonBase)b_Cancel).UseVisualStyleBackColor = true;
		((Control)b_Cancel).Click += btnAbort_Click;
		((Control)Label1).AutoSize = true;
		((Control)Label1).Location = new Point(3, 23);
		((Control)Label1).Name = "Label1";
		((Control)Label1).Size = new Size(35, 13);
		((Control)Label1).TabIndex = 6;
		((Control)Label1).Text = "Drive:";
		((Control)l_Progress).Location = new Point(10, 247);
		((Control)l_Progress).Name = "l_Progress";
		((Control)l_Progress).Size = new Size(200, 13);
		((Control)l_Progress).TabIndex = 7;
		((Control)l_Progress).Text = "0 / 0";
		l_Progress.TextAlign = (ContentAlignment)16;
		((Control)l_speed).Location = new Point(10, 265);
		((Control)l_speed).Name = "l_speed";
		((Control)l_speed).Size = new Size(77, 13);
		((Control)l_speed).TabIndex = 8;
		((Control)l_speed).Text = "0 KB/s";
		l_speed.TextAlign = (ContentAlignment)16;
		((Control)l_DiskName).Location = new Point(6, 44);
		((Control)l_DiskName).Name = "l_DiskName";
		((Control)l_DiskName).Size = new Size(152, 22);
		((Control)l_DiskName).TabIndex = 9;
		((Control)l_DiskName).Text = "No Disc";
		l_DiskName.TextAlign = (ContentAlignment)32;
		((Control)rb_CD).AutoSize = true;
		rb_CD.Checked = true;
		((Control)rb_CD).Location = new Point(9, 19);
		((Control)rb_CD).Name = "rb_CD";
		((Control)rb_CD).Size = new Size(101, 17);
		((Control)rb_CD).TabIndex = 10;
		rb_CD.TabStop = true;
		((Control)rb_CD).Text = "CD/DVD -> ISO";
		((ButtonBase)rb_CD).UseVisualStyleBackColor = true;
		rb_CD.CheckedChanged += rb_CD_CheckedChanged;
		((Control)rb_FromFile).AutoSize = true;
		((Control)rb_FromFile).Location = new Point(204, 19);
		((Control)rb_FromFile).Name = "rb_FromFile";
		((Control)rb_FromFile).Size = new Size(154, 17);
		((Control)rb_FromFile).TabIndex = 11;
		((Control)rb_FromFile).Text = "bin/nrg/mdf/img/cdi -> ISO";
		((ButtonBase)rb_FromFile).UseVisualStyleBackColor = true;
		rb_FromFile.CheckedChanged += rb_ConvertFile_CheckedChanged;
		((Control)gb_FromCD).Controls.Add((Control)(object)Button1);
		((Control)gb_FromCD).Controls.Add((Control)(object)l_DiskSize);
		((Control)gb_FromCD).Controls.Add((Control)(object)Label1);
		((Control)gb_FromCD).Controls.Add((Control)(object)l_DiskName);
		((Control)gb_FromCD).Controls.Add((Control)(object)cmbDrive);
		((Control)gb_FromCD).Location = new Point(12, 63);
		((Control)gb_FromCD).Name = "gb_FromCD";
		((Control)gb_FromCD).Size = new Size(164, 96);
		((Control)gb_FromCD).TabIndex = 12;
		gb_FromCD.TabStop = false;
		((Control)gb_FromCD).Text = "CD/DVD";
		((Control)Button1).BackgroundImage = (Image)componentResourceManager.GetObject("Button1.BackgroundImage");
		((Control)Button1).BackgroundImageLayout = (ImageLayout)4;
		((Control)Button1).Location = new Point(122, 19);
		((Control)Button1).Name = "Button1";
		((Control)Button1).Size = new Size(36, 21);
		((Control)Button1).TabIndex = 11;
		((ButtonBase)Button1).UseVisualStyleBackColor = true;
		((Control)Button1).Click += Button1_Click;
		((Control)l_DiskSize).Location = new Point(6, 66);
		((Control)l_DiskSize).Name = "l_DiskSize";
		((Control)l_DiskSize).Size = new Size(152, 22);
		((Control)l_DiskSize).TabIndex = 10;
		((Control)l_DiskSize).Text = "No Disc";
		l_DiskSize.TextAlign = (ContentAlignment)32;
		((Control)gb_FromFile).Controls.Add((Control)(object)b_OpenFile);
		((Control)gb_FromFile).Controls.Add((Control)(object)tb_ImageSrc);
		((Control)gb_FromFile).Location = new Point(182, 64);
		((Control)gb_FromFile).Name = "gb_FromFile";
		((Control)gb_FromFile).Size = new Size(200, 78);
		((Control)gb_FromFile).TabIndex = 13;
		gb_FromFile.TabStop = false;
		((Control)gb_FromFile).Text = "From image file";
		((Control)b_OpenFile).Location = new Point(6, 44);
		((Control)b_OpenFile).Name = "b_OpenFile";
		((Control)b_OpenFile).Size = new Size(187, 23);
		((Control)b_OpenFile).TabIndex = 13;
		((Control)b_OpenFile).Text = "Open file";
		((ButtonBase)b_OpenFile).UseVisualStyleBackColor = true;
		((Control)b_OpenFile).Click += b_OpenFile_Click;
		((Control)tb_ImageSrc).Location = new Point(6, 19);
		((Control)tb_ImageSrc).Name = "tb_ImageSrc";
		((TextBoxBase)tb_ImageSrc).ReadOnly = true;
		((Control)tb_ImageSrc).Size = new Size(187, 20);
		((Control)tb_ImageSrc).TabIndex = 12;
		((Control)GB_Mode).Controls.Add((Control)(object)rb_FromFile);
		((Control)GB_Mode).Controls.Add((Control)(object)rb_CD);
		((Control)GB_Mode).Location = new Point(12, 11);
		((Control)GB_Mode).Name = "GB_Mode";
		((Control)GB_Mode).Size = new Size(370, 47);
		((Control)GB_Mode).TabIndex = 14;
		GB_Mode.TabStop = false;
		((Control)GB_Mode).Text = "Mode";
		((Control)b_BrowseSave).Location = new Point(301, 189);
		((Control)b_BrowseSave).Name = "b_BrowseSave";
		((Control)b_BrowseSave).Size = new Size(81, 23);
		((Control)b_BrowseSave).TabIndex = 15;
		((Control)b_BrowseSave).Text = "Browse";
		((ButtonBase)b_BrowseSave).UseVisualStyleBackColor = true;
		((Control)b_BrowseSave).Click += b_BrowseSave_Clicked;
		((Control)Label2).Location = new Point(6, 278);
		((Control)Label2).Name = "Label2";
		((Control)Label2).Size = new Size(376, 23);
		((Control)Label2).TabIndex = 16;
		((Control)Label2).Text = "Uses GomuIso9660 from http://gomuiso9660.codeplex.com";
		Label2.TextAlign = (ContentAlignment)32;
		((Control)Label3).AutoSize = true;
		((Control)Label3).Location = new Point(182, 146);
		((Control)Label3).Name = "Label3";
		((Control)Label3).Size = new Size(52, 13);
		((Control)Label3).TabIndex = 17;
		((Control)Label3).Text = "Game ID:";
		((Control)tb_ID).Location = new Point(240, 143);
		((Control)tb_ID).Name = "tb_ID";
		((TextBoxBase)tb_ID).ReadOnly = true;
		((Control)tb_ID).Size = new Size(142, 20);
		((Control)tb_ID).TabIndex = 18;
		((Control)tb_ID).TextChanged += tb_ID_TextChanged;
		((Control)L_Title).Location = new Point(12, 165);
		((Control)L_Title).Name = "L_Title";
		((Control)L_Title).Size = new Size(35, 20);
		((Control)L_Title).TabIndex = 19;
		((Control)L_Title).Text = "Title:";
		L_Title.TextAlign = (ContentAlignment)32;
		((Control)tb_Title).Location = new Point(48, 165);
		((Control)tb_Title).Name = "tb_Title";
		((TextBoxBase)tb_Title).ReadOnly = true;
		((Control)tb_Title).Size = new Size(334, 20);
		((Control)tb_Title).TabIndex = 20;
		((Control)tb_Title).TextChanged += tb_Title_TextChanged;
		((Control)l_TimeLeft).Location = new Point(148, 265);
		((Control)l_TimeLeft).Name = "l_TimeLeft";
		((Control)l_TimeLeft).Size = new Size(52, 13);
		((Control)l_TimeLeft).TabIndex = 21;
		((Control)l_TimeLeft).Text = "00:00";
		l_TimeLeft.TextAlign = (ContentAlignment)16;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(388, 307);
		((Control)this).Controls.Add((Control)(object)l_TimeLeft);
		((Control)this).Controls.Add((Control)(object)tb_Title);
		((Control)this).Controls.Add((Control)(object)L_Title);
		((Control)this).Controls.Add((Control)(object)tb_ID);
		((Control)this).Controls.Add((Control)(object)Label3);
		((Control)this).Controls.Add((Control)(object)Label2);
		((Control)this).Controls.Add((Control)(object)b_BrowseSave);
		((Control)this).Controls.Add((Control)(object)GB_Mode);
		((Control)this).Controls.Add((Control)(object)gb_FromFile);
		((Control)this).Controls.Add((Control)(object)gb_FromCD);
		((Control)this).Controls.Add((Control)(object)l_speed);
		((Control)this).Controls.Add((Control)(object)l_Progress);
		((Control)this).Controls.Add((Control)(object)b_Cancel);
		((Control)this).Controls.Add((Control)(object)b_Start);
		((Control)this).Controls.Add((Control)(object)tbxSaveas);
		((Control)this).Controls.Add((Control)(object)prgCreate);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "ToolsIsoCreator";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "CD/DVD/Image file to ISO";
		((Form)this).Load += Form1_Load;
		((Control)gb_FromCD).ResumeLayout(false);
		((Control)gb_FromCD).PerformLayout();
		((Control)gb_FromFile).ResumeLayout(false);
		((Control)gb_FromFile).PerformLayout();
		((Control)GB_Mode).ResumeLayout(false);
		((Control)GB_Mode).PerformLayout();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
