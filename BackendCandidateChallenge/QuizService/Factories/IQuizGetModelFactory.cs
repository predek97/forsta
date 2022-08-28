using System.Collections.Generic;
using QuizService.Model;
using QuizService.Model.Domain;

namespace QuizService.Factories;

public interface IQuizGetModelFactory
{
    QuizGetModel CreateQuizGetModel(Quiz quiz, IEnumerable<Question> questions,
        IDictionary<int, IList<Answer>> answers);
}