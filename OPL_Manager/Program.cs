using System;
using System.Drawing;
using System.Windows.Forms;

namespace OPL_Manager;

internal class Program
{
	public static OPLM_Main MainFormInst;

	[STAThread]
	private static void Main()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		Application.EnableVisualStyles();
		Application.SetHighDpiMode((HighDpiMode)3);
		Application.SetDefaultFont(new Font(new FontFamily("Microsoft Sans Serif"), 8f));
		MainFormInst = new OPLM_Main();
		Application.Run((Form)(object)MainFormInst);
	}
}
