using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ConsoleApp3.Entity;
using DeveloperAttribute;
using MySql.Data.MySqlClient;
using System.Data;

namespace ConsoleApp3
{    
    class Program
    {
        static MySqlConnection conn = null;
        static void Main(string[] args)
        {
            connectDB();
            UserRepo<User> userRepo = new UserRepo<User>();

            userRepo.getUsers();
        }

        static void connectDB()
        {
            String connStr = @"server=localhost; port=3306;Uid=root;Pwd=root;database=cms;Convert Zero Datetime=True";            
            try
            {
                conn = new MySqlConnection(connStr);
                if (conn != null)                    
                {
                    conn.Open();
                    Console.WriteLine("Connect to database successfully!!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class UserRepo<T>
        {
            public UserRepo()
            {

            }

            public List<T> getUsers()
            {
                //init User;
                List<T> users = new List<T>();

                // Get info attribute of class;
                CustomAttribute tableName =
                        (CustomAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(CustomAttribute));

                if (tableName == null)
                {
                    Console.WriteLine("The attribute was not found.");
                }
                else
                {
                    Console.WriteLine("The Name Attribute is: {0}.", tableName.name);
                }

                //query
                using (conn)
                {
                    String query = "select * from " + tableName.name;
                    MySqlCommand command = new MySqlCommand(query, conn);
                    var reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        PropertyInfo[] properties = typeof(T).GetProperties();

                        foreach(PropertyInfo p in properties)
                        {
                            CustomAttribute collumnAttribute = (CustomAttribute)p.GetCustomAttribute(typeof(CustomAttribute));

                            if (p.PropertyType.Name == "int")
                            {                                
                                int id = Convert.ToInt32(reader[collumnAttribute.name]);
                            }
                                                        
                        }
                    }
                }

                return null;

            }
        }

        public static void GetAttribute(Type t)
        {
            // get info attribute of property
            var properties = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Public
        | BindingFlags.Instance | BindingFlags.Static);

            for (int i = 0; i < properties.Length; i++)
            {
                CustomAttribute collumnAttribute = (CustomAttribute) properties[i].GetCustomAttribute(typeof(CustomAttribute));

                if (collumnAttribute == null)
                {
                    Console.WriteLine("The attribute was not found.");
                } else
                {
                    Console.WriteLine("The Name Attribute is: {0}.", collumnAttribute.name);
                }
            }

            // Get info attribute of class;
            CustomAttribute MyAttribute =
                    (CustomAttribute)Attribute.GetCustomAttribute(t, typeof(CustomAttribute));

            if (MyAttribute == null)
            {
                Console.WriteLine("The attribute was not found.");
            }
            else
            {
                Console.WriteLine("The Name Attribute is: {0}.", MyAttribute.name);
            }
        }
    }    
}
