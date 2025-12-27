using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class HelpChangeLogWindow : BaseForm
{
	private IContainer components;

	internal TableLayoutPanel TableLayoutPanel1;

	internal Button OK_Button;

	internal RichTextBox RichTextBox1;

	public HelpChangeLogWindow()
	{
		InitializeComponent();
	}

	private void OK_Button_Click(object sender, EventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)1;
		((Form)this).Close();
	}

	private void ChangeLogWindow_Load(object sender, EventArgs e)
	{
		((Control)RichTextBox1).Text = Resources.Changelog;
		((Control)this).Text = Translated.MAIN_TOOLBAR_HELP_LOG;
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HelpChangeLogWindow));
		TableLayoutPanel1 = new TableLayoutPanel();
		RichTextBox1 = new RichTextBox();
		OK_Button = new Button();
		((Control)TableLayoutPanel1).SuspendLayout();
		((Control)this).SuspendLayout();
		TableLayoutPanel1.ColumnCount = 1;
		TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle((SizeType)2, 50f));
		TableLayoutPanel1.Controls.Add((Control)(object)RichTextBox1, 0, 0);
		TableLayoutPanel1.Controls.Add((Control)(object)OK_Button, 0, 1);
		((Control)TableLayoutPanel1).Dock = (DockStyle)5;
		((Control)TableLayoutPanel1).Location = new Point(0, 0);
		((Control)TableLayoutPanel1).Name = "TableLayoutPanel1";
		TableLayoutPanel1.RowCount = 2;
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 50f));
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)1, 45f));
		((Control)TableLayoutPanel1).Size = new Size(592, 393);
		((Control)TableLayoutPanel1).TabIndex = 0;
		((Control)RichTextBox1).Dock = (DockStyle)5;
		((Control)RichTextBox1).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)0, (GraphicsUnit)3);
		((Control)RichTextBox1).Location = new Point(3, 3);
		((Control)RichTextBox1).Name = "RichTextBox1";
		((TextBoxBase)RichTextBox1).ReadOnly = true;
		((Control)RichTextBox1).Size = new Size(586, 342);
		((Control)RichTextBox1).TabIndex = 1;
		((Control)RichTextBox1).Text = "";
		((Control)OK_Button).Anchor = (AnchorStyles)0;
		((Control)OK_Button).Location = new Point(262, 359);
		((Control)OK_Button).Name = "OK_Button";
		((Control)OK_Button).Size = new Size(67, 23);
		((Control)OK_Button).TabIndex = 0;
		((Control)OK_Button).Text = "OK";
		((Control)OK_Button).Click += OK_Button_Click;
		((Form)this).AcceptButton = (IButtonControl)(object)OK_Button;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(592, 393);
		((Control)this).Controls.Add((Control)(object)TableLayoutPanel1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "HelpChangeLogWindow";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Change Log";
		((Form)this).Load += ChangeLogWindow_Load;
		((Control)TableLayoutPanel1).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
	}
}
