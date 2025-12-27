using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class SettingsChangeLanguage : BaseForm
{
	private bool canClose;

	private string lng = OplmSettings.Read("LANGUAGE", "");

	private IContainer components;

	internal ComboBox CB_Languages;

	internal Button B_Save;

	internal LinkLabel Link_Translate;

	public SettingsChangeLanguage()
	{
		InitializeComponent();
	}

	private void ChangeLanguage_FormClosing(object sender, FormClosingEventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (!canClose)
		{
			((CancelEventArgs)(object)e).Cancel = true;
			MessageBox.Show(Translated.Global_PleaseUseTheSaveBtn, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
	}

	private void ChangeLanguage_Shown(object sender, EventArgs e)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		((Control)this).Text = Translated.SettingsChangeLanguage_Title;
		((Control)B_Save).Text = Translated.GLOBAL_BUTTON_SAVE;
		((ListControl)CB_Languages).DisplayMember = "Value";
		((ListControl)CB_Languages).ValueMember = "Key";
		CB_Languages.DataSource = (object)new BindingSource((object)Program.MainFormInst.LanguagesAvailable, (string)null);
		int num = 0;
		foreach (KeyValuePair<string, string> item in Program.MainFormInst.LanguagesAvailable)
		{
			if ((item.Key.ToLower() ?? "") == (lng.Trim().ToLower() ?? ""))
			{
				((ListControl)CB_Languages).SelectedIndex = num;
			}
			num++;
		}
	}

	private void B_Save_Click(object sender, EventArgs e)
	{
		canClose = true;
		if ((lng.ToLower() ?? "") != (((ListControl)CB_Languages).SelectedValue.ToString().ToLower() ?? ""))
		{
			OplmSettings.Write("LANGUAGE", ((ListControl)CB_Languages).SelectedValue.ToString());
			((Form)this).DialogResult = (DialogResult)6;
		}
		else
		{
			((Form)this).DialogResult = (DialogResult)7;
		}
	}

	private void Link_Translate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		CommonFuncs.OpenURL("http://oplmanager.oneskyapp.com");
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
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SettingsChangeLanguage));
		CB_Languages = new ComboBox();
		B_Save = new Button();
		Link_Translate = new LinkLabel();
		((Control)this).SuspendLayout();
		CB_Languages.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)CB_Languages).FormattingEnabled = true;
		((Control)CB_Languages).Location = new Point(12, 12);
		((Control)CB_Languages).Name = "CB_Languages";
		((Control)CB_Languages).Size = new Size(230, 21);
		((Control)CB_Languages).TabIndex = 0;
		((Control)B_Save).Location = new Point(248, 11);
		((Control)B_Save).Name = "B_Save";
		((Control)B_Save).Size = new Size(88, 23);
		((Control)B_Save).TabIndex = 1;
		((Control)B_Save).Text = "Save!";
		((ButtonBase)B_Save).UseVisualStyleBackColor = true;
		((Control)B_Save).Click += B_Save_Click;
		((Control)Link_Translate).Location = new Point(12, 37);
		((Control)Link_Translate).Name = "Link_Translate";
		((Control)Link_Translate).Size = new Size(324, 23);
		((Control)Link_Translate).TabIndex = 2;
		Link_Translate.TabStop = true;
		((Control)Link_Translate).Text = "http://oplmanager.oneskyapp.com";
		((Label)Link_Translate).TextAlign = (ContentAlignment)32;
		Link_Translate.LinkClicked += new LinkLabelLinkClickedEventHandler(Link_Translate_LinkClicked);
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(349, 71);
		((Control)this).Controls.Add((Control)(object)Link_Translate);
		((Control)this).Controls.Add((Control)(object)B_Save);
		((Control)this).Controls.Add((Control)(object)CB_Languages);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(365, 110);
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(365, 110);
		((Control)this).Name = "SettingsChangeLanguage";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Change Language";
		((Form)this).FormClosing += new FormClosingEventHandler(ChangeLanguage_FormClosing);
		((Form)this).Shown += ChangeLanguage_Shown;
		((Control)this).ResumeLayout(false);
	}
}
