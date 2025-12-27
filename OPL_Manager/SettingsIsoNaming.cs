using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class SettingsIsoNaming : BaseForm
{
	public bool changes;

	private IContainer components;

	internal Button B_OLD;

	internal Button B_NEW;

	internal Label Label1;

	internal Label Label2;

	internal CheckBox CB_Enforce;

	public SettingsIsoNaming()
	{
		InitializeComponent();
	}

	private void SettingsIsoNaming_Shown(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.SettingsIsoNaming_Title;
		((Control)CB_Enforce).Text = Translated.SettingsIsoNaming_Enforce;
		((Control)B_OLD).Text = Translated.GLOBAL_OLD;
		((Control)B_NEW).Text = Translated.GLOBAL_NEW;
		((Control)B_OLD).Enabled = false;
		((Control)B_NEW).Enabled = false;
		((Control)B_NEW).BackColor = default(Color);
		((Control)B_OLD).BackColor = default(Color);
		if (OplmSettings.ReadBool("ISO_USE_OLD_NAMING_FORMAT"))
		{
			((Control)B_NEW).Enabled = true;
			((Control)B_OLD).BackColor = Color.Green;
		}
		else
		{
			((Control)B_OLD).Enabled = true;
			((Control)B_NEW).BackColor = Color.Green;
		}
		CB_Enforce.Checked = OplmSettings.ReadBool("ISO_FORCE_NAMING_FORMAT");
		CB_Enforce.CheckedChanged += CB_Enforce_CheckedChanged;
	}

	private void B_OLD_Click(object sender, EventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		OplmSettings.Write("ISO_USE_OLD_NAMING_FORMAT", "True");
		MessageBox.Show(Translated.SettingsIsoNaming_Old, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		((Control)B_NEW).Enabled = true;
		((Control)B_OLD).Enabled = false;
		((Control)B_NEW).BackColor = default(Color);
		((Control)B_OLD).BackColor = Color.Green;
		changes = true;
	}

	private void B_NEW_Click(object sender, EventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		OplmSettings.Write("ISO_USE_OLD_NAMING_FORMAT", "False");
		MessageBox.Show(Translated.SettingsIsoNaming_New, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		((Control)B_NEW).Enabled = false;
		((Control)B_OLD).Enabled = true;
		((Control)B_NEW).BackColor = Color.Green;
		((Control)B_OLD).BackColor = default(Color);
		changes = true;
	}

	private void CB_Enforce_CheckedChanged(object sender, EventArgs e)
	{
		OplmSettings.Write("ISO_FORCE_NAMING_FORMAT", CB_Enforce.Checked.ToString());
		changes = true;
	}

	private void SettingsIsoNaming_FormClosing(object sender, FormClosingEventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)1;
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SettingsIsoNaming));
		B_OLD = new Button();
		B_NEW = new Button();
		Label1 = new Label();
		Label2 = new Label();
		CB_Enforce = new CheckBox();
		((Control)this).SuspendLayout();
		((Control)B_OLD).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)B_OLD).Location = new Point(12, 23);
		((Control)B_OLD).Name = "B_OLD";
		((Control)B_OLD).Size = new Size(88, 25);
		((Control)B_OLD).TabIndex = 0;
		((Control)B_OLD).Text = "OLD";
		((ButtonBase)B_OLD).UseVisualStyleBackColor = true;
		((Control)B_OLD).Click += B_OLD_Click;
		((Control)B_NEW).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)B_NEW).Location = new Point(12, 77);
		((Control)B_NEW).Name = "B_NEW";
		((Control)B_NEW).Size = new Size(88, 25);
		((Control)B_NEW).TabIndex = 1;
		((Control)B_NEW).Text = "NEW";
		((ButtonBase)B_NEW).UseVisualStyleBackColor = true;
		((Control)B_NEW).Click += B_NEW_Click;
		((Control)Label1).Location = new Point(103, 23);
		((Control)Label1).Name = "Label1";
		((Control)Label1).Size = new Size(138, 25);
		((Control)Label1).TabIndex = 2;
		((Control)Label1).Text = "SLUS_200.66.Half-Life3.iso";
		Label1.TextAlign = (ContentAlignment)16;
		((Control)Label2).Location = new Point(106, 78);
		((Control)Label2).Name = "Label2";
		((Control)Label2).Size = new Size(135, 25);
		((Control)Label2).TabIndex = 3;
		((Control)Label2).Text = "Half-Life3.iso";
		Label2.TextAlign = (ContentAlignment)16;
		((Control)CB_Enforce).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((ButtonBase)CB_Enforce).ImageAlign = (ContentAlignment)16;
		((Control)CB_Enforce).Location = new Point(13, 106);
		((Control)CB_Enforce).Name = "CB_Enforce";
		((Control)CB_Enforce).Size = new Size(218, 50);
		((Control)CB_Enforce).TabIndex = 4;
		((Control)CB_Enforce).Text = "Mark iso's that don't use the selected format as BAD.";
		((ButtonBase)CB_Enforce).UseVisualStyleBackColor = true;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(244, 158);
		((Control)this).Controls.Add((Control)(object)CB_Enforce);
		((Control)this).Controls.Add((Control)(object)Label2);
		((Control)this).Controls.Add((Control)(object)Label1);
		((Control)this).Controls.Add((Control)(object)B_NEW);
		((Control)this).Controls.Add((Control)(object)B_OLD);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "SettingsIsoNaming";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Iso Naming Format";
		((Form)this).FormClosing += new FormClosingEventHandler(SettingsIsoNaming_FormClosing);
		((Form)this).Shown += SettingsIsoNaming_Shown;
		((Control)this).ResumeLayout(false);
	}
}
