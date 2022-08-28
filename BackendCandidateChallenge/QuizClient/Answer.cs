namespace QuizClient;

//TODO why are structs used for models in this project? Unless there is a reason(which I cannot see), I prefer to stick to classes
public struct Answer
{
    public int Id;
    public string Text;
    public int QuestionId;
}