using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var user = Request.HttpContext.User;

        var options = new RestClientOptions("https://graph.microsoft.com/v1.0/me")
        {
            ThrowOnAnyError = true,
            MaxTimeout = 1000
        };
        var client = new RestClient(options);
        var request = new RestRequest()
            .AddHeader("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9.eyJhdWQiOiI5YjEwNzg1Yi00YzZkLTQ3ODEtYTk2Ni1kNzdlNzU4ZTJlYTgiLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vMDI5OTUyNjctNzA3NS00ZDFjLWI2MmQtZGU3YzlhMzNjMjUwL3YyLjAiLCJpYXQiOjE2Njc1ODM2ODgsIm5iZiI6MTY2NzU4MzY4OCwiZXhwIjoxNjY3NTg3NTg4LCJhaW8iOiJBYlFBUy84VEFBQUFMY0RkcnNmc2hjKzFwaTJ5QVdmUVprMDUrR2NRcjQ5VHE2OTBscVQ3emtzQThyVVB3STVUUVZaUjY0MnY1YVIxdnpQSnpPTGpqRHhDSHFwajIzM1lxczhBQnhSSzNyMlBIMnlMWFo1ZXQrb2pRWkNVR0NHTndJcENGUVhObzNiT1V1L1RpSExmVnBpbzhXb1FRamFWNVNmU1BPeXNHS3MyZElUVTkrUHFOQkdQbGJJZHdVcjlwY1VBN0JQVG5kbitsc3k2Y2JKMmN6UTZSQ1JFcHNyZzAwWEUxYkpIbDRoN1pCUkFic01PMHdrPSIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzkxODgwNDBkLTZjNjctNGM1Yi1iMTEyLTM2YTMwNGI2NmRhZC8iLCJuYW1lIjoiU3VsdG9uYmVrIFJha2hpbW92Iiwibm9uY2UiOiI2MzgwMzE4MDc4OTI1OTcxNjMuWldReVpqWXhZVFV0WVRjMk1TMDBZV0ZoTFdGaFkyWXROV0kyWm1OaE56ZG1NR016WmpJMU1UWmpOR010WkdKbE1TMDBOelJrTFRobVpESXRZMlV3TjJSaVpHSTFaakk0Iiwib2lkIjoiZDY5NzYyMWEtMzA3Zi00MTE1LTgwMDUtN2U2NmZlZDA2MDIwIiwicHJlZmVycmVkX3VzZXJuYW1lIjoic3VsdG9uYmVrLnJha2hpbW92QGdtYWlsLmNvbSIsInJoIjoiMC5BWGtBWjFLWkFuVndIRTIyTGQ1OG1qUENVRnQ0RUp0dFRJRkhxV2JYZm5XT0xxaVVBRWcuIiwic3ViIjoiYXNaUTJRRlhkWWtZY0VFOHR5T2xUM3R5d2hQUUR4N1M0R0NoeFA1M1FXYyIsInRpZCI6IjAyOTk1MjY3LTcwNzUtNGQxYy1iNjJkLWRlN2M5YTMzYzI1MCIsInV0aSI6InZTRXJLS2FoMEUtWEtUSUEyeFlnQUEiLCJ2ZXIiOiIyLjAifQ.vhAjSTdU1cilCBInVXadS5Opl4jzYmavoZU9-zipWxHHeHLilJqwYQAIUoVIa1y_ymhMrZEblxCOccl8Ea7W-p6CsUdDPwlE6I3S2PbNG7YryVml31Mo2eS3zGmz5Im9cF6g2fE8v1q3KFub8uBWPNwcWjng86XiXod_xvNZcpqkhSKTSSp-DKeSMI1k1KJGXJMj-N-TC80tFBhOgM-y7FD286j3rEO37xQ9QvA-iXcrhm4TEX9fBT6j-3OOi34KcaVS-71es_LeqtltMAmkNbxL68m0kTvOwZQ0OYlVGgbocbcihP7yJ-A3EVCIh2RkamxvzrRQJeck_co-nXTz6g");

        var response = await client.GetAsync(request);
        return Ok();
    }

    [HttpPost("/signin-oidc")]
    public IActionResult Post()
    {
        return Ok();
    }
}