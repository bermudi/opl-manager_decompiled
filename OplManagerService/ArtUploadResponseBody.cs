using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[DataContract(Namespace = "http://oplmanager.no-ip.info/")]
public class ArtUploadResponseBody
{
	[DataMember(Order = 0)]
	public bool ArtUploadResult;

	public ArtUploadResponseBody()
	{
	}

	public ArtUploadResponseBody(bool ArtUploadResult)
	{
		this.ArtUploadResult = ArtUploadResult;
	}
}
