using System;
using System.Drawing;
using System.Windows.Forms;
using OPL_Manager.My.Resources;

namespace OPL_Manager;

public class BaseForm : Form
{
	protected override void OnCreateControl()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		((Form)this).OnCreateControl();
		((Control)this).Font = new Font(new FontFamily("Microsoft Sans Serif"), 8f);
	}

	protected override void OnLoad(EventArgs e)
	{
		((Form)this).StartPosition = (FormStartPosition)4;
		((Form)this).Icon = Resources.icon;
		((Form)this).OnLoad(e);
	}
}
