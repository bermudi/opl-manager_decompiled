using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class InputDialog : Form
{
	private IContainer components;

	private Label label1;

	private Button bOK;

	private Button bCancel;

	private TextBox textBox1;

	public string UserInput { get; private set; }

	public InputDialog()
	{
		InitializeComponent();
	}

	public InputDialog(string title, string body, string defaultValue)
	{
		InitializeComponent();
		((Control)bCancel).Text = Translated.GLOBAL_BUTTON_CANCEL;
		((Control)this).Name = title;
		((Control)label1).Text = body;
		((Control)textBox1).Text = defaultValue;
	}

	private void BOK_Click(object sender, EventArgs e)
	{
		UserInput = ((Control)textBox1).Text;
		((Form)this).DialogResult = (DialogResult)1;
		((Form)this).Close();
	}

	private void BCancel_Click(object sender, EventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)2;
		((Form)this).Close();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((Form)this).Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		label1 = new Label();
		bOK = new Button();
		bCancel = new Button();
		textBox1 = new TextBox();
		((Control)this).SuspendLayout();
		((Control)label1).Location = new Point(12, 9);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(424, 56);
		((Control)label1).TabIndex = 0;
		((Control)label1).Text = "Promt text\r\nMulti\r\nLine";
		((Control)bOK).Location = new Point(452, 12);
		((Control)bOK).Name = "bOK";
		((Control)bOK).Size = new Size(75, 23);
		((Control)bOK).TabIndex = 1;
		((Control)bOK).Text = "OK";
		((ButtonBase)bOK).UseVisualStyleBackColor = true;
		((Control)bOK).Click += BOK_Click;
		((Control)bCancel).Location = new Point(452, 42);
		((Control)bCancel).Name = "bCancel";
		((Control)bCancel).Size = new Size(75, 23);
		((Control)bCancel).TabIndex = 2;
		((Control)bCancel).Text = "Cancel";
		((ButtonBase)bCancel).UseVisualStyleBackColor = true;
		((Control)bCancel).Click += BCancel_Click;
		((Control)textBox1).Location = new Point(12, 71);
		((Control)textBox1).Name = "textBox1";
		((Control)textBox1).Size = new Size(515, 23);
		((Control)textBox1).TabIndex = 3;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(7f, 15f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).CancelButton = (IButtonControl)(object)bCancel;
		((Form)this).ClientSize = new Size(539, 101);
		((Control)this).Controls.Add((Control)(object)textBox1);
		((Control)this).Controls.Add((Control)(object)bCancel);
		((Control)this).Controls.Add((Control)(object)bOK);
		((Control)this).Controls.Add((Control)(object)label1);
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(555, 140);
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "InputDialog";
		((Control)this).Text = "InputDialog";
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
