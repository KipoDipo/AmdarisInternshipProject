using HiFive.Application.Contracts.Services.Contracts;
using HiFive.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace HiFive.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IListenerService _listenerService;

	public PaymentController(ICurrentUserService currentUserService, IListenerService listenerService)
	{
		_currentUserService = currentUserService;
		_listenerService = listenerService;
	}

	[HttpPost]
	public async Task<IActionResult> CreateCheckoutSession()
	{
		var options = new SessionCreateOptions
		{
			PaymentMethodTypes = new List<string> { "card" },
			LineItems = new List<SessionLineItemOptions>
			{
				new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = 999,
						Currency = "bgn",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = "Hi-Five Premium",
						},
						Recurring = new SessionLineItemPriceDataRecurringOptions
						{
							Interval = "month"
						}
					},
					Quantity = 1,
				},
			},
			Mode = "subscription",
			SuccessUrl = "http://localhost:5173/subscribed-success",
			CancelUrl = "http://localhost:5173/subscribed-fail",
			Metadata = new Dictionary<string, string>
			{
				{ "userId", _currentUserService.Id.ToString() }
			}
		};

		try
		{
			var service = new SessionService();
			Session session = await service.CreateAsync(options);
			return Ok(new { id = session.Id });
		}
		catch (StripeException e)
		{
			return BadRequest(new { error = e.Message });
		}
	}

	// TODO: This should be done with a webhook
	[HttpPost("subscribe")]
	public async Task<IActionResult> Subscribe()
	{
		await _listenerService.SubscribeListener(_currentUserService.Id);
		return Ok();
	}
}
