using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Pankov.Lab2.Quiz
{
    [Serializable]
    [DataContract]
    [KnownType(typeof(QuizItemContainer))]
    [KnownType(typeof(Question))]
    [KnownType(typeof(Answer))]
    public class Quiz : QuizItemContainer
    {
        [DataMember]
        public string Title { get; set; }
    }
}
