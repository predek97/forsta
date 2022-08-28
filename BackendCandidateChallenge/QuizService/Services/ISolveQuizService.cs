using QuizService.Model;

namespace QuizService.Services;

public interface ISolveQuizService
{
    int EvaluateAttempt(QuizResponseModel quizResponseModel);
}