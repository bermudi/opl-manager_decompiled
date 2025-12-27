using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "GameART", Namespace = "http://oplmanager.no-ip.info/")]
public class GameART
{
	private GameType TYPEField;

	private string IDField;

	private string COVField;

	private string COV2Field;

	private string ICOField;

	private string LGOField;

	private string LABField;

	private ArrayOfString SCRField;

	private ArrayOfString BGField;

	private string ExCOVField;

	private string ExCOV2Field;

	private string ExICOField;

	private string ExLGOField;

	private string ExLABField;

	[DataMember(IsRequired = true)]
	public GameType TYPE
	{
		get
		{
			return TYPEField;
		}
		set
		{
			TYPEField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 1)]
	public string ID
	{
		get
		{
			return IDField;
		}
		set
		{
			IDField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 2)]
	public string COV
	{
		get
		{
			return COVField;
		}
		set
		{
			COVField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 3)]
	public string COV2
	{
		get
		{
			return COV2Field;
		}
		set
		{
			COV2Field = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 4)]
	public string ICO
	{
		get
		{
			return ICOField;
		}
		set
		{
			ICOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 5)]
	public string LGO
	{
		get
		{
			return LGOField;
		}
		set
		{
			LGOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 6)]
	public string LAB
	{
		get
		{
			return LABField;
		}
		set
		{
			LABField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 7)]
	public ArrayOfString SCR
	{
		get
		{
			return SCRField;
		}
		set
		{
			SCRField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 8)]
	public ArrayOfString BG
	{
		get
		{
			return BGField;
		}
		set
		{
			BGField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 9)]
	public string ExCOV
	{
		get
		{
			return ExCOVField;
		}
		set
		{
			ExCOVField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 10)]
	public string ExCOV2
	{
		get
		{
			return ExCOV2Field;
		}
		set
		{
			ExCOV2Field = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 11)]
	public string ExICO
	{
		get
		{
			return ExICOField;
		}
		set
		{
			ExICOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 12)]
	public string ExLGO
	{
		get
		{
			return ExLGOField;
		}
		set
		{
			ExLGOField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 13)]
	public string ExLAB
	{
		get
		{
			return ExLABField;
		}
		set
		{
			ExLABField = value;
		}
	}
}
