using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace OplManagerService;

[DebuggerStepThrough]
[GeneratedCode("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
public class OplManagerServiceSoapClient : ClientBase<OplManagerServiceSoap>, OplManagerServiceSoap
{
	public enum EndpointConfiguration
	{
		OplManagerServiceSoap,
		OplManagerServiceSoap12
	}

	public OplManagerServiceSoapClient(EndpointConfiguration endpointConfiguration)
		: base(GetBindingForEndpoint(endpointConfiguration), GetEndpointAddress(endpointConfiguration))
	{
		base.Endpoint.Name = endpointConfiguration.ToString();
	}

	public OplManagerServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress)
		: base(GetBindingForEndpoint(endpointConfiguration), new EndpointAddress(remoteAddress))
	{
		base.Endpoint.Name = endpointConfiguration.ToString();
	}

	public OplManagerServiceSoapClient(EndpointConfiguration endpointConfiguration, EndpointAddress remoteAddress)
		: base(GetBindingForEndpoint(endpointConfiguration), remoteAddress)
	{
		base.Endpoint.Name = endpointConfiguration.ToString();
	}

	public OplManagerServiceSoapClient(Binding binding, EndpointAddress remoteAddress)
		: base(binding, remoteAddress)
	{
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	GetDesktopAppVersionResponse OplManagerServiceSoap.GetDesktopAppVersion(GetDesktopAppVersionRequest request)
	{
		return base.Channel.GetDesktopAppVersion(request);
	}

	public VersionDesktopInfo GetDesktopAppVersion(int current)
	{
		GetDesktopAppVersionRequest getDesktopAppVersionRequest = new GetDesktopAppVersionRequest();
		getDesktopAppVersionRequest.Body = new GetDesktopAppVersionRequestBody();
		getDesktopAppVersionRequest.Body.current = current;
		return ((OplManagerServiceSoap)this).GetDesktopAppVersion(getDesktopAppVersionRequest).Body.GetDesktopAppVersionResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<GetDesktopAppVersionResponse> OplManagerServiceSoap.GetDesktopAppVersionAsync(GetDesktopAppVersionRequest request)
	{
		return base.Channel.GetDesktopAppVersionAsync(request);
	}

	public Task<GetDesktopAppVersionResponse> GetDesktopAppVersionAsync(int current)
	{
		GetDesktopAppVersionRequest getDesktopAppVersionRequest = new GetDesktopAppVersionRequest();
		getDesktopAppVersionRequest.Body = new GetDesktopAppVersionRequestBody();
		getDesktopAppVersionRequest.Body.current = current;
		return ((OplManagerServiceSoap)this).GetDesktopAppVersionAsync(getDesktopAppVersionRequest);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	ServiceStatusResponse OplManagerServiceSoap.ServiceStatus(ServiceStatusRequest request)
	{
		return base.Channel.ServiceStatus(request);
	}

	public ServerStatus ServiceStatus(string userID, int versionid)
	{
		ServiceStatusRequest serviceStatusRequest = new ServiceStatusRequest();
		serviceStatusRequest.Body = new ServiceStatusRequestBody();
		serviceStatusRequest.Body.userID = userID;
		serviceStatusRequest.Body.versionid = versionid;
		return ((OplManagerServiceSoap)this).ServiceStatus(serviceStatusRequest).Body.ServiceStatusResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<ServiceStatusResponse> OplManagerServiceSoap.ServiceStatusAsync(ServiceStatusRequest request)
	{
		return base.Channel.ServiceStatusAsync(request);
	}

	public Task<ServiceStatusResponse> ServiceStatusAsync(string userID, int versionid)
	{
		ServiceStatusRequest serviceStatusRequest = new ServiceStatusRequest();
		serviceStatusRequest.Body = new ServiceStatusRequestBody();
		serviceStatusRequest.Body.userID = userID;
		serviceStatusRequest.Body.versionid = versionid;
		return ((OplManagerServiceSoap)this).ServiceStatusAsync(serviceStatusRequest);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	ArtSearchSingleResponse OplManagerServiceSoap.ArtSearchSingle(ArtSearchSingleRequest request)
	{
		return base.Channel.ArtSearchSingle(request);
	}

	public GameART ArtSearchSingle(GameType type, string userID, string gameId)
	{
		ArtSearchSingleRequest artSearchSingleRequest = new ArtSearchSingleRequest();
		artSearchSingleRequest.Body = new ArtSearchSingleRequestBody();
		artSearchSingleRequest.Body.type = type;
		artSearchSingleRequest.Body.userID = userID;
		artSearchSingleRequest.Body.gameId = gameId;
		return ((OplManagerServiceSoap)this).ArtSearchSingle(artSearchSingleRequest).Body.ArtSearchSingleResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<ArtSearchSingleResponse> OplManagerServiceSoap.ArtSearchSingleAsync(ArtSearchSingleRequest request)
	{
		return base.Channel.ArtSearchSingleAsync(request);
	}

	public Task<ArtSearchSingleResponse> ArtSearchSingleAsync(GameType type, string userID, string gameId)
	{
		ArtSearchSingleRequest artSearchSingleRequest = new ArtSearchSingleRequest();
		artSearchSingleRequest.Body = new ArtSearchSingleRequestBody();
		artSearchSingleRequest.Body.type = type;
		artSearchSingleRequest.Body.userID = userID;
		artSearchSingleRequest.Body.gameId = gameId;
		return ((OplManagerServiceSoap)this).ArtSearchSingleAsync(artSearchSingleRequest);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	ArtSearchBatchResponse OplManagerServiceSoap.ArtSearchBatch(ArtSearchBatchRequest request)
	{
		return base.Channel.ArtSearchBatch(request);
	}

	public GameART[] ArtSearchBatch(string userID, ArtSearchBatchRequestClass[] games)
	{
		ArtSearchBatchRequest artSearchBatchRequest = new ArtSearchBatchRequest();
		artSearchBatchRequest.Body = new ArtSearchBatchRequestBody();
		artSearchBatchRequest.Body.userID = userID;
		artSearchBatchRequest.Body.games = games;
		return ((OplManagerServiceSoap)this).ArtSearchBatch(artSearchBatchRequest).Body.ArtSearchBatchResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<ArtSearchBatchResponse> OplManagerServiceSoap.ArtSearchBatchAsync(ArtSearchBatchRequest request)
	{
		return base.Channel.ArtSearchBatchAsync(request);
	}

	public Task<ArtSearchBatchResponse> ArtSearchBatchAsync(string userID, ArtSearchBatchRequestClass[] games)
	{
		ArtSearchBatchRequest artSearchBatchRequest = new ArtSearchBatchRequest();
		artSearchBatchRequest.Body = new ArtSearchBatchRequestBody();
		artSearchBatchRequest.Body.userID = userID;
		artSearchBatchRequest.Body.games = games;
		return ((OplManagerServiceSoap)this).ArtSearchBatchAsync(artSearchBatchRequest);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	BatchArtShareCheckResponse OplManagerServiceSoap.BatchArtShareCheck(BatchArtShareCheckRequest request)
	{
		return base.Channel.BatchArtShareCheck(request);
	}

	public BatchArtShareResponseClass[] BatchArtShareCheck(string userID, BatchArtShareRequestClass[] games)
	{
		BatchArtShareCheckRequest batchArtShareCheckRequest = new BatchArtShareCheckRequest();
		batchArtShareCheckRequest.Body = new BatchArtShareCheckRequestBody();
		batchArtShareCheckRequest.Body.userID = userID;
		batchArtShareCheckRequest.Body.games = games;
		return ((OplManagerServiceSoap)this).BatchArtShareCheck(batchArtShareCheckRequest).Body.BatchArtShareCheckResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<BatchArtShareCheckResponse> OplManagerServiceSoap.BatchArtShareCheckAsync(BatchArtShareCheckRequest request)
	{
		return base.Channel.BatchArtShareCheckAsync(request);
	}

	public Task<BatchArtShareCheckResponse> BatchArtShareCheckAsync(string userID, BatchArtShareRequestClass[] games)
	{
		BatchArtShareCheckRequest batchArtShareCheckRequest = new BatchArtShareCheckRequest();
		batchArtShareCheckRequest.Body = new BatchArtShareCheckRequestBody();
		batchArtShareCheckRequest.Body.userID = userID;
		batchArtShareCheckRequest.Body.games = games;
		return ((OplManagerServiceSoap)this).BatchArtShareCheckAsync(batchArtShareCheckRequest);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	ArtUploadResponse OplManagerServiceSoap.ArtUpload(ArtUploadRequest request)
	{
		return base.Channel.ArtUpload(request);
	}

	public bool ArtUpload(string userID, ArtUploadRequestClass uploaded)
	{
		ArtUploadRequest artUploadRequest = new ArtUploadRequest();
		artUploadRequest.Body = new ArtUploadRequestBody();
		artUploadRequest.Body.userID = userID;
		artUploadRequest.Body.uploaded = uploaded;
		return ((OplManagerServiceSoap)this).ArtUpload(artUploadRequest).Body.ArtUploadResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<ArtUploadResponse> OplManagerServiceSoap.ArtUploadAsync(ArtUploadRequest request)
	{
		return base.Channel.ArtUploadAsync(request);
	}

	public Task<ArtUploadResponse> ArtUploadAsync(string userID, ArtUploadRequestClass uploaded)
	{
		ArtUploadRequest artUploadRequest = new ArtUploadRequest();
		artUploadRequest.Body = new ArtUploadRequestBody();
		artUploadRequest.Body.userID = userID;
		artUploadRequest.Body.uploaded = uploaded;
		return ((OplManagerServiceSoap)this).ArtUploadAsync(artUploadRequest);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	ReportArtResponse OplManagerServiceSoap.ReportArt(ReportArtRequest request)
	{
		return base.Channel.ReportArt(request);
	}

	public bool ReportArt(string userID, GameType GameType, ArtType ArtType, string ArtGameID, string ArtFile, string ArtComments, ArtUploadRequestClass ArtReplacement)
	{
		ReportArtRequest reportArtRequest = new ReportArtRequest();
		reportArtRequest.Body = new ReportArtRequestBody();
		reportArtRequest.Body.userID = userID;
		reportArtRequest.Body.GameType = GameType;
		reportArtRequest.Body.ArtType = ArtType;
		reportArtRequest.Body.ArtGameID = ArtGameID;
		reportArtRequest.Body.ArtFile = ArtFile;
		reportArtRequest.Body.ArtComments = ArtComments;
		reportArtRequest.Body.ArtReplacement = ArtReplacement;
		return ((OplManagerServiceSoap)this).ReportArt(reportArtRequest).Body.ReportArtResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<ReportArtResponse> OplManagerServiceSoap.ReportArtAsync(ReportArtRequest request)
	{
		return base.Channel.ReportArtAsync(request);
	}

	public Task<ReportArtResponse> ReportArtAsync(string userID, GameType GameType, ArtType ArtType, string ArtGameID, string ArtFile, string ArtComments, ArtUploadRequestClass ArtReplacement)
	{
		ReportArtRequest reportArtRequest = new ReportArtRequest();
		reportArtRequest.Body = new ReportArtRequestBody();
		reportArtRequest.Body.userID = userID;
		reportArtRequest.Body.GameType = GameType;
		reportArtRequest.Body.ArtType = ArtType;
		reportArtRequest.Body.ArtGameID = ArtGameID;
		reportArtRequest.Body.ArtFile = ArtFile;
		reportArtRequest.Body.ArtComments = ArtComments;
		reportArtRequest.Body.ArtReplacement = ArtReplacement;
		return ((OplManagerServiceSoap)this).ReportArtAsync(reportArtRequest);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	GetGameNameByIdResponse OplManagerServiceSoap.GetGameNameById(GetGameNameByIdRequest request)
	{
		return base.Channel.GetGameNameById(request);
	}

	public string GetGameNameById(GameType type, string GameId)
	{
		GetGameNameByIdRequest getGameNameByIdRequest = new GetGameNameByIdRequest();
		getGameNameByIdRequest.Body = new GetGameNameByIdRequestBody();
		getGameNameByIdRequest.Body.type = type;
		getGameNameByIdRequest.Body.GameId = GameId;
		return ((OplManagerServiceSoap)this).GetGameNameById(getGameNameByIdRequest).Body.GetGameNameByIdResult;
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	Task<GetGameNameByIdResponse> OplManagerServiceSoap.GetGameNameByIdAsync(GetGameNameByIdRequest request)
	{
		return base.Channel.GetGameNameByIdAsync(request);
	}

	public Task<GetGameNameByIdResponse> GetGameNameByIdAsync(GameType type, string GameId)
	{
		GetGameNameByIdRequest getGameNameByIdRequest = new GetGameNameByIdRequest();
		getGameNameByIdRequest.Body = new GetGameNameByIdRequestBody();
		getGameNameByIdRequest.Body.type = type;
		getGameNameByIdRequest.Body.GameId = GameId;
		return ((OplManagerServiceSoap)this).GetGameNameByIdAsync(getGameNameByIdRequest);
	}

	public new virtual Task OpenAsync()
	{
		return Task.Factory.FromAsync(((ICommunicationObject)this).BeginOpen((AsyncCallback)null, (object)null), ((ICommunicationObject)this).EndOpen);
	}

	private static Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
	{
		switch (endpointConfiguration)
		{
		case EndpointConfiguration.OplManagerServiceSoap:
			return new BasicHttpBinding
			{
				MaxBufferSize = int.MaxValue,
				ReaderQuotas = XmlDictionaryReaderQuotas.Max,
				MaxReceivedMessageSize = 2147483647L,
				AllowCookies = true
			};
		case EndpointConfiguration.OplManagerServiceSoap12:
		{
			CustomBinding customBinding = new CustomBinding();
			TextMessageEncodingBindingElement item = new TextMessageEncodingBindingElement
			{
				MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None)
			};
			customBinding.Elements.Add(item);
			HttpTransportBindingElement item2 = new HttpTransportBindingElement
			{
				AllowCookies = true,
				MaxBufferSize = int.MaxValue,
				MaxReceivedMessageSize = 2147483647L
			};
			customBinding.Elements.Add(item2);
			return customBinding;
		}
		default:
			throw new InvalidOperationException($"Could not find endpoint with name '{endpointConfiguration}'.");
		}
	}

	private static EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
	{
		return endpointConfiguration switch
		{
			EndpointConfiguration.OplManagerServiceSoap => new EndpointAddress("http://192.168.93.64:85/API/V6/OplManagerService.asmx"), 
			EndpointConfiguration.OplManagerServiceSoap12 => new EndpointAddress("http://192.168.93.64:85/API/V6/OplManagerService.asmx"), 
			_ => throw new InvalidOperationException($"Could not find endpoint with name '{endpointConfiguration}'."), 
		};
	}
}
