using System;
namespace Pankov.Lab2.Quiz.Serialization
{
    public interface IQuizSerializer<T>
    {
        Pankov.Lab2.Quiz.Quiz Deserialize(T data);
        T Serialize(Pankov.Lab2.Quiz.Quiz q);
    }
}
