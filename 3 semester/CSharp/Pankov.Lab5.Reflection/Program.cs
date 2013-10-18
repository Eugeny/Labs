using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Pankov.Lab5.Reflection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Assembly a = Assembly.GetAssembly(typeof(System.Runtime.Serialization.Formatters.Soap.SoapFormatter));
            Console.WriteLine(Reflect(a));
            Console.ReadLine();
        }

        public static String Reflect(Assembly asm)
        {
            string res = "";
            foreach (Module m in asm.GetModules())
            {
                res += String.Format("{0} [{1}]\n", m.Name, m.ModuleVersionId);
                foreach (Type t in m.GetTypes())
                {
                    string mods = "";
                    if (t.IsPublic) mods += "public ";
                    else if (t.IsNotPublic) mods += "private ";
                    else mods += "internal ";
                    if (t.IsSealed) mods += "sealed ";
                    if (t.IsAbstract && !t.IsInterface) mods += "abstract ";
                    if (t.IsClass) mods += "class ";
                    if (t.IsEnum) mods += "enum ";
                    if (t.IsInterface) mods += "interface ";
                    res += String.Format("|\n|- {0}{1}\n", mods, t.Name);

                    bool hasMethods = false;
                    foreach (MethodInfo mi in t.GetMethods())
                        if (mi.DeclaringType == t)
                        {
                            hasMethods = true;
                            string n = mi.Name;
                            string mmods = "";
                            if (mi.IsPublic) mmods += "public ";
                            if (mi.IsPrivate) mmods += "private ";
                            if (mi.IsConstructor) mmods += "constructor ";
                            if (mi.IsGenericMethod) n += "<>";
                            if (mi.IsStatic) mmods += "static ";
                            if (mi.IsVirtual) mmods += "virtual ";
                            List<string> prms = new List<string>();
                            foreach (ParameterInfo pi in mi.GetParameters())
                                prms.Add(pi.ParameterType.Name);
                            res += String.Format("|\t|- {0}{1} {2}({3})\n", mmods, mi.ReturnType.Name, n, string.Join(", ", prms));
                        }
                    if (hasMethods)
                        res += "|\t-\n";
                }
            }
            return res;
        }
    }
}
