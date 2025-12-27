using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "VersionDesktopInfo", Namespace = "http://oplmanager.no-ip.info/")]
public class VersionDesktopInfo
{
	private int majorField;

	private int minorField;

	private int versionidField;

	private string dateField;

	private string urlField;

	private string changesField;

	[DataMember(IsRequired = true)]
	public int major
	{
		get
		{
			return majorField;
		}
		set
		{
			majorField = value;
		}
	}

	[DataMember(IsRequired = true)]
	public int minor
	{
		get
		{
			return minorField;
		}
		set
		{
			minorField = value;
		}
	}

	[DataMember(IsRequired = true)]
	public int versionid
	{
		get
		{
			return versionidField;
		}
		set
		{
			versionidField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 3)]
	public string date
	{
		get
		{
			return dateField;
		}
		set
		{
			dateField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 4)]
	public string url
	{
		get
		{
			return urlField;
		}
		set
		{
			urlField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 5)]
	public string changes
	{
		get
		{
			return changesField;
		}
		set
		{
			changesField = value;
		}
	}
}
