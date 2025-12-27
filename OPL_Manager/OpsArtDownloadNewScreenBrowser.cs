using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class OpsArtDownloadNewScreenBrowser : BaseForm
{
	private List<string> images;

	public byte[] imgBytes;

	private GameInfo GAME;

	private IContainer components;

	internal TextBox TB_GameID;

	internal PictureBox PictureBox1;

	internal Button B_Prev;

	internal Button B_Next;

	internal Label L_Current;

	internal Button B_Select;

	internal Button B_Close;

	internal Button Button1;

	public OpsArtDownloadNewScreenBrowser()
	{
		InitializeComponent();
	}

	public OpsArtDownloadNewScreenBrowser(OPLM_Main mainF)
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _game, string screenType, ArrayOfString screens)
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		GAME = _game;
		((Control)this).Text = Translated.OpsArtDownloadScreenBrowser_Title;
		((Control)B_Select).Text = Translated.OpsArtDownloadScreenBrowser_Select;
		((Control)B_Close).Text = Translated.GLOBAL_BUTTON_CLOSE;
		((Control)TB_GameID).Text = GAME.ID + screenType;
		((Form)this).DialogResult = (DialogResult)2;
		if (screens.Count == 0)
		{
			PictureBox1.Image = null;
			((Control)B_Prev).Enabled = false;
			((Control)B_Next).Enabled = false;
			((Control)B_Select).Enabled = false;
			MessageBox.Show(Translated.OpsArtDownload_NoScrFound);
			((Form)this).Close();
		}
		else
		{
			images = new List<string>();
			images.AddRange(screens);
			((Control)L_Current).Tag = 0;
			updateCurrentImg();
		}
		return ((Form)this).ShowDialog();
	}

	public void LoadWebImageToPictureBox(PictureBox pb, string ImageURL)
	{
		pb.Image = (Image)(object)Resources.loading_anim;
		pb.Image = CommonFuncs.HttpGetImgAsImage(ImageURL);
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		if ((int)((Control)L_Current).Tag - 1 < 0)
		{
			((Control)L_Current).Tag = images.Count - 1;
		}
		else
		{
			((Control)L_Current).Tag = (int)((Control)L_Current).Tag - 1;
		}
		updateCurrentImg();
	}

	private void Button2_Click(object sender, EventArgs e)
	{
		if ((int)((Control)L_Current).Tag + 1 > images.Count - 1)
		{
			((Control)L_Current).Tag = 0;
		}
		else
		{
			((Control)L_Current).Tag = (int)((Control)L_Current).Tag + 1;
		}
		updateCurrentImg();
	}

	private void updateCurrentImg()
	{
		((Control)L_Current).Text = (int)((Control)L_Current).Tag + 1 + " / " + images.Count;
		LoadWebImageToPictureBox(PictureBox1, images[(int)((Control)L_Current).Tag]);
	}

	private void Button4_Click(object sender, EventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)2;
		((Form)this).Close();
	}

	private void Button3_Click(object sender, EventArgs e)
	{
		imgBytes = CommonFuncs.HttpGetImgToByteArray(images[(int)((Control)L_Current).Tag]);
		((Form)this).DialogResult = (DialogResult)1;
		((Form)this).Close();
	}

	private void Button1_Click_1(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		OpsArtDownloadReport opsArtDownloadReport = new OpsArtDownloadReport();
		try
		{
			opsArtDownloadReport.ShowDialog(GAME, ArtType.SCR, images, (int)((Control)L_Current).Tag);
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
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Expected O, but got Unknown
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsArtDownloadNewScreenBrowser));
		TB_GameID = new TextBox();
		PictureBox1 = new PictureBox();
		B_Prev = new Button();
		B_Next = new Button();
		L_Current = new Label();
		B_Select = new Button();
		B_Close = new Button();
		Button1 = new Button();
		((ISupportInitialize)PictureBox1).BeginInit();
		((Control)this).SuspendLayout();
		((Control)TB_GameID).Enabled = false;
		((Control)TB_GameID).Location = new Point(9, 240);
		((Control)TB_GameID).Name = "TB_GameID";
		((TextBoxBase)TB_GameID).ReadOnly = true;
		((Control)TB_GameID).Size = new Size(136, 20);
		((Control)TB_GameID).TabIndex = 3;
		TB_GameID.TextAlign = (HorizontalAlignment)2;
		PictureBox1.Image = (Image)componentResourceManager.GetObject("PictureBox1.Image");
		((Control)PictureBox1).Location = new Point(12, 12);
		((Control)PictureBox1).MaximumSize = new Size(250, 188);
		((Control)PictureBox1).MinimumSize = new Size(250, 188);
		((Control)PictureBox1).Name = "PictureBox1";
		((Control)PictureBox1).Size = new Size(250, 188);
		PictureBox1.SizeMode = (PictureBoxSizeMode)4;
		PictureBox1.TabIndex = 5;
		PictureBox1.TabStop = false;
		((Control)B_Prev).BackgroundImage = (Image)componentResourceManager.GetObject("B_Prev.BackgroundImage");
		((Control)B_Prev).BackgroundImageLayout = (ImageLayout)4;
		((Control)B_Prev).Location = new Point(9, 206);
		((Control)B_Prev).Name = "B_Prev";
		((Control)B_Prev).Size = new Size(32, 32);
		((Control)B_Prev).TabIndex = 6;
		((ButtonBase)B_Prev).UseVisualStyleBackColor = true;
		((Control)B_Prev).Click += Button1_Click;
		((Control)B_Next).BackgroundImage = (Image)componentResourceManager.GetObject("B_Next.BackgroundImage");
		((Control)B_Next).BackgroundImageLayout = (ImageLayout)4;
		((Control)B_Next).Location = new Point(113, 206);
		((Control)B_Next).Name = "B_Next";
		((Control)B_Next).Size = new Size(32, 32);
		((Control)B_Next).TabIndex = 7;
		((ButtonBase)B_Next).UseVisualStyleBackColor = true;
		((Control)B_Next).Click += Button2_Click;
		((Control)L_Current).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_Current).Location = new Point(47, 214);
		((Control)L_Current).Name = "L_Current";
		((Control)L_Current).Size = new Size(60, 17);
		((Control)L_Current).TabIndex = 8;
		((Control)L_Current).Text = "0 of 0";
		L_Current.TextAlign = (ContentAlignment)32;
		((Control)B_Select).Location = new Point(164, 206);
		((Control)B_Select).Name = "B_Select";
		((Control)B_Select).Size = new Size(98, 32);
		((Control)B_Select).TabIndex = 9;
		((Control)B_Select).Text = "Select!";
		((ButtonBase)B_Select).UseVisualStyleBackColor = true;
		((Control)B_Select).Click += Button3_Click;
		((Control)B_Close).Location = new Point(164, 244);
		((Control)B_Close).Name = "B_Close";
		((Control)B_Close).Size = new Size(98, 29);
		((Control)B_Close).TabIndex = 10;
		((Control)B_Close).Text = "Close";
		((ButtonBase)B_Close).UseVisualStyleBackColor = true;
		((Control)B_Close).Click += Button4_Click;
		((Control)Button1).Location = new Point(9, 263);
		((Control)Button1).Name = "Button1";
		((Control)Button1).Size = new Size(136, 27);
		((Control)Button1).TabIndex = 23;
		((Control)Button1).Text = "Report ScreenShot";
		((ButtonBase)Button1).UseVisualStyleBackColor = true;
		((Control)Button1).Click += Button1_Click_1;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(274, 292);
		((Control)this).Controls.Add((Control)(object)Button1);
		((Control)this).Controls.Add((Control)(object)B_Close);
		((Control)this).Controls.Add((Control)(object)B_Select);
		((Control)this).Controls.Add((Control)(object)L_Current);
		((Control)this).Controls.Add((Control)(object)B_Next);
		((Control)this).Controls.Add((Control)(object)B_Prev);
		((Control)this).Controls.Add((Control)(object)PictureBox1);
		((Control)this).Controls.Add((Control)(object)TB_GameID);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(290, 330);
		((Control)this).Name = "OpsArtDownloadNewScreenBrowser";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "ScreenShot Database Browser";
		((ISupportInitialize)PictureBox1).EndInit();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
