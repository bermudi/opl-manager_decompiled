using System;
using System.Collections;
using System.Windows.Forms;

namespace OPL_Manager;

internal class ListViewComparer : IComparer
{
	private int m_ColumnNumber;

	private SortOrder m_SortOrder;

	public ListViewComparer(int column_number, SortOrder sort_order)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		m_ColumnNumber = column_number;
		m_SortOrder = sort_order;
	}

	public SortOrder getOrder()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return m_SortOrder;
	}

	public int Compare(object x, object y)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Invalid comparison between Unknown and I4
		ListViewItem val = (ListViewItem)x;
		ListViewItem val2 = (ListViewItem)y;
		string text = ((val.SubItems.Count <= m_ColumnNumber) ? "" : ((m_ColumnNumber != 3) ? val.SubItems[m_ColumnNumber].Text : val.SubItems[m_ColumnNumber].Tag.ToString()));
		string text2 = ((val2.SubItems.Count <= m_ColumnNumber) ? "" : ((m_ColumnNumber != 3) ? val2.SubItems[m_ColumnNumber].Text : val2.SubItems[m_ColumnNumber].Tag.ToString()));
		if ((int)m_SortOrder == 1)
		{
			if (CommonFuncs.IsNumeric(text) && CommonFuncs.IsNumeric(text2))
			{
				return double.Parse(text).CompareTo(double.Parse(text2));
			}
			if (CommonFuncs.IsDate(text) && CommonFuncs.IsDate(text2))
			{
				return DateTime.Parse(text).CompareTo(DateTime.Parse(text2));
			}
			return string.Compare(text, text2);
		}
		if (CommonFuncs.IsNumeric(text) && CommonFuncs.IsNumeric(text2))
		{
			return double.Parse(text2).CompareTo(double.Parse(text));
		}
		if (CommonFuncs.IsDate(text) && CommonFuncs.IsDate(text2))
		{
			return DateTime.Parse(text2).CompareTo(DateTime.Parse(text));
		}
		return string.Compare(text2, text);
	}
}
