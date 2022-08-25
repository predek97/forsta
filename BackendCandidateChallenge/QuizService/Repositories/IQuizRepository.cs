using System.Collections.Generic;
using QuizService.Model.Domain;

namespace QuizService.Repositories;

public interface IQuizRepository
{
    Quiz GetQuiz(int id);
    IEnumerable<Quiz> GetQuizzes();
}