using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NHibernate.Collection.Observable;

namespace ContactsLib.Entities
{
    public class ContactGroup : IEnumerable, INotifyPropertyChanged
    {
        private IList<Contact> _Contacts = new ObservableCollection<Contact>();
        private string _Name;

        public ContactGroup()
        {
        }

        public ContactGroup(string name)
        {
            Name = name;
            Init();
        }

        public virtual long ID { get; set; }

        public virtual string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("_Name"));
            }
        }

        public virtual IList<Contact> Contacts
        {
            get { return _Contacts; }
            set
            {
                _Contacts = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Contacts"));
            }
        }

        #region INotifyPropertyChanged Members

        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        private void Init()
        {
            //(Contacts as ObservableCollection<Contact>).CollectionChanged +=
              //  delegate { ContactList.Instance.InvalidateContactList(); };
        }

        public override string ToString()
        {
            return Name;
        }

        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Contacts.GetEnumerator();
        }

        #endregion
    }
}