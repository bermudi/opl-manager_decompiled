namespace OPL_Manager;

public class ComboCfgDescValTextClass
{
	public string Value { get; private set; }

	public string ValueText { get; private set; }

	public string Description { get; private set; }

	public ComboCfgDescValTextClass(string _ValueText, string _Value, string _Description)
	{
		ValueText = _ValueText;
		Value = _Value;
		Description = _Description;
	}
}
