using System;
using System.Collections.Generic;
using System.Linq;
using QuizService.Model;
using QuizService.Model.Domain;

namespace QuizService.Factories;

public class QuizResponseModelFactory : IQuizResponseModelFactory
{
    public QuizResponseModel CreateQuizResponseModel(Quiz quiz, IEnumerable<Question> questions, IDictionary<int, IList<Answer>> answers)
    {
        return new QuizResponseModel
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Questions = questions.Select(question => new QuizResponseModel.QuestionItem
            {
                Id = question.Id,
                Text = question.Text,
                Answers = answers.ContainsKey(question.Id)
                    ? answers[question.Id].Select(answer => new QuizResponseModel.AnswerItem
                    {
                        Id = answer.Id,
                        Text = answer.Text
                    })
                    : Array.Empty<QuizResponseModel.AnswerItem>(),
                CorrectAnswerId = question.CorrectAnswerId
            }),
            Links = new Dictionary<string, string>
            {
                {"self", $"/api/quizzes/{quiz.Id}"},
                {"questions", $"/api/quizzes/{quiz.Id}/questions"}
            }
        };
    }
}