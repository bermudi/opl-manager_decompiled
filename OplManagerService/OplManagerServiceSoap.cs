using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace OplManagerService;

[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
[ServiceContract(Namespace = "http://oplmanager.no-ip.info/", ConfigurationName = "OplManagerService.OplManagerServiceSoap")]
public interface OplManagerServiceSoap
{
	[OperationContract(Action = "http://oplmanager.no-ip.info/GetDesktopAppVersion", ReplyAction = "*")]
	GetDesktopAppVersionResponse GetDesktopAppVersion(GetDesktopAppVersionRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/GetDesktopAppVersion", ReplyAction = "*")]
	Task<GetDesktopAppVersionResponse> GetDesktopAppVersionAsync(GetDesktopAppVersionRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ServiceStatus", ReplyAction = "*")]
	ServiceStatusResponse ServiceStatus(ServiceStatusRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ServiceStatus", ReplyAction = "*")]
	Task<ServiceStatusResponse> ServiceStatusAsync(ServiceStatusRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ArtSearchSingle", ReplyAction = "*")]
	ArtSearchSingleResponse ArtSearchSingle(ArtSearchSingleRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ArtSearchSingle", ReplyAction = "*")]
	Task<ArtSearchSingleResponse> ArtSearchSingleAsync(ArtSearchSingleRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ArtSearchBatch", ReplyAction = "*")]
	ArtSearchBatchResponse ArtSearchBatch(ArtSearchBatchRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ArtSearchBatch", ReplyAction = "*")]
	Task<ArtSearchBatchResponse> ArtSearchBatchAsync(ArtSearchBatchRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/BatchArtShareCheck", ReplyAction = "*")]
	BatchArtShareCheckResponse BatchArtShareCheck(BatchArtShareCheckRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/BatchArtShareCheck", ReplyAction = "*")]
	Task<BatchArtShareCheckResponse> BatchArtShareCheckAsync(BatchArtShareCheckRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ArtUpload", ReplyAction = "*")]
	ArtUploadResponse ArtUpload(ArtUploadRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ArtUpload", ReplyAction = "*")]
	Task<ArtUploadResponse> ArtUploadAsync(ArtUploadRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ReportArt", ReplyAction = "*")]
	ReportArtResponse ReportArt(ReportArtRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/ReportArt", ReplyAction = "*")]
	Task<ReportArtResponse> ReportArtAsync(ReportArtRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/GetGameNameById", ReplyAction = "*")]
	GetGameNameByIdResponse GetGameNameById(GetGameNameByIdRequest request);

	[OperationContract(Action = "http://oplmanager.no-ip.info/GetGameNameById", ReplyAction = "*")]
	Task<GetGameNameByIdResponse> GetGameNameByIdAsync(GetGameNameByIdRequest request);
}
