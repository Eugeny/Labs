using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactsLib.Entities;
using System.Runtime.Serialization;
using ContactsLib.StorageBackends;
using System.Collections;

namespace ContactsLib
{
    [DataContract]
    public class ContactList : IEnumerable<Contact>
    {
        [DataMember]
        public List<Contact> Contacts { get; private set; }

        public IEnumerable<Contact> Sorted
        {
            get
            {
                List<Contact> ll = new List<Contact>();
                ll.AddRange(Contacts.Select(i => i));
                ll.Sort();
                return ll;
            }
        }

        public ContactList()
        {
            Contacts = new List<Contact>();
        }

        public void Add(Contact c)
        {
            Contacts.Add(c);
        }

        public void Store<S>(object descriptor) where S : StorageBackend
        {
            StorageBackend sb = Activator.CreateInstance<S>();
            sb.Store(this, descriptor);
        }

        public static ContactList Load<S>(object descriptor) where S : StorageBackend
        {
            StorageBackend sb = Activator.CreateInstance<S>();
            return sb.Load(descriptor);
        }

        public Contact this[int idx]
        {
            get
            {
                return Contacts[idx];
            }
        }

        #region IEnumerable
        public IEnumerator<Contact> GetEnumerator()
        {
            return Contacts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Contacts.GetEnumerator();
        }
        #endregion

        public void Remove(Contact contact)
        {
            Contacts.Remove(contact);
        }

    }
}
