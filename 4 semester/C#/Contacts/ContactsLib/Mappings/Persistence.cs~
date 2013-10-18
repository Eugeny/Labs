using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

namespace ContactsLib.Mappings
{
    public class Persistence
    {
        private static ISessionFactory _sessionFactory;

        private static ISession session;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(Assembly.GetExecutingAssembly());
                    configuration.AddAssembly(Assembly.Load("NHibernate.Collection.Observable"));
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession Session
        {
            get
            {
                if (session == null)
                    session
                        = SessionFactory.OpenSession();
                return session;
            }
        }
    }
}