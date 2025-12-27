using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class DialogLoadingGames : BaseForm
{
	private int gamesCount;

	private IContainer components;

	internal Label LProgress;

	public DialogLoadingGames()
	{
		InitializeComponent();
	}

	private void LoadingGamesDialog_Load(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.DialogLoadingGames_Title;
		((Control)LProgress).Text = string.Format(Translated.DialogLoadingGames_Progress, gamesCount);
	}

	public void IncrementCount()
	{
		if (!((Control)this).Visible)
		{
			Application.DoEvents();
			((Control)this).Show();
			Application.DoEvents();
		}
		gamesCount++;
		((Control)LProgress).Text = string.Format(Translated.DialogLoadingGames_Progress, gamesCount);
		Application.DoEvents();
	}

	public void AddCacheCount(int increment)
	{
		gamesCount += increment;
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(DialogLoadingGames));
		LProgress = new Label();
		((Control)this).SuspendLayout();
		((Control)LProgress).Dock = (DockStyle)5;
		((Control)LProgress).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)0, (GraphicsUnit)3);
		((Control)LProgress).Location = new Point(0, 0);
		((Control)LProgress).Name = "LProgress";
		((Control)LProgress).Size = new Size(284, 61);
		((Control)LProgress).TabIndex = 0;
		((Control)LProgress).Text = "0";
		LProgress.TextAlign = (ContentAlignment)32;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(284, 61);
		((Form)this).ControlBox = false;
		((Control)this).Controls.Add((Control)(object)LProgress);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(300, 100);
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(300, 100);
		((Control)this).Name = "DialogLoadingGames";
		((Form)this).ShowIcon = false;
		((Form)this).ShowInTaskbar = false;
		((Form)this).SizeGripStyle = (SizeGripStyle)2;
		((Form)this).StartPosition = (FormStartPosition)1;
		((Control)this).Text = "Loading...";
		((Form)this).TopMost = true;
		((Form)this).Load += LoadingGamesDialog_Load;
		((Control)this).ResumeLayout(false);
	}
}
