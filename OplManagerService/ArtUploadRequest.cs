using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[MessageContract(IsWrapped = false)]
public class ArtUploadRequest
{
	[MessageBodyMember(Name = "ArtUpload", Namespace = "http://oplmanager.no-ip.info/", Order = 0)]
	public ArtUploadRequestBody Body;

	public ArtUploadRequest()
	{
	}

	public ArtUploadRequest(ArtUploadRequestBody Body)
	{
		this.Body = Body;
	}
}
