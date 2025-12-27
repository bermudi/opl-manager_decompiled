using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[DataContract(Namespace = "http://oplmanager.no-ip.info/")]
public class ArtSearchBatchRequestBody
{
	[DataMember(EmitDefaultValue = false, Order = 0)]
	public string userID;

	[DataMember(EmitDefaultValue = false, Order = 1)]
	public ArtSearchBatchRequestClass[] games;

	public ArtSearchBatchRequestBody()
	{
	}

	public ArtSearchBatchRequestBody(string userID, ArtSearchBatchRequestClass[] games)
	{
		this.userID = userID;
		this.games = games;
	}
}
