using System.Windows.Forms;

namespace OPL_Manager;

public class CustomListView : ListView
{
	public ListViewItem SelectedItem => ((ListView)this).SelectedItems[0];

	public int SelectedGameID => (int)((ListView)this).SelectedItems[0].Tag;

	public GameInfo SelectedGame => GameProvider.get_GetGame((int)((ListView)this).SelectedItems[0].Tag);
}
