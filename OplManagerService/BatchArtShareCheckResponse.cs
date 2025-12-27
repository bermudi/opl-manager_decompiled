using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[MessageContract(IsWrapped = false)]
public class BatchArtShareCheckResponse
{
	[MessageBodyMember(Name = "BatchArtShareCheckResponse", Namespace = "http://oplmanager.no-ip.info/", Order = 0)]
	public BatchArtShareCheckResponseBody Body;

	public BatchArtShareCheckResponse()
	{
	}

	public BatchArtShareCheckResponse(BatchArtShareCheckResponseBody Body)
	{
		this.Body = Body;
	}
}
