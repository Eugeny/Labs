using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Pankov.Lab2.Quiz
{
    [Serializable]
    [DataContract]
    public class QuizItemContainer : QuizItem, IEnumerable
    {
        [DataMember]
        [XmlArray("Items")]
        public List<QuizItem> Items { get; private set; }

        public QuizItemContainer()
        {
            Items = new List<QuizItem>();
        }

        public void Add(object i)
        {
            Items.Add((QuizItem)i);
        }

        public override bool IsAnswered()
        {
            return Items.Any(x => x.IsAnswered());
        }

        public override bool IsAnsweredCorrectly()
        {
            return Items.All(x => x.IsAnsweredCorrectly());
        }

        public override int GetQuestionCount()
        {
            return Items.Sum(x => x.GetQuestionCount());
        }

        public override int GetCorrectAnswerCount()
        {
            return Items.Sum(x => x.GetCorrectAnswerCount());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public QuizItem this[int idx]
        {
            get { return Items[idx]; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is QuizItemContainer)) return false;
            return ((QuizItemContainer)obj == this);
        }

        public override bool Equals(QuizItem other)
        {
            if (!(other is QuizItemContainer)) return false;
            return (Items.Count == ((QuizItemContainer)other).Items.Count && Items.All(x => ((QuizItemContainer)other).Items.Any(y => y == x)));
        }

    }
}
