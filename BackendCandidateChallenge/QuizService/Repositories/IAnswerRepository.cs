using System.Collections.Generic;
using QuizService.Model.Domain;

namespace QuizService.Repositories;

public interface IAnswerRepository
{
    IDictionary<int, IList<Answer>> GetAnswers(int QuizId);
}