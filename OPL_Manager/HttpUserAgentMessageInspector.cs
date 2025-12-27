using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace OPL_Manager;

public class HttpUserAgentMessageInspector : IClientMessageInspector
{
	private const string USER_AGENT_HTTP_HEADER = "user-agent";

	private string m_userAgent;

	public HttpUserAgentMessageInspector(string userAgent)
	{
		m_userAgent = userAgent;
	}

	public void AfterReceiveReply(ref Message reply, object correlationState)
	{
	}

	public object BeforeSendRequest(ref Message request, IClientChannel channel)
	{
		object value = null;
		if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out value))
		{
			HttpRequestMessageProperty httpRequestMessageProperty = value as HttpRequestMessageProperty;
			if (string.IsNullOrEmpty(httpRequestMessageProperty.Headers["user-agent"]))
			{
				httpRequestMessageProperty.Headers["user-agent"] = m_userAgent;
			}
		}
		else
		{
			HttpRequestMessageProperty httpRequestMessageProperty = new HttpRequestMessageProperty();
			httpRequestMessageProperty.Headers.Add("user-agent", m_userAgent);
			request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessageProperty);
		}
		return null;
	}
}
