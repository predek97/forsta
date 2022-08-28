using System.Collections.Generic;
using System.Data;
using Dapper;
using QuizService.Model.Domain;

namespace QuizService.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly IDbConnection _connection;

    public QuestionRepository(IDbConnection connection)
    {
        _connection = connection;
    }


    public IEnumerable<Question> GetQuestions(int quizId)
    {
        const string questionsSql = "SELECT * FROM Question WHERE QuizId = @QuizId;";
        return _connection.Query<Question>(questionsSql, new {QuizId = quizId});
    }

    public Question GetQuestion(int questionId)
    {
        const string questionsSql = "SELECT * FROM Question WHERE Id = @QuestionId;";
        return _connection.QuerySingleOrDefault<Question>(questionsSql, new {QuestionId = questionId});
    }
}