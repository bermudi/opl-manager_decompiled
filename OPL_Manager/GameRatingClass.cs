using System.Drawing;

namespace OPL_Manager;

public class GameRatingClass
{
	public string Value { get; set; }

	public string ValueText { get; set; }

	public Bitmap Icon { get; private set; }

	public GameRatingClass(string _ValueText, string _Value, Bitmap _Icon)
	{
		ValueText = _ValueText;
		Value = _Value;
		Icon = _Icon;
	}
}
