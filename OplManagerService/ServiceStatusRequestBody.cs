using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[DataContract(Namespace = "http://oplmanager.no-ip.info/")]
public class ServiceStatusRequestBody
{
	[DataMember(EmitDefaultValue = false, Order = 0)]
	public string userID;

	[DataMember(Order = 1)]
	public int versionid;

	public ServiceStatusRequestBody()
	{
	}

	public ServiceStatusRequestBody(string userID, int versionid)
	{
		this.userID = userID;
		this.versionid = versionid;
	}
}
