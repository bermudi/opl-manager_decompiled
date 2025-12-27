using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class OpsCheatEditor1 : BaseForm
{
	private string CHT_File;

	private bool SavedChanges = true;

	private GameInfo SelectedGame;

	private IContainer components;

	internal TextBox TB_CheatCode;

	internal Button B_Save;

	internal Button B_Delete;

	internal TextBox TB_FilePath;

	internal ListView Lv_Codes;

	internal ColumnHeader ColumnHeader1;

	internal ColumnHeader ColumnHeader2;

	internal SplitContainer SplitContainer1;

	internal Label L_TipDoubleClick;

	internal LinkLabel Link_CheatDocs;

	public OpsCheatEditor1()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _SelectedGame)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Expected O, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Expected O, but got Unknown
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Expected O, but got Unknown
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Expected O, but got Unknown
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Expected O, but got Unknown
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Expected O, but got Unknown
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Expected O, but got Unknown
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Expected O, but got Unknown
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Expected O, but got Unknown
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Expected O, but got Unknown
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Expected O, but got Unknown
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Expected O, but got Unknown
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Expected O, but got Unknown
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Expected O, but got Unknown
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Expected O, but got Unknown
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Expected O, but got Unknown
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Expected O, but got Unknown
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Expected O, but got Unknown
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Expected O, but got Unknown
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Expected O, but got Unknown
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Expected O, but got Unknown
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Expected O, but got Unknown
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Expected O, but got Unknown
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Expected O, but got Unknown
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Invalid comparison between Unknown and I4
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"SAFEMODE",
			Translated.OpsCheatEditor1_Codes_Safemode
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"COMPATIBILITY_0x01",
			Translated.OpsCheatEditor1_Codes_Comp1
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"COMPATIBILITY_0x02",
			Translated.OpsCheatEditor1_Codes_Comp2
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"COMPATIBILITY_0x03",
			Translated.OpsCheatEditor1_Codes_Comp3
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"COMPATIBILITY_0x04",
			Translated.OpsCheatEditor1_Codes_Comp4
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"COMPATIBILITY_0x05",
			Translated.OpsCheatEditor1_Codes_Comp5
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"COMPATIBILITY_0x06",
			Translated.OpsCheatEditor1_Codes_Comp6
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"COMPATIBILITY_0x07",
			Translated.OpsCheatEditor1_Codes_Comp7
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"CODECACHE_ADDON_0",
			Translated.OpsCheatEditor1_Codes_CodecacheAddon0
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"SUBCDSTATUS",
			Translated.OpsCheatEditor1_Codes_SubCdStatus
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"FAKELC",
			Translated.OpsCheatEditor1_Codes_FakeLC
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"SMOOTH",
			Translated.OpsCheatEditor1_Codes_Smooth
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"NOPAL",
			Translated.OpsCheatEditor1_Codes_NoPal
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"FORCEPAL",
			Translated.OpsCheatEditor1_Codes_ForcePal
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"480p",
			Translated.OpsCheatEditor1_Codes_480p
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"WIDESCREEN",
			Translated.OpsCheatEditor1_Codes_Widescreen
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"ULTRA_WIDESCREEN",
			Translated.OpsCheatEditor1_Codes_UltraWidescreen
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"EYEFINITY",
			Translated.OpsCheatEditor1_Codes_Eyefinity
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"XPOS_####",
			Translated.OpsCheatEditor1_Codes_XPOS
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"YPOS_##",
			Translated.OpsCheatEditor1_Codes_YPOS
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"DWSTRETCH_####",
			Translated.OpsCheatEditor1_Codes_DWSTRETCH
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"DWCROP_####",
			Translated.OpsCheatEditor1_Codes_DWCROP
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"HDTVFIX",
			Translated.OpsCheatEditor1_Codes_HDTVfix
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"MUTE_CDDA",
			Translated.OpsCheatEditor1_Codes_MuteCDDA
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"UNDO_MUTE_CDDA",
			Translated.OpsCheatEditor1_Codes_UndoMuteCDDA
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"MUTE_VAB",
			Translated.OpsCheatEditor1_Codes_MuteVAB
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"00507028 00000001",
			Translated.OpsCheatEditor1_Codes_00507028
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"005070B8 00000001",
			Translated.OpsCheatEditor1_Codes_005070B8
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"D2LS",
			Translated.OpsCheatEditor1_Codes_D2LS
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"D2LS_ALT",
			Translated.OpsCheatEditor1_Codes_D2LS_ALT
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"NOVMC0",
			Translated.OpsCheatEditor1_Codes_NoVMC0
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"NOVMC1",
			Translated.OpsCheatEditor1_Codes_NoVMC1
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"IGR0",
			Translated.OpsCheatEditor1_Codes_IGR0
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"IGR1",
			Translated.OpsCheatEditor1_Codes_IGR1
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"IGR2",
			Translated.OpsCheatEditor1_Codes_IGR2
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"IGR3",
			Translated.OpsCheatEditor1_Codes_IGR3
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"IGR4",
			Translated.OpsCheatEditor1_Codes_IGR4
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"IGR5",
			Translated.OpsCheatEditor1_Codes_IGR5
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"NOIGR",
			Translated.OpsCheatEditor1_Codes_NOIGR
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"CACHE1",
			Translated.OpsCheatEditor1_Codes_Cache1
		}));
		Lv_Codes.Items.Add(new ListViewItem(new string[2]
		{
			"USBDELAY_#",
			Translated.OpsCheatEditor1_Codes_UsbDelay
		}));
		Lv_Codes.Columns[0].Width = -1;
		Lv_Codes.Columns[1].Width = -1;
		foreach (ListViewItem item in Lv_Codes.Items)
		{
			ListViewItem val = item;
			val.UseItemStyleForSubItems = false;
			val.SubItems[0].BackColor = Color.Yellow;
			val.SubItems[0].Font = new Font(val.SubItems[0].Font, (FontStyle)1);
		}
		SelectedGame = _SelectedGame;
		((Control)this).Text = Translated.OpsCheatEditor1_Title;
		((Control)B_Save).Text = Translated.GLOBAL_BUTTON_SAVE;
		((Control)B_Delete).Text = Translated.OpsCheatEditor__Delete;
		((Control)L_TipDoubleClick).Text = Translated.OpsCheatEditor1_TipDoubleClick;
		CHT_File = SelectedGame.FileDiscFolderPath + Path.GetFileNameWithoutExtension(SelectedGame.FileDiscOnly) + "/CHEATS.TXT";
		((Control)TB_FilePath).Text = CHT_File;
		if (File.Exists(CHT_File))
		{
			((Control)TB_CheatCode).Text = File.ReadAllText(CHT_File);
		}
		else
		{
			if ((int)MessageBox.Show(Translated.OpsCheatEditor_CreateNewMessage, Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) != 6)
			{
				return (DialogResult)7;
			}
			InitCht();
		}
		((TextBoxBase)TB_CheatCode).Select(((Control)TB_CheatCode).Text.Length, 0);
		((Control)TB_CheatCode).TextChanged += TB_CheatCode_TextChanged;
		return ((Form)this).ShowDialog();
	}

	private void InitCht()
	{
		((Control)TB_CheatCode).Text = "";
	}

	private void WriteCht()
	{
		FileInfo fileInfo = new FileInfo(CHT_File);
		fileInfo.Directory.Create();
		File.WriteAllText(fileInfo.FullName, ((Control)TB_CheatCode).Text);
		SavedChanges = true;
	}

	private void B_Save_Click(object sender, EventArgs e)
	{
		WriteCht();
	}

	private void TB_CheatCode_TextChanged(object sender, EventArgs e)
	{
		SavedChanges = false;
	}

	private void CheatEditor_FormClosing(object sender, FormClosingEventArgs e)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		if (!SavedChanges && (int)MessageBox.Show(Translated.OpsCheatEditor_UnsavedChangesAlert, Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			WriteCht();
		}
		if (SavedChanges)
		{
			((Form)this).DialogResult = (DialogResult)6;
		}
		else
		{
			((Form)this).DialogResult = (DialogResult)7;
		}
	}

	private void Button1_Click(object sender, EventArgs e)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if ((int)MessageBox.Show(Translated.OpsCheatEditor_DeleteAlert, Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6 && File.Exists(CHT_File))
		{
			File.Delete(CHT_File);
			MessageBox.Show(Translated.OpsCheatEditor_DeleteConfirm, Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
			SavedChanges = true;
			((Form)this).Close();
		}
	}

	private void Lv_Codes_DoubleClick(object sender, EventArgs e)
	{
		string text = "";
		int lineFromCharIndex = ((TextBoxBase)TB_CheatCode).GetLineFromCharIndex(((TextBoxBase)TB_CheatCode).GetFirstCharIndexOfCurrentLine());
		if (((TextBoxBase)TB_CheatCode).Lines.Length != 0 && ((TextBoxBase)TB_CheatCode).Lines[lineFromCharIndex].Trim().Length > 0)
		{
			text = Environment.NewLine;
		}
		TB_CheatCode.Paste(text + "$" + Lv_Codes.SelectedItems[0].Text);
	}

	private void Link_CheatDocs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		CommonFuncs.OpenURL(((Control)Link_CheatDocs).Text);
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
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Expected O, but got Unknown
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Expected O, but got Unknown
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Expected O, but got Unknown
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsCheatEditor1));
		TB_CheatCode = new TextBox();
		B_Save = new Button();
		B_Delete = new Button();
		TB_FilePath = new TextBox();
		Lv_Codes = new ListView();
		ColumnHeader1 = new ColumnHeader();
		ColumnHeader2 = new ColumnHeader();
		SplitContainer1 = new SplitContainer();
		L_TipDoubleClick = new Label();
		Link_CheatDocs = new LinkLabel();
		((ISupportInitialize)SplitContainer1).BeginInit();
		((Control)SplitContainer1.Panel1).SuspendLayout();
		((Control)SplitContainer1.Panel2).SuspendLayout();
		((Control)SplitContainer1).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)TB_CheatCode).Anchor = (AnchorStyles)15;
		((Control)TB_CheatCode).Location = new Point(0, 0);
		((TextBoxBase)TB_CheatCode).Multiline = true;
		((Control)TB_CheatCode).Name = "TB_CheatCode";
		TB_CheatCode.ScrollBars = (ScrollBars)3;
		((Control)TB_CheatCode).Size = new Size(400, 332);
		((Control)TB_CheatCode).TabIndex = 0;
		((Control)B_Save).Anchor = (AnchorStyles)6;
		((Control)B_Save).Location = new Point(3, 335);
		((Control)B_Save).Name = "B_Save";
		((Control)B_Save).Size = new Size(135, 23);
		((Control)B_Save).TabIndex = 1;
		((Control)B_Save).Text = "Save";
		((ButtonBase)B_Save).UseVisualStyleBackColor = true;
		((Control)B_Save).Click += B_Save_Click;
		((Control)B_Delete).Anchor = (AnchorStyles)10;
		((Control)B_Delete).Location = new Point(262, 335);
		((Control)B_Delete).Name = "B_Delete";
		((Control)B_Delete).Size = new Size(135, 23);
		((Control)B_Delete).TabIndex = 2;
		((Control)B_Delete).Text = "Delete cheat file";
		((ButtonBase)B_Delete).UseVisualStyleBackColor = true;
		((Control)B_Delete).Click += Button1_Click;
		((Control)TB_FilePath).Anchor = (AnchorStyles)13;
		((Control)TB_FilePath).Location = new Point(13, 4);
		((Control)TB_FilePath).Name = "TB_FilePath";
		((TextBoxBase)TB_FilePath).ReadOnly = true;
		((Control)TB_FilePath).Size = new Size(802, 20);
		((Control)TB_FilePath).TabIndex = 3;
		((Control)Lv_Codes).Anchor = (AnchorStyles)15;
		Lv_Codes.Columns.AddRange((ColumnHeader[])(object)new ColumnHeader[2] { ColumnHeader1, ColumnHeader2 });
		Lv_Codes.FullRowSelect = true;
		Lv_Codes.GridLines = true;
		Lv_Codes.HeaderStyle = (ColumnHeaderStyle)1;
		((Control)Lv_Codes).Location = new Point(0, 0);
		Lv_Codes.MultiSelect = false;
		((Control)Lv_Codes).Name = "Lv_Codes";
		((Control)Lv_Codes).Size = new Size(396, 332);
		((Control)Lv_Codes).TabIndex = 4;
		Lv_Codes.UseCompatibleStateImageBehavior = false;
		Lv_Codes.View = (View)1;
		((Control)Lv_Codes).DoubleClick += Lv_Codes_DoubleClick;
		ColumnHeader1.Text = "Special Codes";
		ColumnHeader1.Width = 123;
		ColumnHeader2.Text = "Description";
		ColumnHeader2.Width = 246;
		((Control)SplitContainer1).Anchor = (AnchorStyles)15;
		((Control)SplitContainer1).Location = new Point(13, 30);
		((Control)SplitContainer1).Name = "SplitContainer1";
		((Control)SplitContainer1.Panel1).Controls.Add((Control)(object)TB_CheatCode);
		((Control)SplitContainer1.Panel1).Controls.Add((Control)(object)B_Save);
		((Control)SplitContainer1.Panel1).Controls.Add((Control)(object)B_Delete);
		SplitContainer1.Panel1MinSize = 280;
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)Lv_Codes);
		((Control)SplitContainer1.Panel2).Controls.Add((Control)(object)L_TipDoubleClick);
		SplitContainer1.Panel2MinSize = 392;
		((Control)SplitContainer1).Size = new Size(800, 361);
		SplitContainer1.SplitterDistance = 400;
		((Control)SplitContainer1).TabIndex = 5;
		((Control)L_TipDoubleClick).Anchor = (AnchorStyles)14;
		((Control)L_TipDoubleClick).Location = new Point(3, 335);
		((Control)L_TipDoubleClick).Name = "L_TipDoubleClick";
		((Control)L_TipDoubleClick).Size = new Size(391, 22);
		((Control)L_TipDoubleClick).TabIndex = 6;
		((Control)L_TipDoubleClick).Text = "Double click a code to add it to the cheat file.";
		L_TipDoubleClick.TextAlign = (ContentAlignment)512;
		((Control)Link_CheatDocs).Anchor = (AnchorStyles)14;
		((Control)Link_CheatDocs).Location = new Point(10, 394);
		((Control)Link_CheatDocs).Name = "Link_CheatDocs";
		((Control)Link_CheatDocs).Size = new Size(803, 23);
		((Control)Link_CheatDocs).TabIndex = 7;
		Link_CheatDocs.TabStop = true;
		((Control)Link_CheatDocs).Text = "https://bitbucket.org/ShaolinAssassin/popstarter-documentation-stuff/wiki/special-cheats";
		((Label)Link_CheatDocs).TextAlign = (ContentAlignment)512;
		Link_CheatDocs.LinkClicked += new LinkLabelLinkClickedEventHandler(Link_CheatDocs_LinkClicked);
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(823, 423);
		((Control)this).Controls.Add((Control)(object)Link_CheatDocs);
		((Control)this).Controls.Add((Control)(object)SplitContainer1);
		((Control)this).Controls.Add((Control)(object)TB_FilePath);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).MinimumSize = new Size(690, 330);
		((Control)this).Name = "OpsCheatEditor1";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Cheat Editor";
		((Form)this).FormClosing += new FormClosingEventHandler(CheatEditor_FormClosing);
		((Control)SplitContainer1.Panel1).ResumeLayout(false);
		((Control)SplitContainer1.Panel1).PerformLayout();
		((Control)SplitContainer1.Panel2).ResumeLayout(false);
		((ISupportInitialize)SplitContainer1).EndInit();
		((Control)SplitContainer1).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
