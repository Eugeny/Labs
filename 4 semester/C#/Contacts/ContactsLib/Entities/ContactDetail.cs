using System;
using System.ComponentModel;
using ContactsLib.Mappings;

namespace ContactsLib.Entities
{
    public class ContactDetail : INotifyPropertyChanged
    {
        private string _Content;
        private String _Name;

        public ContactDetail()
        {
        }

        public ContactDetail(string name, string value)
        {
            Name = name;
            Content = value;
        }

        public virtual long ID { get; set; }

        public virtual string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public virtual string Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Content"));
            }
        }

        #region INotifyPropertyChanged Members

        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        public virtual void Destroy()
        {
            Persistence.Session.Delete(this);
            Persistence.Session.Flush();
        }

        public virtual void Persist()
        {
            Persistence.Session.SaveOrUpdate(this);
            Persistence.Session.Flush();
        }
    }
}