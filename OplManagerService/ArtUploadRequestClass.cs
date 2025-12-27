using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[DataContract(Name = "ArtUploadRequestClass", Namespace = "http://oplmanager.no-ip.info/")]
public class ArtUploadRequestClass
{
	private string FileNameField;

	private byte[] FileDataField;

	private string FileHashField;

	private string GameIDField;

	private GameType GameTypeField;

	[DataMember(EmitDefaultValue = false)]
	public string FileName
	{
		get
		{
			return FileNameField;
		}
		set
		{
			FileNameField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 1)]
	public byte[] FileData
	{
		get
		{
			return FileDataField;
		}
		set
		{
			FileDataField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 2)]
	public string FileHash
	{
		get
		{
			return FileHashField;
		}
		set
		{
			FileHashField = value;
		}
	}

	[DataMember(EmitDefaultValue = false, Order = 3)]
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

	[DataMember(IsRequired = true, Order = 4)]
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
}
