using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[DataContract(Namespace = "http://oplmanager.no-ip.info/")]
public class ArtSearchSingleResponseBody
{
	[DataMember(EmitDefaultValue = false, Order = 0)]
	public GameART ArtSearchSingleResult;

	public ArtSearchSingleResponseBody()
	{
	}

	public ArtSearchSingleResponseBody(GameART ArtSearchSingleResult)
	{
		this.ArtSearchSingleResult = ArtSearchSingleResult;
	}
}
