using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[DataContract(Namespace = "http://oplmanager.no-ip.info/")]
public class GetGameNameByIdRequestBody
{
	[DataMember(Order = 0)]
	public GameType type;

	[DataMember(EmitDefaultValue = false, Order = 1)]
	public string GameId;

	public GetGameNameByIdRequestBody()
	{
	}

	public GetGameNameByIdRequestBody(GameType type, string GameId)
	{
		this.type = type;
		this.GameId = GameId;
	}
}
