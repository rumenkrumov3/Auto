using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Polly;
using Restful.Data;
using RestFul.Extensions;
using RestFul.Factory;
using RestFul.Models;
using RestFul.Models.GetUser;
using RestSharp;
using System.Net;

public class RestApiTests
{
    private BaseConfig _baseConfig;
    private RestClient _client;
    private PolicyBuilder<RestResponse> _policy;

    [SetUp]
    public void SetUp()
    {
        _baseConfig = new BaseConfig();
        _client = new RestClient(_baseConfig.Settings.BaseURL);
        _policy = Policy.HandleResult<RestResponse>(e => e.StatusCode != HttpStatusCode.OK);
    }

    [Test]
    public async Task GetAllTheBookiesIds()
    {        
        var restRequest = new RestRequest(_baseConfig.Settings.SubURL, Method.Get);

        var policyResult = await _policy
                        .WaitAndRetryAsync(10, retryDuration => TimeSpan.FromSeconds(1))
                        .ExecuteAsync(() => _client.ExecuteAsync(restRequest));

        Assert.True(policyResult.IsSuccessful);
        Assert.True(policyResult.StatusCode.isSuccessfulStatusCode());
    }

    [Test]
    public async Task GetSingleUser()
    {
        var expectedUser = new UserGet()
        {
            Id = 2,
            Email = "janet.weaver@reqres.in",
            FirstName = "Janet",
            LastName = "Weaver",      
            Avatar = "https://reqres.in/img/faces/2-image.jpg"
        };

        try
        {
            var restRequest = new RestRequest(string.Concat(_baseConfig.Settings.SubURL, "2"), Method.Get);

            var policyResult = await _policy
                             .WaitAndRetryAsync(10, retryDuration => TimeSpan.FromSeconds(1))
                             .ExecuteAsync(() => _client.ExecuteAsync(restRequest));

            Assert.True(policyResult.IsSuccessful);
            Assert.True(policyResult.StatusCode.isSuccessfulStatusCode());

            var actualUser = JsonConvert.DeserializeObject<UserResponse>(policyResult.Content);
            actualUser.Data.Should().BeEquivalentTo(expectedUser);
        }
        catch (Exception e)
        {

            Console.WriteLine($"Message: {e.Message}");
        }      
    }

    [Test]
    public async Task GetSingleUserNotFound()
    {
        var restRequest = new RestRequest(string.Concat(_baseConfig.Settings.SubURL, "23"), Method.Get);

        var policyResult = await Policy
                             .HandleResult<RestResponse>(e => e.StatusCode != HttpStatusCode.NotFound)
                             .WaitAndRetryAsync(10, retryDuration => TimeSpan.FromSeconds(1))
                             .ExecuteAsync(() => _client.ExecuteAsync(restRequest));

        policyResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task CreateUser() 
    {
        var user = new UserFactory().Generate();
        var serialziedUser = JsonConvert.SerializeObject(user);

        var restRequest = new RestRequest(_baseConfig.Settings.SubURL, Method.Post).AddJsonBody(serialziedUser);

        var policyResult = await Policy
                             .HandleResult<RestResponse>(e => e.StatusCode != HttpStatusCode.Created)
                             .WaitAndRetryAsync(10, retryDuration => TimeSpan.FromSeconds(1))
                             .ExecuteAsync(() => _client.ExecuteAsync(restRequest));

        var userData = JsonConvert.DeserializeObject<UserPost>(policyResult.Content);

        policyResult.StatusCode.Should().Be(HttpStatusCode.Created);
        userData.Name.Should().Be(user.Name);
        userData.Job.Should().Be(user.Job);
    }

    [TearDown]
    public void TearDown() 
    {    
    }
}