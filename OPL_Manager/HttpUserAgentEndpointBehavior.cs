using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace OPL_Manager;

public class HttpUserAgentEndpointBehavior : IEndpointBehavior
{
	private string m_userAgent;

	public HttpUserAgentEndpointBehavior(string agent)
	{
		m_userAgent = agent;
	}

	public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
	{
	}

	public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
	{
		HttpUserAgentMessageInspector item = new HttpUserAgentMessageInspector(m_userAgent);
		clientRuntime.ClientMessageInspectors.Add(item);
	}

	public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
	{
	}

	public void Validate(ServiceEndpoint endpoint)
	{
	}
}
