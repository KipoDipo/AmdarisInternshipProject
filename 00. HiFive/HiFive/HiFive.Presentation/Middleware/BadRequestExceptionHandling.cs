using System.Text.Json;

namespace HiFive.Presentation.Middleware;

public class BadRequestExceptionHandling
{
	private readonly RequestDelegate _next;
	private readonly ILogger<BadRequestExceptionHandling> _logger;

	public BadRequestExceptionHandling(RequestDelegate next, ILogger<BadRequestExceptionHandling> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Bad request exception occurred: {Message}", ex.Message);
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			context.Response.ContentType = "application/json";
			var result = JsonSerializer.Serialize(new
			{
				StatusCode = context.Response.StatusCode,
				Message = ex.Message,
				Details = ex.InnerException?.Message
			});

			await context.Response.WriteAsync(result);
		}
	}
}
