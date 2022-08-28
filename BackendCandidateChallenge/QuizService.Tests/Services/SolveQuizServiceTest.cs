using System.Collections.Generic;
using Moq;
using QuizService.Model;
using QuizService.Model.Domain;
using QuizService.Repositories;
using QuizService.Services;
using Xunit;

namespace QuizService.Tests.Services;

public class SolveQuizServiceTest
{
    private Mock<IQuestionRepository> _repositoryMock;
    private ISolveQuizService _solveQuizService;
    
    public SolveQuizServiceTest()
    {
        _repositoryMock = new Mock<IQuestionRepository>();
        _solveQuizService = new SolveQuizService(_repositoryMock.Object);
    }

    [Fact]
    public void should_add_points_when_correct_answer_and_should_not_when_incorrect()
    {
        //arrange
        var answers = new Dictionary<int, int>
        {
            {1, 1},
            {2, 2},
            {3, 3}
        };
        var quizResponseModel = new QuizResponseModel(answers);
        _repositoryMock.Setup(_ => _.GetQuestion(1)).Returns(new Question{CorrectAnswerId = 1});
        _repositoryMock.Setup(_ => _.GetQuestion(2)).Returns(new Question {CorrectAnswerId = 1});
        _repositoryMock.Setup(_ => _.GetQuestion(3)).Returns(new Question {CorrectAnswerId = 3});
        
        //act
        var result = _solveQuizService.EvaluateAttempt(quizResponseModel);
        
        //assert
        Assert.Equal(2, result);
    }
}