using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QuizService.Model;
using Xunit;

namespace QuizService.Tests;

public class QuizzesControllerTest
{
    const string QuizApiEndPoint = "/api/quizzes/";
    private readonly IConfiguration _configuration;

    public QuizzesControllerTest()
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
    }

    [Fact]
    public async Task PostNewQuizAddsQuiz()
    {
        var quiz = new QuizCreateModel("Test title");
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(quiz));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"),
                content);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
        }
    }

    [Fact]
    public async Task AQuizExistGetReturnsQuiz()
    {
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 1;
            var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
            var quiz = JsonConvert.DeserializeObject<QuizGetModel>(await response.Content.ReadAsStringAsync());
            Assert.Equal(quizId, quiz.Id);
            Assert.Equal("My first quiz", quiz.Title);
        }
    }

    [Fact]
    public async Task AQuizDoesNotExistGetFails()
    {
        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 999;
            var response = await client.GetAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}"));
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Fact]
    public async Task AQuizDoesNotExists_WhenPostingAQuestion_ReturnsNotFound()
    {
        const string QuizApiEndPoint = "/api/quizzes/999/questions";

        using (var testHost = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>()))
        {
            var client = testHost.CreateClient();
            const long quizId = 999;
            var question = new QuestionCreateModel("The answer to everything is what?");
            var content = new StringContent(JsonConvert.SerializeObject(question));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}"),content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    [Theory]
    [InlineData(6, 9, 2)]
    [InlineData(6, 8, 1)]
    [InlineData(7, 9, 1)]
    [InlineData(7, 8, 0)]
    public async Task AQuizCanBeAnsweredAndPointed(int answerToFirst, int answerToSecond, int expectedScore)
    {
        //arrange 
        const int quizId = 3;
        var answers = new Dictionary<int, int>
        {
            {3, answerToFirst},
            {4, answerToSecond}
        };
        var quizAnswerRequest = new QuizResponseModel(answers);
        
        //act
        using var testHost = new TestServer(new WebHostBuilder().UseConfiguration(_configuration).UseStartup<Startup>());
        var client = testHost.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(quizAnswerRequest));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var response =
            await client.PostAsync(new Uri(testHost.BaseAddress, $"{QuizApiEndPoint}{quizId}/responses"), content);

        //assert
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        var responseModel = JsonConvert.DeserializeObject<QuizResultModel>(await response.Content.ReadAsStringAsync());
        Assert.Equal(expectedScore, responseModel.Score);
    }
}