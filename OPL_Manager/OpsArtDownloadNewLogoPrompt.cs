using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OplManagerService;

namespace OPL_Manager;

public class OpsArtDownloadNewLogoPrompt : BaseForm
{
	public byte[] imageBytes;

	private string url;

	private GameInfo GAME;

	private IContainer components;

	internal Button B_Report;

	internal Button B_YES;

	internal Button B_NO;

	internal PictureBox PB_Logo;

	public OpsArtDownloadNewLogoPrompt()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _Game, string _url)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		GAME = _Game;
		url = _url;
		imageBytes = CommonFuncs.HttpGetImgToByteArray(url);
		PB_Logo.Image = CommonFuncs.ByteArrayToImage(imageBytes);
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

	private void B_Report_Click(object sender, EventArgs e)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		OpsArtDownloadReport opsArtDownloadReport = new OpsArtDownloadReport();
		try
		{
			opsArtDownloadReport.ShowDialog(GAME, ArtType.LGO, new List<string> { url }, 0);
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
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsArtDownloadNewLogoPrompt));
		B_Report = new Button();
		B_YES = new Button();
		B_NO = new Button();
		PB_Logo = new PictureBox();
		((ISupportInitialize)PB_Logo).BeginInit();
		((Control)this).SuspendLayout();
		((Control)B_Report).Location = new Point(187, 143);
		((Control)B_Report).Name = "B_Report";
		((Control)B_Report).Size = new Size(125, 27);
		((Control)B_Report).TabIndex = 25;
		((Control)B_Report).Text = "Report Logo";
		((ButtonBase)B_Report).UseVisualStyleBackColor = true;
		((Control)B_Report).Click += B_Report_Click;
		((Control)B_YES).BackgroundImage = (Image)componentResourceManager.GetObject("B_YES.BackgroundImage");
		((Control)B_YES).BackgroundImageLayout = (ImageLayout)4;
		((Control)B_YES).Location = new Point(12, 143);
		((Control)B_YES).Name = "B_YES";
		((Control)B_YES).Size = new Size(50, 50);
		((Control)B_YES).TabIndex = 22;
		((Control)B_YES).Click += OK_Button_Click;
		((Control)B_NO).BackgroundImage = (Image)componentResourceManager.GetObject("B_NO.BackgroundImage");
		((Control)B_NO).BackgroundImageLayout = (ImageLayout)4;
		B_NO.DialogResult = (DialogResult)2;
		((Control)B_NO).Location = new Point(68, 143);
		((Control)B_NO).Name = "B_NO";
		((Control)B_NO).Size = new Size(50, 50);
		((Control)B_NO).TabIndex = 23;
		((Control)B_NO).Click += Cancel_Button_Click;
		((Control)PB_Logo).BackgroundImageLayout = (ImageLayout)4;
		((Control)PB_Logo).Location = new Point(12, 12);
		((Control)PB_Logo).Name = "PB_Logo";
		((Control)PB_Logo).Size = new Size(300, 125);
		PB_Logo.SizeMode = (PictureBoxSizeMode)4;
		PB_Logo.TabIndex = 24;
		PB_Logo.TabStop = false;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(324, 206);
		((Control)this).Controls.Add((Control)(object)B_Report);
		((Control)this).Controls.Add((Control)(object)B_YES);
		((Control)this).Controls.Add((Control)(object)B_NO);
		((Control)this).Controls.Add((Control)(object)PB_Logo);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(340, 245);
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(340, 245);
		((Control)this).Name = "OpsArtDownloadNewLogoPrompt";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Choose Logo";
		((ISupportInitialize)PB_Logo).EndInit();
		((Control)this).ResumeLayout(false);
	}
}
