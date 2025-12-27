using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OplManagerService;

namespace OPL_Manager;

public class OpsArtDownloadNewSpinePrompt : BaseForm
{
	public byte[] imageBytes;

	private string url;

	private GameInfo GAME;

	private IContainer components;

	internal Button B_ReportSpine;

	internal Button B_YES;

	internal Button B_NO;

	internal PictureBox PB_Spine;

	public OpsArtDownloadNewSpinePrompt()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _Game, string _url)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		GAME = _Game;
		url = _url;
		imageBytes = CommonFuncs.HttpGetImgToByteArray(url);
		PB_Spine.Image = CommonFuncs.ByteArrayToImage(imageBytes);
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

	private void B_ReportSpine_Click(object sender, EventArgs e)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		OpsArtDownloadReport opsArtDownloadReport = new OpsArtDownloadReport();
		try
		{
			opsArtDownloadReport.ShowDialog(GAME, ArtType.LAB, new List<string> { url }, 0);
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
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsArtDownloadNewSpinePrompt));
		B_ReportSpine = new Button();
		B_YES = new Button();
		B_NO = new Button();
		PB_Spine = new PictureBox();
		((ISupportInitialize)PB_Spine).BeginInit();
		((Control)this).SuspendLayout();
		((Control)B_ReportSpine).Location = new Point(25, 315);
		((Control)B_ReportSpine).Name = "B_ReportSpine";
		((Control)B_ReportSpine).Size = new Size(125, 27);
		((Control)B_ReportSpine).TabIndex = 25;
		((Control)B_ReportSpine).Text = "Report Spine";
		((ButtonBase)B_ReportSpine).UseVisualStyleBackColor = true;
		((Control)B_ReportSpine).Click += B_ReportSpine_Click;
		((Control)B_YES).BackgroundImage = (Image)componentResourceManager.GetObject("B_YES.BackgroundImage");
		((Control)B_YES).BackgroundImageLayout = (ImageLayout)4;
		((Control)B_YES).Location = new Point(24, 259);
		((Control)B_YES).Name = "B_YES";
		((Control)B_YES).Size = new Size(50, 50);
		((Control)B_YES).TabIndex = 22;
		((Control)B_YES).Click += OK_Button_Click;
		((Control)B_NO).BackgroundImage = (Image)componentResourceManager.GetObject("B_NO.BackgroundImage");
		((Control)B_NO).BackgroundImageLayout = (ImageLayout)4;
		B_NO.DialogResult = (DialogResult)2;
		((Control)B_NO).Location = new Point(100, 259);
		((Control)B_NO).Name = "B_NO";
		((Control)B_NO).Size = new Size(50, 50);
		((Control)B_NO).TabIndex = 23;
		((Control)B_NO).Click += Cancel_Button_Click;
		((Control)PB_Spine).BackgroundImageLayout = (ImageLayout)4;
		((Control)PB_Spine).Location = new Point(79, 12);
		((Control)PB_Spine).Name = "PB_Spine";
		((Control)PB_Spine).Size = new Size(18, 240);
		PB_Spine.SizeMode = (PictureBoxSizeMode)4;
		PB_Spine.TabIndex = 24;
		PB_Spine.TabStop = false;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(168, 350);
		((Control)this).Controls.Add((Control)(object)B_ReportSpine);
		((Control)this).Controls.Add((Control)(object)B_YES);
		((Control)this).Controls.Add((Control)(object)B_NO);
		((Control)this).Controls.Add((Control)(object)PB_Spine);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(184, 388);
		((Control)this).Name = "OpsArtDownloadNewSpinePrompt";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Choose Spine";
		((ISupportInitialize)PB_Spine).EndInit();
		((Control)this).ResumeLayout(false);
	}
}
