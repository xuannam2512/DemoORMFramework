using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperAttribute
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }    

    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class CustomAttribute : Attribute
    {
        public string name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TableAttribute : CustomAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class CollumnAttribute : CustomAttribute
    {
    }
}
