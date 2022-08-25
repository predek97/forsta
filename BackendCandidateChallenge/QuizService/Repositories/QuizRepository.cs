using System.Collections.Generic;
using System.Data;
using Dapper;
using QuizService.Model.Domain;

namespace QuizService.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly IDbConnection _connection;
    
    public QuizRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public Quiz GetQuiz(int id)
    {
        const string quizSql = "SELECT * FROM Quiz WHERE Id = @Id;";
        return _connection.QuerySingleOrDefault<Quiz>(quizSql, new {Id = id});
    }

    public IEnumerable<Quiz> GetQuizzes()
    {
        const string sql = "SELECT * FROM Quiz;";
        return _connection.Query<Quiz>(sql);
    }
}