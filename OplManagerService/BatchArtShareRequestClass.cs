using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "BatchArtShareRequestClass", Namespace = "http://oplmanager.no-ip.info/")]
public class BatchArtShareRequestClass
{
	private GameType GameTypeField;

	private string GameIDField;

	private string HashedICOField;

	private string HashedCOVField;

	private string HashedCOV2Field;

	private string HashedLABField;

	private string HashedLGOField;

	private string HashedSCRField;

	private string HashedSCR2Field;

	private string HashedBGField;

	private string OrigICOField;

	private string OrigCOVField;

	private string OrigCOV2Field;

	private string OrigLABField;

	private string OrigLGOField;

	private string OrigSCRField;

	private string OrigSCR2Field;

	private string OrigBGField;

	[DataMember(IsRequired = true)]
	public GameType GameType
	{
		get
		{
			return GameTypeField;
		}
		set
		{
			GameTypeField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 1)]
	public string GameID
	{
		get
		{
			return GameIDField;
		}
		set
		{
			GameIDField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 2)]
	public string HashedICO
	{
		get
		{
			return HashedICOField;
		}
		set
		{
			HashedICOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 3)]
	public string HashedCOV
	{
		get
		{
			return HashedCOVField;
		}
		set
		{
			HashedCOVField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 4)]
	public string HashedCOV2
	{
		get
		{
			return HashedCOV2Field;
		}
		set
		{
			HashedCOV2Field = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 5)]
	public string HashedLAB
	{
		get
		{
			return HashedLABField;
		}
		set
		{
			HashedLABField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 6)]
	public string HashedLGO
	{
		get
		{
			return HashedLGOField;
		}
		set
		{
			HashedLGOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 7)]
	public string HashedSCR
	{
		get
		{
			return HashedSCRField;
		}
		set
		{
			HashedSCRField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 8)]
	public string HashedSCR2
	{
		get
		{
			return HashedSCR2Field;
		}
		set
		{
			HashedSCR2Field = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 9)]
	public string HashedBG
	{
		get
		{
			return HashedBGField;
		}
		set
		{
			HashedBGField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 10)]
	public string OrigICO
	{
		get
		{
			return OrigICOField;
		}
		set
		{
			OrigICOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 11)]
	public string OrigCOV
	{
		get
		{
			return OrigCOVField;
		}
		set
		{
			OrigCOVField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 12)]
	public string OrigCOV2
	{
		get
		{
			return OrigCOV2Field;
		}
		set
		{
			OrigCOV2Field = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 13)]
	public string OrigLAB
	{
		get
		{
			return OrigLABField;
		}
		set
		{
			OrigLABField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 14)]
	public string OrigLGO
	{
		get
		{
			return OrigLGOField;
		}
		set
		{
			OrigLGOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 15)]
	public string OrigSCR
	{
		get
		{
			return OrigSCRField;
		}
		set
		{
			OrigSCRField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 16)]
	public string OrigSCR2
	{
		get
		{
			return OrigSCR2Field;
		}
		set
		{
			OrigSCR2Field = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 17)]
	public string OrigBG
	{
		get
		{
			return OrigBGField;
		}
		set
		{
			OrigBGField = value;
		}
	}
}
