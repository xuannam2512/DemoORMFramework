using DeveloperAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Entity
{
    [Table(name = "users")]
    class User
    {
        public User()
        {

        }
        
        private int id;
        [Collumn(name = "ID")]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        
        private String name;
        [Collumn(name = "Name")]
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        
        private int age;
        [Collumn(name = "Age")]
        public int Age
        {
            get { return age;  }
            set { age = value;  }
        }
    }
}
