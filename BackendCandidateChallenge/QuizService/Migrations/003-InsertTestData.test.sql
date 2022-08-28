INSERT INTO [Quiz] (Title) VALUES ('First test quiz');
INSERT INTO [Question] (Text, QuizId) VALUES ('First test question', 3);
INSERT INTO [Answer] (Text, QuestionId) VALUES ('My first and correct answer to first test q', 3);
INSERT INTO [Answer] (Text, QuestionId) VALUES ('My second and incorrect answer to first test q', 3);
UPDATE [Question] SET CorrectAnswerId = 6 WHERE Id = 3;
INSERT INTO [Question] (Text, QuizId) VALUES ('Second test question', 3);
INSERT INTO [Answer] (Text, QuestionId) VALUES ('My first and incorrect answer to second test q', 4);
INSERT INTO [Answer] (Text, QuestionId) VALUES ('My second and correct answer to second test q', 4);
UPDATE [Question] SET CorrectAnswerId = 9 WHERE Id = 4;
