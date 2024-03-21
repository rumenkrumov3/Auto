using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Restful.Data;
using RestFul.Factory;
using RestFul.Models;
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
        var restRequest = new RestRequest(_baseConfig.Settings.SubURL, Method.Get);
        var response = _client.Execute(restRequest);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public void CreateAndGetUser() 
    {
        var user = new UserFactory().Generate();
        var serialziedUser = JsonConvert.SerializeObject(user);

        var restRequest = new RestRequest(_baseConfig.Settings.SubURL, Method.Post);
        restRequest.AddBody(serialziedUser);

        var createdUserResponse = _client.Execute<User>(restRequest);

        createdUserResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createdUserResponse.Data.Name.Should().Be(user.Name);
        createdUserResponse.Data.Job.Should().Be(user.Job);
    }

    [TearDown]
    public void TearDown() 
    {
    
    }
}