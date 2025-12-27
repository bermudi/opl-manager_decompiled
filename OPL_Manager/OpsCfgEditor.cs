using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OPL_Manager.My.Resources;
using OplManagerService;

namespace OPL_Manager;

public class OpsCfgEditor : BaseForm
{
	private ConfigClass CFG_File;

	private readonly OPLM_Main MainF = Program.MainFormInst;

	private Bitmap[] Stars = (Bitmap[])(object)new Bitmap[6];

	private int Current_Rating;

	private int Display_Rating;

	private IContainer components;

	internal ToolTip ToolTip1;

	internal TextBox TB_CfgPreview;

	internal OpenFileDialog OpenFileDialog1;

	internal GroupBox GB_Config;

	internal Button B_Next;

	internal Button B_Previous;

	internal Button B_Save;

	internal Button B_Set_Rating_Sys;

	internal ComboBox Parental_Select;

	internal PictureBox RatingBox;

	internal Label RatingText;

	internal PictureBox Parental_Img;

	internal Button Parental_Next;

	internal Button Parental_Prev;

	internal TableLayoutPanel TableOutter;

	internal TextBox TB_Title;

	internal Label L_title;

	internal Label L_Players;

	internal ComboBox ComboPlayers;

	internal Label L_Vmode;

	internal ComboBox ComboVmode;

	internal Label L_ID;

	internal TextBox TB_GameID;

	internal Label L_Genre;

	internal ComboBox Combo_Genre;

	internal Label L_Release;

	internal TextBox TB_Release;

	internal Label L_Developer;

	internal TextBox TB_Developer;

	internal Label L_Notes;

	internal TextBox TB_NOTES;

	internal ComboBox ComboAspectRatio;

	internal Label L_Aspect;

	internal Label L_Scan;

	internal ComboBox ComboScan;

	internal GroupBox GB_VMC;

	internal CheckBox CB_VMC1;

	internal CheckBox CB_VMC0;

	internal Button B_VMC1_New;

	internal Button B_VMC0_New;

	internal Label Label17;

	internal Label Label16;

	internal TextBox TB_VMC1;

	internal TextBox TB_VMC0;

	internal GroupBox GBox_Comp;

	internal CheckBox CB_Comp_8;

	internal CheckBox CB_Comp_7;

	internal CheckBox CB_Comp_6;

	internal CheckBox CB_Comp_5;

	internal CheckBox CB_Comp_4;

	internal CheckBox CB_Comp_3;

	internal CheckBox CB_Comp_2;

	internal CheckBox CB_Comp_1;

	internal GroupBox GB_Ratings;

	internal LinkLabel L_ThemeNotice;

	internal ComboBox ComboDMA;

	internal Label Label2;

	internal GroupBox GroupBox_CompDev;

	internal CheckBox CB_CompDev_USB;

	internal CheckBox CB_CompDev_ETH;

	internal CheckBox CB_CompDev_HDD;

	internal GroupBox GroupBox_GSM;

	internal NumericUpDown GSM_Y;

	internal Label GSM_L_XY;

	internal NumericUpDown GSM_X;

	internal ComboBox GSM_Vmode;

	internal Label GSM_L_Vmode;

	internal CheckBox GSM_Enabled;

	internal CheckBox GSM_SkipVideos;

	internal Button B_Edit;

	internal Label L_DescriptionSize;

	internal GroupBox GroupCheats;

	internal Label Cheats_L_CheatDev;

	internal ComboBox Combo_CheatDev;

	internal CheckBox Cheat_Enabled;

	internal Panel Panel1;

	internal Button B_Get_Title;

	internal Button B_VMC0_Browse;

	internal Button B_VMC1_Browse;

	internal GroupBox GBox_Description;

	internal TextBox TB_Description;

	internal GroupBox GroupGlobalSettings;

	internal RadioButton RadioButton3;

	internal RadioButton Source_GSM;

	internal Panel Panel_Global_GSM;

	internal Label Label3;

	internal Panel Panel_Global_Cheats;

	internal Label Label1;

	internal RadioButton RadioButton1;

	internal RadioButton Source_CHT;

	internal Panel Panel_Global_PadEmu;

	internal Label Label4;

	internal RadioButton RadioButton2;

	internal RadioButton Source_PAD;

	internal Label L_GlobalSettings_PerGame;

	internal Label L_GlobalSettings_Global;

	internal CheckBox GSM_FieldFix;

	public OpsCfgEditor()
	{
		InitializeComponent();
	}

	private void SetLanguage()
	{
		((Control)L_title).Text = Translated.OpsCfgEditor_Title;
		((Control)B_Get_Title).Text = Translated.CfgEditor_GetTitle;
		((Control)L_ID).Text = Translated.OpsCfgEditor_ID;
		((Control)L_Genre).Text = Translated.OpsCfgEditor_Genre;
		ObjectCollection items = Combo_Genre.Items;
		object[] array = new string[29]
		{
			"",
			Translated.OpsCfgEditor_Genre_Opt01,
			Translated.OpsCfgEditor_Genre_Opt02,
			Translated.OpsCfgEditor_Genre_Opt03,
			Translated.OpsCfgEditor_Genre_Opt04,
			Translated.OpsCfgEditor_Genre_Opt05,
			Translated.OpsCfgEditor_Genre_Opt06,
			Translated.OpsCfgEditor_Genre_Opt07,
			Translated.OpsCfgEditor_Genre_Opt08,
			Translated.OpsCfgEditor_Genre_Opt09,
			Translated.OpsCfgEditor_Genre_Opt10,
			Translated.OpsCfgEditor_Genre_Opt11,
			Translated.OpsCfgEditor_Genre_Opt12,
			Translated.OpsCfgEditor_Genre_Opt13,
			Translated.OpsCfgEditor_Genre_Opt14,
			Translated.OpsCfgEditor_Genre_Opt15,
			Translated.OpsCfgEditor_Genre_Opt16,
			Translated.OpsCfgEditor_Genre_Opt17,
			Translated.OpsCfgEditor_Genre_Opt18,
			Translated.OpsCfgEditor_Genre_Opt19,
			Translated.OpsCfgEditor_Genre_Opt20,
			Translated.OpsCfgEditor_Genre_Opt21,
			Translated.OpsCfgEditor_Genre_Opt22,
			Translated.OpsCfgEditor_Genre_Opt23,
			Translated.OpsCfgEditor_Genre_Opt24,
			Translated.OpsCfgEditor_Genre_Opt25,
			Translated.OpsCfgEditor_Genre_Opt26,
			Translated.OpsCfgEditor_Genre_Opt27,
			Translated.OpsCfgEditor_Genre_Opt28
		};
		items.AddRange(array);
		((Control)L_Players).Text = Translated.OpsCfgEditor_Players;
		((Control)L_Release).Text = Translated.OpsCfgEditor_Release;
		((Control)L_Vmode).Text = Translated.OpsCfgEditor_Vmode;
		((Control)L_Developer).Text = Translated.OpsCfgEditor_Developer;
		((Control)L_Aspect).Text = Translated.OpsCfgEditor_Aspect;
		((Control)L_Notes).Text = Translated.OpsCfgEditor_Notes;
		((Control)L_Scan).Text = Translated.OpsCfgEditor_Scan;
		((Control)Cheats_L_CheatDev).Text = Translated.OpsCfgEditorCheatDevice;
		((Control)GBox_Comp).Text = Translated.OpsCfgEditor_CompatibilityModes;
		((Control)CB_Comp_1).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 1";
		((Control)CB_Comp_2).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 2";
		((Control)CB_Comp_3).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 3";
		((Control)CB_Comp_4).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 4";
		((Control)CB_Comp_5).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 5";
		((Control)CB_Comp_6).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 6";
		((Control)CB_Comp_7).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 7";
		((Control)CB_Comp_8).Text = Translated.OpsCfgEditor_CompatibilityModes_Mode + " 8";
		((Control)GB_VMC).Text = Translated.OpsCfgEditor_Vmc;
		((Control)B_VMC0_New).Text = Translated.GLOBAL_NEW;
		((Control)B_VMC1_New).Text = Translated.GLOBAL_NEW;
		((Control)B_VMC0_Browse).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)B_VMC1_Browse).Text = Translated.GLOBAL_BUTTON_BROWSE;
		((Control)GroupBox_CompDev).Text = Translated.OpsCfgEditor_CompatibleDevices;
		((Control)GSM_Enabled).Text = Translated.OpsCfgEditorGsmEnable;
		((Control)GBox_Description).Text = Translated.OpsCfgEditor_Description;
		((Control)GSM_SkipVideos).Text = Translated.OpsCfgEditorGsmSkipVideos;
		((Control)GSM_FieldFix).Text = Translated.OpsCfgEditorGsmFieldFix;
		((Control)GSM_L_Vmode).Text = Translated.OpsCfgEditor_Vmode;
		((Control)GSM_L_XY).Text = Translated.OpsCfgEditorGsmAdjustXY;
		((Control)GroupCheats).Text = Translated.OpsCfgEditorCheats;
		((Control)Cheat_Enabled).Text = Translated.Global_Enable;
		((Control)Cheats_L_CheatDev).Text = Translated.OpsCfgEditorCheatDevice;
		((Control)GroupGlobalSettings).Text = Translated.OpsCfgEditorGlobalSettings;
		((Control)GroupGlobalSettings).Text = Translated.OpsCfgEditorGlobalSettings;
		((Control)L_GlobalSettings_PerGame).Text = Translated.OpsCfgEditorGlobalSettingsPerGame;
		((Control)L_GlobalSettings_Global).Text = Translated.OpsCfgEditorGlobalSettingsGlobal;
		((Control)GB_Ratings).Text = Translated.OpsCfgEditor_GameRating;
		((Control)B_Set_Rating_Sys).Text = Translated.OpsCfgEditor_RatingSet;
		((Control)GB_Config).Text = Translated.OpsCfgEditor_ConfigPreview;
		((Control)L_ThemeNotice).Text = Translated.OpsCfgEditor_ThemeReqNotice;
	}

	private void OpsCfgEditorShown(object sender, EventArgs e)
	{
		SetLanguage();
		((Control)this).Text = ((Control)this).Text + MainF.SelectedGame.Title;
		((Control)TB_GameID).Text = MainF.SelectedGame.ID;
		((Control)TB_Title).Text = MainF.SelectedGame.Title;
		((Control)B_Get_Title).Enabled = (MainF.SelectedGame.Type == GameType.PS2) | (MainF.SelectedGame.Type == GameType.POPS);
		((Control)GBox_Comp).Enabled = MainF.SelectedGame.Type == GameType.PS2;
		((Control)GB_VMC).Enabled = MainF.SelectedGame.Type == GameType.PS2;
		((Control)GroupBox_GSM).Enabled = MainF.SelectedGame.Type == GameType.PS2;
		ComboSetUp_Scan();
		ComboSetUp_Players();
		ComboSetUp_Parental();
		ComboSetUp_Aspect();
		ComboSetUp_Vmode();
		ComboSetUp_DMA();
		ComboSetUp_GSM();
		StartRating();
		LoadCFGFile();
		InstallHandlers();
	}

	private void LoadCFGFile()
	{
		_ = MainF.SelectedGame;
		string expectedCFGFilePath = MainF.SelectedGame.ExpectedCFGFilePath;
		TextBox textboxxx = TB_CfgPreview;
		CFG_File = new ConfigClass(expectedCFGFilePath, skipCheck: true, ref textboxxx);
		TB_CfgPreview = textboxxx;
		if (!string.IsNullOrEmpty(CFG_File.GetValue("$Compatibility=")))
		{
			string text = Convert.ToString(int.Parse(CFG_File.GetValue("$Compatibility=")), 2).PadLeft(8, '0');
			int num = 0;
			CheckBox[] array = (CheckBox[])(object)new CheckBox[8] { CB_Comp_8, CB_Comp_7, CB_Comp_6, CB_Comp_5, CB_Comp_4, CB_Comp_3, CB_Comp_2, CB_Comp_1 };
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Checked = text[num] != '0';
				num++;
			}
		}
		string text2 = CFG_File.GetValue("Device=").Trim().ToLower();
		if (!string.IsNullOrEmpty(text2))
		{
			CB_CompDev_USB.Checked = false;
			CB_CompDev_ETH.Checked = false;
			CB_CompDev_HDD.Checked = false;
			switch (text2)
			{
			case "device/1":
				CB_CompDev_USB.Checked = true;
				break;
			case "device/2":
				CB_CompDev_USB.Checked = true;
				CB_CompDev_ETH.Checked = true;
				break;
			case "device/3":
				CB_CompDev_USB.Checked = true;
				CB_CompDev_HDD.Checked = true;
				break;
			case "device/4":
				CB_CompDev_ETH.Checked = true;
				CB_CompDev_HDD.Checked = true;
				break;
			case "device/5":
				CB_CompDev_ETH.Checked = true;
				break;
			case "device/6":
				CB_CompDev_HDD.Checked = true;
				break;
			case "device/all":
				CB_CompDev_USB.Checked = true;
				CB_CompDev_ETH.Checked = true;
				CB_CompDev_HDD.Checked = true;
				break;
			}
		}
		if (!string.IsNullOrEmpty(CFG_File.GetValue("Title=")))
		{
			((Control)TB_Title).Text = CFG_File.GetValue("Title=");
		}
		else
		{
			UpdateTextBox(TB_Title, null);
		}
		string text3 = CFG_File.GetValue("Rating=").ToLower();
		if (!string.IsNullOrEmpty(text3))
		{
			Stars_Load(int.Parse(text3.Replace("rating/", "")));
		}
		((Control)Combo_Genre).Text = CFG_File.GetValue("Genre=");
		((Control)TB_Release).Text = CFG_File.GetValue("Release=");
		ComboBox combobox = ComboVmode;
		ComboBoxSelectMatch(ref combobox, CFG_File.GetValue("Vmode=").ToLower());
		ComboVmode = combobox;
		ComboBox combobox2 = ComboPlayers;
		ComboBoxSelectMatch(ref combobox2, CFG_File.GetValue("Players=").ToLower());
		ComboPlayers = combobox2;
		ComboBox combobox3 = ComboScan;
		ComboBoxSelectMatch(ref combobox3, CFG_File.GetValue("Scan=").ToLower());
		ComboScan = combobox3;
		ComboBox combobox4 = ComboAspectRatio;
		ComboBoxSelectMatch(ref combobox4, CFG_File.GetValue("Aspect=").ToLower());
		ComboAspectRatio = combobox4;
		((Control)TB_Developer).Text = CFG_File.GetValue("Developer=");
		((Control)TB_NOTES).Text = CFG_File.GetValue("NOTES=");
		((Control)TB_Description).Text = CFG_File.GetValue("Description=").Trim();
		string text4 = CFG_File.GetValue("Parental=").ToLower();
		if (!string.IsNullOrEmpty(text4))
		{
			int num2 = 0;
			bool flag = false;
			foreach (KeyValuePair<string, List<GameRatingClass>> staticRating in ConfigClass.StaticRatings)
			{
				int num3 = 0;
				bool flag2 = false;
				foreach (GameRatingClass item in staticRating.Value)
				{
					if ((item.Value ?? "") == (text4 ?? ""))
					{
						Parental_Img.Image = (Image)(object)item.Icon;
						((ListControl)Parental_Select).SelectedIndex = num2;
						((Control)Parental_Img).Tag = new GameRatingTag(num3, staticRating.Key);
						flag2 = true;
						break;
					}
					num3++;
				}
				if (flag2)
				{
					break;
				}
				num2++;
			}
		}
		if (!string.IsNullOrEmpty(CFG_File.GetValue("$VMC_0=")))
		{
			((Control)TB_VMC0).Enabled = true;
			((Control)B_VMC0_New).Enabled = true;
			CB_VMC0.Checked = true;
			((Control)TB_VMC0).Text = CFG_File.GetValue("$VMC_0=");
			CB_VMC0_Click(null, null);
		}
		if (!string.IsNullOrEmpty(CFG_File.GetValue("$VMC_1=")))
		{
			((Control)TB_VMC1).Enabled = true;
			((Control)B_VMC1_New).Enabled = true;
			CB_VMC1.Checked = true;
			((Control)TB_VMC1).Text = CFG_File.GetValue("$VMC_1=");
			CB_VMC1_Click(null, null);
		}
		if (!CB_CompDev_HDD.Checked)
		{
			((Control)ComboDMA).Enabled = false;
			ComboBox combobox5 = ComboDMA;
			ComboBoxSelectMatch(ref combobox5, "");
			ComboDMA = combobox5;
		}
		else
		{
			ComboBox combobox6 = ComboDMA;
			ComboBoxSelectMatch(ref combobox6, CFG_File.GetValue("$DMA="));
			ComboDMA = combobox6;
		}
		GSM_Enabled.Checked = CFG_File.GetValue("$EnableGSM=").Trim().ToLower() == "1";
		GSM_X.Value = CFG_File.GetValueInt("$GSMXOffset=");
		GSM_Y.Value = CFG_File.GetValueInt("$GSMYOffset=");
		ComboBox combobox7 = GSM_Vmode;
		ComboBoxSelectMatch(ref combobox7, CFG_File.GetValue("$GSMVMode="));
		GSM_Vmode = combobox7;
		GSM_SkipVideos.Checked = CFG_File.GetValue("$GSMSkipVideos=").Trim().ToLower() == "1";
		GSM_FieldFix.Checked = CFG_File.GetValue(((Control)GSM_FieldFix).Tag.ToString()).Trim().ToLower() == "1";
		((Control)GSM_X).Enabled = GSM_Enabled.Checked;
		((Control)GSM_Y).Enabled = GSM_Enabled.Checked;
		((Control)GSM_Vmode).Enabled = GSM_Enabled.Checked;
		((Control)GSM_SkipVideos).Enabled = GSM_Enabled.Checked;
		((Control)GSM_FieldFix).Enabled = GSM_Enabled.Checked;
		Cheat_Enabled.Checked = CFG_File.GetValue("$EnableCheat=").Trim().ToLower() == "1";
		((Control)Combo_CheatDev).Text = CFG_File.GetValue("Cheat=");
		((Control)Combo_CheatDev).Enabled = Cheat_Enabled.Checked;
		Source_CHT.Checked = CFG_File.GetValue(((Control)Source_CHT).Tag.ToString()).Trim().ToLower() == "1";
		Source_GSM.Checked = CFG_File.GetValue(((Control)Source_GSM).Tag.ToString()).Trim().ToLower() == "1";
		Source_PAD.Checked = CFG_File.GetValue(((Control)Source_PAD).Tag.ToString()).Trim().ToLower() == "1";
	}

	private void ComboBoxSelectMatch(ref ComboBox combobox, string value)
	{
		value = value.ToLower();
		List<ComboCfgDescValTextClass> list = (List<ComboCfgDescValTextClass>)combobox.DataSource;
		int i = 0;
		for (int num = list.Count - 1; i <= num; i++)
		{
			if ((list[i].Value.ToLower() ?? "") == (value ?? ""))
			{
				((ListControl)combobox).SelectedIndex = i;
				break;
			}
		}
	}

	private void Stars_Load(int rate)
	{
		Current_Rating = rate;
		RatingBox.Image = (Image)(object)Stars[rate];
		((Control)RatingText).Text = rate.ToString();
	}

	private void StartRating()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		for (int i = 0; i <= 5; i++)
		{
			Stars[i] = (Bitmap)DrawRateBlock(i);
		}
		((Control)RatingBox).Size = new Size(160, 32);
		RatingBox.SizeMode = (PictureBoxSizeMode)1;
		((Control)RatingBox).Cursor = Cursors.Hand;
		RatingBox.BorderStyle = (BorderStyle)0;
		RatingBox.Image = (Image)(object)Stars[0];
	}

	public Image DrawRateBlock(int Rating)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		Bitmap star_xxl = Resources.star_xxl;
		Bitmap star_xxl_ = Resources.star_xxl_2;
		Bitmap val = new Bitmap(((Image)star_xxl).Width * 5, ((Image)star_xxl).Height);
		Graphics val2 = Graphics.FromImage((Image)(object)val);
		int i = 0;
		for (int num = Rating - 1; i <= num; i++)
		{
			val2.DrawImage((Image)(object)star_xxl, ((Image)star_xxl).Width * i, 0);
			val2.Flush();
		}
		for (int j = Rating; j <= 4; j++)
		{
			val2.DrawImage((Image)(object)star_xxl_, ((Image)star_xxl).Width * j, 0);
			val2.Flush();
		}
		val2.Dispose();
		return (Image)(object)val;
	}

	private void RatingBox_MouseClick(object sender, MouseEventArgs e)
	{
		Current_Rating = e.Location.X / (int)Math.Round((double)((Control)RatingBox).Width / 5.0) + 1;
		((Control)RatingText).Text = Current_Rating.ToString();
		CFG_File.SetValue("Rating=", "rating/" + Current_Rating);
		CFG_File.SetValue("RatingText=", Current_Rating.ToString());
	}

	private void RatingBox_MouseLeave(object sender, EventArgs e)
	{
		RatingBox.Image = (Image)(object)Stars[Current_Rating];
		Display_Rating = Current_Rating;
	}

	private void RatingBox_MouseMove(object sender, MouseEventArgs e)
	{
		int num = e.Location.X / (int)Math.Round((double)((Control)RatingBox).Width / 5.0) + 1;
		if (Display_Rating != num)
		{
			Display_Rating = num;
			RatingBox.Image = (Image)(object)Stars[num];
		}
	}

	private void ComboSetUp_Parental()
	{
		foreach (KeyValuePair<string, List<GameRatingClass>> staticRating in ConfigClass.StaticRatings)
		{
			Parental_Select.Items.Add((object)staticRating.Key);
		}
		((ListControl)Parental_Select).SelectedIndex = 0;
		string text = OplmSettings.Read("DefaultRating", "ESRB");
		Parental_Img.Image = (Image)(object)ConfigClass.StaticRatings[text][0].Icon;
		((Control)Parental_Img).Tag = new GameRatingTag(0, text);
		Parental_Select.SelectedItem = text;
	}

	private void Parental_Select_SelectedIndexChanged(object sender, EventArgs e)
	{
		string text = Parental_Select.SelectedItem.ToString();
		if (ConfigClass.StaticRatings.ContainsKey(text))
		{
			Parental_Img.Image = (Image)(object)ConfigClass.StaticRatings[text][0].Icon;
			((Control)Parental_Img).Tag = new GameRatingTag(0, text);
			CFG_File.SetValue("Parental=", ConfigClass.StaticRatings[text][0].Value);
			CFG_File.SetValue("ParentalText=", ConfigClass.StaticRatings[text][0].ValueText);
		}
	}

	private void ParentalUpDown(object sender, EventArgs e)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		GameRatingTag gameRatingTag = (GameRatingTag)((Control)Parental_Img).Tag;
		if (((Control)(Button)sender).Name == "Parental_Next")
		{
			gameRatingTag.IncreaseCurrent();
		}
		else
		{
			gameRatingTag.DecreaseCurrent();
		}
		Parental_Img.Image = (Image)(object)gameRatingTag.SelectedItem().Icon;
		CFG_File.SetValue("Parental=", gameRatingTag.SelectedItem().Value);
		CFG_File.SetValue("ParentalText=", gameRatingTag.SelectedItem().ValueText);
	}

	private void ComboSetUp_Scan()
	{
		ComboScan.DropDown += AdjustWidthComboBox_DropDown;
		ComboScan.DataSource = ConfigClass.StaticScan;
		((ListControl)ComboScan).DisplayMember = "Description";
		((ListControl)ComboScan).ValueMember = "Value";
	}

	private void ComboSetUp_DMA()
	{
		ComboDMA.DropDown += AdjustWidthComboBox_DropDown;
		ComboDMA.DataSource = ConfigClass.StaticDma;
		((ListControl)ComboDMA).DisplayMember = "Description";
		((ListControl)ComboDMA).ValueMember = "Value";
	}

	private void ComboSetUp_GSM()
	{
		GSM_Vmode.DropDown += AdjustWidthComboBox_DropDown;
		GSM_Vmode.DataSource = ConfigClass.StaticGsm;
		((ListControl)GSM_Vmode).DisplayMember = "Description";
		((ListControl)GSM_Vmode).ValueMember = "Value";
	}

	private void ComboSetUp_Aspect()
	{
		ComboAspectRatio.DataSource = ConfigClass.StaticAspect;
		((ListControl)ComboAspectRatio).DisplayMember = "Description";
		((ListControl)ComboAspectRatio).ValueMember = "Value";
	}

	private void ComboSetUp_Vmode()
	{
		ComboVmode.DataSource = ConfigClass.StaticVmode;
		((ListControl)ComboVmode).DisplayMember = "Description";
		((ListControl)ComboVmode).ValueMember = "Value";
	}

	private void ComboSetUp_Players()
	{
		ComboPlayers.DataSource = ConfigClass.StaticPlayers;
		((ListControl)ComboPlayers).DisplayMember = "Description";
		((ListControl)ComboPlayers).ValueMember = "Value";
	}

	private void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		ComboBox val = (ComboBox)sender;
		int num = val.DropDownWidth;
		Graphics val2 = ((Control)val).CreateGraphics();
		Font font = ((Control)val).Font;
		int num2 = ((val.Items.Count > val.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0);
		foreach (ComboCfgDescValTextClass item in ((ComboBox)sender).Items)
		{
			int num3 = (int)Math.Round(val2.MeasureString(item.Description.Trim(), font).Width) + num2;
			if (num < num3)
			{
				num = num3;
			}
		}
		val.DropDownWidth = num;
	}

	private void InstallHandlers()
	{
		((Control)TB_Title).TextChanged += UpdateTextBox;
		((Control)TB_Release).TextChanged += UpdateTextBox;
		((Control)TB_Developer).TextChanged += UpdateTextBox;
		((Control)TB_NOTES).TextChanged += UpdateTextBox;
		((Control)TB_Description).TextChanged += UpdateTextBox;
		ComboPlayers.SelectedIndexChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateComboBoxNormal((ComboBox)sender);
		};
		ComboVmode.SelectedIndexChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateComboBoxNormal((ComboBox)sender);
		};
		ComboAspectRatio.SelectedIndexChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateComboBoxNormal((ComboBox)sender);
		};
		ComboScan.SelectedIndexChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateComboBoxNormal((ComboBox)sender);
		};
		((Control)Combo_Genre).TextChanged += delegate(object? CB, EventArgs e)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			CFG_File.SetValue(((Control)(ComboBox)CB).Tag.ToString(), ((Control)(ComboBox)CB).Text);
		};
		((Control)Combo_CheatDev).TextChanged += delegate(object? CB, EventArgs e)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			CFG_File.SetValue(((Control)(ComboBox)CB).Tag.ToString(), ((Control)(ComboBox)CB).Text);
		};
		ComboDMA.SelectedIndexChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateComboBoxNormal((ComboBox)sender);
		};
		GSM_Vmode.SelectedIndexChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateComboBoxNormal((ComboBox)sender);
		};
		Parental_Select.SelectedIndexChanged += Parental_Select_SelectedIndexChanged;
		((Control)Parental_Next).Click += ParentalUpDown;
		((Control)Parental_Prev).Click += ParentalUpDown;
		CompatibilityOptionsChecked(updateCfg: false);
		CheckBox[] array = (CheckBox[])(object)new CheckBox[8] { CB_Comp_1, CB_Comp_2, CB_Comp_3, CB_Comp_4, CB_Comp_5, CB_Comp_6, CB_Comp_7, CB_Comp_8 };
		for (int num = 0; num < array.Length; num++)
		{
			array[num].CheckedChanged += delegate
			{
				CompatibilityOptionsChecked(updateCfg: true);
			};
		}
		CompDevOptionsChecked(updateCfg: false);
		array = (CheckBox[])(object)new CheckBox[3] { CB_CompDev_ETH, CB_CompDev_HDD, CB_CompDev_USB };
		for (int num = 0; num < array.Length; num++)
		{
			array[num].CheckedChanged += delegate
			{
				CompDevOptionsChecked(updateCfg: true);
			};
		}
		GSM_Enabled.CheckedChanged += GSM_Enabled_CheckedChanged;
		GSM_X.ValueChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateGsmNUD((NumericUpDown)sender);
		};
		GSM_Y.ValueChanged += delegate(object? sender, EventArgs e)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			UpdateGsmNUD((NumericUpDown)sender);
		};
		GSM_SkipVideos.CheckedChanged += CB_Generic_CheckedChanged;
		GSM_FieldFix.CheckedChanged += CB_Generic_CheckedChanged;
		((Control)TB_VMC0).TextChanged += delegate
		{
			CFG_File.SetValue("$VMC_0=", ((Control)TB_VMC0).Text);
		};
		((Control)TB_VMC1).TextChanged += delegate
		{
			CFG_File.SetValue("$VMC_1=", ((Control)TB_VMC1).Text);
		};
		Cheat_Enabled.CheckedChanged += Cheat_Enabled_CheckedChanged;
		Source_CHT.CheckedChanged += CB_Generic_CheckedChanged;
		Source_GSM.CheckedChanged += CB_Generic_CheckedChanged;
		Source_PAD.CheckedChanged += CB_Generic_CheckedChanged;
	}

	private void Ctrl_CheckedChanged(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	private void UpdateComboBoxNormal(ComboBox CB)
	{
		ComboCfgDescValTextClass comboCfgDescValTextClass = (ComboCfgDescValTextClass)CB.SelectedItem;
		string text = ((Control)CB).Tag.ToString();
		CFG_File.SetValue(text, comboCfgDescValTextClass.Value);
		CFG_File.SetValue(text.Insert(text.Length - 1, "Text"), comboCfgDescValTextClass.ValueText);
	}

	private void UpdateTextBox(object sender, EventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		TextBox val = (TextBox)sender;
		CFG_File.SetValue(((Control)val).Tag.ToString(), ((Control)val).Text.Trim().Replace(Environment.NewLine, " "));
	}

	private void UpdateGsmNUD(NumericUpDown sender)
	{
		CFG_File.SetValue(((Control)sender).Tag.ToString(), (sender.Value == 0m) ? "" : sender.Value.ToString());
	}

	private int CompatibilityOptionsChecked(bool updateCfg)
	{
		int num = 0;
		int num2 = 0;
		List<string> list = new List<string>();
		CheckBox[] array = (CheckBox[])(object)new CheckBox[8] { CB_Comp_1, CB_Comp_2, CB_Comp_3, CB_Comp_4, CB_Comp_5, CB_Comp_6, CB_Comp_7, CB_Comp_8 };
		foreach (CheckBox val in array)
		{
			if (val.Checked)
			{
				((Control)val).ForeColor = Color.Green;
				num2 = (int)Math.Round((double)num2 + Math.Pow(2.0, num));
				list.Add((num + 1).ToString());
			}
			else
			{
				((Control)val).ForeColor = Color.Black;
			}
			num++;
		}
		string val2 = ((num2 != 0) ? num2.ToString() : "");
		if (updateCfg)
		{
			CFG_File.SetValue("$Compatibility=", val2);
			if (list.Count > 0)
			{
				CFG_File.SetValue("Modes=", string.Join("+", list));
			}
			else
			{
				CFG_File.SetValue("Modes=", "");
			}
		}
		return num2;
	}

	private void CompDevOptionsChecked(bool updateCfg)
	{
		string val = "";
		if (CB_CompDev_USB.Checked & !CB_CompDev_ETH.Checked & !CB_CompDev_HDD.Checked)
		{
			val = "device/1";
		}
		else if (CB_CompDev_USB.Checked & CB_CompDev_ETH.Checked & !CB_CompDev_HDD.Checked)
		{
			val = "device/2";
		}
		else if (CB_CompDev_USB.Checked & !CB_CompDev_ETH.Checked & CB_CompDev_HDD.Checked)
		{
			val = "device/3";
		}
		else if (!CB_CompDev_USB.Checked & CB_CompDev_ETH.Checked & CB_CompDev_HDD.Checked)
		{
			val = "device/4";
		}
		else if (!CB_CompDev_USB.Checked & CB_CompDev_ETH.Checked & !CB_CompDev_HDD.Checked)
		{
			val = "device/5";
		}
		else if (!CB_CompDev_USB.Checked & !CB_CompDev_ETH.Checked & CB_CompDev_HDD.Checked)
		{
			val = "device/6";
		}
		else if (CB_CompDev_USB.Checked & CB_CompDev_ETH.Checked & CB_CompDev_HDD.Checked)
		{
			val = "device/all";
		}
		if (updateCfg)
		{
			CFG_File.SetValue("Device=", val);
			CFG_File.SetValue("DeviceText=", ConfigClass.StaticDevice.FirstOrDefault((KeyValuePair<string, string> x) => (x.Key ?? "") == (val ?? "")).Value);
		}
		CheckBox[] array = (CheckBox[])(object)new CheckBox[3] { CB_CompDev_HDD, CB_CompDev_ETH, CB_CompDev_USB };
		foreach (CheckBox val2 in array)
		{
			if (val2.Checked)
			{
				((Control)val2).ForeColor = Color.Green;
			}
			else
			{
				((Control)val2).ForeColor = Color.Black;
			}
		}
		((Control)ComboDMA).Enabled = CB_CompDev_HDD.Checked;
		if (!CB_CompDev_HDD.Checked)
		{
			ComboBox combobox = ComboDMA;
			ComboBoxSelectMatch(ref combobox, "");
			ComboDMA = combobox;
		}
	}

	private void B_SAVE_Click(object sender, EventArgs e)
	{
		if (CB_VMC0.Checked)
		{
			CheckVMC(((Control)TB_VMC0).Text);
			TB_VMCx_TextChanged(TB_VMC0, null);
		}
		if (CB_VMC1.Checked)
		{
			CheckVMC(((Control)TB_VMC1).Text);
			TB_VMCx_TextChanged(TB_VMC1, null);
		}
		CFG_File.WriteCFG();
	}

	private void CheckVMC(string name)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Invalid comparison between Unknown and I4
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		string text = name + ".bin";
		if (!File.Exists(OplFolders.VMC + text) && (int)MessageBox.Show(string.Format(Translated.OpsCfgEditor_CreateVMC, text), Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			OpsCfgEditorVmcCreate opsCfgEditorVmcCreate = new OpsCfgEditorVmcCreate();
			try
			{
				((Control)opsCfgEditorVmcCreate.TbName).Text = name;
				((TextBoxBase)opsCfgEditorVmcCreate.TbName).ReadOnly = true;
				((Form)opsCfgEditorVmcCreate).ShowDialog();
			}
			finally
			{
				((IDisposable)opsCfgEditorVmcCreate)?.Dispose();
			}
		}
	}

	private void CFG_Editor_FormClosing(object sender, FormClosingEventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		if (!(((int)((Form)this).DialogResult == 7) | ((int)((Form)this).DialogResult == 6)))
		{
			PromptSave();
		}
	}

	private void Button2_Click(object sender, EventArgs e)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Invalid comparison between Unknown and I4
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if ((int)MessageBox.Show(string.Format(Translated.OpsCfgEditor_DefaultRatingSystemPrompt, Parental_Select.SelectedItem.ToString()), Translated.OpsCfgEditor_DefaultRatingSystemPromptTitle, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			OplmSettings.Write("DefaultRating", Parental_Select.SelectedItem.ToString());
			MessageBox.Show(string.Format(Translated.OpsCfgEditor_DefaultRatingSystemPromptSucess, Parental_Select.SelectedItem.ToString()), Translated.Global_Information, (MessageBoxButtons)0, (MessageBoxIcon)64);
		}
	}

	private void B_Previous_Click(object sender, EventArgs e)
	{
		PromptSave();
		((Form)this).DialogResult = (DialogResult)7;
		((Form)this).Close();
	}

	private void B_Next_Click(object sender, EventArgs e)
	{
		PromptSave();
		((Form)this).DialogResult = (DialogResult)6;
		((Form)this).Close();
	}

	private void PromptSave()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Invalid comparison between Unknown and I4
		if (CFG_File.Changed && (int)MessageBox.Show(Translated.Global_SaveChanges, Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			CFG_File.WriteCFG();
		}
	}

	private void B_VMC0_Generic_Click(object sender, EventArgs e)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Invalid comparison between Unknown and I4
		OpsCfgEditorVmcCreate opsCfgEditorVmcCreate = new OpsCfgEditorVmcCreate();
		try
		{
			((Control)opsCfgEditorVmcCreate.TbName).Text = ((Control)TB_GameID).Text + "_0";
			if ((int)((Form)opsCfgEditorVmcCreate).ShowDialog() == 1)
			{
				((Control)TB_VMC0).Text = opsCfgEditorVmcCreate.resultVmcFile;
			}
		}
		finally
		{
			((IDisposable)opsCfgEditorVmcCreate)?.Dispose();
		}
	}

	private void B_VMC1_Generic_Click(object sender, EventArgs e)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Invalid comparison between Unknown and I4
		OpsCfgEditorVmcCreate opsCfgEditorVmcCreate = new OpsCfgEditorVmcCreate();
		try
		{
			((Control)opsCfgEditorVmcCreate.TbName).Text = ((Control)TB_GameID).Text + "_1";
			if ((int)((Form)opsCfgEditorVmcCreate).ShowDialog() == 1)
			{
				((Control)TB_VMC1).Text = opsCfgEditorVmcCreate.resultVmcFile;
			}
		}
		finally
		{
			((IDisposable)opsCfgEditorVmcCreate)?.Dispose();
		}
	}

	private void B_VMC0_Browse_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		OpsCfgEditorVmcBrowse opsCfgEditorVmcBrowse = new OpsCfgEditorVmcBrowse();
		try
		{
			if ((int)((Form)opsCfgEditorVmcBrowse).ShowDialog() == 1)
			{
				((Control)TB_VMC0).Text = opsCfgEditorVmcBrowse.selectedFile;
			}
		}
		finally
		{
			((IDisposable)opsCfgEditorVmcBrowse)?.Dispose();
		}
	}

	private void B_VMC1_Browse_Click(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		OpsCfgEditorVmcBrowse opsCfgEditorVmcBrowse = new OpsCfgEditorVmcBrowse();
		try
		{
			if ((int)((Form)opsCfgEditorVmcBrowse).ShowDialog() == 1)
			{
				((Control)TB_VMC1).Text = opsCfgEditorVmcBrowse.selectedFile;
			}
		}
		finally
		{
			((IDisposable)opsCfgEditorVmcBrowse)?.Dispose();
		}
	}

	private void TB_VMCx_TextChanged(object sender, EventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		TextBox val = (TextBox)sender;
		if (((Control)val).Enabled && ((Control)val).Text.Trim().Length > 0)
		{
			((Control)val).BackColor = (File.Exists(Path.Combine(OplFolders.VMC, ((Control)val).Text + ".bin")) ? Color.Green : Color.Red);
		}
	}

	private void TB_VMCx_EnabledChanged(object sender, EventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		TextBox val = (TextBox)sender;
		if (!((Control)val).Enabled)
		{
			((Control)val).BackColor = Control.DefaultBackColor;
		}
	}

	private void CB_VMC0_Click(object sender, EventArgs e)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Invalid comparison between Unknown and I4
		if (sender != null && !CB_VMC0.Checked)
		{
			string path = Path.Combine(OplFolders.VMC, ((Control)TB_VMC0).Text + ".bin");
			if (File.Exists(path) && (int)MessageBox.Show(Translated.OpsCfgEditor_VmcDeleteQuestion + Environment.NewLine + Path.GetFileName(path), Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
			{
				File.Delete(path);
			}
		}
		if (CB_VMC0.Checked)
		{
			((Control)TB_VMC0).Enabled = true;
			((Control)B_VMC0_New).Enabled = true;
			((Control)B_VMC0_Browse).Enabled = true;
		}
		else
		{
			((Control)TB_VMC0).Text = "";
			((Control)TB_VMC0).Enabled = false;
			((Control)B_VMC0_New).Enabled = false;
			((Control)B_VMC0_Browse).Enabled = false;
		}
	}

	private void CB_VMC1_Click(object sender, EventArgs e)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Invalid comparison between Unknown and I4
		if (sender != null && !CB_VMC1.Checked)
		{
			string path = Path.Combine(OplFolders.VMC, ((Control)TB_VMC1).Text + ".bin");
			if (File.Exists(path) && (int)MessageBox.Show(Translated.OpsCfgEditor_VmcDeleteQuestion + Environment.NewLine + Path.GetFileName(path), Translated.Global_Question, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
			{
				File.Delete(path);
			}
		}
		if (CB_VMC1.Checked)
		{
			((Control)TB_VMC1).Enabled = true;
			((Control)B_VMC1_New).Enabled = true;
			((Control)B_VMC1_Browse).Enabled = true;
		}
		else
		{
			((Control)TB_VMC1).Text = "";
			((Control)TB_VMC1).Enabled = false;
			((Control)B_VMC1_New).Enabled = false;
			((Control)B_VMC1_Browse).Enabled = false;
		}
	}

	private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		CommonFuncs.OpenURL("http://www.ps2-home.com/forum/viewtopic.php?f=13&t=189");
	}

	private void GSM_Enabled_CheckedChanged(object sender, EventArgs e)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Invalid comparison between Unknown and I4
		if (!GSM_Enabled.Checked && (int)MessageBox.Show(Translated.OpsCfgEditor_GsmDisableDelete, Translated.OpsCfgEditor_GsmDisable, (MessageBoxButtons)4, (MessageBoxIcon)32) == 6)
		{
			CFG_File.SetValue(((Control)GSM_X).Tag.ToString(), "");
			CFG_File.SetValue(((Control)GSM_Y).Tag.ToString(), "");
			CFG_File.SetValue(((Control)GSM_Vmode).Tag.ToString(), "");
			CFG_File.SetValue(((Control)GSM_SkipVideos).Tag.ToString(), "");
			GSM_X.Value = 0m;
			GSM_Y.Value = 0m;
			((ListControl)GSM_Vmode).SelectedIndex = 0;
			GSM_SkipVideos.Checked = false;
			GSM_FieldFix.Checked = false;
		}
		((Control)GSM_X).Enabled = GSM_Enabled.Checked;
		((Control)GSM_Y).Enabled = GSM_Enabled.Checked;
		((Control)GSM_Vmode).Enabled = GSM_Enabled.Checked;
		((Control)GSM_SkipVideos).Enabled = GSM_Enabled.Checked;
		((Control)GSM_FieldFix).Enabled = GSM_Enabled.Checked;
		CFG_File.SetValue(((Control)GSM_Enabled).Tag.ToString(), GSM_Enabled.Checked ? "1" : "");
		CFG_File.SetValue("$GSMSource=", GSM_Enabled.Checked ? "1" : "");
		if (GSM_Enabled.Checked && !Source_GSM.Checked)
		{
			Source_GSM.Checked = true;
		}
	}

	private void CB_Generic_CheckedChanged(object sender, EventArgs e)
	{
		CFG_File.SetValue(((dynamic)sender).Tag.ToString(), (((dynamic)sender).Checked) ? "1" : "");
	}

	private void Cheat_Enabled_CheckedChanged(object sender, EventArgs e)
	{
		((Control)Combo_CheatDev).Enabled = Cheat_Enabled.Checked;
		CFG_File.SetValue(((Control)Cheat_Enabled).Tag.ToString(), Cheat_Enabled.Checked ? "1" : "");
	}

	private void B_Edit_Click(object sender, EventArgs e)
	{
		if (((TextBoxBase)TB_CfgPreview).ReadOnly)
		{
			((TextBoxBase)TB_CfgPreview).ReadOnly = false;
			return;
		}
		((TextBoxBase)TB_CfgPreview).ReadOnly = true;
		CFG_File.WriteFromTextBoxAndSave();
		((Form)this).DialogResult = (DialogResult)4;
		((Form)this).Close();
	}

	private void TB_Description_TextChanged(object sender, EventArgs e)
	{
		((Control)L_DescriptionSize).Text = ((TextBoxBase)TB_Description).TextLength + "/" + ((TextBoxBase)TB_Description).MaxLength;
	}

	private void B_Get_Title_Click(object sender, EventArgs e)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		string gameNameById = CommonFuncs.SoapAPI.GetGameNameById(MainF.SelectedGame.Type, MainF.SelectedGame.ID);
		if (!string.IsNullOrEmpty(gameNameById))
		{
			MessageBox.Show(Translated.CfgEditor_GetTitleSuccess + Environment.NewLine + gameNameById, Translated.Global_Information);
			((Control)TB_Title).Text = gameNameById;
		}
		else
		{
			MessageBox.Show(Translated.CfgEditor_GetTitleError, Translated.Global_Information);
			((Control)B_Get_Title).Enabled = false;
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
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Expected O, but got Unknown
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Expected O, but got Unknown
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected O, but got Unknown
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Expected O, but got Unknown
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Expected O, but got Unknown
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Expected O, but got Unknown
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Expected O, but got Unknown
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Expected O, but got Unknown
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Expected O, but got Unknown
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Expected O, but got Unknown
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Expected O, but got Unknown
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Expected O, but got Unknown
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Expected O, but got Unknown
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Expected O, but got Unknown
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Expected O, but got Unknown
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Expected O, but got Unknown
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Expected O, but got Unknown
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Expected O, but got Unknown
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Expected O, but got Unknown
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Expected O, but got Unknown
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Expected O, but got Unknown
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Expected O, but got Unknown
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Expected O, but got Unknown
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected O, but got Unknown
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Expected O, but got Unknown
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Expected O, but got Unknown
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Expected O, but got Unknown
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Expected O, but got Unknown
		//IL_0da4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dae: Expected O, but got Unknown
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_118b: Expected O, but got Unknown
		//IL_11af: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b9: Expected O, but got Unknown
		//IL_11cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d5: Expected O, but got Unknown
		//IL_12a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Expected O, but got Unknown
		//IL_1323: Unknown result type (might be due to invalid IL or missing references)
		//IL_132d: Expected O, but got Unknown
		//IL_1b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b93: Expected O, but got Unknown
		//IL_1bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c30: Expected O, but got Unknown
		//IL_1c4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d39: Expected O, but got Unknown
		//IL_1d57: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3e: Expected O, but got Unknown
		//IL_1e5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f39: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f43: Expected O, but got Unknown
		//IL_1f61: Unknown result type (might be due to invalid IL or missing references)
		//IL_204a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2054: Expected O, but got Unknown
		//IL_2072: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f1: Expected O, but got Unknown
		//IL_210b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2225: Unknown result type (might be due to invalid IL or missing references)
		//IL_222f: Expected O, but got Unknown
		//IL_2249: Unknown result type (might be due to invalid IL or missing references)
		//IL_235f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2369: Expected O, but got Unknown
		//IL_2383: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2403: Expected O, but got Unknown
		//IL_241c: Unknown result type (might be due to invalid IL or missing references)
		//IL_248a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2494: Expected O, but got Unknown
		//IL_24a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24aa: Expected O, but got Unknown
		//IL_24b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c0: Expected O, but got Unknown
		//IL_24cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d6: Expected O, but got Unknown
		//IL_26f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2702: Expected O, but got Unknown
		//IL_2714: Unknown result type (might be due to invalid IL or missing references)
		//IL_271e: Expected O, but got Unknown
		//IL_2730: Unknown result type (might be due to invalid IL or missing references)
		//IL_273a: Expected O, but got Unknown
		//IL_274c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2756: Expected O, but got Unknown
		//IL_2768: Unknown result type (might be due to invalid IL or missing references)
		//IL_2772: Expected O, but got Unknown
		//IL_2784: Unknown result type (might be due to invalid IL or missing references)
		//IL_278e: Expected O, but got Unknown
		//IL_27a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_27aa: Expected O, but got Unknown
		//IL_27bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_27c6: Expected O, but got Unknown
		//IL_28a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_29df: Unknown result type (might be due to invalid IL or missing references)
		//IL_29e9: Expected O, but got Unknown
		//IL_32c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_34b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_34c1: Expected O, but got Unknown
		//IL_3534: Unknown result type (might be due to invalid IL or missing references)
		//IL_353e: Expected O, but got Unknown
		//IL_36c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_36cb: Expected O, but got Unknown
		//IL_373f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3749: Expected O, but got Unknown
		//IL_3e97: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ea1: Expected O, but got Unknown
		//IL_3eb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_3ebe: Expected O, but got Unknown
		//IL_3f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_3f2c: Expected O, but got Unknown
		components = new Container();
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsCfgEditor));
		ToolTip1 = new ToolTip(components);
		B_Next = new Button();
		B_Previous = new Button();
		B_Save = new Button();
		B_Set_Rating_Sys = new Button();
		CB_Comp_1 = new CheckBox();
		CB_Comp_2 = new CheckBox();
		CB_Comp_3 = new CheckBox();
		CB_Comp_4 = new CheckBox();
		CB_Comp_5 = new CheckBox();
		CB_Comp_6 = new CheckBox();
		CB_Comp_7 = new CheckBox();
		CB_Comp_8 = new CheckBox();
		TB_VMC0 = new TextBox();
		TB_VMC1 = new TextBox();
		TB_Title = new TextBox();
		B_Edit = new Button();
		TB_Description = new TextBox();
		TB_NOTES = new TextBox();
		TB_GameID = new TextBox();
		TB_CfgPreview = new TextBox();
		OpenFileDialog1 = new OpenFileDialog();
		GB_Config = new GroupBox();
		Parental_Select = new ComboBox();
		RatingBox = new PictureBox();
		RatingText = new Label();
		Parental_Img = new PictureBox();
		Parental_Next = new Button();
		Parental_Prev = new Button();
		GB_VMC = new GroupBox();
		B_VMC1_Browse = new Button();
		B_VMC0_Browse = new Button();
		CB_VMC1 = new CheckBox();
		CB_VMC0 = new CheckBox();
		B_VMC1_New = new Button();
		B_VMC0_New = new Button();
		Label17 = new Label();
		Label16 = new Label();
		GBox_Comp = new GroupBox();
		ComboDMA = new ComboBox();
		Label2 = new Label();
		ComboScan = new ComboBox();
		L_Scan = new Label();
		L_Aspect = new Label();
		ComboAspectRatio = new ComboBox();
		L_Notes = new Label();
		TB_Developer = new TextBox();
		L_Developer = new Label();
		TB_Release = new TextBox();
		L_Release = new Label();
		Combo_Genre = new ComboBox();
		L_Genre = new Label();
		L_ID = new Label();
		ComboVmode = new ComboBox();
		L_Vmode = new Label();
		ComboPlayers = new ComboBox();
		L_Players = new Label();
		L_title = new Label();
		TableOutter = new TableLayoutPanel();
		Panel1 = new Panel();
		B_Get_Title = new Button();
		GroupCheats = new GroupBox();
		Cheats_L_CheatDev = new Label();
		Combo_CheatDev = new ComboBox();
		Cheat_Enabled = new CheckBox();
		GroupBox_CompDev = new GroupBox();
		CB_CompDev_USB = new CheckBox();
		CB_CompDev_ETH = new CheckBox();
		CB_CompDev_HDD = new CheckBox();
		GroupBox_GSM = new GroupBox();
		GSM_FieldFix = new CheckBox();
		GSM_SkipVideos = new CheckBox();
		GSM_Y = new NumericUpDown();
		GSM_L_XY = new Label();
		GSM_X = new NumericUpDown();
		GSM_Vmode = new ComboBox();
		GSM_L_Vmode = new Label();
		GSM_Enabled = new CheckBox();
		L_DescriptionSize = new Label();
		GB_Ratings = new GroupBox();
		L_ThemeNotice = new LinkLabel();
		GBox_Description = new GroupBox();
		GroupGlobalSettings = new GroupBox();
		L_GlobalSettings_PerGame = new Label();
		L_GlobalSettings_Global = new Label();
		Panel_Global_Cheats = new Panel();
		Label1 = new Label();
		RadioButton1 = new RadioButton();
		Source_CHT = new RadioButton();
		Panel_Global_PadEmu = new Panel();
		Label4 = new Label();
		RadioButton2 = new RadioButton();
		Source_PAD = new RadioButton();
		Panel_Global_GSM = new Panel();
		RadioButton3 = new RadioButton();
		Label3 = new Label();
		Source_GSM = new RadioButton();
		((Control)GB_Config).SuspendLayout();
		((ISupportInitialize)RatingBox).BeginInit();
		((ISupportInitialize)Parental_Img).BeginInit();
		((Control)GB_VMC).SuspendLayout();
		((Control)GBox_Comp).SuspendLayout();
		((Control)TableOutter).SuspendLayout();
		((Control)Panel1).SuspendLayout();
		((Control)GroupCheats).SuspendLayout();
		((Control)GroupBox_CompDev).SuspendLayout();
		((Control)GroupBox_GSM).SuspendLayout();
		((ISupportInitialize)GSM_Y).BeginInit();
		((ISupportInitialize)GSM_X).BeginInit();
		((Control)GB_Ratings).SuspendLayout();
		((Control)GBox_Description).SuspendLayout();
		((Control)GroupGlobalSettings).SuspendLayout();
		((Control)Panel_Global_Cheats).SuspendLayout();
		((Control)Panel_Global_PadEmu).SuspendLayout();
		((Control)Panel_Global_GSM).SuspendLayout();
		((Control)this).SuspendLayout();
		((Control)B_Next).BackgroundImage = (Image)componentResourceManager.GetObject("B_Next.BackgroundImage");
		((Control)B_Next).BackgroundImageLayout = (ImageLayout)3;
		((Control)B_Next).Location = new Point(762, 539);
		((Control)B_Next).Name = "B_Next";
		((Control)B_Next).Size = new Size(48, 48);
		((Control)B_Next).TabIndex = 5;
		ToolTip1.SetToolTip((Control)(object)B_Next, "Next Game");
		((ButtonBase)B_Next).UseVisualStyleBackColor = true;
		((Control)B_Next).Click += B_Next_Click;
		((Control)B_Previous).BackgroundImage = (Image)componentResourceManager.GetObject("B_Previous.BackgroundImage");
		((Control)B_Previous).BackgroundImageLayout = (ImageLayout)3;
		((Control)B_Previous).Location = new Point(708, 539);
		((Control)B_Previous).Name = "B_Previous";
		((Control)B_Previous).Size = new Size(48, 48);
		((Control)B_Previous).TabIndex = 4;
		ToolTip1.SetToolTip((Control)(object)B_Previous, "Previous Game");
		((ButtonBase)B_Previous).UseVisualStyleBackColor = true;
		((Control)B_Previous).Click += B_Previous_Click;
		((Control)B_Save).BackgroundImage = (Image)componentResourceManager.GetObject("B_Save.BackgroundImage");
		((Control)B_Save).BackgroundImageLayout = (ImageLayout)3;
		((Control)B_Save).Location = new Point(634, 539);
		((Control)B_Save).Name = "B_Save";
		((Control)B_Save).Size = new Size(48, 48);
		((Control)B_Save).TabIndex = 3;
		ToolTip1.SetToolTip((Control)(object)B_Save, "Save Changes");
		((ButtonBase)B_Save).UseVisualStyleBackColor = true;
		((Control)B_Save).Click += B_SAVE_Click;
		((Control)B_Set_Rating_Sys).Location = new Point(133, 30);
		((Control)B_Set_Rating_Sys).Name = "B_Set_Rating_Sys";
		((Control)B_Set_Rating_Sys).Size = new Size(89, 23);
		((Control)B_Set_Rating_Sys).TabIndex = 0;
		((Control)B_Set_Rating_Sys).Text = "Set as default";
		ToolTip1.SetToolTip((Control)(object)B_Set_Rating_Sys, "Set the current selected rating system as the default.");
		((ButtonBase)B_Set_Rating_Sys).UseVisualStyleBackColor = true;
		((Control)B_Set_Rating_Sys).Click += Button2_Click;
		((Control)CB_Comp_1).AutoSize = true;
		((Control)CB_Comp_1).Location = new Point(7, 20);
		((Control)CB_Comp_1).Name = "CB_Comp_1";
		((Control)CB_Comp_1).Size = new Size(62, 17);
		((Control)CB_Comp_1).TabIndex = 0;
		((Control)CB_Comp_1).Text = "Mode 1";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_1, "Mode 1 - Loads an alternate EE core at a higher address (may fix games that freeze on a green screen)");
		((ButtonBase)CB_Comp_1).UseVisualStyleBackColor = true;
		((Control)CB_Comp_2).AutoSize = true;
		((Control)CB_Comp_2).Location = new Point(7, 43);
		((Control)CB_Comp_2).Name = "CB_Comp_2";
		((Control)CB_Comp_2).Size = new Size(62, 17);
		((Control)CB_Comp_2).TabIndex = 1;
		((Control)CB_Comp_2).Text = "Mode 2";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_2, "Mode 2 - Uses an alternative read method ");
		((ButtonBase)CB_Comp_2).UseVisualStyleBackColor = true;
		((Control)CB_Comp_3).AutoSize = true;
		((Control)CB_Comp_3).Location = new Point(6, 66);
		((Control)CB_Comp_3).Name = "CB_Comp_3";
		((Control)CB_Comp_3).Size = new Size(62, 17);
		((Control)CB_Comp_3).TabIndex = 2;
		((Control)CB_Comp_3).Text = "Mode 3";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_3, "Mode 3 - Unhook  syscalls. (similar to HDL's mode3)");
		((ButtonBase)CB_Comp_3).UseVisualStyleBackColor = true;
		((Control)CB_Comp_4).AutoSize = true;
		((Control)CB_Comp_4).Location = new Point(6, 89);
		((Control)CB_Comp_4).Name = "CB_Comp_4";
		((Control)CB_Comp_4).Size = new Size(62, 17);
		((Control)CB_Comp_4).TabIndex = 3;
		((Control)CB_Comp_4).Text = "Mode 4";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_4, "Mode 4 - skip PSS file reads  ( bypasses the loading of many FMVs)");
		((ButtonBase)CB_Comp_4).UseVisualStyleBackColor = true;
		((Control)CB_Comp_5).AutoSize = true;
		((Control)CB_Comp_5).Location = new Point(77, 19);
		((Control)CB_Comp_5).Name = "CB_Comp_5";
		((Control)CB_Comp_5).Size = new Size(62, 17);
		((Control)CB_Comp_5).TabIndex = 4;
		((Control)CB_Comp_5).Text = "Mode 5";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_5, "Mode 5 - Emulate DVD-DL");
		((ButtonBase)CB_Comp_5).UseVisualStyleBackColor = true;
		((Control)CB_Comp_6).AutoSize = true;
		((Control)CB_Comp_6).Location = new Point(77, 42);
		((Control)CB_Comp_6).Name = "CB_Comp_6";
		((Control)CB_Comp_6).Size = new Size(62, 17);
		((Control)CB_Comp_6).TabIndex = 5;
		((Control)CB_Comp_6).Text = "Mode 6";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_6, "Mode 6 - Disables IGR  (In Game Reset)");
		((ButtonBase)CB_Comp_6).UseVisualStyleBackColor = true;
		((Control)CB_Comp_7).AutoSize = true;
		((Control)CB_Comp_7).Location = new Point(77, 65);
		((Control)CB_Comp_7).Name = "CB_Comp_7";
		((Control)CB_Comp_7).Size = new Size(62, 17);
		((Control)CB_Comp_7).TabIndex = 6;
		((Control)CB_Comp_7).Text = "Mode 7";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_7, "Mode 7 - Reduced cdvdfsv buffer");
		((ButtonBase)CB_Comp_7).UseVisualStyleBackColor = true;
		((Control)CB_Comp_8).AutoSize = true;
		((Control)CB_Comp_8).Location = new Point(77, 88);
		((Control)CB_Comp_8).Name = "CB_Comp_8";
		((Control)CB_Comp_8).Size = new Size(62, 17);
		((Control)CB_Comp_8).TabIndex = 7;
		((Control)CB_Comp_8).Text = "Mode 8";
		ToolTip1.SetToolTip((Control)(object)CB_Comp_8, "Mode 8 - Hide dev9 module  (fix for NFS:U2)");
		((ButtonBase)CB_Comp_8).UseVisualStyleBackColor = true;
		((Control)TB_VMC0).Enabled = false;
		((Control)TB_VMC0).Location = new Point(31, 47);
		((Control)TB_VMC0).Name = "TB_VMC0";
		((TextBoxBase)TB_VMC0).ReadOnly = true;
		((Control)TB_VMC0).Size = new Size(162, 20);
		((Control)TB_VMC0).TabIndex = 3;
		((Control)TB_VMC0).Tag = "$VMC_0=";
		ToolTip1.SetToolTip((Control)(object)TB_VMC0, "File name of VMC 0");
		((Control)TB_VMC0).EnabledChanged += TB_VMCx_EnabledChanged;
		((Control)TB_VMC0).TextChanged += TB_VMCx_TextChanged;
		((Control)TB_VMC1).Enabled = false;
		((Control)TB_VMC1).Location = new Point(31, 110);
		((Control)TB_VMC1).Name = "TB_VMC1";
		((TextBoxBase)TB_VMC1).ReadOnly = true;
		((Control)TB_VMC1).Size = new Size(162, 20);
		((Control)TB_VMC1).TabIndex = 7;
		((Control)TB_VMC1).Tag = "$VMC_1=";
		ToolTip1.SetToolTip((Control)(object)TB_VMC1, "File Name of VMC1");
		((Control)TB_VMC1).EnabledChanged += TB_VMCx_EnabledChanged;
		((Control)TB_VMC1).TextChanged += TB_VMCx_TextChanged;
		((Control)TB_Title).Location = new Point(0, 2);
		((TextBoxBase)TB_Title).MaxLength = 255;
		((Control)TB_Title).Name = "TB_Title";
		((Control)TB_Title).Size = new Size(357, 20);
		((Control)TB_Title).TabIndex = 0;
		((Control)TB_Title).Tag = "Title=";
		ToolTip1.SetToolTip((Control)(object)TB_Title, "The Game Title!");
		((Control)B_Edit).BackgroundImage = (Image)componentResourceManager.GetObject("B_Edit.BackgroundImage");
		((Control)B_Edit).BackgroundImageLayout = (ImageLayout)3;
		((Control)B_Edit).Location = new Point(580, 539);
		((Control)B_Edit).Name = "B_Edit";
		((Control)B_Edit).Size = new Size(48, 48);
		((Control)B_Edit).TabIndex = 52;
		ToolTip1.SetToolTip((Control)(object)B_Edit, "Click to enable manual edit.\r\nClick again to save the edit.");
		((ButtonBase)B_Edit).UseVisualStyleBackColor = true;
		((Control)B_Edit).Click += B_Edit_Click;
		((TextBoxBase)TB_Description).BorderStyle = (BorderStyle)0;
		((Control)TB_Description).Location = new Point(7, 16);
		((TextBoxBase)TB_Description).MaxLength = 255;
		((TextBoxBase)TB_Description).Multiline = true;
		((Control)TB_Description).Name = "TB_Description";
		TB_Description.ScrollBars = (ScrollBars)3;
		((Control)TB_Description).Size = new Size(343, 135);
		((Control)TB_Description).TabIndex = 1;
		((Control)TB_Description).Tag = "Description=";
		ToolTip1.SetToolTip((Control)(object)TB_Description, "Game Description to show on the info page");
		((Control)TB_NOTES).Location = new Point(375, 123);
		((TextBoxBase)TB_NOTES).MaxLength = 70;
		((Control)TB_NOTES).Name = "TB_NOTES";
		((Control)TB_NOTES).Size = new Size(169, 20);
		((Control)TB_NOTES).TabIndex = 8;
		((Control)TB_NOTES).Tag = "Notes=";
		((Control)TB_GameID).Location = new Point(103, 33);
		((Control)TB_GameID).Name = "TB_GameID";
		((TextBoxBase)TB_GameID).ReadOnly = true;
		((Control)TB_GameID).Size = new Size(170, 20);
		((Control)TB_GameID).TabIndex = 4;
		((Control)TB_GameID).TabStop = false;
		((TextBoxBase)TB_CfgPreview).BorderStyle = (BorderStyle)0;
		((Control)TB_CfgPreview).Dock = (DockStyle)5;
		((Control)TB_CfgPreview).Location = new Point(3, 16);
		((TextBoxBase)TB_CfgPreview).Multiline = true;
		((Control)TB_CfgPreview).Name = "TB_CfgPreview";
		((TextBoxBase)TB_CfgPreview).ReadOnly = true;
		TB_CfgPreview.ScrollBars = (ScrollBars)3;
		((Control)TB_CfgPreview).Size = new Size(224, 321);
		((Control)TB_CfgPreview).TabIndex = 0;
		((FileDialog)OpenFileDialog1).Filter = "Images|*.png;";
		((Control)GB_Config).Controls.Add((Control)(object)TB_CfgPreview);
		((Control)GB_Config).Location = new Point(580, 193);
		((Control)GB_Config).Name = "GB_Config";
		((Control)GB_Config).Size = new Size(230, 340);
		((Control)GB_Config).TabIndex = 51;
		GB_Config.TabStop = false;
		((Control)GB_Config).Text = "Config Preview";
		Parental_Select.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)Parental_Select).FormattingEnabled = true;
		((Control)Parental_Select).Location = new Point(133, 93);
		((Control)Parental_Select).Name = "Parental_Select";
		((Control)Parental_Select).Size = new Size(89, 21);
		((Control)Parental_Select).TabIndex = 3;
		((Control)RatingBox).Location = new Point(6, 137);
		((Control)RatingBox).Name = "RatingBox";
		((Control)RatingBox).Size = new Size(160, 32);
		RatingBox.TabIndex = 58;
		RatingBox.TabStop = false;
		((Control)RatingBox).MouseClick += new MouseEventHandler(RatingBox_MouseClick);
		((Control)RatingBox).MouseLeave += RatingBox_MouseLeave;
		((Control)RatingBox).MouseMove += new MouseEventHandler(RatingBox_MouseMove);
		((Control)RatingText).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)1, (GraphicsUnit)3);
		((Control)RatingText).Location = new Point(172, 137);
		((Control)RatingText).Name = "RatingText";
		((Control)RatingText).Size = new Size(50, 32);
		((Control)RatingText).TabIndex = 5;
		((Control)RatingText).Text = "0";
		RatingText.TextAlign = (ContentAlignment)32;
		((Control)Parental_Img).Location = new Point(6, 19);
		((Control)Parental_Img).Name = "Parental_Img";
		((Control)Parental_Img).Size = new Size(117, 112);
		Parental_Img.SizeMode = (PictureBoxSizeMode)4;
		Parental_Img.TabIndex = 61;
		Parental_Img.TabStop = false;
		((Control)Parental_Next).BackgroundImage = (Image)componentResourceManager.GetObject("Parental_Next.BackgroundImage");
		((Control)Parental_Next).BackgroundImageLayout = (ImageLayout)4;
		((Control)Parental_Next).Location = new Point(133, 61);
		((Control)Parental_Next).Name = "Parental_Next";
		((Control)Parental_Next).Size = new Size(25, 25);
		((Control)Parental_Next).TabIndex = 1;
		((ButtonBase)Parental_Next).UseVisualStyleBackColor = true;
		((Control)Parental_Prev).BackgroundImage = (Image)componentResourceManager.GetObject("Parental_Prev.BackgroundImage");
		((Control)Parental_Prev).BackgroundImageLayout = (ImageLayout)4;
		((Control)Parental_Prev).Location = new Point(195, 59);
		((Control)Parental_Prev).Name = "Parental_Prev";
		((Control)Parental_Prev).Size = new Size(25, 25);
		((Control)Parental_Prev).TabIndex = 2;
		((ButtonBase)Parental_Prev).UseVisualStyleBackColor = true;
		((Control)GB_VMC).Controls.Add((Control)(object)B_VMC1_Browse);
		((Control)GB_VMC).Controls.Add((Control)(object)B_VMC0_Browse);
		((Control)GB_VMC).Controls.Add((Control)(object)CB_VMC1);
		((Control)GB_VMC).Controls.Add((Control)(object)CB_VMC0);
		((Control)GB_VMC).Controls.Add((Control)(object)B_VMC1_New);
		((Control)GB_VMC).Controls.Add((Control)(object)B_VMC0_New);
		((Control)GB_VMC).Controls.Add((Control)(object)Label17);
		((Control)GB_VMC).Controls.Add((Control)(object)Label16);
		((Control)GB_VMC).Controls.Add((Control)(object)TB_VMC1);
		((Control)GB_VMC).Controls.Add((Control)(object)TB_VMC0);
		((Control)GB_VMC).Location = new Point(170, 200);
		((Control)GB_VMC).Name = "GB_VMC";
		((Control)GB_VMC).Size = new Size(200, 140);
		((Control)GB_VMC).TabIndex = 1;
		GB_VMC.TabStop = false;
		((Control)GB_VMC).Text = "Virtual Memory Cards (VMCs)";
		((Control)B_VMC1_Browse).Enabled = false;
		((Control)B_VMC1_Browse).Location = new Point(115, 79);
		((Control)B_VMC1_Browse).Name = "B_VMC1_Browse";
		((Control)B_VMC1_Browse).Size = new Size(75, 23);
		((Control)B_VMC1_Browse).TabIndex = 40;
		((Control)B_VMC1_Browse).Text = "Browse";
		((ButtonBase)B_VMC1_Browse).UseVisualStyleBackColor = true;
		((Control)B_VMC1_Browse).Click += B_VMC1_Browse_Click;
		((Control)B_VMC0_Browse).Enabled = false;
		((Control)B_VMC0_Browse).Location = new Point(115, 18);
		((Control)B_VMC0_Browse).Name = "B_VMC0_Browse";
		((Control)B_VMC0_Browse).Size = new Size(75, 23);
		((Control)B_VMC0_Browse).TabIndex = 39;
		((Control)B_VMC0_Browse).Text = "Browse";
		((ButtonBase)B_VMC0_Browse).UseVisualStyleBackColor = true;
		((Control)B_VMC0_Browse).Click += B_VMC0_Browse_Click;
		((Control)CB_VMC1).AutoSize = true;
		((Control)CB_VMC1).Location = new Point(6, 113);
		((Control)CB_VMC1).Name = "CB_VMC1";
		((Control)CB_VMC1).Size = new Size(15, 14);
		((Control)CB_VMC1).TabIndex = 8;
		((ButtonBase)CB_VMC1).UseVisualStyleBackColor = true;
		((Control)CB_VMC1).Click += CB_VMC1_Click;
		((Control)CB_VMC0).AutoSize = true;
		((Control)CB_VMC0).Location = new Point(6, 49);
		((Control)CB_VMC0).Name = "CB_VMC0";
		((Control)CB_VMC0).Size = new Size(15, 14);
		((Control)CB_VMC0).TabIndex = 2;
		((ButtonBase)CB_VMC0).UseVisualStyleBackColor = true;
		((Control)CB_VMC0).Click += CB_VMC0_Click;
		((Control)B_VMC1_New).Enabled = false;
		((Control)B_VMC1_New).Location = new Point(34, 79);
		((Control)B_VMC1_New).Name = "B_VMC1_New";
		((Control)B_VMC1_New).Size = new Size(75, 23);
		((Control)B_VMC1_New).TabIndex = 6;
		((Control)B_VMC1_New).Text = "Create New";
		((ButtonBase)B_VMC1_New).UseVisualStyleBackColor = true;
		((Control)B_VMC1_New).Click += B_VMC1_Generic_Click;
		((Control)B_VMC0_New).Enabled = false;
		((Control)B_VMC0_New).Location = new Point(34, 18);
		((Control)B_VMC0_New).Name = "B_VMC0_New";
		((Control)B_VMC0_New).Size = new Size(75, 23);
		((Control)B_VMC0_New).TabIndex = 1;
		((Control)B_VMC0_New).Text = "Create New";
		((ButtonBase)B_VMC0_New).UseVisualStyleBackColor = true;
		((Control)B_VMC0_New).Click += B_VMC0_Generic_Click;
		((Control)Label17).AutoSize = true;
		((Control)Label17).Location = new Point(5, 84);
		((Control)Label17).Name = "Label17";
		((Control)Label17).Size = new Size(23, 13);
		((Control)Label17).TabIndex = 4;
		((Control)Label17).Text = "#1:";
		((Control)Label16).AutoSize = true;
		((Control)Label16).Location = new Point(5, 22);
		((Control)Label16).Name = "Label16";
		((Control)Label16).Size = new Size(23, 13);
		((Control)Label16).TabIndex = 38;
		((Control)Label16).Text = "#0:";
		((Control)GBox_Comp).Anchor = (AnchorStyles)15;
		((Control)GBox_Comp).Controls.Add((Control)(object)ComboDMA);
		((Control)GBox_Comp).Controls.Add((Control)(object)Label2);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_8);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_7);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_6);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_5);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_4);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_3);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_2);
		((Control)GBox_Comp).Controls.Add((Control)(object)CB_Comp_1);
		((Control)GBox_Comp).Location = new Point(10, 200);
		((Control)GBox_Comp).Name = "GBox_Comp";
		((Control)GBox_Comp).Size = new Size(150, 160);
		((Control)GBox_Comp).TabIndex = 0;
		GBox_Comp.TabStop = false;
		((Control)GBox_Comp).Text = "Compatiblity modes";
		ComboDMA.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)ComboDMA).FormattingEnabled = true;
		ComboDMA.Items.AddRange(new object[8] { "MDMA 0", "MDMA 1", "MDMA 2", "UDMA 0", "UDMA 1", "UDMA 2", "UDMA 3", "UDMA 4" });
		((Control)ComboDMA).Location = new Point(46, 109);
		((Control)ComboDMA).Name = "ComboDMA";
		((Control)ComboDMA).Size = new Size(93, 21);
		((Control)ComboDMA).TabIndex = 9;
		((Control)ComboDMA).Tag = "$DMA=";
		((Control)Label2).AutoSize = true;
		((Control)Label2).Location = new Point(6, 112);
		((Control)Label2).Name = "Label2";
		((Control)Label2).Size = new Size(34, 13);
		((Control)Label2).TabIndex = 8;
		((Control)Label2).Text = "DMA:";
		ComboScan.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)ComboScan).FormattingEnabled = true;
		((Control)ComboScan).Location = new Point(103, 153);
		((Control)ComboScan).Name = "ComboScan";
		((Control)ComboScan).Size = new Size(170, 21);
		((Control)ComboScan).TabIndex = 4;
		((Control)ComboScan).Tag = "Scan=";
		((Control)L_Scan).Anchor = (AnchorStyles)8;
		((Control)L_Scan).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Scan).Location = new Point(0, 155);
		((Control)L_Scan).Margin = new Padding(0);
		((Control)L_Scan).Name = "L_Scan";
		((Control)L_Scan).Size = new Size(100, 21);
		((Control)L_Scan).TabIndex = 21;
		((Control)L_Scan).Text = "Scan:";
		L_Scan.TextAlign = (ContentAlignment)64;
		((Control)L_Aspect).Anchor = (AnchorStyles)8;
		((Control)L_Aspect).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Aspect).Location = new Point(3, 124);
		((Control)L_Aspect).Margin = new Padding(0);
		((Control)L_Aspect).Name = "L_Aspect";
		((Control)L_Aspect).Size = new Size(97, 21);
		((Control)L_Aspect).TabIndex = 13;
		((Control)L_Aspect).Text = "Aspect:";
		L_Aspect.TextAlign = (ContentAlignment)64;
		ComboAspectRatio.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)ComboAspectRatio).FormattingEnabled = true;
		((Control)ComboAspectRatio).Location = new Point(103, 123);
		((Control)ComboAspectRatio).Name = "ComboAspectRatio";
		((Control)ComboAspectRatio).Size = new Size(170, 21);
		((Control)ComboAspectRatio).TabIndex = 3;
		((Control)ComboAspectRatio).Tag = "Aspect=";
		((Control)L_Notes).Anchor = (AnchorStyles)8;
		((Control)L_Notes).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Notes).Location = new Point(282, 124);
		((Control)L_Notes).Margin = new Padding(0);
		((Control)L_Notes).Name = "L_Notes";
		((Control)L_Notes).Size = new Size(90, 21);
		((Control)L_Notes).TabIndex = 25;
		((Control)L_Notes).Text = "Notes:";
		L_Notes.TextAlign = (ContentAlignment)64;
		((Control)TB_Developer).Location = new Point(375, 93);
		((TextBoxBase)TB_Developer).MaxLength = 70;
		((Control)TB_Developer).Name = "TB_Developer";
		((Control)TB_Developer).Size = new Size(169, 20);
		((Control)TB_Developer).TabIndex = 7;
		((Control)TB_Developer).Tag = "Developer=";
		((Control)L_Developer).Anchor = (AnchorStyles)8;
		((Control)L_Developer).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Developer).Location = new Point(279, 94);
		((Control)L_Developer).Margin = new Padding(0);
		((Control)L_Developer).Name = "L_Developer";
		((Control)L_Developer).Size = new Size(93, 21);
		((Control)L_Developer).TabIndex = 30;
		((Control)L_Developer).Text = "Developer:";
		L_Developer.TextAlign = (ContentAlignment)64;
		((Control)TB_Release).Location = new Point(375, 63);
		((TextBoxBase)TB_Release).MaxLength = 70;
		((Control)TB_Release).Name = "TB_Release";
		((Control)TB_Release).Size = new Size(169, 20);
		((Control)TB_Release).TabIndex = 6;
		((Control)TB_Release).Tag = "Release=";
		((Control)L_Release).Anchor = (AnchorStyles)8;
		((Control)L_Release).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Release).Location = new Point(276, 64);
		((Control)L_Release).Margin = new Padding(0);
		((Control)L_Release).Name = "L_Release";
		((Control)L_Release).Size = new Size(96, 21);
		((Control)L_Release).TabIndex = 28;
		((Control)L_Release).Text = "Release:";
		L_Release.TextAlign = (ContentAlignment)64;
		((ListControl)Combo_Genre).FormattingEnabled = true;
		Combo_Genre.ItemHeight = 13;
		((Control)Combo_Genre).Location = new Point(375, 33);
		((Control)Combo_Genre).Name = "Combo_Genre";
		((Control)Combo_Genre).Size = new Size(169, 21);
		((Control)Combo_Genre).TabIndex = 5;
		((Control)Combo_Genre).Tag = "Genre=";
		((Control)L_Genre).Anchor = (AnchorStyles)8;
		((Control)L_Genre).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Genre).Location = new Point(279, 34);
		((Control)L_Genre).Margin = new Padding(0);
		((Control)L_Genre).Name = "L_Genre";
		((Control)L_Genre).Size = new Size(93, 21);
		((Control)L_Genre).TabIndex = 5;
		((Control)L_Genre).Text = "Genre:\r\na";
		L_Genre.TextAlign = (ContentAlignment)64;
		((Control)L_ID).Anchor = (AnchorStyles)8;
		((Control)L_ID).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_ID).Location = new Point(3, 35);
		((Control)L_ID).Margin = new Padding(0);
		((Control)L_ID).Name = "L_ID";
		((Control)L_ID).Size = new Size(97, 20);
		((Control)L_ID).TabIndex = 4;
		((Control)L_ID).Text = "Game ID:";
		L_ID.TextAlign = (ContentAlignment)64;
		ComboVmode.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)ComboVmode).FormattingEnabled = true;
		ComboVmode.Items.AddRange(new object[4] { "", "NTSC", "PAL", "Multi" });
		((Control)ComboVmode).Location = new Point(103, 93);
		((Control)ComboVmode).Name = "ComboVmode";
		((Control)ComboVmode).Size = new Size(170, 21);
		((Control)ComboVmode).TabIndex = 2;
		((Control)ComboVmode).Tag = "Vmode=";
		((Control)L_Vmode).Anchor = (AnchorStyles)8;
		((Control)L_Vmode).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Vmode).Location = new Point(3, 95);
		((Control)L_Vmode).Margin = new Padding(0);
		((Control)L_Vmode).Name = "L_Vmode";
		((Control)L_Vmode).Size = new Size(97, 20);
		((Control)L_Vmode).TabIndex = 7;
		((Control)L_Vmode).Text = "V-Mode:";
		L_Vmode.TextAlign = (ContentAlignment)64;
		ComboPlayers.DropDownStyle = (ComboBoxStyle)2;
		((ListControl)ComboPlayers).FormattingEnabled = true;
		ComboPlayers.Items.AddRange(new object[5] { "", "1", "2", "3", "4" });
		((Control)ComboPlayers).Location = new Point(103, 63);
		((Control)ComboPlayers).Name = "ComboPlayers";
		((Control)ComboPlayers).Size = new Size(170, 21);
		((Control)ComboPlayers).TabIndex = 1;
		((Control)ComboPlayers).Tag = "Players=";
		((Control)L_Players).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_Players).Location = new Point(0, 60);
		((Control)L_Players).Margin = new Padding(0);
		((Control)L_Players).Name = "L_Players";
		((Control)L_Players).Size = new Size(100, 21);
		((Control)L_Players).TabIndex = 19;
		((Control)L_Players).Text = "No. of Players:";
		L_Players.TextAlign = (ContentAlignment)64;
		((Control)L_title).Anchor = (AnchorStyles)8;
		((Control)L_title).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_title).Location = new Point(0, 5);
		((Control)L_title).Margin = new Padding(0);
		((Control)L_title).Name = "L_title";
		((Control)L_title).Size = new Size(100, 20);
		((Control)L_title).TabIndex = 3;
		((Control)L_title).Text = "Title:";
		L_title.TextAlign = (ContentAlignment)64;
		TableOutter.ColumnCount = 4;
		TableOutter.ColumnStyles.Add(new ColumnStyle());
		TableOutter.ColumnStyles.Add(new ColumnStyle());
		TableOutter.ColumnStyles.Add(new ColumnStyle());
		TableOutter.ColumnStyles.Add(new ColumnStyle());
		TableOutter.Controls.Add((Control)(object)L_title, 0, 0);
		TableOutter.Controls.Add((Control)(object)ComboPlayers, 1, 2);
		TableOutter.Controls.Add((Control)(object)L_Vmode, 0, 3);
		TableOutter.Controls.Add((Control)(object)ComboVmode, 1, 3);
		TableOutter.Controls.Add((Control)(object)L_ID, 0, 1);
		TableOutter.Controls.Add((Control)(object)TB_GameID, 1, 1);
		TableOutter.Controls.Add((Control)(object)L_Genre, 2, 1);
		TableOutter.Controls.Add((Control)(object)Combo_Genre, 3, 1);
		TableOutter.Controls.Add((Control)(object)L_Release, 2, 2);
		TableOutter.Controls.Add((Control)(object)TB_Release, 3, 2);
		TableOutter.Controls.Add((Control)(object)L_Developer, 2, 3);
		TableOutter.Controls.Add((Control)(object)TB_Developer, 3, 3);
		TableOutter.Controls.Add((Control)(object)L_Notes, 2, 4);
		TableOutter.Controls.Add((Control)(object)TB_NOTES, 3, 4);
		TableOutter.Controls.Add((Control)(object)ComboAspectRatio, 1, 4);
		TableOutter.Controls.Add((Control)(object)L_Aspect, 0, 4);
		TableOutter.Controls.Add((Control)(object)L_Scan, 0, 5);
		TableOutter.Controls.Add((Control)(object)ComboScan, 1, 5);
		TableOutter.Controls.Add((Control)(object)L_Players, 0, 2);
		TableOutter.Controls.Add((Control)(object)Panel1, 1, 0);
		((Control)TableOutter).Location = new Point(10, 12);
		((Control)TableOutter).Name = "TableOutter";
		TableOutter.RowCount = 6;
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 30f));
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 30f));
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 30f));
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 30f));
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 30f));
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 30f));
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 500f));
		TableOutter.RowStyles.Add(new RowStyle((SizeType)1, 30f));
		((Control)TableOutter).Size = new Size(559, 181);
		((Control)TableOutter).TabIndex = 7;
		TableOutter.SetColumnSpan((Control)(object)Panel1, 3);
		((Control)Panel1).Controls.Add((Control)(object)B_Get_Title);
		((Control)Panel1).Controls.Add((Control)(object)TB_Title);
		((Control)Panel1).Dock = (DockStyle)5;
		((Control)Panel1).Location = new Point(103, 3);
		((Control)Panel1).Name = "Panel1";
		((Control)Panel1).Size = new Size(453, 24);
		((Control)Panel1).TabIndex = 81;
		((Control)B_Get_Title).BackgroundImageLayout = (ImageLayout)0;
		((Control)B_Get_Title).Location = new Point(360, 2);
		((Control)B_Get_Title).Margin = new Padding(0);
		((Control)B_Get_Title).Name = "B_Get_Title";
		((Control)B_Get_Title).Size = new Size(87, 20);
		((Control)B_Get_Title).TabIndex = 53;
		((Control)B_Get_Title).Text = "Get Title";
		((ButtonBase)B_Get_Title).UseVisualStyleBackColor = true;
		((Control)B_Get_Title).Click += B_Get_Title_Click;
		((Control)GroupCheats).Controls.Add((Control)(object)Cheats_L_CheatDev);
		((Control)GroupCheats).Controls.Add((Control)(object)Combo_CheatDev);
		((Control)GroupCheats).Controls.Add((Control)(object)Cheat_Enabled);
		((Control)GroupCheats).Location = new Point(380, 345);
		((Control)GroupCheats).Name = "GroupCheats";
		((Control)GroupCheats).Size = new Size(190, 55);
		((Control)GroupCheats).TabIndex = 82;
		GroupCheats.TabStop = false;
		((Control)GroupCheats).Text = "Cheats";
		((Control)Cheats_L_CheatDev).Anchor = (AnchorStyles)8;
		((Control)Cheats_L_CheatDev).Font = new Font("Microsoft Sans Serif", 9f, (FontStyle)0, (GraphicsUnit)3);
		((Control)Cheats_L_CheatDev).Location = new Point(75, 8);
		((Control)Cheats_L_CheatDev).Name = "Cheats_L_CheatDev";
		((Control)Cheats_L_CheatDev).Size = new Size(103, 16);
		((Control)Cheats_L_CheatDev).TabIndex = 81;
		((Control)Cheats_L_CheatDev).Text = "Cheat device:";
		Cheats_L_CheatDev.TextAlign = (ContentAlignment)32;
		((ListControl)Combo_CheatDev).FormattingEnabled = true;
		Combo_CheatDev.Items.AddRange(new object[3] { "", "Codebreaker", "ps2rd" });
		((Control)Combo_CheatDev).Location = new Point(75, 26);
		((Control)Combo_CheatDev).Name = "Combo_CheatDev";
		((Control)Combo_CheatDev).Size = new Size(107, 21);
		((Control)Combo_CheatDev).TabIndex = 80;
		((Control)Combo_CheatDev).Tag = "Cheat=";
		((Control)Cheat_Enabled).AutoSize = true;
		((Control)Cheat_Enabled).Location = new Point(4, 25);
		((Control)Cheat_Enabled).Name = "Cheat_Enabled";
		((Control)Cheat_Enabled).Size = new Size(65, 17);
		((Control)Cheat_Enabled).TabIndex = 0;
		((Control)Cheat_Enabled).Tag = "$EnableCheat=";
		((Control)Cheat_Enabled).Text = "Enabled";
		((ButtonBase)Cheat_Enabled).UseVisualStyleBackColor = true;
		((Control)GroupBox_CompDev).Controls.Add((Control)(object)CB_CompDev_USB);
		((Control)GroupBox_CompDev).Controls.Add((Control)(object)CB_CompDev_ETH);
		((Control)GroupBox_CompDev).Controls.Add((Control)(object)CB_CompDev_HDD);
		((Control)GroupBox_CompDev).Location = new Point(380, 200);
		((Control)GroupBox_CompDev).Name = "GroupBox_CompDev";
		((Control)GroupBox_CompDev).Size = new Size(190, 40);
		((Control)GroupBox_CompDev).TabIndex = 3;
		GroupBox_CompDev.TabStop = false;
		((Control)GroupBox_CompDev).Text = "Compatible devices";
		((Control)CB_CompDev_USB).AutoSize = true;
		((Control)CB_CompDev_USB).Location = new Point(133, 19);
		((Control)CB_CompDev_USB).Name = "CB_CompDev_USB";
		((Control)CB_CompDev_USB).Size = new Size(48, 17);
		((Control)CB_CompDev_USB).TabIndex = 2;
		((Control)CB_CompDev_USB).Text = "USB";
		((ButtonBase)CB_CompDev_USB).UseVisualStyleBackColor = true;
		((Control)CB_CompDev_ETH).AutoSize = true;
		((Control)CB_CompDev_ETH).Location = new Point(75, 19);
		((Control)CB_CompDev_ETH).Name = "CB_CompDev_ETH";
		((Control)CB_CompDev_ETH).Size = new Size(48, 17);
		((Control)CB_CompDev_ETH).TabIndex = 1;
		((Control)CB_CompDev_ETH).Text = "ETH";
		((ButtonBase)CB_CompDev_ETH).UseVisualStyleBackColor = true;
		((Control)CB_CompDev_HDD).AutoSize = true;
		((Control)CB_CompDev_HDD).Location = new Point(4, 19);
		((Control)CB_CompDev_HDD).Name = "CB_CompDev_HDD";
		((Control)CB_CompDev_HDD).Size = new Size(50, 17);
		((Control)CB_CompDev_HDD).TabIndex = 0;
		((Control)CB_CompDev_HDD).Text = "HDD";
		((ButtonBase)CB_CompDev_HDD).UseVisualStyleBackColor = true;
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_FieldFix);
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_SkipVideos);
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_Y);
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_L_XY);
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_X);
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_Vmode);
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_L_Vmode);
		((Control)GroupBox_GSM).Controls.Add((Control)(object)GSM_Enabled);
		((Control)GroupBox_GSM).Location = new Point(380, 245);
		((Control)GroupBox_GSM).Name = "GroupBox_GSM";
		((Control)GroupBox_GSM).Size = new Size(190, 95);
		((Control)GroupBox_GSM).TabIndex = 4;
		GroupBox_GSM.TabStop = false;
		((Control)GSM_FieldFix).Enabled = false;
		((Control)GSM_FieldFix).Location = new Point(4, 63);
		((Control)GSM_FieldFix).Name = "GSM_FieldFix";
		((Control)GSM_FieldFix).Size = new Size(101, 30);
		((Control)GSM_FieldFix).TabIndex = 17;
		((Control)GSM_FieldFix).Tag = "$GSMFIELDFix=";
		((Control)GSM_FieldFix).Text = "Emulate FIELD Flipping";
		((ButtonBase)GSM_FieldFix).UseVisualStyleBackColor = true;
		((Control)GSM_SkipVideos).Enabled = false;
		((Control)GSM_SkipVideos).Location = new Point(2, 23);
		((Control)GSM_SkipVideos).Name = "GSM_SkipVideos";
		((Control)GSM_SkipVideos).Size = new Size(74, 34);
		((Control)GSM_SkipVideos).TabIndex = 15;
		((Control)GSM_SkipVideos).Tag = "$GSMSkipVideos=";
		((Control)GSM_SkipVideos).Text = "Skip Videos";
		((ButtonBase)GSM_SkipVideos).UseVisualStyleBackColor = true;
		((Control)GSM_Y).Enabled = false;
		((Control)GSM_Y).Location = new Point(136, 28);
		GSM_Y.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		((Control)GSM_Y).Name = "GSM_Y";
		((Control)GSM_Y).Size = new Size(48, 20);
		((Control)GSM_Y).TabIndex = 14;
		((Control)GSM_Y).Tag = "$GSMYOffset=";
		((Control)GSM_L_XY).Location = new Point(82, 12);
		((Control)GSM_L_XY).Name = "GSM_L_XY";
		((Control)GSM_L_XY).Size = new Size(102, 13);
		((Control)GSM_L_XY).TabIndex = 13;
		((Control)GSM_L_XY).Text = "H/V Adjust";
		GSM_L_XY.TextAlign = (ContentAlignment)32;
		((Control)GSM_X).Enabled = false;
		((Control)GSM_X).Location = new Point(82, 28);
		GSM_X.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		((Control)GSM_X).Name = "GSM_X";
		((Control)GSM_X).Size = new Size(48, 20);
		((Control)GSM_X).TabIndex = 12;
		((Control)GSM_X).Tag = "$GSMXOffset=";
		GSM_Vmode.DropDownStyle = (ComboBoxStyle)2;
		((Control)GSM_Vmode).Enabled = false;
		((ListControl)GSM_Vmode).FormattingEnabled = true;
		GSM_Vmode.Items.AddRange(new object[29]
		{
			"NTSC", "NTSC Non Interlaced", "PAL", "PAL Non Interlaced", "PAL @60Hz", "PAL @60Hz Non Interlaced", "PS1 NTSC (HDTV 480p @60Hz)", "PS1 PAL (HDTV 576p @50Hz)", "HDTV 480p @60Hz", "HDTV 576p @50Hz",
			"HDTV 720p @60Hz", "HDTV 1080i @60Hz", "HDTV 1080i @60Hz Non Interlaced", "VGA 640x480p @60Hz", "VGA 640x480p @72Hz", "VGA 640x480p @75Hz", "VGA 640x480p @85Hz", "VGA 640x960i @60Hz", "VGA 800x600p @56Hz", "VGA 800x600p @60Hz",
			"VGA 800x600p @72Hz", "VGA 800x600p @75Hz", "VGA 800x600p @85Hz", "VGA 1024x768p @60Hz", "VGA 1024x768p @70Hz", "VGA 1024x768p @75Hz", "VGA 1024x768p @85Hz", "VGA 1280x1024p @60Hz", "VGA 1280x1024p @75Hz"
		});
		((Control)GSM_Vmode).Location = new Point(111, 68);
		((Control)GSM_Vmode).Name = "GSM_Vmode";
		((Control)GSM_Vmode).Size = new Size(71, 21);
		((Control)GSM_Vmode).TabIndex = 11;
		((Control)GSM_Vmode).Tag = "$GSMVMode=";
		((Control)GSM_L_Vmode).Location = new Point(110, 53);
		((Control)GSM_L_Vmode).Name = "GSM_L_Vmode";
		((Control)GSM_L_Vmode).Size = new Size(72, 13);
		((Control)GSM_L_Vmode).TabIndex = 10;
		((Control)GSM_L_Vmode).Text = "V-Mode";
		GSM_L_Vmode.TextAlign = (ContentAlignment)32;
		((Control)GSM_Enabled).AutoSize = true;
		((Control)GSM_Enabled).Location = new Point(6, 0);
		((Control)GSM_Enabled).Margin = new Padding(0, 3, 0, 3);
		((Control)GSM_Enabled).Name = "GSM_Enabled";
		((Control)GSM_Enabled).Size = new Size(50, 17);
		((Control)GSM_Enabled).TabIndex = 0;
		((Control)GSM_Enabled).Tag = "$EnableGSM=";
		((Control)GSM_Enabled).Text = "GSM";
		((ButtonBase)GSM_Enabled).UseVisualStyleBackColor = true;
		((Control)L_DescriptionSize).Dock = (DockStyle)2;
		((Control)L_DescriptionSize).Location = new Point(3, 170);
		((Control)L_DescriptionSize).Name = "L_DescriptionSize";
		((Control)L_DescriptionSize).Size = new Size(352, 15);
		((Control)L_DescriptionSize).TabIndex = 53;
		((Control)L_DescriptionSize).Text = "0/255";
		L_DescriptionSize.TextAlign = (ContentAlignment)32;
		((Control)GB_Ratings).Controls.Add((Control)(object)RatingText);
		((Control)GB_Ratings).Controls.Add((Control)(object)RatingBox);
		((Control)GB_Ratings).Controls.Add((Control)(object)Parental_Select);
		((Control)GB_Ratings).Controls.Add((Control)(object)Parental_Img);
		((Control)GB_Ratings).Controls.Add((Control)(object)B_Set_Rating_Sys);
		((Control)GB_Ratings).Controls.Add((Control)(object)Parental_Next);
		((Control)GB_Ratings).Controls.Add((Control)(object)Parental_Prev);
		((Control)GB_Ratings).Location = new Point(580, 12);
		((Control)GB_Ratings).Name = "GB_Ratings";
		((Control)GB_Ratings).Size = new Size(230, 175);
		((Control)GB_Ratings).TabIndex = 0;
		GB_Ratings.TabStop = false;
		((Control)GB_Ratings).Text = "Game Rating";
		((Control)L_ThemeNotice).Font = new Font("Microsoft Sans Serif", 10f, (FontStyle)0, (GraphicsUnit)3);
		((Control)L_ThemeNotice).Location = new Point(9, 539);
		((Control)L_ThemeNotice).Name = "L_ThemeNotice";
		((Control)L_ThemeNotice).Size = new Size(561, 43);
		((Control)L_ThemeNotice).TabIndex = 2;
		L_ThemeNotice.TabStop = true;
		((Control)L_ThemeNotice).Text = "Note: You'll need a compatible theme to be able to use all these new features. Click to check the list of compatible themes.";
		L_ThemeNotice.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkLabel1_LinkClicked);
		((Control)GBox_Description).Controls.Add((Control)(object)L_DescriptionSize);
		((Control)GBox_Description).Controls.Add((Control)(object)TB_Description);
		((Control)GBox_Description).Location = new Point(10, 345);
		((Control)GBox_Description).Name = "GBox_Description";
		((Control)GBox_Description).Size = new Size(358, 188);
		((Control)GBox_Description).TabIndex = 83;
		GBox_Description.TabStop = false;
		((Control)GBox_Description).Text = "Description";
		((Control)GroupGlobalSettings).Controls.Add((Control)(object)L_GlobalSettings_PerGame);
		((Control)GroupGlobalSettings).Controls.Add((Control)(object)L_GlobalSettings_Global);
		((Control)GroupGlobalSettings).Controls.Add((Control)(object)Panel_Global_Cheats);
		((Control)GroupGlobalSettings).Controls.Add((Control)(object)Panel_Global_PadEmu);
		((Control)GroupGlobalSettings).Controls.Add((Control)(object)Panel_Global_GSM);
		((Control)GroupGlobalSettings).Location = new Point(380, 405);
		((Control)GroupGlobalSettings).Name = "GroupGlobalSettings";
		((Control)GroupGlobalSettings).Size = new Size(190, 128);
		((Control)GroupGlobalSettings).TabIndex = 83;
		GroupGlobalSettings.TabStop = false;
		((Control)GroupGlobalSettings).Text = "Global Settings";
		((Control)L_GlobalSettings_PerGame).Font = new Font("Microsoft Sans Serif", 8.25f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_GlobalSettings_PerGame).Location = new Point(14, 16);
		((Control)L_GlobalSettings_PerGame).Name = "L_GlobalSettings_PerGame";
		((Control)L_GlobalSettings_PerGame).Size = new Size(80, 16);
		((Control)L_GlobalSettings_PerGame).TabIndex = 88;
		((Control)L_GlobalSettings_PerGame).Text = "Per Game";
		L_GlobalSettings_PerGame.TextAlign = (ContentAlignment)16;
		((Control)L_GlobalSettings_Global).Font = new Font("Microsoft Sans Serif", 8.25f, (FontStyle)1, (GraphicsUnit)3);
		((Control)L_GlobalSettings_Global).Location = new Point(92, 16);
		((Control)L_GlobalSettings_Global).Name = "L_GlobalSettings_Global";
		((Control)L_GlobalSettings_Global).Size = new Size(75, 16);
		((Control)L_GlobalSettings_Global).TabIndex = 87;
		((Control)L_GlobalSettings_Global).Text = "Global";
		L_GlobalSettings_Global.TextAlign = (ContentAlignment)64;
		((Control)Panel_Global_Cheats).Controls.Add((Control)(object)Label1);
		((Control)Panel_Global_Cheats).Controls.Add((Control)(object)RadioButton1);
		((Control)Panel_Global_Cheats).Controls.Add((Control)(object)Source_CHT);
		((Control)Panel_Global_Cheats).Location = new Point(17, 69);
		((Control)Panel_Global_Cheats).Name = "Panel_Global_Cheats";
		((Control)Panel_Global_Cheats).Size = new Size(150, 20);
		((Control)Panel_Global_Cheats).TabIndex = 86;
		((Control)Label1).Location = new Point(17, 0);
		((Control)Label1).Name = "Label1";
		((Control)Label1).Size = new Size(113, 20);
		((Control)Label1).TabIndex = 87;
		((Control)Label1).Text = "Cheats Source";
		Label1.TextAlign = (ContentAlignment)32;
		((Control)RadioButton1).AutoSize = true;
		RadioButton1.Checked = true;
		((Control)RadioButton1).Dock = (DockStyle)4;
		((Control)RadioButton1).Location = new Point(136, 0);
		((Control)RadioButton1).Name = "RadioButton1";
		((Control)RadioButton1).Size = new Size(14, 20);
		((Control)RadioButton1).TabIndex = 85;
		RadioButton1.TabStop = true;
		((ButtonBase)RadioButton1).UseVisualStyleBackColor = true;
		((Control)Source_CHT).AutoSize = true;
		((Control)Source_CHT).Dock = (DockStyle)3;
		((Control)Source_CHT).Location = new Point(0, 0);
		((Control)Source_CHT).Name = "Source_CHT";
		((Control)Source_CHT).Size = new Size(14, 20);
		((Control)Source_CHT).TabIndex = 84;
		((Control)Source_CHT).Tag = "$CheatsSource=";
		((ButtonBase)Source_CHT).UseVisualStyleBackColor = true;
		((Control)Panel_Global_PadEmu).Controls.Add((Control)(object)Label4);
		((Control)Panel_Global_PadEmu).Controls.Add((Control)(object)RadioButton2);
		((Control)Panel_Global_PadEmu).Controls.Add((Control)(object)Source_PAD);
		((Control)Panel_Global_PadEmu).Location = new Point(17, 99);
		((Control)Panel_Global_PadEmu).Name = "Panel_Global_PadEmu";
		((Control)Panel_Global_PadEmu).Size = new Size(150, 20);
		((Control)Panel_Global_PadEmu).TabIndex = 86;
		((Control)Label4).Location = new Point(20, 0);
		((Control)Label4).Name = "Label4";
		((Control)Label4).Size = new Size(110, 20);
		((Control)Label4).TabIndex = 89;
		((Control)Label4).Text = "PADEMU Source";
		Label4.TextAlign = (ContentAlignment)32;
		((Control)RadioButton2).AutoSize = true;
		RadioButton2.Checked = true;
		((Control)RadioButton2).Dock = (DockStyle)4;
		((Control)RadioButton2).Location = new Point(136, 0);
		((Control)RadioButton2).Name = "RadioButton2";
		((Control)RadioButton2).Size = new Size(14, 20);
		((Control)RadioButton2).TabIndex = 85;
		RadioButton2.TabStop = true;
		((ButtonBase)RadioButton2).UseVisualStyleBackColor = true;
		((Control)Source_PAD).AutoSize = true;
		((Control)Source_PAD).Dock = (DockStyle)3;
		((Control)Source_PAD).Location = new Point(0, 0);
		((Control)Source_PAD).Name = "Source_PAD";
		((Control)Source_PAD).Size = new Size(14, 20);
		((Control)Source_PAD).TabIndex = 84;
		((Control)Source_PAD).Tag = "$PADEMUSource=";
		((ButtonBase)Source_PAD).UseVisualStyleBackColor = true;
		((Control)Panel_Global_GSM).Controls.Add((Control)(object)RadioButton3);
		((Control)Panel_Global_GSM).Controls.Add((Control)(object)Label3);
		((Control)Panel_Global_GSM).Controls.Add((Control)(object)Source_GSM);
		((Control)Panel_Global_GSM).Location = new Point(17, 39);
		((Control)Panel_Global_GSM).Name = "Panel_Global_GSM";
		((Control)Panel_Global_GSM).Size = new Size(150, 20);
		((Control)Panel_Global_GSM).TabIndex = 84;
		((Control)RadioButton3).AutoSize = true;
		RadioButton3.Checked = true;
		((Control)RadioButton3).Dock = (DockStyle)4;
		((Control)RadioButton3).Location = new Point(136, 0);
		((Control)RadioButton3).Name = "RadioButton3";
		((Control)RadioButton3).Size = new Size(14, 20);
		((Control)RadioButton3).TabIndex = 85;
		RadioButton3.TabStop = true;
		((ButtonBase)RadioButton3).UseVisualStyleBackColor = true;
		((Control)Label3).Location = new Point(15, 0);
		((Control)Label3).Name = "Label3";
		((Control)Label3).Size = new Size(115, 20);
		((Control)Label3).TabIndex = 88;
		((Control)Label3).Text = "GSM Source";
		Label3.TextAlign = (ContentAlignment)32;
		((Control)Source_GSM).AutoSize = true;
		((Control)Source_GSM).Dock = (DockStyle)3;
		((Control)Source_GSM).Location = new Point(0, 0);
		((Control)Source_GSM).Name = "Source_GSM";
		((Control)Source_GSM).Size = new Size(14, 20);
		((Control)Source_GSM).TabIndex = 84;
		((Control)Source_GSM).Tag = "$GSMSource=";
		((ButtonBase)Source_GSM).UseVisualStyleBackColor = true;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(814, 591);
		((Control)this).Controls.Add((Control)(object)GroupGlobalSettings);
		((Control)this).Controls.Add((Control)(object)GBox_Description);
		((Control)this).Controls.Add((Control)(object)GroupBox_GSM);
		((Control)this).Controls.Add((Control)(object)GroupBox_CompDev);
		((Control)this).Controls.Add((Control)(object)GB_VMC);
		((Control)this).Controls.Add((Control)(object)GBox_Comp);
		((Control)this).Controls.Add((Control)(object)GroupCheats);
		((Control)this).Controls.Add((Control)(object)B_Edit);
		((Control)this).Controls.Add((Control)(object)L_ThemeNotice);
		((Control)this).Controls.Add((Control)(object)GB_Ratings);
		((Control)this).Controls.Add((Control)(object)GB_Config);
		((Control)this).Controls.Add((Control)(object)B_Next);
		((Control)this).Controls.Add((Control)(object)B_Previous);
		((Control)this).Controls.Add((Control)(object)TableOutter);
		((Control)this).Controls.Add((Control)(object)B_Save);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).FormBorderStyle = (FormBorderStyle)3;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Control)this).MaximumSize = new Size(830, 630);
		((Form)this).MinimizeBox = false;
		((Control)this).MinimumSize = new Size(830, 550);
		((Control)this).Name = "OpsCfgEditor";
		((Form)this).ShowInTaskbar = false;
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "CFG Editor - ";
		((Form)this).FormClosing += new FormClosingEventHandler(CFG_Editor_FormClosing);
		((Form)this).Shown += OpsCfgEditorShown;
		((Control)GB_Config).ResumeLayout(false);
		((Control)GB_Config).PerformLayout();
		((ISupportInitialize)RatingBox).EndInit();
		((ISupportInitialize)Parental_Img).EndInit();
		((Control)GB_VMC).ResumeLayout(false);
		((Control)GB_VMC).PerformLayout();
		((Control)GBox_Comp).ResumeLayout(false);
		((Control)GBox_Comp).PerformLayout();
		((Control)TableOutter).ResumeLayout(false);
		((Control)TableOutter).PerformLayout();
		((Control)Panel1).ResumeLayout(false);
		((Control)Panel1).PerformLayout();
		((Control)GroupCheats).ResumeLayout(false);
		((Control)GroupCheats).PerformLayout();
		((Control)GroupBox_CompDev).ResumeLayout(false);
		((Control)GroupBox_CompDev).PerformLayout();
		((Control)GroupBox_GSM).ResumeLayout(false);
		((Control)GroupBox_GSM).PerformLayout();
		((ISupportInitialize)GSM_Y).EndInit();
		((ISupportInitialize)GSM_X).EndInit();
		((Control)GB_Ratings).ResumeLayout(false);
		((Control)GBox_Description).ResumeLayout(false);
		((Control)GBox_Description).PerformLayout();
		((Control)GroupGlobalSettings).ResumeLayout(false);
		((Control)Panel_Global_Cheats).ResumeLayout(false);
		((Control)Panel_Global_Cheats).PerformLayout();
		((Control)Panel_Global_PadEmu).ResumeLayout(false);
		((Control)Panel_Global_PadEmu).PerformLayout();
		((Control)Panel_Global_GSM).ResumeLayout(false);
		((Control)Panel_Global_GSM).PerformLayout();
		((Control)this).ResumeLayout(false);
	}
}
