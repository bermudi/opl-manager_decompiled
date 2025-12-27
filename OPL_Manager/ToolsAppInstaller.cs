using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class ToolsAppInstaller : BaseForm
{
	private IContainer components;

	internal Label Label1;

	internal TextBox TB_SrcELF;

	internal Button B_SrcElf;

	internal ComboBox CB_Device;

	internal Label L_Device;

	internal TextBox TB_Folder;

	internal Label L_Folder;

	internal TextBox TB_Title;

	internal Label L_FileName;

	internal TextBox TB_FileName;

	internal Label L_Title;

	internal TextBox TB_CfgLine;

	internal Button B_Add;

	internal OpenFileDialog OpenFileDialog1;

	public ToolsAppInstaller()
	{
		InitializeComponent();
	}

	private void ToolsAppInstaller_Shown(object sender, EventArgs e)
	{
		((Control)B_SrcElf).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)L_Device).Text = Translated.GLOBAL_DEVICE;
		((Control)L_Folder).Text = Translated.GLOBAL_FOLDER;
		((Control)L_FileName).Text = Translated.GLOBAL_FILENAME;
		((Control)L_Title).Text = Translated.GLOBAL_TITLE;
		((Control)B_Add).Text = Translated.GLOBAL_BUTTON_SAVE;
	}

	private void TB_SrcELF_DragEnter(object sender, DragEventArgs e)
	{
		e.Effect = (DragDropEffects)1;
	}

	private void TB_SrcELF_DragDrop(object sender, DragEventArgs e)
	{
		string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
		if (array.Length == 1)
		{
			SetSourceFile(array[0]);
		}
	}

	private void B_SrcElf_Click(object sender, EventArgs e)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)((CommonDialog)OpenFileDialog1).ShowDialog() == 1)
		{
			SetSourceFile(((FileDialog)OpenFileDialog1).FileName);
		}
	}

	private void SetSourceFile(string file)
	{
		((Control)TB_SrcELF).Text = file;
		((Control)TB_Title).Text = Path.GetFileNameWithoutExtension(file);
		((Control)TB_FileName).Text = Path.GetFileName(file);
	}

	private void InputsChangedHandler(object sender, EventArgs e)
	{
		UpdateCfgLine();
	}

	private void UpdateCfgLine()
	{
		if (!string.IsNullOrEmpty(((Control)TB_Title).Text) && !string.IsNullOrEmpty(((Control)CB_Device).Text) && !string.IsNullOrEmpty(((Control)TB_Folder).Text) && !string.IsNullOrEmpty(((Control)TB_FileName).Text))
		{
			((Control)TB_CfgLine).Text = $"{((Control)TB_Title).Text}={((Control)CB_Device).Text}{((Control)TB_Folder).Text}{((Control)TB_FileName).Text}";
			((Control)B_Add).Enabled = true;
		}
		else
		{
			((Control)TB_CfgLine).Text = "";
			((Control)B_Add).Enabled = false;
		}
	}

	private void B_Add_Click(object sender, EventArgs e)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		string text = ((Control)TB_SrcELF).Text;
		string text2 = Path.Combine(OplFolders.Main, ((Control)TB_Folder).Text, ((Control)TB_FileName).Text);
		if (File.Exists(text))
		{
			if (!File.Exists(text2))
			{
				File.Copy(text, text2);
			}
			using (StreamWriter streamWriter = new StreamWriter(File.Open(Path.Combine(OplFolders.Main, "conf_apps.cfg"), FileMode.Append)))
			{
				streamWriter.WriteLine(((Control)TB_CfgLine).Text);
			}
			MessageBox.Show(Translated.GlobalString_OperationComplete, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
			((Form)this).Close();
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
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Expected O, but got Unknown
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsAppInstaller));
		Label1 = new Label();
		TB_SrcELF = new TextBox();
		B_SrcElf = new Button();
		CB_Device = new ComboBox();
		L_Device = new Label();
		TB_Folder = new TextBox();
		L_Folder = new Label();
		TB_Title = new TextBox();
		L_FileName = new Label();
		TB_FileName = new TextBox();
		L_Title = new Label();
		TB_CfgLine = new TextBox();
		B_Add = new Button();
		OpenFileDialog1 = new OpenFileDialog();
		((Control)this).SuspendLayout();
		((Control)Label1).Location = new Point(0, 12);
		((Control)Label1).Name = "Label1";
		((Control)Label1).Size = new Size(38, 20);
		((Control)Label1).TabIndex = 0;
		((Control)Label1).Text = "ELF:";
		Label1.TextAlign = (ContentAlignment)64;
		((Control)TB_SrcELF).AllowDrop = true;
		((Control)TB_SrcELF).Location = new Point(44, 13);
		((Control)TB_SrcELF).Name = "TB_SrcELF";
		((Control)TB_SrcELF).Size = new Size(272, 20);
		((Control)TB_SrcELF).TabIndex = 1;
		((Control)TB_SrcELF).TextChanged += InputsChangedHandler;
		((Control)TB_SrcELF).DragDrop += new DragEventHandler(TB_SrcELF_DragDrop);
		((Control)TB_SrcELF).DragEnter += new DragEventHandler(TB_SrcELF_DragEnter);
		((Control)B_SrcElf).Location = new Point(322, 12);
		((Control)B_SrcElf).Name = "B_SrcElf";
		((Control)B_SrcElf).Size = new Size(75, 23);
		((Control)B_SrcElf).TabIndex = 2;
		((Control)B_SrcElf).Text = "Browse";
		((ButtonBase)B_SrcElf).UseVisualStyleBackColor = true;
		((Control)B_SrcElf).Click += B_SrcElf_Click;
		((ListControl)CB_Device).FormattingEnabled = true;
		CB_Device.Items.AddRange(new object[12]
		{
			"", "smb:/", "mc0:/", "mc1:/", "mc2:/", "mc3:/", "pfs0:/", "mass:/", "mass0:/", "mass1:/",
			"mass2:/", "mass3:/"
		});
		((Control)CB_Device).Location = new Point(12, 71);
		((Control)CB_Device).Name = "CB_Device";
		((Control)CB_Device).Size = new Size(81, 21);
		((Control)CB_Device).TabIndex = 3;
		((Control)CB_Device).TextChanged += InputsChangedHandler;
		((Control)L_Device).Location = new Point(12, 53);
		((Control)L_Device).Name = "L_Device";
		((Control)L_Device).Size = new Size(81, 15);
		((Control)L_Device).TabIndex = 4;
		((Control)L_Device).Text = "Device";
		L_Device.TextAlign = (ContentAlignment)512;
		((Control)TB_Folder).Location = new Point(100, 71);
		((Control)TB_Folder).Name = "TB_Folder";
		((Control)TB_Folder).Size = new Size(100, 20);
		((Control)TB_Folder).TabIndex = 5;
		((Control)TB_Folder).Text = "APPS/";
		((Control)TB_Folder).TextChanged += InputsChangedHandler;
		((Control)L_Folder).Location = new Point(100, 53);
		((Control)L_Folder).Name = "L_Folder";
		((Control)L_Folder).Size = new Size(100, 15);
		((Control)L_Folder).TabIndex = 6;
		((Control)L_Folder).Text = "Folder";
		L_Folder.TextAlign = (ContentAlignment)512;
		((Control)TB_Title).Location = new Point(12, 125);
		((Control)TB_Title).Name = "TB_Title";
		((Control)TB_Title).Size = new Size(324, 20);
		((Control)TB_Title).TabIndex = 7;
		((Control)TB_Title).TextChanged += InputsChangedHandler;
		((Control)L_FileName).Location = new Point(203, 53);
		((Control)L_FileName).Name = "L_FileName";
		((Control)L_FileName).Size = new Size(208, 15);
		((Control)L_FileName).TabIndex = 9;
		((Control)L_FileName).Text = "File Name";
		L_FileName.TextAlign = (ContentAlignment)512;
		((Control)TB_FileName).Location = new Point(203, 71);
		((Control)TB_FileName).Name = "TB_FileName";
		((TextBoxBase)TB_FileName).ReadOnly = true;
		((Control)TB_FileName).Size = new Size(197, 20);
		((Control)TB_FileName).TabIndex = 8;
		((Control)TB_FileName).TextChanged += InputsChangedHandler;
		((Control)L_Title).Location = new Point(12, 102);
		((Control)L_Title).Name = "L_Title";
		((Control)L_Title).Size = new Size(324, 20);
		((Control)L_Title).TabIndex = 10;
		((Control)L_Title).Text = "Title";
		L_Title.TextAlign = (ContentAlignment)512;
		((Control)TB_CfgLine).Location = new Point(12, 156);
		((TextBoxBase)TB_CfgLine).Multiline = true;
		((Control)TB_CfgLine).Name = "TB_CfgLine";
		((TextBoxBase)TB_CfgLine).ReadOnly = true;
		((Control)TB_CfgLine).Size = new Size(388, 43);
		((Control)TB_CfgLine).TabIndex = 11;
		((Control)B_Add).Enabled = false;
		((Control)B_Add).Location = new Point(339, 118);
		((Control)B_Add).Name = "B_Add";
		((Control)B_Add).Size = new Size(61, 32);
		((Control)B_Add).TabIndex = 13;
		((Control)B_Add).Text = "Save";
		((ButtonBase)B_Add).UseVisualStyleBackColor = true;
		((Control)B_Add).Click += B_Add_Click;
		((FileDialog)OpenFileDialog1).FileName = "OpenFileDialog1";
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(412, 208);
		((Control)this).Controls.Add((Control)(object)B_Add);
		((Control)this).Controls.Add((Control)(object)TB_CfgLine);
		((Control)this).Controls.Add((Control)(object)L_Title);
		((Control)this).Controls.Add((Control)(object)L_FileName);
		((Control)this).Controls.Add((Control)(object)TB_FileName);
		((Control)this).Controls.Add((Control)(object)TB_Title);
		((Control)this).Controls.Add((Control)(object)L_Folder);
		((Control)this).Controls.Add((Control)(object)TB_Folder);
		((Control)this).Controls.Add((Control)(object)L_Device);
		((Control)this).Controls.Add((Control)(object)CB_Device);
		((Control)this).Controls.Add((Control)(object)B_SrcElf);
		((Control)this).Controls.Add((Control)(object)TB_SrcELF);
		((Control)this).Controls.Add((Control)(object)Label1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "ToolsAppInstaller";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "APP Installer";
		((Form)this).Shown += ToolsAppInstaller_Shown;
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
