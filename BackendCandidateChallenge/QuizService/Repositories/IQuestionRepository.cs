using System.Collections.Generic;
using QuizService.Model.Domain;

namespace QuizService.Repositories;

public interface IQuestionRepository
{
    IEnumerable<Question> GetQuestions(int quizId);
}