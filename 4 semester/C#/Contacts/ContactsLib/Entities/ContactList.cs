using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ContactsLib.Mappings;

namespace ContactsLib.Entities
{
    public class ContactList : INotifyPropertyChanged
    {
        public static ContactList Instance;
        internal bool IsLoading;
        private ObservableCollection<ContactGroup> _groups = new ObservableCollection<ContactGroup>();

        public ContactList()
        {
            Instance = this;
        }

        public ObservableCollection<ContactGroup> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Groups"));
            }
        }

        public ContactGroup DefaultGroup
        {
            get { return GetGroup("Ungrouped"); }
        }

        public IEnumerable<Contact> Contacts
        {
            get { return Groups.SelectMany(g => g.Contacts); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        public void OnDeserialization(object sender)
        {
            Instance = this;
        }

        public Dictionary<string, int> GetGroupStats()
        {
            var r = new Dictionary<string, int>();
            foreach (ContactGroup g in Groups)
                r[g.Name] = g.Contacts.Count;
            return r;
        }

        public void Reload()
        {
            IsLoading = true;

            Groups.Clear();
            foreach (ContactGroup g in Persistence.Session.CreateCriteria<ContactGroup>().List<ContactGroup>())
                Groups.Add(g);

            IsLoading = false;
        }

        public void InvalidateContactList()
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Contacts"));
        }

        public ContactGroup GetGroup(string name)
        {
            ContactGroup g = Groups.FirstOrDefault(x => x.Name == name);
            if (g == null)
            {
                g = new ContactGroup(name);
                Persistence.Session.Save(g);
                Persistence.Session.Flush();
                Groups.Add(g);
            }
            return g;
        }

        public ContactGroup GetGroupOf(Contact c)
        {
            return Groups.FirstOrDefault(x => x.Contacts.Contains(c));
        }

        public void Remove(Contact c)
        {
            GetGroupOf(c).Contacts.Remove(c);
            List<ContactGroup> deadGroups = Groups.Where(g => g.Contacts.Count == 0).ToList();
            foreach (ContactGroup g in deadGroups)
            {
                Groups.Remove(g);
                Persistence.Session.Delete(g);
                Persistence.Session.Flush();
            }
            c.Destroy();
        }

        public void Add(Contact contact, string g)
        {
            GetGroup(g).Contacts.Add(contact);
            InvalidateContactList();
        }
    }
}