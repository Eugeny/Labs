using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections;

namespace Pankov.Lab2.Quiz.Serialization
{
    public class LinqXmlSerializer : BaseSerializer<XDocument, XElement>
    {
        public override XDocument Serialize(Quiz q)
        {
            return new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), WalkSave(q));
        }

        public override Quiz Deserialize(XDocument data)
        {
            return (Quiz)WalkLoad(data.Root);
        }

        protected override XElement SaveElement(QuizItem i)
        {
            XElement result = null;
            if (i is Quiz)
                result = new XElement("quiz", new XAttribute("title", ((Quiz)i).Title));
            else if (i is QuizItemContainer)
                result = new XElement("container");
            else if (i is Question)
            {
                Question question = (Question)i;
                XElement q = new XElement(
                        "question",
                        new XAttribute("text", question.Text),
                        new XAttribute("multichoice", question.Multichoice)
                );
                foreach (Answer answer in question)
                    q.Add(new XElement("answer",
                        new XAttribute("text", answer.Text),
                        new XAttribute("correct", answer.Correct)
                    ));
                result = q;
            }
            return result;
        }

        protected override void Append(XElement p, XElement n)
        {
            p.Add(n);
        }

        protected override QuizItem LoadElement(XElement e)
        {
            QuizItem result = null;
            if (e.Name == "quiz")
            {
                result = new Quiz();
                ((Quiz)result).Title = e.Attribute("title").Value;
            }
            if (e.Name == "container")
                result = new QuizItemContainer();
            if (e.Name == "question")
            {
                result = new Question(e.Attribute("text").Value, bool.Parse(e.Attribute("multichoice").Value));
                foreach (XElement ch in e.Descendants())
                    ((Question)result).Answers.Add(new Answer(
                        ch.Attribute("text").Value,
                        bool.Parse(ch.Attribute("correct").Value)
                    ));
            }
            return result;
        }

        protected override bool IsContainer(XElement p)
        {
            return p.HasElements && p.Name != "question";
        }

        protected override IEnumerable GetChildren(XElement p)
        {
            return p.Elements();
        }
    }
}
