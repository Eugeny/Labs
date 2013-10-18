using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Pankov.Lab2.Quiz
{
    [Serializable]
    [DataContract]
    public class Answer : IEquatable<Answer>
    {
        [DataMember]
        public bool Correct { get; private set; }
        public bool Selected { get; set; }
        [DataMember]
        public string Text { get; private set; }

        public Answer(string text, bool correct)
        {
            Text = text;
            Correct = correct;
            Selected = false;
        }

        public bool Equals(Answer other)
        {
            return (Text == other.Text && Correct == other.Correct);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Answer)) return false;
            return ((Answer)obj == this);
        }

        public static bool operator ==(Answer a, Answer b) { return a.Equals(b); }
        public static bool operator !=(Answer a, Answer b) { return !(a == b); }
    }
}
