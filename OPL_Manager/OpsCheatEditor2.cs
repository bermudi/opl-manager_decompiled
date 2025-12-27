using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class OpsCheatEditor2 : BaseForm
{
	private string CHT_File;

	private bool SavedChanges = true;

	private GameInfo SelectedGame;

	private IContainer components;

	internal TextBox TB_CheatCode;

	internal Button B_Save;

	internal Button B_Delete;

	internal TextBox TB_FilePath;

	public OpsCheatEditor2()
	{
		InitializeComponent();
	}

	public DialogResult ShowDialog(GameInfo _SelectedGame)
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Invalid comparison between Unknown and I4
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		SelectedGame = _SelectedGame;
		((Control)this).Text = Translated.OpsCheatEditor2_Title;
		((Control)B_Save).Text = Translated.GLOBAL_BUTTON_SAVE;
		((Control)B_Delete).Text = Translated.OpsCheatEditor__Delete;
		CHT_File = OplFolders.Main + "CHT\\" + SelectedGame.ID + ".cht";
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
		((Control)TB_CheatCode).Text = "\"" + SelectedGame.Title + " /ID " + SelectedGame.ID + "\"" + Environment.NewLine + "Mastercode" + Environment.NewLine;
	}

	private void WriteCht()
	{
		File.WriteAllText(CHT_File, ((Control)TB_CheatCode).Text);
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
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OpsCheatEditor2));
		TB_CheatCode = new TextBox();
		B_Save = new Button();
		B_Delete = new Button();
		TB_FilePath = new TextBox();
		((Control)this).SuspendLayout();
		((Control)TB_CheatCode).Anchor = (AnchorStyles)15;
		((Control)TB_CheatCode).Location = new Point(12, 30);
		((TextBoxBase)TB_CheatCode).Multiline = true;
		((Control)TB_CheatCode).Name = "TB_CheatCode";
		TB_CheatCode.ScrollBars = (ScrollBars)3;
		((Control)TB_CheatCode).Size = new Size(520, 266);
		((Control)TB_CheatCode).TabIndex = 0;
		((Control)B_Save).Anchor = (AnchorStyles)6;
		((Control)B_Save).Location = new Point(98, 304);
		((Control)B_Save).Name = "B_Save";
		((Control)B_Save).Size = new Size(137, 23);
		((Control)B_Save).TabIndex = 1;
		((Control)B_Save).Text = "Save";
		((ButtonBase)B_Save).UseVisualStyleBackColor = true;
		((Control)B_Save).Click += B_Save_Click;
		((Control)B_Delete).Anchor = (AnchorStyles)10;
		((Control)B_Delete).Location = new Point(267, 304);
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
		((Control)TB_FilePath).Size = new Size(519, 20);
		((Control)TB_FilePath).TabIndex = 3;
		((ContainerControl)this).AutoScaleDimensions = new SizeF(6f, 13f);
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)1;
		((Form)this).ClientSize = new Size(544, 339);
		((Control)this).Controls.Add((Control)(object)TB_FilePath);
		((Control)this).Controls.Add((Control)(object)B_Delete);
		((Control)this).Controls.Add((Control)(object)B_Save);
		((Control)this).Controls.Add((Control)(object)TB_CheatCode);
		((Control)this).Font = new Font("Microsoft Sans Serif", 8f, (FontStyle)0, (GraphicsUnit)3);
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Control)this).MinimumSize = new Size(560, 330);
		((Control)this).Name = "OpsCheatEditor2";
		((Form)this).StartPosition = (FormStartPosition)4;
		((Control)this).Text = "Cheat Editor";
		((Form)this).FormClosing += new FormClosingEventHandler(CheatEditor_FormClosing);
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
