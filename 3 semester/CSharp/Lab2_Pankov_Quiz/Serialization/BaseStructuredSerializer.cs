using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Pankov.Lab2.Quiz.Serialization
{
    public abstract class BaseSerializer<T, E> : IQuizSerializer<T>
    {
        public abstract T Serialize(Quiz q);
        public abstract Quiz Deserialize(T data);

        protected abstract E SaveElement(QuizItem i);
        protected abstract QuizItem LoadElement(E e);
        protected abstract void Append(E p, E n);
        protected abstract bool IsContainer(E p);
        protected abstract IEnumerable GetChildren(E p);

        protected E WalkSave(QuizItem i)
        {
            E el = SaveElement(i);
            if (i is QuizItemContainer)
            {
                foreach (QuizItem item in (QuizItemContainer)i)
                    Append(el, WalkSave(item));
            }
            return el;
        }

        protected QuizItem WalkLoad(E e)
        {
            QuizItem qi = LoadElement(e);
            if (IsContainer(e))
            {
                foreach (E ch in GetChildren(e))
                    ((QuizItemContainer)qi).Items.Add(WalkLoad(ch));
            }
            return qi;
        }
    }
}
