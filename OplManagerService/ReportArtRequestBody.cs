using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[EditorBrowsable(EditorBrowsableState.Advanced)]
[DataContract(Namespace = "http://oplmanager.no-ip.info/")]
public class ReportArtRequestBody
{
	[DataMember(EmitDefaultValue = false, Order = 0)]
	public string userID;

	[DataMember(Order = 1)]
	public GameType GameType;

	[DataMember(Order = 2)]
	public ArtType ArtType;

	[DataMember(EmitDefaultValue = false, Order = 3)]
	public string ArtGameID;

	[DataMember(EmitDefaultValue = false, Order = 4)]
	public string ArtFile;

	[DataMember(EmitDefaultValue = false, Order = 5)]
	public string ArtComments;

	[DataMember(EmitDefaultValue = false, Order = 6)]
	public ArtUploadRequestClass ArtReplacement;

	public ReportArtRequestBody()
	{
	}

	public ReportArtRequestBody(string userID, GameType GameType, ArtType ArtType, string ArtGameID, string ArtFile, string ArtComments, ArtUploadRequestClass ArtReplacement)
	{
		this.userID = userID;
		this.GameType = GameType;
		this.ArtType = ArtType;
		this.ArtGameID = ArtGameID;
		this.ArtFile = ArtFile;
		this.ArtComments = ArtComments;
		this.ArtReplacement = ArtReplacement;
	}
}
