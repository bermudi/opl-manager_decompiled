using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class OpsGameHash : BaseForm
{
	private IContainer components;

	internal BackgroundWorker BackgroundWorker1;

	internal ProgressBar ProgressBar1;

	internal Button B_Start;

	internal TextBox TB_File;

	internal Button B_Stop;

	internal Label L_Progress;

	internal TextBox TB_ID;

	internal TextBox TB_MD5;

	internal Label Label1;

	internal LinkLabel Link_RedumpMD5;

	internal LinkLabel Link_RedumpID;

	public OpsGameHash()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(string strTextBox, string ID)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		((Control)TB_File).Text = strTextBox;
		((Control)TB_ID).Text = ID;
		((Control)L_Progress).Text = Translated.Global_String_Progress + " 0%";
		((Control)B_Start).Text = Translated.GLOBAL_BUTTON_START;
		((Control)B_Stop).Text = Translated.GLOBAL_BUTTON_STOP;
		return ((Form)this).ShowDialog();
	}

	private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
		string? path = e.Argument.ToString();
		byte[] array = new byte[4096];
		long num = 0L;
		int num2 = 0;
		using Stream stream = CommonFuncs.OpenIsoOrZsoStream(path);
		long length = stream.Length;
		using HashAlgorithm hashAlgorithm = MD5.Create();
		int num3;
		do
		{
			if (BackgroundWorker1.CancellationPending)
			{
				return;
			}
			num3 = stream.Read(array, 0, array.Length);
			num += num3;
			hashAlgorithm.TransformBlock(array, 0, num3, array, 0);
			int num4 = (int)Math.Round((double)num / (double)length * 100.0);
			if (num4 != num2)
			{
				BackgroundWorker1.ReportProgress(num4);
				num2 = num4;
			}
		}
		while (num3 != 0);
		hashAlgorithm.TransformFinalBlock(array, 0, 0);
		e.Result = MakeHashString(hashAlgorithm.Hash);
	}

	private static string MakeHashString(byte[] hashBytes)
	{
		StringBuilder stringBuilder = new StringBuilder(32);
		foreach (byte b in hashBytes)
		{
			stringBuilder.Append(b.ToString("X2").ToLower());
		}
		return stringBuilder.ToString();
	}

	private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		ProgressBar1.Value = e.ProgressPercentage;
		string text = Translated.Global_String_Progress + e.ProgressPercentage + "%";
		((Control)L_Progress).Text = text;
		((Control)ProgressBar1).Update();
		((Control)L_Progress).Update();
	}

	private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (e == null || e.Result == null)
		{
			MessageBox.Show(Translated.Global_String_OperationCanceled, Translated.Global_Information);
		}
		else
		{
			((Control)TB_MD5).Text = e.Result.ToString();
			((Control)Link_RedumpMD5).Visible = true;
			((Control)Link_RedumpMD5).Enabled = true;
		}
		ProgressBar1.Value = 0;
		((Control)L_Progress).Text = Translated.Global_String_Progress + " 0%";
		((Control)B_Start).Enabled = true;
		((Control)B_Stop).Enabled = false;
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		((Control)B_Start).Enabled = false;
		((Control)B_Stop).Enabled = true;
		BackgroundWorker1.RunWorkerAsync(((Control)TB_File).Text);
	}

	private void Button2_Click(object sender, EventArgs e)
	{
		BackgroundWorker1.CancelAsync();
	}

	private void Link_Redump_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		CommonFuncs.OpenURL("http://redump.org/discs/quicksearch/" + ((Control)TB_MD5).Text);
	}

	private void Link_RedumpID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		CommonFuncs.OpenURL("http://redump.org/discs/quicksearch/" + ((Control)TB_ID).Text.Replace("_", "-").Replace(".", ""));
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
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Expected O, but got Unknown
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Expected O, but got Unknown
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Expected O, but got Unknown
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsGameHash));
		BackgroundWorker1 = new BackgroundWorker();
		ProgressBar1 = new ProgressBar();
		B_Start = new Button();
		TB_File = new TextBox();
		B_Stop = new Button();
		L_Progress = new Label();
		TB_ID = new TextBox();
		TB_MD5 = new TextBox();
		Label1 = new Label();
		Link_RedumpMD5 = new LinkLabel();
		Link_RedumpID = new LinkLabel();
		((Control)this).SuspendLayout();
		BackgroundWorker1.WorkerReportsProgress = true;
		BackgroundWorker1.WorkerSupportsCancellation = true;
		BackgroundWorker1.DoWork += backgroundWorker1_DoWork;
		BackgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
		BackgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
		((Control)ProgressBar1).Location = new Point(12, 64);
		((Control)ProgressBar1).Name = "ProgressBar1";
		((Control)ProgressBar1).Size = new Size(304, 23);
		((Control)ProgressBar1).TabIndex = 0;
		((Control)B_Start).Location = new Point(163, 37);
		((Control)B_Start).Name = "B_Start";
		((Control)B_Start).Size = new Size(75, 23);
		((Control)B_Start).TabIndex = 1;
		((Control)B_Start).Text = "Start";
		((ButtonBase)B_Start).UseVisualStyleBackColor = true;
		((Control)B_Start).Click += Button1_Click;
		((Control)TB_File).Location = new Point(12, 12);
		((Control)TB_File).Name = "TB_File";
		((TextBoxBase)TB_File).ReadOnly = true;
		((Control)TB_File).Size = new Size(304, 20);
		((Control)TB_File).TabIndex = 2;
		((Control)B_Stop).Enabled = false;
		((Control)B_Stop).Location = new Point(241, 37);
		((Control)B_Stop).Name = "B_Stop";
		((Control)B_Stop).Size = new Size(75, 23);
		((Control)B_Stop).TabIndex = 3;
		((Control)B_Stop).Text = "Stop";
		((ButtonBase)B_Stop).UseVisualStyleBackColor = true;
		((Control)B_Stop).Click += Button2_Click;
		((Control)L_Progress).Location = new Point(12, 90);
		((Control)L_Progress).Name = "L_Progress";
		((Control)L_Progress).Size = new Size(304, 13);
		((Control)L_Progress).TabIndex = 4;
		((Control)L_Progress).Text = "Progress: 0%";
		L_Progress.TextAlign = (ContentAlignment)32;
		((Control)TB_ID).Location = new Point(12, 38);
		((Control)TB_ID).Name = "TB_ID";
		((TextBoxBase)TB_ID).ReadOnly = true;
		((Control)TB_ID).Size = new Size(145, 20);
		((Control)TB_ID).TabIndex = 5;
		((Control)TB_MD5).Location = new Point(51, 108);
		((Control)TB_MD5).Name = "TB_MD5";
		((TextBoxBase)TB_MD5).ReadOnly = true;
		((Control)TB_MD5).Size = new Size(265, 20);
		((Control)TB_MD5).TabIndex = 6;
		((Control)Label1).AutoSize = true;
		((Control)Label1).Location = new Point(12, 111);
		((Control)Label1).Name = "Label1";
		((Control)Label1).Size = new Size(33, 13);
		((Control)Label1).TabIndex = 7;
		((Control)Label1).Text = "MD5:";
		((Control)Link_RedumpMD5).Enabled = false;
		((Control)Link_RedumpMD5).Location = new Point(163, 131);
		((Control)Link_RedumpMD5).Name = "Link_RedumpMD5";
		((Control)Link_RedumpMD5).Size = new Size(75, 23);
		((Control)Link_RedumpMD5).TabIndex = 8;
		Link_RedumpMD5.TabStop = true;
		((Control)Link_RedumpMD5).Text = "Redump MD5";
		((Label)Link_RedumpMD5).TextAlign = (ContentAlignment)64;
		((Control)Link_RedumpMD5).Visible = false;
		Link_RedumpMD5.LinkClicked += new LinkLabelLinkClickedEventHandler(Link_Redump_LinkClicked);
		((Control)Link_RedumpID).Location = new Point(244, 131);
		((Control)Link_RedumpID).Name = "Link_RedumpID";
		((Control)Link_RedumpID).Size = new Size(72, 23);
		((Control)Link_RedumpID).TabIndex = 9;
		Link_RedumpID.TabStop = true;
		((Control)Link_RedumpID).Text = "Redump ID";
		((Label)Link_RedumpID).TextAlign = (ContentAlignment)64;
		Link_RedumpID.LinkClicked += new LinkLabelLinkClickedEventHandler(Link_RedumpID_LinkClicked);
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(328, 154);
		((Control)this).Controls.Add((Control)(object)Link_RedumpID);
		((Control)this).Controls.Add((Control)(object)Link_RedumpMD5);
		((Control)this).Controls.Add((Control)(object)Label1);
		((Control)this).Controls.Add((Control)(object)TB_MD5);
		((Control)this).Controls.Add((Control)(object)TB_ID);
		((Control)this).Controls.Add((Control)(object)L_Progress);
		((Control)this).Controls.Add((Control)(object)B_Stop);
		((Control)this).Controls.Add((Control)(object)TB_File);
		((Control)this).Controls.Add((Control)(object)B_Start);
		((Control)this).Controls.Add((Control)(object)ProgressBar1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "OpsGameHash";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "MD5";
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
