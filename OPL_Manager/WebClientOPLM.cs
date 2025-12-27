using System.ComponentModel;
using System.Net;

namespace OPL_Manager;

[DesignerCategory("Code")]
public class WebClientOPLM : WebClient
{
	public WebClientOPLM()
	{
		base.Headers.Add(HttpRequestHeader.UserAgent, "OPL-Manager/24");
	}
}
