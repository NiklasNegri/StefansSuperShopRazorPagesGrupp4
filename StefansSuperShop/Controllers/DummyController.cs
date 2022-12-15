using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace StefansSuperShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DummyController : ControllerBase
{
	public DummyController()
	{

	}

	[HttpGet]
	[AllowAnonymous]
	public void ReachMe()
	{
		Console.WriteLine("You've reached me");
	}
}
