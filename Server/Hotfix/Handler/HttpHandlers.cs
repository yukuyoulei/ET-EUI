using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
	[HttpHandler(SceneType.Process, "/http")]
	public class HttpHandlers : IHttpHandler
	{
		public async ETTask Handle(Entity domain, HttpListenerContext context)
		{
			Log.Debug($"{context.Request}");
			var stream = context.Response.OutputStream;
			stream.Write(Encoding.UTF8.GetBytes($"Hello everyone, this is HttpHandler\n{context.Request.Url.ToString()}"));
			await ETTask.CompletedTask;
		}
	}
}
