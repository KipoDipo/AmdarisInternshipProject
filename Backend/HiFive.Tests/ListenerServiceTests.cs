using HiFive.Application.DTOs.Listener;
using HiFive.Application.Services;
using HiFive.Domain.Models.Users;
using HiFive.Infrastructure;
using HiFive.Infrastructure.Db;
using HiFive.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace HiFive.Tests;

public class ListenerServiceTests
{
	DbContextOptions<ApplicationDbContext> options;

	public ListenerServiceTests()
	{
		options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase("testing_grounds")
			.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
			.Options;
	}


	[Fact]
	public async Task GetListenerById_ReturnsListener()
	{
		// Arrange
		Guid id1 = Guid.NewGuid();
		Guid id2 = Guid.NewGuid();

		var fakeUsers = new List<Listener>
		{
			new Listener() { Id = id1, DisplayName = "Display1"},
			new Listener() { Id = id2, DisplayName = "Display2" }
		}.AsQueryable();

		using (var context = new ApplicationDbContext(options))
		{
			context.Listeners.AddRange(fakeUsers);
			await context.SaveChangesAsync();
		}

		using (var context = new ApplicationDbContext(options))
		{
			var store = new Mock<IUserStore<ApplicationUser>>();
			var mockUserManager = new Mock<UserManager<ApplicationUser>>(
				store.Object, null, null, null, null, null, null, null, null
			);

			mockUserManager.Setup(x => x.Users)
				.Returns(context.Users);

			var listenerService = new ListenerService(new UnitOfWork(context, mockUserManager.Object, null), new Validator());

			// Act
			var result = await listenerService.GetListenerByIdAsync(id1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(id1, result.Id);
			Assert.Equal("Display1", result.DisplayName);
		}
	}

	[Fact]
	public async Task CreateListener_ReturnsListener()
	{
		// Arrange

		var listener = new ListenerCreateDto()
		{
			UserName = "TestListener",
			Password = "Abc123!",
			Email = "aaaa@a.aa",
			DisplayName = "DisplayName",
		};

		using (var context = new ApplicationDbContext(options))
		{
			var store = new Mock<IUserStore<ApplicationUser>>();
			var mockUserManager = new Mock<UserManager<ApplicationUser>>(
				store.Object, null, null, null, null, null, null, null, null
			);

			mockUserManager.Setup(x => x.Users)
				.Returns(context.Users);

			Guid id = Guid.NewGuid();
			var listenerObj = new Listener();

			mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), listener.Password))
				.Callback((ApplicationUser user, string password) =>
				{
					user.Id = id;
					user.PasswordHash = password.GetHashCode().ToString();
					user.Email = listener.Email;
					user.UserName = listener.UserName;
					user.NormalizedEmail = listener.Email.ToUpper();
					user.NormalizedUserName = listener.UserName.ToUpper();
				})
				.Returns(Task.FromResult(IdentityResult.Success));

			var listenerService = new ListenerService(new UnitOfWork(context, mockUserManager.Object, null), new Validator());

			// Act
			var result = await listenerService.CreateListenerAsync(listener);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(id, result.Id);
			Assert.Equal("DisplayName", result.DisplayName);
		}
	}
}
