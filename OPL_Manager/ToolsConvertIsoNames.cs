using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class ToolsConvertIsoNames : BaseForm
{
	private enum Format
	{
		OLD,
		NEW
	}

	private IContainer components;

	internal ListView ISOsList;

	internal ColumnHeader Col_Format;

	internal ColumnHeader Col_Original;

	internal ColumnHeader Col_Converted;

	internal ColumnHeader Col_Medium;

	internal SplitContainer SplitContainer1;

	internal Label L_Info;

	internal GroupBox GB_Selected;

	internal Button B_ToOld;

	internal Button B_ToNew;

	internal GroupBox GroupBox2;

	internal Button B_DeselectAll;

	internal Button B_SelectAll;

	internal Button B_Apply;

	public ToolsConvertIsoNames()
	{
		InitializeComponent();
	}

	private void ToolsConvertIsoNames_Shown(object sender, EventArgs e)
	{
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		((Control)this).Text = Translated.ToolsConvertIsoNames_Title;
		Col_Medium.Text = Translated.ToolsConvertIsoNames_TableMedium;
		Col_Format.Text = Translated.ToolsConvertIsoNames_TableFormat;
		Col_Original.Text = Translated.ToolsConvertIsoNames_TableOriginal;
		Col_Converted.Text = Translated.ToolsConvertIsoNames_TableConverted;
		((Control)L_Info).Text = Translated.ToolsConvertIsoNames_Explanation;
		((Control)GB_Selected).Text = Translated.ToolsConvertIsoNames_WithSelected;
		((Control)B_ToOld).Text = Translated.ToolsConvertIsoNames_ToOld;
		((Control)B_ToNew).Text = Translated.ToolsConvertIsoNames_ToNew;
		((Control)B_SelectAll).Text = Translated.ToolsConvertIsoNames_SelectAll;
		((Control)B_DeselectAll).Text = Translated.ToolsConvertIsoNames_DeselectAll;
		((Control)B_Apply).Text = Translated.ToolsConvertIsoNames_Rename;
		foreach (GameInfo game in GameProvider.GameList)
		{
			if (game.Type == GameType.PS2 && !string.IsNullOrEmpty(game.ID))
			{
				ListViewItem val = new ListViewItem();
				val.Tag = game;
				val.Text = game.DiscTypeText;
				val.SubItems.Add(game.IsNewFormat ? Format.NEW.ToString() : Format.OLD.ToString());
				val.SubItems.Add(game.FileDiscOnly);
				val.SubItems.Add("");
				ISOsList.Items.Add(val);
			}
		}
		ISOsList.AutoResizeColumns((ColumnHeaderAutoResizeStyle)1);
	}

	private void Button2_Click(object sender, EventArgs e)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		Format format = ((sender == B_ToNew) ? Format.NEW : Format.OLD);
		foreach (ListViewItem selectedItem in ISOsList.SelectedItems)
		{
			ListViewItem val = selectedItem;
			val.SubItems[3].Text = "";
			GameInfo gameInfo = (GameInfo)val.Tag;
			Format num = (Format)Enum.Parse(typeof(Format), val.SubItems[1].Text);
			string text = Path.GetExtension(gameInfo.FileDiscFullPath);
			if (string.IsNullOrEmpty(text))
			{
				text = ".iso";
			}
			text = text.ToLower();
			if (num == Format.NEW)
			{
				if (format == Format.OLD)
				{
					val.SubItems[3].Text = gameInfo.ID + "." + gameInfo.Title + text;
				}
			}
			else if (format == Format.NEW)
			{
				val.SubItems[3].Text = gameInfo.Title + text;
			}
		}
		ISOsList.AutoResizeColumns((ColumnHeaderAutoResizeStyle)1);
	}

	private void B_Select_Deselect(object sender, EventArgs e)
	{
		int i = 0;
		for (int num = ISOsList.Items.Count - 1; i <= num; i++)
		{
			ISOsList.Items[i].Selected = sender == B_SelectAll;
		}
	}

	private void Button5_Click(object sender, EventArgs e)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		foreach (ListViewItem item in ISOsList.Items)
		{
			ListViewItem val = item;
			if (!string.IsNullOrEmpty(val.SubItems[3].Text.Trim()))
			{
				string fileDiscFullPath = ((GameInfo)val.Tag).FileDiscFullPath;
				string text = Path.Combine(Path.GetDirectoryName(fileDiscFullPath), val.SubItems[3].Text);
				if (File.Exists(fileDiscFullPath) && !File.Exists(text))
				{
					File.Move(fileDiscFullPath, text);
				}
			}
		}
		MessageBox.Show(Translated.GlobalString_OperationComplete, Translated.Global_Information);
		((Form)this).DialogResult = (DialogResult)1;
		((Form)this).Close();
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
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Expected O, but got Unknown
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Expected O, but got Unknown
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_076a: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ToolsConvertIsoNames));
		ISOsList = new ListView();
		Col_Medium = new ColumnHeader();
		Col_Format = new ColumnHeader();
		Col_Original = new ColumnHeader();
		Col_Converted = new ColumnHeader();
		SplitContainer1 = new SplitContainer();
		GroupBox2 = new GroupBox();
		B_Apply = new Button();
		B_DeselectAll = new Button();
		B_SelectAll = new Button();
		L_Info = new Label();
		GB_Selected = new GroupBox();
		B_ToOld = new Button();
		B_ToNew = new Button();
		((ISupportInitialize)SplitContainer1).BeginInit();
		((Control)SplitContainer1.Panel1).SuspendLayout();
		((Control)SplitContainer1.Panel2).SuspendLayout();
		((Control)SplitContainer1).SuspendLayout();
		((Control)GroupBox2).SuspendLayout();
		((Control)GB_Selected).SuspendLayout();
		((Control)this).SuspendLayout();
		ISOsList.Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[4] { Col_Medium, Col_Format, Col_Original, Col_Converted });
		((Control)ISOsList).Dock = (DockStyle)5;
		ISOsList.FullRowSelect = true;
		((Control)ISOsList).Location = new Point(0, 0);
		((Control)ISOsList).Name = "ISOsList";
		((Control)ISOsList).Size = new Size(490, 443);
		((Control)ISOsList).TabIndex = 1;
		ISOsList.UseCompatibleStateImageBehavior = false;
		ISOsList.View = (View)1;
		Col_Medium.Text = "Medium";
		Col_Medium.Width = 72;
		Col_Format.Text = "Format";
		Col_Format.Width = 75;
		Col_Original.Text = "Original";
		Col_Original.Width = 74;
		Col_Converted.Text = "Converted";
		Col_Converted.Width = 102;
		SplitContainer1.Dock = (DockStyle)5;
		((Control)SplitContainer1).Location = new Point(0, 0);
		((Control)SplitContainer1).Name = "SplitContainer1";
		((Control)SplitContainer1.Panel1).Controls.Add((Control)(object)ISOsList);
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)GroupBox2);
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)L_Info);
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)GB_Selected);
		((Control)SplitContainer1).Size = new Size(784, 443);
		SplitContainer1.SplitterDistance = 490;
		((Control)SplitContainer1).TabIndex = 2;
		((Control)GroupBox2).Controls.Add((Control)(object)B_Apply);
		((Control)GroupBox2).Controls.Add((Control)(object)B_DeselectAll);
		((Control)GroupBox2).Controls.Add((Control)(object)B_SelectAll);
		((Control)GroupBox2).Location = new Point(5, 183);
		((Control)GroupBox2).Name = "GroupBox2";
		((Control)GroupBox2).Size = new Size(275, 81);
		((Control)GroupBox2).TabIndex = 5;
		GroupBox2.TabStop = false;
		((Control)B_Apply).Location = new Point(75, 48);
		((Control)B_Apply).Name = "B_Apply";
		((Control)B_Apply).Size = new Size(114, 23);
		((Control)B_Apply).TabIndex = 6;
		((Control)B_Apply).Text = "Rename";
		((ButtonBase)B_Apply).UseVisualStyleBackColor = true;
		((Control)B_Apply).Click += Button5_Click;
		((Control)B_DeselectAll).Location = new Point(154, 19);
		((Control)B_DeselectAll).Name = "B_DeselectAll";
		((Control)B_DeselectAll).Size = new Size(112, 23);
		((Control)B_DeselectAll).TabIndex = 5;
		((Control)B_DeselectAll).Text = "Deselect All";
		((ButtonBase)B_DeselectAll).UseVisualStyleBackColor = true;
		((Control)B_DeselectAll).Click += B_Select_Deselect;
		((Control)B_SelectAll).Location = new Point(6, 19);
		((Control)B_SelectAll).Name = "B_SelectAll";
		((Control)B_SelectAll).Size = new Size(112, 23);
		((Control)B_SelectAll).TabIndex = 4;
		((Control)B_SelectAll).Text = "Select All";
		((ButtonBase)B_SelectAll).UseVisualStyleBackColor = true;
		((Control)B_SelectAll).Click += B_Select_Deselect;
		L_Info.BorderStyle = (BorderStyle)1;
		((Control)L_Info).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Info).Location = new Point(5, 9);
		((Control)L_Info).Name = "L_Info";
		((Control)L_Info).Size = new Size(275, 84);
		((Control)L_Info).TabIndex = 3;
		((Control)L_Info).Text = "What is NEW and OLD format? \r\nExample:\r\nTitle: Half-Life 3\r\nOLD: SLUS_200.66.Half-Life 3.iso\r\nNEW:Half-Life 3.iso\r\n";
		L_Info.TextAlign = (ContentAlignment)16;
		((Control)GB_Selected).Controls.Add((Control)(object)B_ToOld);
		((Control)GB_Selected).Controls.Add((Control)(object)B_ToNew);
		((Control)GB_Selected).Location = new Point(5, 96);
		((Control)GB_Selected).Name = "GB_Selected";
		((Control)GB_Selected).Size = new Size(275, 80);
		((Control)GB_Selected).TabIndex = 2;
		GB_Selected.TabStop = false;
		((Control)GB_Selected).Text = "With Selected ISOs";
		((Control)B_ToOld).Location = new Point(6, 48);
		((Control)B_ToOld).Name = "B_ToOld";
		((Control)B_ToOld).Size = new Size(260, 23);
		((Control)B_ToOld).TabIndex = 2;
		((Control)B_ToOld).Text = "Convert to OLD format";
		((ButtonBase)B_ToOld).UseVisualStyleBackColor = true;
		((Control)B_ToOld).Click += Button2_Click;
		((Control)B_ToNew).Location = new Point(6, 19);
		((Control)B_ToNew).Name = "B_ToNew";
		((Control)B_ToNew).Size = new Size(260, 23);
		((Control)B_ToNew).TabIndex = 1;
		((Control)B_ToNew).Text = "Convert to NEW format";
		((ButtonBase)B_ToNew).UseVisualStyleBackColor = true;
		((Control)B_ToNew).Click += Button2_Click;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(784, 443);
		((Control)this).Controls.Add((Control)(object)SplitContainer1);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).Name = "ToolsConvertIsoNames";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Convert ISO file names";
		((Form)this).Shown += ToolsConvertIsoNames_Shown;
		((Control)SplitContainer1.Panel1).ResumeLayout(false);
		((Control)SplitContainer1.Panel2).ResumeLayout(false);
		((ISupportInitialize)SplitContainer1).EndInit();
		((Control)SplitContainer1).ResumeLayout(false);
		((Control)GroupBox2).ResumeLayout(false);
		((Control)GB_Selected).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
	}
}
