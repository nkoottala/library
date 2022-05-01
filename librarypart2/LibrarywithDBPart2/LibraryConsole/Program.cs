using System;
using LibraryDBConnectLibrary;
using LbAssgLibrary;
using System.Collections.Generic;
using System.Linq;


namespace LibraryConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBConnect dbc=new DBConnect();
            DTOUser dtouser=new DTOUser();
            DTORole dtorole=new DTORole();
            List<DTORole> roles = new List<DTORole>();
            List<DTOUser> users = new List<DTOUser>();
            dbc.viewroledata(roles);
            // dtorole.AddRole(roles, "Default");
            while (true)
            {
                
                Console.WriteLine("Welcome to County Library System");
                Console.WriteLine("   Roles Management <Press R>");
                Console.WriteLine("   User Management  <Press U>");
               // Console.WriteLine("   User Login L");
                Console.WriteLine("   Exit <Press E>");
                string option = Console.ReadLine();

                if (option.ToUpper() == "R")
                {
                    Console.Clear();
                    string roleoption;
                    Console.WriteLine("Role Addition <Press RA>");
                    Console.WriteLine("Role Deletion <Press RD>");
                    Console.WriteLine("Role List <Press RL>");
                    roleoption = Console.ReadLine();
                    if (roleoption.ToUpper() == "RA")
                    {
                        dtorole.AddRole(roles, "UserInput");
                        dbc.insertroledata(roles);

                    }
                    else if (roleoption.ToUpper() == "RD")
                    {
                        Console.WriteLine("Enter the Role Name to delete");
                        string rolename = Console.ReadLine();
                        if (dbc.getroleid(rolename) > 0)
                        {
                            if (dbc.deleteroledata(rolename) == 1)
                                Console.WriteLine("Role Deleted Successfully");
                            else
                                Console.WriteLine("You cannot Delete Role assigned to User");
                        }
                        else
                            Console.WriteLine("Role doesnot Exists");
                    }
                    else if (roleoption.ToUpper() == "RL")
                    {
                        roles.Clear();
                        dbc.viewroledata(roles);
                        Console.WriteLine("List of available Roles");
                        foreach (DTORole i in roles)
                            Console.WriteLine(i.role);
                    }

                }

                else if (option.ToUpper() == "U")
                {
                    Console.Clear();
                    Console.WriteLine("Register for new user <Press R>");
                    Console.WriteLine("Authenticate for Existing User <Press A>");
                    Console.WriteLine("Update User Information <Press UP>");
                    Console.WriteLine("Delete User <Press UD>");
                  //  Console.WriteLine("Print User <Press PU>");
                    Console.WriteLine("Print Profile <Press PP>");
                    Console.WriteLine("Login User <Press L>");
                    string input_user = Console.ReadLine();
                    if (input_user.ToUpper() == "R")
                    {
                        users.Clear();
                        while (true)
                        {
                            users.AddRange(dtouser.register(roles));
                            Console.WriteLine("Do you want to add another user(Yes/No)");
                            if (Console.ReadLine().ToUpper() != "YES")
                            {
                                Console.Clear();
                                dbc.insertuserdata(users);
                                Console.WriteLine("List of Available users :");
                                foreach (var j in users)
                                    Console.WriteLine(j.username);
                                break;
                            }
                        }

                    }
                    // Begin If for input_user="L" for User Login
                    else if (input_user.ToUpper() == "A")
                    {
                        Console.Clear();

                        Console.WriteLine("Enter your Username");
                        string usrname = Console.ReadLine();
                        Console.WriteLine("Enter your Password");
                        string psswd = Console.ReadLine();
                        if (dtouser.authenticate(users, usrname, psswd))
                        {
                            Console.WriteLine("Login Successful  " + usrname);
                        }
                        else
                        {
                            Console.WriteLine("Login Failed");
                        }

                    }
                    // End If for input_user="L" for User Login
                    else if (input_user.ToUpper() == "UP")
                    {
                        string usrnameinput;
                        users.Clear();
                        Console.WriteLine("Enter the Username to Modify");
                        usrnameinput = Console.ReadLine();
                        dbc.viewuserdata(users, usrnameinput);
                        dtouser.update_userlist(users, usrnameinput);
                        if (dbc.updateuserdata(users) == 1)
                        {
                            Console.WriteLine(usrnameinput + " User Updated Successfully");
                        }
                        else
                            Console.WriteLine("Falied to Update the User");
                    }
                    else if (input_user.ToUpper() == "UD")
                    {
                        string usrnameinput;
                        Console.Clear();
                        users.Clear();
                        Console.WriteLine("Enter your Username to Delete");
                        usrnameinput = Console.ReadLine();
                        dbc.viewuserdata(users, usrnameinput);
                        dtouser.PrintUser(users, "PP");
                        Console.WriteLine("Please confirm (Y) to Delete (Y/N)");
                        if (Console.ReadLine() == "Y")
                        {
                            if (dbc.deletuserdata(usrnameinput) == 1)
                            {
                                Console.WriteLine(usrnameinput + " User Deleted Successfully");
                            }

                        }
                    }
                    else if (input_user.ToUpper() == "PU")
                    {
                        Console.Clear();
                        users.Clear();
                        users.AddRange(dbc.viewuserdata(users, null));
                        dtouser.PrintUser(users, "PU");

                    }
                    else if (input_user.ToUpper() == "PP")
                    {
                        Console.Clear();
                        users.Clear();
                        Console.WriteLine("PP Pressed");
                        dbc.viewuserdata(users, "");
                        dtouser.PrintUser(users, "PP");
                    }
                    else if (input_user.ToUpper() == "L")
                    {
                        Console.WriteLine("Enter your Username");
                        string usrname = Console.ReadLine();
                        Console.WriteLine("Enter your Password");
                        string psswd = Console.ReadLine();
                        users.Clear();
                        dbc.viewuserdata(users,usrname);
                        if (dtouser.authenticate(users, usrname, psswd))
                        {
                            Console.WriteLine(usrname + " Logged in Successfully");
                           /* Console.WriteLine("User Modification <Press UM>");
                            Console.WriteLine("Logout <Press O>");
                            string louser_input = Console.ReadLine();
                            if (louser_input.ToUpper() == "UM")
                            {
                                dtouser.update_userlist(users, usrname);
                            }
                            else if (louser_input.ToUpper() == "O")
                            {
                                dtouser.logout(users, usrname);
                                Console.WriteLine("User Logged out :" + usrname);
                            }
                           */
                        }
                        else
                        {
                            Console.WriteLine("Invalid Login");
                        }

                    }


                }
              /*  else if (option == "L")
                {
                    DTOUser userauth;
                    userauth = dbc.autheticateuser("nbalan", "Nish4632#");
                    Console.WriteLine(userauth.firstname);
                    Console.WriteLine(userauth.lastname);
                    Console.WriteLine(userauth.username);
                    Console.WriteLine(userauth.password);
                    Console.WriteLine(userauth.role);
                }*/
                else if (option == "E")
                    Environment.Exit(0);
            }
        }
    }
}
