using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[MessageContract(IsWrapped = false)]
public class GetDesktopAppVersionResponse
{
	[MessageBodyMember(Name = "GetDesktopAppVersionResponse", Namespace = "http://oplmanager.no-ip.info/", Order = 0)]
	public GetDesktopAppVersionResponseBody Body;

	public GetDesktopAppVersionResponse()
	{
	}

	public GetDesktopAppVersionResponse(GetDesktopAppVersionResponseBody Body)
	{
		this.Body = Body;
	}
}
