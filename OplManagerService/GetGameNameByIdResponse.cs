using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[MessageContract(IsWrapped = false)]
public class GetGameNameByIdResponse
{
	[MessageBodyMember(Name = "GetGameNameByIdResponse", Namespace = "http://oplmanager.no-ip.info/", Order = 0)]
	public GetGameNameByIdResponseBody Body;

	public GetGameNameByIdResponse()
	{
	}

	public GetGameNameByIdResponse(GetGameNameByIdResponseBody Body)
	{
		this.Body = Body;
	}
}
