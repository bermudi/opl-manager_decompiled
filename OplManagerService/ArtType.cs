using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace OplManagerService;

[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "ArtType", Namespace = "http://oplmanager.no-ip.info/")]
public enum ArtType
{
	[EnumMember]
	ICO,
	[EnumMember]
	COV,
	[EnumMember]
	COV2,
	[EnumMember]
	SCR,
	[EnumMember]
	SCR2,
	[EnumMember]
	BG,
	[EnumMember]
	LAB,
	[EnumMember]
	LGO
}
