using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "ServerStatus", Namespace = "http://oplmanager.no-ip.info/")]
public class ServerStatus
{
	private string userIDField;

	private long usersOnlineField;

	private long countIcosField;

	private long countCovField;

	private long countCov2Field;

	private long countScrField;

	private long countBgField;

	private long countLabField;

	private long countLgoField;

	private string serverTimeField;

	[DataMember(EmitDefaultValue = false)]
	public string userID
	{
		get
		{
			return userIDField;
		}
		set
		{
			userIDField = value;
		}
	}

	[DataMember(IsRequired = true)]
	public long usersOnline
	{
		get
		{
			return usersOnlineField;
		}
		set
		{
			usersOnlineField = value;
		}
	}

	[DataMember(IsRequired = true, Order = 2)]
	public long countIcos
	{
		get
		{
			return countIcosField;
		}
		set
		{
			countIcosField = value;
		}
	}

	[DataMember(IsRequired = true, Order = 3)]
	public long countCov
	{
		get
		{
			return countCovField;
		}
		set
		{
			countCovField = value;
		}
	}

	[DataMember(IsRequired = true, Order = 4)]
	public long countCov2
	{
		get
		{
			return countCov2Field;
		}
		set
		{
			countCov2Field = value;
		}
	}

	[DataMember(IsRequired = true, Order = 5)]
	public long countScr
	{
		get
		{
			return countScrField;
		}
		set
		{
			countScrField = value;
		}
	}

	[DataMember(IsRequired = true, Order = 6)]
	public long countBg
	{
		get
		{
			return countBgField;
		}
		set
		{
			countBgField = value;
		}
	}

	[DataMember(IsRequired = true, Order = 7)]
	public long countLab
	{
		get
		{
			return countLabField;
		}
		set
		{
			countLabField = value;
		}
	}

	[DataMember(IsRequired = true, Order = 8)]
	public long countLgo
	{
		get
		{
			return countLgoField;
		}
		set
		{
			countLgoField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 9)]
	public string serverTime
	{
		get
		{
			return serverTimeField;
		}
		set
		{
			serverTimeField = value;
		}
	}
}
