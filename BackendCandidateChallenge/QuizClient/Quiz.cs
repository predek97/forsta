namespace QuizClient;

public struct Quiz
{
    public int Id;
    public string Title;
    //TODO This is the only line that justifies those structs. And it still could've been avoided if we just written body of this method
    public static Quiz NotFound => default(Quiz);
}