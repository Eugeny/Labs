using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ContactsLib.Mappings;

namespace ContactsLib.Entities
{
    public class Contact : IComparable<Contact>, INotifyPropertyChanged
    {
        private IList<ContactDetail> _Details = new ObservableCollection<ContactDetail>();
        private string _Name;

        public Contact()
        {
        }

        public Contact(string name)
        {
            Name = name;
        }

        public virtual long ID { get; set; }

        public virtual string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("_Name"));
            }
        }

        public virtual IList<ContactDetail> Details
        {
            get { return _Details; }
            set
            {
                _Details = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Details"));
            }
        }

        public virtual string Group
        {
            get { return ContactList.Instance.GetGroupOf(this).Name; }
        }

        #region IComparable<Contact> Members

        public virtual int CompareTo(Contact other)
        {
            return Name.CompareTo(other.Name);
        }

        #endregion

        #region INotifyPropertyChanged Members

        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        public virtual void Destroy()
        {
            foreach (var contactDetail in Details)
                contactDetail.Destroy();
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