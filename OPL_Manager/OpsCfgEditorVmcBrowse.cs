using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class OpsCfgEditorVmcBrowse : BaseForm
{
	public string selectedFile;

	private IContainer components;

	internal Button B_Select;

	internal ListView LV_Vmcs;

	internal ColumnHeader ColumnHeader2;

	internal ColumnHeader ColumnHeader1;

	public OpsCfgEditorVmcBrowse()
	{
		InitializeComponent();
	}

	private void OpsCfgEditorVmcBrowse_Load(object sender, EventArgs e)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		((Control)this).Text = Translated.OpsCfgEditorVmcBrowse_Title;
		ColumnHeader2.Text = Translated.OpsCfgEditorVmcBrowse_ColSize;
		ColumnHeader1.Text = Translated.OpsCfgEditorVmcBrowse_ColName;
		((Control)B_Select).Text = Translated.OpsCfgEditorVmcBrowse_Select;
		if (!Directory.Exists(OplFolders.VMC))
		{
			MessageBox.Show(Translated.OpsCfgEditorVmcBrowse_MsgVmcFolderMissing, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)64);
			((Form)this).Close();
		}
		string[] files = Directory.GetFiles(OplFolders.VMC, "*.bin", SearchOption.TopDirectoryOnly);
		if (files.Length == 0)
		{
			MessageBox.Show(Translated.OpsCfgEditorVmcBrowse_MsgNoVmcFilesFound, Translated.Global_Error, (MessageBoxButtons)0, (MessageBoxIcon)64);
			((Form)this).Close();
		}
		string[] array = files;
		foreach (string obj in array)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(obj);
			ListViewItem val = new ListViewItem(CommonFuncs.FormatFileSize(new FileInfo(obj).Length))
			{
				Tag = fileNameWithoutExtension
			};
			val.SubItems.Add(fileNameWithoutExtension);
			LV_Vmcs.Items.Add(val);
		}
	}

	private void B_OK_Click(object sender, EventArgs e)
	{
		selectedFile = LV_Vmcs.SelectedItems[0].Tag.ToString();
		((Form)this).DialogResult = (DialogResult)1;
	}

	private void LV_Vmcs_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		ListViewItem item = LV_Vmcs.HitTest(e.Location).Item;
		if (item != null)
		{
			selectedFile = item.Tag.ToString();
			((Form)this).DialogResult = (DialogResult)1;
		}
	}

	private void LV_Vmcs_SelectedIndexChanged(object sender, EventArgs e)
	{
		((Control)B_Select).Enabled = LV_Vmcs.SelectedItems.Count == 1;
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
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsCfgEditorVmcBrowse));
		B_Select = new Button();
		LV_Vmcs = new ListView();
		ColumnHeader2 = new ColumnHeader();
		ColumnHeader1 = new ColumnHeader();
		((Control)this).SuspendLayout();
		((Control)B_Select).Anchor = (AnchorStyles)2;
		((Control)B_Select).Enabled = false;
		((Control)B_Select).Location = new Point(144, 284);
		((Control)B_Select).Name = "B_Select";
		((Control)B_Select).Size = new Size(75, 21);
		((Control)B_Select).TabIndex = 1;
		((Control)B_Select).Text = "Select";
		((ButtonBase)B_Select).UseVisualStyleBackColor = true;
		((Control)B_Select).Click += B_OK_Click;
		((Control)LV_Vmcs).Anchor = (AnchorStyles)15;
		LV_Vmcs.Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[2] { ColumnHeader2, ColumnHeader1 });
		LV_Vmcs.FullRowSelect = true;
		((Control)LV_Vmcs).Location = new Point(12, 12);
		LV_Vmcs.MultiSelect = false;
		((Control)LV_Vmcs).Name = "LV_Vmcs";
		((Control)LV_Vmcs).Size = new Size(342, 266);
		((Control)LV_Vmcs).TabIndex = 2;
		LV_Vmcs.UseCompatibleStateImageBehavior = false;
		LV_Vmcs.View = (View)1;
		LV_Vmcs.SelectedIndexChanged += LV_Vmcs_SelectedIndexChanged;
		((Control)LV_Vmcs).MouseDoubleClick += new MouseEventHandler(LV_Vmcs_MouseDoubleClick);
		ColumnHeader2.Text = "Size";
		ColumnHeader1.Text = "VMC Name";
		ColumnHeader1.Width = 267;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(367, 317);
		((Control)this).Controls.Add((Control)(object)LV_Vmcs);
		((Control)this).Controls.Add((Control)(object)B_Select);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "OpsCfgEditorVmcBrowse";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Select a VMC from the list";
		((Form)this).Load += OpsCfgEditorVmcBrowse_Load;
		((Control)this).ResumeLayout(false);
	}
}
