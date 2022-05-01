using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace LbAssgLibrary
{
    public class DTORole
    {
        public string role { get; set; }

        /* public void savetodb(SqlConnection connect)
         {

         }*/

        public void AddRole(List<DTORole> roles, string roledefault)
        {
            if (roledefault=="Default")
            {
                //  roles.Add(new DTORole { role = "Guest" });
                //  roles.Add(new DTORole { role = "Administrator" });
                //  roles.Add(new DTORole { role = "Librarian" });
                //  roles.Add(new DTORole { role = "Patron" });
              
            }
           //To add new role
           else if (roledefault != "Default")
            {
                while (true)
                {
                    Console.Clear();
                    bool role_exists = false;

                    Console.WriteLine("Enter the Role name:");

                    string input_role = Console.ReadLine();
                    foreach (DTORole i in roles)
                        if (i.role == input_role)
                        {
                            Console.WriteLine("Role Already Exists");
                            role_exists = true;
                            break;
                        }
                    if (role_exists == false)
                        roles.Add(new DTORole { role = input_role });

                    Console.WriteLine("Do you want to add another Role (Press Yes)");

                    if (Console.ReadLine().ToUpper() != "YES")
                    {
                        Console.WriteLine("List of available Roles");
                        foreach (DTORole i in roles)
                            Console.WriteLine(i.role);
                        break;
                    }

                }//End While
            }
          
        }

    }
}
