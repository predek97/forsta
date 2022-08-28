using QuizService.Model;
using QuizService.Repositories;

namespace QuizService.Services;

public class SolveQuizService : ISolveQuizService
{
    private readonly IQuestionRepository _questionRepository;

    public SolveQuizService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public int EvaluateAttempt(QuizResponseModel quizResponseModel)
    {
        int score = 0;
        
        foreach (var (questionId, answerId) in quizResponseModel.answers)
        {
            var question = _questionRepository.GetQuestion(questionId);
            if (question.CorrectAnswerId == answerId)
                score++;
        }

        return score;
    }
}