using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[MessageContract(IsWrapped = false)]
public class ArtUploadResponse
{
	[MessageBodyMember(Name = "ArtUploadResponse", Namespace = "http://oplmanager.no-ip.info/", Order = 0)]
	public ArtUploadResponseBody Body;

	public ArtUploadResponse()
	{
	}

	public ArtUploadResponse(ArtUploadResponseBody Body)
	{
		this.Body = Body;
	}
}
