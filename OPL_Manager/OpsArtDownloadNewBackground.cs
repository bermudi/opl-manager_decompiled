using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class OpsArtDownloadNewBackground : BaseForm
{
	private List<string> images = new List<string>();

	private int currentImg;

	private GameInfo GAME;

	public byte[] imgBytes;

	private IContainer components;

	internal Button B_Close;

	internal Button B_Select;

	internal Label L_Current;

	internal Button B_Next;

	internal Button B_Prev;

	internal PictureBox PictureBox1;

	internal TextBox TB_GameID;

	internal Label L_ID;

	internal Button B_Report;

	public OpsArtDownloadNewBackground()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _game, ArrayOfString _images)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		GAME = _game;
		((Control)TB_GameID).Text = GAME.ID;
		images.Clear();
		images.AddRange(_images);
		currentImg = 0;
		((Form)this).DialogResult = (DialogResult)2;
		return ((Form)this).ShowDialog();
	}

	private void OpsArtDownloadNewBackground_Shown(object sender, EventArgs e)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (images.Count > 0)
		{
			((Control)B_Next).Enabled = true;
			((Control)B_Prev).Enabled = true;
			((Control)B_Select).Enabled = true;
			RefreshImage();
		}
		else
		{
			MessageBox.Show(Translated.OpsArtDownload_NoBgFound);
			((Form)this).Close();
		}
	}

	private void RefreshImage()
	{
		PictureBox1.Image = CommonFuncs.HttpGetImgAsImage(images[currentImg]);
		((Control)L_Current).Text = currentImg + 1 + " / " + images.Count;
		((Control)B_Next).Enabled = currentImg < images.Count - 1;
		((Control)B_Prev).Enabled = currentImg > 0;
	}

	private void B_Prev_Click(object sender, EventArgs e)
	{
		currentImg--;
		RefreshImage();
	}

	private void B_Next_Click(object sender, EventArgs e)
	{
		currentImg++;
		RefreshImage();
	}

	private void B_Close_Click(object sender, EventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)2;
		((Form)this).Close();
	}

	private void B_Select_Click(object sender, EventArgs e)
	{
		imgBytes = CommonFuncs.HttpGetImgToByteArray(images[currentImg]);
		((Form)this).DialogResult = (DialogResult)1;
		((Form)this).Close();
	}

	private void B_Report_Click(object sender, EventArgs e)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		OpsArtDownloadReport opsArtDownloadReport = new OpsArtDownloadReport();
		try
		{
			opsArtDownloadReport.ShowDialog(GAME, ArtType.BG, images, currentImg);
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
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Expected O, but got Unknown
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Expected O, but got Unknown
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0655: Expected O, but got Unknown
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsArtDownloadNewBackground));
		B_Close = new Button();
		B_Select = new Button();
		L_Current = new Label();
		B_Next = new Button();
		B_Prev = new Button();
		PictureBox1 = new PictureBox();
		TB_GameID = new TextBox();
		L_ID = new Label();
		B_Report = new Button();
		((ISupportInitialize)PictureBox1).BeginInit();
		((Control)this).SuspendLayout();
		((Control)B_Close).Location = new Point(257, 258);
		((Control)B_Close).Name = "B_Close";
		((Control)B_Close).Size = new Size(75, 28);
		((Control)B_Close).TabIndex = 18;
		((Control)B_Close).Text = "Close";
		((ButtonBase)B_Close).UseVisualStyleBackColor = true;
		((Control)B_Close).Click += B_Close_Click;
		((Control)B_Select).Enabled = false;
		((Control)B_Select).Location = new Point(176, 258);
		((Control)B_Select).Name = "B_Select";
		((Control)B_Select).Size = new Size(75, 28);
		((Control)B_Select).TabIndex = 17;
		((Control)B_Select).Text = "Select!";
		((ButtonBase)B_Select).UseVisualStyleBackColor = true;
		((Control)B_Select).Click += B_Select_Click;
		((Control)L_Current).AutoSize = true;
		((Control)L_Current).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_Current).Location = new Point(60, 266);
		((Control)L_Current).Name = "L_Current";
		((Control)L_Current).Size = new Size(41, 17);
		((Control)L_Current).TabIndex = 16;
		((Control)L_Current).Text = "0 / 0";
		((Control)B_Next).BackgroundImage = (Image)componentResourceManager.GetObject("B_Next.BackgroundImage");
		((Control)B_Next).BackgroundImageLayout = (ImageLayout)4;
		((Control)B_Next).Enabled = false;
		((Control)B_Next).Location = new Point(116, 258);
		((Control)B_Next).Name = "B_Next";
		((Control)B_Next).Size = new Size(32, 32);
		((Control)B_Next).TabIndex = 15;
		((ButtonBase)B_Next).UseVisualStyleBackColor = true;
		((Control)B_Next).Click += B_Next_Click;
		((Control)B_Prev).BackgroundImage = (Image)componentResourceManager.GetObject("B_Prev.BackgroundImage");
		((Control)B_Prev).BackgroundImageLayout = (ImageLayout)4;
		((Control)B_Prev).Enabled = false;
		((Control)B_Prev).Location = new Point(12, 258);
		((Control)B_Prev).Name = "B_Prev";
		((Control)B_Prev).Size = new Size(32, 32);
		((Control)B_Prev).TabIndex = 14;
		((ButtonBase)B_Prev).UseVisualStyleBackColor = true;
		((Control)B_Prev).Click += B_Prev_Click;
		((Control)PictureBox1).Anchor = (AnchorStyles)15;
		((Control)PictureBox1).BackgroundImageLayout = (ImageLayout)0;
		PictureBox1.Image = (Image)componentResourceManager.GetObject("PictureBox1.Image");
		((Control)PictureBox1).Location = new Point(12, 12);
		((Control)PictureBox1).MaximumSize = new Size(320, 240);
		((Control)PictureBox1).MinimumSize = new Size(320, 240);
		((Control)PictureBox1).Name = "PictureBox1";
		((Control)PictureBox1).Size = new Size(320, 240);
		PictureBox1.SizeMode = (PictureBoxSizeMode)4;
		PictureBox1.TabIndex = 13;
		PictureBox1.TabStop = false;
		((Control)TB_GameID).Enabled = false;
		((Control)TB_GameID).Location = new Point(46, 292);
		((Control)TB_GameID).Name = "TB_GameID";
		((TextBoxBase)TB_GameID).ReadOnly = true;
		((Control)TB_GameID).Size = new Size(111, 20);
		((Control)TB_GameID).TabIndex = 12;
		((Control)L_ID).AutoSize = true;
		((Control)L_ID).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_ID).Location = new Point(12, 293);
		((Control)L_ID).Name = "L_ID";
		((Control)L_ID).Size = new Size(28, 17);
		((Control)L_ID).TabIndex = 11;
		((Control)L_ID).Text = "ID:";
		((Control)B_Report).Location = new Point(176, 293);
		((Control)B_Report).Name = "B_Report";
		((Control)B_Report).Size = new Size(156, 27);
		((Control)B_Report).TabIndex = 19;
		((Control)B_Report).Text = "Report Background";
		((ButtonBase)B_Report).UseVisualStyleBackColor = true;
		((Control)B_Report).Click += B_Report_Click;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(349, 322);
		((Control)this).Controls.Add((Control)(object)B_Report);
		((Control)this).Controls.Add((Control)(object)B_Close);
		((Control)this).Controls.Add((Control)(object)B_Select);
		((Control)this).Controls.Add((Control)(object)L_Current);
		((Control)this).Controls.Add((Control)(object)B_Next);
		((Control)this).Controls.Add((Control)(object)B_Prev);
		((Control)this).Controls.Add((Control)(object)PictureBox1);
		((Control)this).Controls.Add((Control)(object)TB_GameID);
		((Control)this).Controls.Add((Control)(object)L_ID);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MinimumSize = new Size(365, 360);
		((Control)this).Name = "OpsArtDownloadNewBackground";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Background";
		((Form)this).Shown += OpsArtDownloadNewBackground_Shown;
		((ISupportInitialize)PictureBox1).EndInit();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
