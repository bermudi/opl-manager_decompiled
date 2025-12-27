using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class OpsCfgEditorVmcCreate : BaseForm
{
	public string resultVmcFile;

	private IContainer components;

	internal TextBox TbName;

	internal Label L_Name;

	internal Label L_Size;

	internal ComboBox CbSize;

	internal Button B_Create;

	public OpsCfgEditorVmcCreate()
	{
		InitializeComponent();
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		string text = ((Control)TbName).Text.Trim() + ".bin";
		if (!File.Exists(Path.Join(OplFolders.VMC, text)))
		{
			using (Process process = new Process())
			{
				process.StartInfo.FileName = Program.MainFormInst.app_folder + "\\lib\\genvmc.exe";
				process.StartInfo.WorkingDirectory = Program.MainFormInst.app_folder;
				process.StartInfo.Arguments = ((Control)CbSize).Text + " \"" + OplFolders.VMC + text + "\"";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				process.Start();
				process.WaitForExit();
				string text2 = process.StandardOutput.ReadToEnd();
				if (!text2.Contains("VMC file created!"))
				{
					MessageBox.Show(text2, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)16);
					return;
				}
				MessageBox.Show(text2, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
				resultVmcFile = ((Control)TbName).Text.Trim();
				((Form)this).DialogResult = (DialogResult)1;
			}
			((Form)this).Close();
		}
		else
		{
			MessageBox.Show(Translated.OpsCfgEditorVmcCreate_AlreadyExists, Translated.Global_Error);
		}
	}

	private void OpsCfgEditorVmc_Shown(object sender, EventArgs e)
	{
		((TextBoxBase)TbName).SelectionLength = 0;
		((ListControl)CbSize).SelectedIndex = 0;
		((Control)this).Text = Translated.OpsCfgEditorVmcCreate_Title;
		((Control)L_Name).Text = Translated.OpsCfgEditorVmcCreate_Name;
		((Control)L_Size).Text = Translated.OpsCfgEditorVmcCreate_Size;
		((Control)B_Create).Text = Translated.Global_ButtonCreate;
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
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsCfgEditorVmcCreate));
		TbName = new TextBox();
		L_Name = new Label();
		L_Size = new Label();
		CbSize = new ComboBox();
		B_Create = new Button();
		((Control)this).SuspendLayout();
		((Control)TbName).Location = new Point(94, 12);
		((Control)TbName).Name = "TbName";
		((Control)TbName).Size = new Size(209, 20);
		((Control)TbName).TabIndex = 1;
		((Control)TbName).Text = "example_name";
		((Control)L_Name).Location = new Point(7, 11);
		((Control)L_Name).Name = "L_Name";
		((Control)L_Name).Size = new Size(81, 20);
		((Control)L_Name).TabIndex = 0;
		((Control)L_Name).Text = "Name:";
		L_Name.TextAlign = (ContentAlignment)64;
		((Control)L_Size).Location = new Point(10, 41);
		((Control)L_Size).Name = "L_Size";
		((Control)L_Size).Size = new Size(78, 21);
		((Control)L_Size).TabIndex = 2;
		((Control)L_Size).Text = "Size (MB):";
		L_Size.TextAlign = (ContentAlignment)64;
		CbSize.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)CbSize).FormattingEnabled = true;
		CbSize.Items.AddRange(new object[5] { "8", "16", "32", "64", "512" });
		((Control)CbSize).Location = new Point(94, 42);
		((Control)CbSize).Name = "CbSize";
		((Control)CbSize).Size = new Size(105, 21);
		((Control)CbSize).TabIndex = 3;
		((Control)B_Create).Location = new Point(205, 42);
		((Control)B_Create).Name = "B_Create";
		((Control)B_Create).Size = new Size(98, 21);
		((Control)B_Create).TabIndex = 4;
		((Control)B_Create).Text = "Create";
		((ButtonBase)B_Create).UseVisualStyleBackColor = true;
		((Control)B_Create).Click += Button1_Click;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(315, 73);
		((Control)this).Controls.Add((Control)(object)B_Create);
		((Control)this).Controls.Add((Control)(object)CbSize);
		((Control)this).Controls.Add((Control)(object)L_Size);
		((Control)this).Controls.Add((Control)(object)L_Name);
		((Control)this).Controls.Add((Control)(object)TbName);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "OpsCfgEditorVmcCreate";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Create new VMC";
		((Form)this).Shown += OpsCfgEditorVmc_Shown;
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
