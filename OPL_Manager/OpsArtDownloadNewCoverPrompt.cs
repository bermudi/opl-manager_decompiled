using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OplManagerService;

namespace OPL_Manager;

public class OpsArtDownloadNewCoverPrompt : BaseForm
{
	public byte[] imageBytes;

	private string url;

	private GameInfo GAME;

	private IContainer components;

	internal Button B_YES;

	internal Button B_NO;

	internal PictureBox PictureBox1;

	internal Button Button1;

	public OpsArtDownloadNewCoverPrompt()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _game, string _url)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		GAME = _game;
		url = _url;
		imageBytes = CommonFuncs.HttpGetImgToByteArray(url);
		PictureBox1.Image = CommonFuncs.ByteArrayToImage(imageBytes);
		return ((Form)this).ShowDialog();
	}

	private void OK_Button_Click(object sender, EventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)6;
		((Form)this).Close();
	}

	private void Cancel_Button_Click(object sender, EventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)7;
		((Form)this).Close();
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		OpsArtDownloadReport opsArtDownloadReport = new OpsArtDownloadReport();
		try
		{
			opsArtDownloadReport.ShowDialog(GAME, ArtType.COV, new List<string> { url }, 0);
		}
		finally
		{
			((IDisposable)opsArtDownloadReport)?.Dispose();
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
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Expected O, but got Unknown
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsArtDownloadNewCoverPrompt));
		B_YES = new Button();
		B_NO = new Button();
		PictureBox1 = new PictureBox();
		Button1 = new Button();
		((ISupportInitialize)PictureBox1).BeginInit();
		((Control)this).SuspendLayout();
		((Control)B_YES).BackgroundImage = (Image)componentResourceManager.GetObject("B_YES.BackgroundImage");
		((Control)B_YES).BackgroundImageLayout = (ImageLayout)4;
		((Control)B_YES).Location = new Point(12, 218);
		((Control)B_YES).Name = "B_YES";
		((Control)B_YES).Size = new Size(50, 50);
		((Control)B_YES).TabIndex = 0;
		((Control)B_YES).Click += OK_Button_Click;
		((Control)B_NO).BackgroundImage = (Image)componentResourceManager.GetObject("B_NO.BackgroundImage");
		((Control)B_NO).BackgroundImageLayout = (ImageLayout)4;
		B_NO.DialogResult = (DialogResult)2;
		((Control)B_NO).Location = new Point(102, 218);
		((Control)B_NO).Name = "B_NO";
		((Control)B_NO).Size = new Size(50, 50);
		((Control)B_NO).TabIndex = 1;
		((Control)B_NO).Click += Cancel_Button_Click;
		((Control)PictureBox1).BackgroundImageLayout = (ImageLayout)4;
		((Control)PictureBox1).Location = new Point(12, 12);
		((Control)PictureBox1).Name = "PictureBox1";
		((Control)PictureBox1).Size = new Size(140, 200);
		PictureBox1.SizeMode = (PictureBoxSizeMode)4;
		PictureBox1.TabIndex = 1;
		PictureBox1.TabStop = false;
		((Control)Button1).Location = new Point(12, 274);
		((Control)Button1).Name = "Button1";
		((Control)Button1).Size = new Size(140, 27);
		((Control)Button1).TabIndex = 21;
		((Control)Button1).Text = "Report Cover";
		((ButtonBase)Button1).UseVisualStyleBackColor = true;
		((Control)Button1).Click += Button1_Click;
		((Form)this).AcceptButton = (IButtonControl)(object)B_YES;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).CancelButton = (IButtonControl)(object)B_NO;
		((Form)this).ClientSize = new Size(159, 302);
		((Control)this).Controls.Add((Control)(object)Button1);
		((Control)this).Controls.Add((Control)(object)B_YES);
		((Control)this).Controls.Add((Control)(object)B_NO);
		((Control)this).Controls.Add((Control)(object)PictureBox1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(175, 340);
		((Control)this).Name = "OpsArtDownloadNewCoverPrompt";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Choose Cover";
		((ISupportInitialize)PictureBox1).EndInit();
		((Control)this).ResumeLayout(false);
	}
}
