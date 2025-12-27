using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "BatchArtShareResponseClass", Namespace = "http://oplmanager.no-ip.info/")]
public class BatchArtShareResponseClass
{
	private GameType GameTypeField;

	private string GameIDField;

	private string FileField;

	[DataMember(IsRequired = true)]
	public GameType GameType
	{
		get
		{
			return GameTypeField;
		}
		set
		{
			GameTypeField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 1)]
	public string GameID
	{
		get
		{
			return GameIDField;
		}
		set
		{
			GameIDField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 2)]
	public string File
	{
		get
		{
			return FileField;
		}
		set
		{
			FileField = value;
		}
	}
}
