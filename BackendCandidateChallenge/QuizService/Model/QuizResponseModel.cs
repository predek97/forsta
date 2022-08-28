using System.Collections.Generic;

namespace QuizService.Model;

public class QuizResponseModel
{
    public QuizResponseModel(Dictionary<int, int> answers)
    {
        this.answers = answers;
    }

    public Dictionary<int, int> answers { get; }
}