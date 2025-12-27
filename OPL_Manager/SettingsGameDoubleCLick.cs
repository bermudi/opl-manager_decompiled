using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class SettingsGameDoubleCLick : BaseForm
{
	private const string sampleID = "SLES_001.02";

	private const string sampleFile = "D:\\Games\\Folder With Space\\SLES_001.02.Game Title.iso";

	private IContainer components;

	internal TableLayoutPanel TableLayoutPanel1;

	internal Button OK_Button;

	internal Button Cancel_Button;

	internal Label L_Available;

	internal TextBox TB_Exe;

	internal TextBox TB_params;

	internal Label Label4;

	internal Label Label5;

	internal TextBox TB_Preview;

	internal Label L_Preview;

	internal Label L_Note;

	internal CheckBox CB_Enable;

	internal Button B_BrowseEXE;

	internal OpenFileDialog OpenFileDialog1;

	internal GroupBox gb1;

	public SettingsGameDoubleCLick()
	{
		InitializeComponent();
	}

	private void OK_Button_Click(object sender, EventArgs e)
	{
		OplmSettings.Write("DoubleClickEnabled", CB_Enable.Checked.ToString());
		OplmSettings.Write("DoubleClickExe", ((Control)TB_Exe).Text.Trim());
		OplmSettings.Write("DoubleClickParams", ((Control)TB_params).Text.Trim());
		((Form)this).DialogResult = (DialogResult)1;
		((Form)this).Close();
	}

	private void Cancel_Button_Click(object sender, EventArgs e)
	{
		((Form)this).DialogResult = (DialogResult)2;
		((Form)this).Close();
	}

	private void TB_Exe_TextChanged(object sender, EventArgs e)
	{
		updatePreview();
	}

	private void TB_params_TextChanged(object sender, EventArgs e)
	{
		updatePreview();
	}

	private void updatePreview()
	{
		if (!(string.IsNullOrEmpty(((Control)TB_params).Text.Trim()) & string.IsNullOrEmpty(((Control)TB_Exe).Text.Trim())))
		{
			string text = ((Control)TB_params).Text;
			text = text.Replace("[ID]", "SLES_001.02");
			text = text.Replace("[FILE]", "\"D:\\Games\\Folder With Space\\SLES_001.02.Game Title.iso\"");
			text = text.Replace("[id]", "SLES_001.02");
			text = text.Replace("[file]", "\"D:\\Games\\Folder With Space\\SLES_001.02.Game Title.iso\"");
			((Control)TB_Preview).Text = "\"" + ((Control)TB_Exe).Text + "\" " + text;
		}
	}

	private void GameDoubleAction_Shown(object sender, EventArgs e)
	{
		((Control)this).Text = Translated.SettingsGameDoubleClick_Title;
		((Control)L_Available).Text = Translated.SettingsGameDoubleClick_Available;
		((Control)L_Preview).Text = Translated.SettingsGameDoubleClick_Preview;
		((Control)L_Note).Text = Translated.SettingsGameDoubleClick_Note;
		((Control)B_BrowseEXE).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)OK_Button).Text = Translated.GLOBAL_BUTTON_SAVE_CLOSE;
		((Control)Cancel_Button).Text = Translated.GLOBAL_BUTTON_CANCEL;
		((Control)CB_Enable).Text = Translated.SettingsGameDoubleClick_Enable;
		CB_Enable.Checked = OplmSettings.Read("DoubleClickEnabled", predef: false);
		((Control)TB_Exe).Text = OplmSettings.Read("DoubleClickExe", "");
		((Control)TB_params).Text = OplmSettings.Read("DoubleClickParams", "");
		((Control)gb1).Enabled = CB_Enable.Checked;
	}

	private void CB_Enable_CheckedChanged(object sender, EventArgs e)
	{
		((Control)gb1).Enabled = CB_Enable.Checked;
		updatePreview();
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)((CommonDialog)OpenFileDialog1).ShowDialog() == 1)
		{
			((Control)TB_Exe).Text = ((FileDialog)OpenFileDialog1).FileName;
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
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Expected O, but got Unknown
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Expected O, but got Unknown
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Expected O, but got Unknown
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0918: Expected O, but got Unknown
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SettingsGameDoubleCLick));
		TableLayoutPanel1 = new TableLayoutPanel();
		OK_Button = new Button();
		Cancel_Button = new Button();
		L_Available = new Label();
		TB_Exe = new TextBox();
		TB_params = new TextBox();
		Label4 = new Label();
		Label5 = new Label();
		TB_Preview = new TextBox();
		L_Preview = new Label();
		L_Note = new Label();
		CB_Enable = new CheckBox();
		B_BrowseEXE = new Button();
		OpenFileDialog1 = new OpenFileDialog();
		gb1 = new GroupBox();
		((Control)TableLayoutPanel1).SuspendLayout();
		((Control)gb1).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)TableLayoutPanel1).Anchor = (AnchorStyles)10;
		TableLayoutPanel1.ColumnCount = 2;
		TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle((SizeType)2, 63.13131f));
		TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle((SizeType)2, 36.86869f));
		TableLayoutPanel1.Controls.Add((Control)(object)OK_Button, 0, 0);
		TableLayoutPanel1.Controls.Add((Control)(object)Cancel_Button, 1, 0);
		((Control)TableLayoutPanel1).Location = new Point(407, 346);
		((Control)TableLayoutPanel1).Name = "TableLayoutPanel1";
		TableLayoutPanel1.RowCount = 1;
		TableLayoutPanel1.RowStyles.Add(new RowStyle((SizeType)2, 50f));
		((Control)TableLayoutPanel1).Size = new Size(215, 29);
		((Control)TableLayoutPanel1).TabIndex = 0;
		((Control)OK_Button).Anchor = (AnchorStyles)0;
		((Control)OK_Button).Location = new Point(6, 3);
		((Control)OK_Button).Name = "OK_Button";
		((Control)OK_Button).Size = new Size(122, 23);
		((Control)OK_Button).TabIndex = 0;
		((Control)OK_Button).Text = "Save and Close";
		((Control)OK_Button).Click += OK_Button_Click;
		((Control)Cancel_Button).Anchor = (AnchorStyles)0;
		Cancel_Button.DialogResult = (DialogResult)2;
		((Control)Cancel_Button).Location = new Point(138, 3);
		((Control)Cancel_Button).Name = "Cancel_Button";
		((Control)Cancel_Button).Size = new Size(74, 23);
		((Control)Cancel_Button).TabIndex = 1;
		((Control)Cancel_Button).Text = "Cancel";
		((Control)Cancel_Button).Click += Cancel_Button_Click;
		((Control)L_Available).AutoSize = true;
		((Control)L_Available).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_Available).Location = new Point(6, 25);
		((Control)L_Available).Name = "L_Available";
		((Control)L_Available).Size = new Size(243, 17);
		((Control)L_Available).TabIndex = 1;
		((Control)L_Available).Text = "Available parameters: [FILE] [ID]";
		((Control)TB_Exe).Location = new Point(55, 51);
		((Control)TB_Exe).Name = "TB_Exe";
		((Control)TB_Exe).Size = new Size(436, 20);
		((Control)TB_Exe).TabIndex = 4;
		((Control)TB_Exe).TextChanged += TB_Exe_TextChanged;
		((Control)TB_params).Location = new Point(79, 78);
		((Control)TB_params).Name = "TB_params";
		((Control)TB_params).Size = new Size(412, 20);
		((Control)TB_params).TabIndex = 5;
		((Control)TB_params).TextChanged += TB_params_TextChanged;
		((Control)Label4).AutoSize = true;
		((Control)Label4).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)Label4).Location = new Point(6, 52);
		((Control)Label4).Name = "Label4";
		((Control)Label4).Size = new Size(43, 17);
		((Control)Label4).TabIndex = 8;
		((Control)Label4).Text = "EXE:";
		((Control)Label5).AutoSize = true;
		((Control)Label5).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)Label5).Location = new Point(6, 78);
		((Control)Label5).Name = "Label5";
		((Control)Label5).Size = new Size(67, 17);
		((Control)Label5).TabIndex = 9;
		((Control)Label5).Text = "Params:";
		((Control)TB_Preview).Location = new Point(9, 132);
		((TextBoxBase)TB_Preview).Multiline = true;
		((Control)TB_Preview).Name = "TB_Preview";
		((TextBoxBase)TB_Preview).ReadOnly = true;
		((Control)TB_Preview).Size = new Size(563, 70);
		((Control)TB_Preview).TabIndex = 10;
		((Control)L_Preview).AutoSize = true;
		((Control)L_Preview).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_Preview).Location = new Point(6, 112);
		((Control)L_Preview).Name = "L_Preview";
		((Control)L_Preview).Size = new Size(134, 17);
		((Control)L_Preview).TabIndex = 11;
		((Control)L_Preview).Text = "Example Preview:";
		((Control)L_Note).AutoSize = true;
		((Control)L_Note).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_Note).Location = new Point(6, 205);
		((Control)L_Note).Name = "L_Note";
		((Control)L_Note).Size = new Size(251, 65);
		((Control)L_Note).TabIndex = 12;
		((Control)L_Note).Text = "EXE: The application executable.\r\nExample: D:\\PS2\\Tools\\SomeTool.exe\r\n\r\nParams:The parameters to pass to the app.\r\nExample: -file [file] -gameid [id]";
		((Control)CB_Enable).AutoSize = true;
		((Control)CB_Enable).Location = new Point(12, 12);
		((Control)CB_Enable).Name = "CB_Enable";
		((Control)CB_Enable).Size = new Size(155, 17);
		((Control)CB_Enable).TabIndex = 13;
		((Control)CB_Enable).Text = "Enable Double Click Action";
		((ButtonBase)CB_Enable).UseVisualStyleBackColor = true;
		CB_Enable.CheckedChanged += CB_Enable_CheckedChanged;
		((Control)B_BrowseEXE).Location = new Point(497, 49);
		((Control)B_BrowseEXE).Name = "B_BrowseEXE";
		((Control)B_BrowseEXE).Size = new Size(75, 23);
		((Control)B_BrowseEXE).TabIndex = 14;
		((Control)B_BrowseEXE).Text = "Browse";
		((ButtonBase)B_BrowseEXE).UseVisualStyleBackColor = true;
		((Control)B_BrowseEXE).Click += Button1_Click;
		((FileDialog)OpenFileDialog1).FileName = "OpenFileDialog1";
		((Control)gb1).Controls.Add((Control)(object)L_Available);
		((Control)gb1).Controls.Add((Control)(object)B_BrowseEXE);
		((Control)gb1).Controls.Add((Control)(object)TB_Exe);
		((Control)gb1).Controls.Add((Control)(object)TB_params);
		((Control)gb1).Controls.Add((Control)(object)L_Note);
		((Control)gb1).Controls.Add((Control)(object)Label4);
		((Control)gb1).Controls.Add((Control)(object)L_Preview);
		((Control)gb1).Controls.Add((Control)(object)Label5);
		((Control)gb1).Controls.Add((Control)(object)TB_Preview);
		((Control)gb1).Location = new Point(12, 36);
		((Control)gb1).Name = "gb1";
		((Control)gb1).Size = new Size(616, 295);
		((Control)gb1).TabIndex = 15;
		gb1.TabStop = false;
		((Form)this).AcceptButton = (IButtonControl)(object)OK_Button;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).CancelButton = (IButtonControl)(object)Cancel_Button;
		((Form)this).ClientSize = new Size(624, 376);
		((Control)this).Controls.Add((Control)(object)gb1);
		((Control)this).Controls.Add((Control)(object)CB_Enable);
		((Control)this).Controls.Add((Control)(object)TableLayoutPanel1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(640, 415);
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(640, 415);
		((Control)this).Name = "SettingsGameDoubleCLick";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Set Double Click Action";
		((Form)this).Shown += GameDoubleAction_Shown;
		((Control)TableLayoutPanel1).ResumeLayout(false);
		((Control)gb1).ResumeLayout(false);
		((Control)gb1).PerformLayout();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
