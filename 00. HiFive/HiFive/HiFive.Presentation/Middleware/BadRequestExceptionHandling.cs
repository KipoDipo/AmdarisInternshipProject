using HiFive.Application.Exceptions;
using HiFive.Infrastructure.Exceptions;
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
		catch (IdentityCreationException ex)
		{
			_logger.LogWarning(ex, "Identity creation request exception occurred: {Message}", ex.Message);
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			context.Response.ContentType = "application/json";

			var result = JsonSerializer.Serialize(new
			{
				StatusCode = context.Response.StatusCode,
				Message = ex.Message,
				Errors = ex.Errors
			});

			await context.Response.WriteAsync(result);
		}
		catch (UnauthorizedException ex)
		{
			_logger.LogWarning(ex, "Unauthorized exception occured: {Message}", ex.Message);
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.Response.ContentType = "application/json";

			var result = JsonSerializer.Serialize(new
			{
				StatusCode = context.Response.StatusCode,
				Message = ex.Message,
			});

			await context.Response.WriteAsync(result);
		}
		catch (HiFiveException ex)
		{
			_logger.LogWarning(ex, "Application exception occurred: {Message}", ex.Message);
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
		catch (Exception ex)
		{
			_logger.LogError(ex, "Internal error occurred: {Message}", ex.Message);
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			context.Response.ContentType = "application/json";
			var result = JsonSerializer.Serialize(new
			{
				StatusCode = context.Response.StatusCode,
				Message = "Internal server error..."
			});

			await context.Response.WriteAsync(result);

		}
	}
}
