using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using QuizService.Model.Domain;

namespace QuizService.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly IDbConnection _connection;

    public AnswerRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public IDictionary<int, IList<Answer>> GetAnswers(int quizId)
    {
        const string answersSql = "SELECT a.Id, a.Text, a.QuestionId FROM Answer a INNER JOIN Question q ON a.QuestionId = q.Id WHERE q.QuizId = @QuizId;";
        return _connection.Query<Answer>(answersSql, new {QuizId = quizId})
            .Aggregate(new Dictionary<int, IList<Answer>>(), (dict, answer) => {
                if (!dict.ContainsKey(answer.QuestionId))
                    dict.Add(answer.QuestionId, new List<Answer>());
                dict[answer.QuestionId].Add(answer);
                return dict;
            });
    }
}