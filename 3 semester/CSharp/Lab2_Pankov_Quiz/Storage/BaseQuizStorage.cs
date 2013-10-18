using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pankov.Lab2.Quiz.Storage
{
   public abstract class BaseQuizStorage
    {
       public abstract void Save(Quiz q);
       public abstract Quiz Load(string title);


       protected void WalkSave(QuizItem i, Stream s)
       {
           if (i is QuizItemContainer)
           {
               s.WriteByte(0);
               QuizItemContainer c = (QuizItemContainer)i;
               s.WriteByte((byte)c.Items.Count);
               foreach (QuizItem item in c)
                   WalkSave(item, s);
           }
           if (i is Question)
           {
               s.WriteByte(1);
               Question c = (Question)i;
               s.WriteByte((byte)c.Answers.Count);
               Util.WriteString(s, c.Text);
               s.WriteByte((byte)(c.Multichoice ? 1 : 0));
               foreach (Answer item in c)
               {
                   Util.WriteString(s, item.Text);
                   s.WriteByte((byte)(item.Correct ? 1 : 0));
               }
           }
       }

       protected QuizItem WalkLoad(Stream s)
       {
           QuizItem result = null;
           int type = s.ReadByte();
           if (type == 0)
           {
               int count = s.ReadByte();
               QuizItemContainer res = new QuizItemContainer();
               for (int i = 0; i < count; i++)
                   res.Items.Add(WalkLoad(s));
               result = res;
           }
           if (type == 1)
           {
               int count = s.ReadByte();
               Question res = new Question(Util.ReadString(s), s.ReadByte() == 1);
               for (int i = 0; i < count; i++)
                   res.Answers.Add(new Answer(Util.ReadString(s), s.ReadByte() == 1));
               result = res;
           }
           return result;
       }
    }
}
