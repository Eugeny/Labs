using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pankov.Lab2.Quiz;

namespace Lab2_Tests
{
    public static class Fixture
    {
        public static Quiz GetQuiz()
        {
            Quiz q = new Quiz();
            QuizItemContainer q1 = new QuizItemContainer();
            Question qe1 = new Question("Q1", false);
            qe1.Answers.AddRange(new List<Answer> { new Answer("A1", false), new Answer("A2", true) });
            Question qe2 = new Question("Q2", true);
            qe2.Answers.AddRange(new List<Answer> { new Answer("A1", false), new Answer("A2", true), new Answer("A3", true) });
            q1.Add(qe1);
            q1.Add(qe2);
            Question qe3 = new Question("Q2", true);
            qe3.Answers.AddRange(new List<Answer> { new Answer("A1", true), new Answer("A2", true), new Answer("A3", false) });
            q.Add(q1);
            q.Add(qe3);

            q.Title = "Test";
            return q;
        }

        public static Question GetQuestion()
        {
            Question qe2 = new Question("Q2", true);
            qe2.Answers.AddRange(new List<Answer> { new Answer("A1", false), new Answer("A2", true), new Answer("A3", true) });
            return qe2;
        }
    }
}
