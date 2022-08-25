using System.Collections.Generic;
using QuizService.Model;
using QuizService.Model.Domain;

namespace QuizService.Factories;

public interface IQuizResponseModelFactory
{
    QuizResponseModel CreateQuizResponseModel(Quiz quiz, IEnumerable<Question> questions,
        IDictionary<int, IList<Answer>> answers);
}