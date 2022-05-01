using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LbAssgLibrary
{
    public class DTOUser
    {
        public string role { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string emailid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public  string phoneno { get; set; }
        public string loginlogoutstatus { get; set; }
        public string streetaddress { get; set; }
        public string city { get; set; }
        public string pincode { get; set; }
        public string accountstatus { get; set; }
        


       



        // Begin - Method to Add items in the List- register()
        public List<DTOUser> register(List<DTORole> roles)
        {
           List<DTOUser> users = new List<DTOUser>();

                 Console.Clear();
                 Console.WriteLine("Enter your firstname");
                 string f_name = Console.ReadLine();
                 Console.WriteLine("Enter your lastname");
                 string l_name = Console.ReadLine();
                 Console.WriteLine("Enter your emailid");
                 string mailid = Console.ReadLine();
                 Console.WriteLine("Enter your Phone number");
                 string pno = Console.ReadLine();
                 Console.WriteLine("Enter your username");
                 string uname = Console.ReadLine();
                 Console.WriteLine("Enter your password");
                 string pswd = Console.ReadLine();
                 Console.WriteLine("Enter your Street Address");
                 string staddress=Console.ReadLine();
                 Console.WriteLine("Enter your City");
                 string cty = Console.ReadLine();
                Console.WriteLine("Enter your Pincode");
                string p_code = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Please enter the assigned role");
                string input_role = Console.ReadLine();
                bool flag_role=false;
                foreach (DTORole i in roles)
                {
                    if (i.role == input_role)
                    {
                        
                        users.Add(new DTOUser
                        {
                            firstname = f_name,
                            lastname = l_name,
                            emailid = mailid,
                            username = uname,
                            password = pswd,
                            role = input_role,
                            phoneno=pno,
                            streetaddress=staddress,
                            city=cty,
                            pincode=p_code,
                            loginlogoutstatus="O",
                            accountstatus="Open"
                        }
                        );

                        flag_role = true;
                        break;
                    }
                    
                }
                if (flag_role == true)
                    break;
                else
                    Console.WriteLine("Enter a valid Role");
            }
                                      
            return users;
        }
        // End - Method to Add itesm in the list - register()

        //Begin - Authenticate User login
        public bool authenticate(List<DTOUser> usrlist,string usrname,string psswd)
        {
            bool login_status = false;
            if(usrname == null || psswd==null)
            {
                Console.WriteLine("Enter your Username");
                usrname = Console.ReadLine();
                Console.WriteLine("Enter your Password");
                psswd = Console.ReadLine();
            }
           
            
            foreach (var j in usrlist)
            {
                if (j.username == usrname && j.password == psswd)
                {
                    //Console.Clear();
                    login_status = true;
                    j.loginlogoutstatus = "L";
                    //Console.WriteLine("Login Successful");
                    //Console.WriteLine("User Details: First Name : " + j.firstname + "Last Name : " + j.lastname + "Phone Number :" + j.phoneno + "Role :" + j.role);
                    break;
                }


            }
           // if (login_status == false)
             //   Console.WriteLine("Invalid Login");
            return login_status;
        }
            
        // End -Method to Authenticate User

      

        //Begin - Method to Print User/User Profile
        public string PrintUser(List<DTOUser> usrlist,string opt)
        {
            string userinfo="";
            if (opt.ToUpper() == "PU")
            {
                Console.Clear();
                Console.WriteLine("List of User Names");
                foreach (var j in usrlist)
                {
                    Console.WriteLine(j.username);
                    userinfo = userinfo + j.username;
                }
               
            }
            else if (opt.ToUpper() == "PP")
            {
               // Console.Clear();
                Console.WriteLine("List of User Profiles");
                foreach (var j in usrlist)
                {
                    Console.WriteLine("User Name   : " + j.username);
                    Console.WriteLine("First Name  : " + j.firstname);
                    Console.WriteLine("Last Name   : "+ j.lastname);
                    Console.WriteLine("Email ID    : " + j.emailid);
                    Console.WriteLine("Phone No    : " + j.phoneno);
                    Console.WriteLine("Role        : " + j.role);
                    Console.WriteLine("Login Status: " +j.loginlogoutstatus);
                    Console.WriteLine("-----------------------");
                    userinfo = userinfo + j.username + j.firstname + j.lastname + j.emailid + j.phoneno + j.role;
                }
              
            }
            else if(opt.ToUpper() =="PPT")
            {
                foreach (var j in usrlist)
                {
                    userinfo = userinfo + j.username + j.firstname + j.lastname + j.emailid + j.phoneno + j.role;
                }

            }
            else if(opt.ToUpper()=="PUT")
            {
                foreach (var j in usrlist)
                    userinfo = userinfo + j.username;
            }
            return userinfo;
        }

        public void update_userlist(List<DTOUser> userupdate,string usr_update)
        {
            if (usr_update==String.Empty)
            {
                Console.WriteLine("Enter Username to Modify");
                usr_update = Console.ReadLine();
            }

            foreach(var j in userupdate)
            {
                if (j.username == usr_update)
                {
                    Console.WriteLine("User Name  : " + j.username);
                    Console.WriteLine("First Name : " + j.firstname);
                    Console.WriteLine("Last Name  : " + j.lastname);
                    Console.WriteLine("Email ID   : " + j.emailid);
                    Console.WriteLine("Phone No   : " + j.phoneno);
                    Console.WriteLine("Role       : " + j.role);
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Update Last Name or Press <Enter> if no change");
                    string lnameupdate=Console.ReadLine();

                    if(lnameupdate!= String.Empty)
                    {
                        j.lastname = lnameupdate;
                        
                    }

                    Console.WriteLine("Enter the Phone number or Press <Enter> if no change ");
                    string phnoupdate=Console.ReadLine();

                    if(phnoupdate!=String.Empty)
                    {
                        j.phoneno=phnoupdate;
                    }
                    Console.WriteLine("Enter the email id or Press <Enter> if no change");
                    string emailupdate=Console.ReadLine();

                    if(emailupdate!= String.Empty)
                    {
                          j.emailid=emailupdate;
                    }

                }
            }
        }
        public char logout(List<DTOUser> userlogout,string username)
        {
            char logoutstat='N';
            foreach (var j in userlogout)
            {
                if (j.username == username)
                {
                    j.loginlogoutstatus = "O";
                    logoutstat = 'O';
                    break;
                }
            }
                    return logoutstat;
        }

    }
}
