using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class SettingAutoUpdate : BaseForm
{
	private bool canClose;

	private IContainer components;

	internal CheckBox CB_Update;

	internal Button B_Save;

	public SettingAutoUpdate()
	{
		InitializeComponent();
	}

	private void Tracking_FormClosing(object sender, FormClosingEventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (!canClose)
		{
			((CancelEventArgs)(object)e).Cancel = true;
			MessageBox.Show(Translated.Global_PleaseUseTheSaveBtn, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
	}

	private void Save_Click(object sender, EventArgs e)
	{
		OplmSettings.Write("AUTOUPDATE", CB_Update.Checked.ToString());
		canClose = true;
		Program.MainFormInst.VersionCheck();
		((Form)this).Close();
	}

	private void Setting_AutoUpdate_Shown(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.SettingAutoUpdate_Title;
		((Control)CB_Update).Text = Translated.SettingAutoUpdate_Text;
		((Control)B_Save).Text = Translated.GLOBAL_BUTTON_SAVE;
		CB_Update.Checked = OplmSettings.Read("AUTOUPDATE", predef: true);
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
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SettingAutoUpdate));
		CB_Update = new CheckBox();
		B_Save = new Button();
		((Control)this).SuspendLayout();
		CB_Update.CheckAlign = (ContentAlignment)1;
		((Control)CB_Update).Location = new Point(12, 12);
		((Control)CB_Update).Name = "CB_Update";
		((Control)CB_Update).Size = new Size(260, 48);
		((Control)CB_Update).TabIndex = 0;
		((Control)CB_Update).Text = "Automatically check for updates on startup.";
		((ButtonBase)CB_Update).TextAlign = (ContentAlignment)1;
		((ButtonBase)CB_Update).UseVisualStyleBackColor = true;
		((Control)B_Save).Location = new Point(104, 66);
		((Control)B_Save).Name = "B_Save";
		((Control)B_Save).Size = new Size(80, 23);
		((Control)B_Save).TabIndex = 1;
		((Control)B_Save).Text = "Save";
		((ButtonBase)B_Save).UseVisualStyleBackColor = true;
		((Control)B_Save).Click += Save_Click;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(284, 101);
		((Control)this).Controls.Add((Control)(object)B_Save);
		((Control)this).Controls.Add((Control)(object)CB_Update);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)1;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(300, 140);
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(300, 140);
		((Control)this).Name = "SettingAutoUpdate";
		((Form)this).SizeGripStyle = (SizeGripStyle)2;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Auto-Update";
		((Form)this).FormClosing += new FormClosingEventHandler(Tracking_FormClosing);
		((Form)this).Shown += Setting_AutoUpdate_Shown;
		((Control)this).ResumeLayout(false);
	}
}
