using ZstdNet;

namespace BloodTypess.Web.Middleware
{
	public class ZstdCompressionMiddleware
	{
		private readonly RequestDelegate _next;

		public ZstdCompressionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var acceptEncoding = context.Request.Headers["Accept-Encoding"].ToString();

			if (acceptEncoding.Contains("zstd"))
			{
				var originalBodyStream = context.Response.Body;
				using var tempStream = new MemoryStream();
				context.Response.Body = tempStream;

				await _next(context); // Allow MVC to process

				tempStream.Seek(0, SeekOrigin.Begin);
				var uncompressedData = tempStream.ToArray();

				using var compressor = new Compressor();
				var compressedData = compressor.Wrap(uncompressedData);

				context.Response.Body = originalBodyStream;
				context.Response.Headers["Content-Encoding"] = "zstd";
				context.Response.Headers["Vary"] = "Accept-Encoding";
				context.Response.ContentLength = compressedData.Length;

				await context.Response.Body.WriteAsync(compressedData, 0, compressedData.Length);
			}
			else
			{
				await _next(context);
			}
		}
	}
}
