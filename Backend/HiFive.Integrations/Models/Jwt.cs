﻿namespace HiFive.Integrations.Models;

public class Jwt
{
	public string Issuer { get; set; }
	public string Audience { get; set; }
	public string SecretKey { get; set; }
}