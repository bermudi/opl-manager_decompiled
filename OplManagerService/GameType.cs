using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace OplManagerService;

[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "GameType", Namespace = "http://oplmanager.no-ip.info/")]
public enum GameType
{
	[EnumMember]
	INVALID,
	[EnumMember]
	POPS,
	[EnumMember]
	PS2,
	[EnumMember]
	APP
}
