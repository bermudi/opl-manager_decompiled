using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "ArtSearchBatchRequestClass", Namespace = "http://oplmanager.no-ip.info/")]
public class ArtSearchBatchRequestClass
{
	private string GameIDField;

	private GameType GameTypeField;

	[DataMember(EmitDefaultValue = false)]
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
}
