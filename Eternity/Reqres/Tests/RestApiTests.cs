using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
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

    [SetUp]
    public void SetUp()
    {
        _baseConfig = new BaseConfig();
        _client = new RestClient(_baseConfig.Settings.BaseURL);
    }

    [Test]
    public void GetAllTheBookiesIds()
    {        
        var restRequest = new RestRequest(_baseConfig.Settings.SubURL);
        var response = _client.ExecuteGet(restRequest);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public void GetSingleUser()
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
            var restRequest = new RestRequest(string.Concat(_baseConfig.Settings.SubURL, "2"));
            var response = _client.ExecuteGet(restRequest);

            Assert.True(response.IsSuccessful);
            Assert.True(response.StatusCode.isSuccessfulStatusCode());

            var actualUser = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            actualUser.Data.Should().BeEquivalentTo(expectedUser);
        }
        catch (Exception e)
        {

            Console.WriteLine($"Message: {e.Message}");
        }      
    }

    [Test]
    public void GetSingleUserNotFound()
    {
        var restRequest = new RestRequest(string.Concat(_baseConfig.Settings.SubURL, "23"), Method.Get);
        var response = _client.Execute(restRequest);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public void CreateUser() 
    {
        var user = new UserFactory().Generate();
        var serialziedUser = JsonConvert.SerializeObject(user);

        var restRequest = new RestRequest(_baseConfig.Settings.SubURL, Method.Post);
        restRequest.AddBody(serialziedUser);

        var createdUserResponse = _client.Execute<UserPost>(restRequest);

        createdUserResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createdUserResponse.Data.Name.Should().Be(user.Name);
        createdUserResponse.Data.Job.Should().Be(user.Job);
    }

    [TearDown]
    public void TearDown() 
    {    
    }
}