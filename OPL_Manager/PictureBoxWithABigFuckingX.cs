using System;
using System.Drawing;
using System.Windows.Forms;

namespace OPL_Manager;

public class PictureBoxWithABigFuckingX : PictureBox
{
	public bool HasErrorBorder { get; set; }

	protected override void OnPaint(PaintEventArgs e)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		((PictureBox)this).OnPaint(e);
		if (HasErrorBorder)
		{
			Pen val = new Pen(Color.Red, 5f);
			try
			{
				e.Graphics.DrawLine(val, 0, 0, ((Control)this).Width, ((Control)this).Height);
				e.Graphics.DrawLine(val, ((Control)this).Width, 0, 0, ((Control)this).Height);
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
	}
}
