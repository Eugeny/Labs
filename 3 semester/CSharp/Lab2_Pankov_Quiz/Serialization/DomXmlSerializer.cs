using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Pankov.Lab2.Quiz.Serialization
{
    public class DomXmlSerializer : BaseSerializer<XmlDocument, XmlNode>
    {
        private XmlDocument doc;

        public override XmlDocument Serialize(Quiz q)
        {
            doc = new XmlDocument();
            doc.AppendChild(WalkSave(q));
            return doc;
        }

        public override Quiz Deserialize(XmlDocument data)
        {
            return (Quiz)WalkLoad(data.ChildNodes[0]);
        }

        private XmlAttribute CreateAttribute(string name, string val)
        {
            XmlAttribute a = doc.CreateAttribute(name);
            a.Value = val;
            return a;
        }

        protected override XmlNode SaveElement(QuizItem i)
        {
            XmlElement result = null;
            if (i is Quiz)
            {
                result = doc.CreateElement("quiz");
                result.Attributes.Append(CreateAttribute("title", ((Quiz)i).Title));
            }
            else if (i is QuizItemContainer)
                result = doc.CreateElement("container");
            else if (i is Question)
            {
                Question question = (Question)i;
                XmlElement q = doc.CreateElement("question");
                q.Attributes.Append(CreateAttribute("text", question.Text));
                q.Attributes.Append(CreateAttribute("multichoice", question.Multichoice.ToString()));

                foreach (Answer answer in question)
                {
                    XmlElement e = doc.CreateElement("answer");
                    e.Attributes.Append(CreateAttribute("text", answer.Text));
                    e.Attributes.Append(CreateAttribute("correct", answer.Correct.ToString()));
                    q.AppendChild(e);
                }
                result = q;
            }
            return result;
        }

        protected override QuizItem LoadElement(XmlNode e)
        {
            QuizItem result = null;
            if (e.Name == "quiz")
            {
                result = new Quiz();
                ((Quiz)result).Title = e.Attributes["title"].Value;
            }
            if (e.Name == "container")
                result = new QuizItemContainer();
            if (e.Name == "question")
            {
                result = new Question(e.Attributes["text"].Value, bool.Parse(e.Attributes["multichoice"].Value));
                foreach (XmlNode ch in e.ChildNodes)
                    ((Question)result).Answers.Add(new Answer(
                        ch.Attributes["text"].Value,
                        bool.Parse(ch.Attributes["correct"].Value)
                    ));
            }
            return result;
        }

        protected override void Append(XmlNode p, XmlNode n)
        {
            p.AppendChild(n);
        }

        protected override bool IsContainer(XmlNode p)
        {
            return p.HasChildNodes && p.Name != "question";
        }

        protected override IEnumerable GetChildren(XmlNode p)
        {
            return p.ChildNodes;
        }
    }
}
