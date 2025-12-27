using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[DataContract(Namespace = "http://oplmanager.no-ip.info/")]
public class ArtSearchSingleRequestBody
{
	[DataMember(Order = 0)]
	public GameType type;

	[DataMember(EmitDefaultValue = false, Order = 1)]
	public string userID;

	[DataMember(EmitDefaultValue = false, Order = 2)]
	public string gameId;

	public ArtSearchSingleRequestBody()
	{
	}

	public ArtSearchSingleRequestBody(GameType type, string userID, string gameId)
	{
		this.type = type;
		this.userID = userID;
		this.gameId = gameId;
	}
}
