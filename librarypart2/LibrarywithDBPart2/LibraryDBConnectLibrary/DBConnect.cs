using System;
using System.Data.SqlClient;
using LbAssgLibrary;
using System.Collections.Generic;
using System.Data;
namespace LibraryDBConnectLibrary
{
    public class DBConnect
    {
       public  string connectionstring;
       SqlConnection dbcon;
        public SqlConnection DBopen()
        {
            connectionstring= @"Data Source=LAPTOP-UDQPO2MH; Initial Catalog=librarydb; User ID=libadmin; Password=Misha4632#";
            dbcon = new SqlConnection(connectionstring);
            dbcon.Open();
            return dbcon;
        }
        public void DBclose()
        {
            dbcon.Close();
        }
        public DTOUser autheticateuser(string usrname,string psswd)
        {
            DTOUser userinfo = new DTOUser();
            string sqlstring = "select username,psswd,firstname,lastname,lstatus,accountstatus,emailid,phoneno,streetaddress,city,pincode,rolename from userlist, rolelist where rolelist.roleid=userlist.roleid and userlist.username='" + usrname + "' and psswd='" + psswd + "'";
           
            
            SqlCommand sqlcmd;
            SqlDataReader sqldata;
           
            sqlcmd = new SqlCommand(sqlstring,DBopen());
            sqldata= sqlcmd.ExecuteReader();
            sqldata.Read();
            userinfo.username = sqldata[0].ToString();
            userinfo.password = sqldata[1].ToString();
            userinfo.firstname = sqldata[2].ToString();
            userinfo.lastname = sqldata[3].ToString();
            userinfo.loginlogoutstatus = sqldata[4].ToString();
            userinfo.accountstatus = sqldata[5].ToString();
            userinfo.emailid = sqldata[6].ToString();
            userinfo.phoneno  =sqldata[7].ToString();
            userinfo.streetaddress = sqldata[8].ToString();
            userinfo.city = sqldata[9].ToString();
            userinfo.pincode = sqldata[10].ToString();
            userinfo.role=sqldata[11].ToString();
            DBclose();
            return userinfo;

        }
        public void insertuserdata(List<DTOUser> usrlist)
        {
            string sqlstring = "userinsert";
            SqlCommand sqlcmd;
            SqlDataReader sqldata;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqlcmd.CommandType = CommandType.StoredProcedure;
            int rolenametoid = 0;
            foreach (var u in usrlist)
            {
                sqlcmd.Parameters.Add(new SqlParameter("@usrname", u.username));
                sqlcmd.Parameters.Add(new SqlParameter("@psswd", u.password));
                sqlcmd.Parameters.Add(new SqlParameter("@firstname", u.firstname));
                sqlcmd.Parameters.Add(new SqlParameter("@lastname", u.lastname));
                sqlcmd.Parameters.Add(new SqlParameter("@emailid", u.emailid));
                sqlcmd.Parameters.Add(new SqlParameter("@streetaddress", u.streetaddress));
                sqlcmd.Parameters.Add(new SqlParameter("@city", u.city));
                sqlcmd.Parameters.Add(new SqlParameter("@pincode", u.pincode));
                sqlcmd.Parameters.Add(new SqlParameter("@phoneno", u.phoneno));
                rolenametoid = getroleid(u.role);
                sqlcmd.Parameters.Add(new SqlParameter("@roleid", rolenametoid));
                sqlcmd.Parameters.Add(new SqlParameter("@lstatus", u.loginlogoutstatus));
                sqlcmd.Parameters.Add(new SqlParameter("@accountstatus", u.accountstatus));
                sqldata = sqlcmd.ExecuteReader();
                sqlcmd.Parameters.Clear();
                sqldata.Close();

            }
            DBclose();

        }
        public int updateuserdata(List<DTOUser> usrlist)
        {
            string sqlstring = "userupdate";
            SqlCommand sqlcmd;
            SqlDataReader sqldata;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqlcmd.CommandType = CommandType.StoredProcedure;
            SqlParameter returnparam;
            returnparam = new SqlParameter("@returnval", SqlDbType.Int);
            returnparam.Direction = ParameterDirection.Output;
            int retval=0;
            foreach (var u in usrlist)
            {
                sqlcmd.Parameters.Add(new SqlParameter("@usrname", u.username));
                sqlcmd.Parameters.Add(new SqlParameter("@lastname", u.lastname));
                sqlcmd.Parameters.Add(new SqlParameter("@emailid", u.emailid));
                sqlcmd.Parameters.Add(new SqlParameter("@streetaddress", u.streetaddress));
                sqlcmd.Parameters.Add(new SqlParameter("@city", u.city));
                sqlcmd.Parameters.Add(new SqlParameter("@pincode", u.pincode));
                sqlcmd.Parameters.Add(new SqlParameter("@phoneno", u.phoneno));
                sqlcmd.Parameters.Add(returnparam);
                sqldata = sqlcmd.ExecuteReader();
                retval = (int)sqlcmd.Parameters["@returnval"].Value;
                sqlcmd.Parameters.Clear();
                sqldata.Close();
            }
            
            DBclose();
            return retval;
        }

        public int deletuserdata(string username)
        {
            string sqlstring = "userdelete";
            SqlCommand sqlcmd;
            // SqlDataReader sqldata;
            SqlParameter returnparam;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@usrname", username));
            returnparam = new SqlParameter("@returnval", SqlDbType.Int);
            returnparam.Direction = ParameterDirection.Output;
            sqlcmd.Parameters.Add(returnparam);
            sqlcmd.ExecuteNonQuery();
            //sqldata = sqlcmd.ExecuteReader();
            //Console.WriteLine(sqldata.ToString());
            int retval= (int)sqlcmd.Parameters["@returnval"].Value;
            DBclose();
            return retval;
        }

        public List<DTOUser> viewuserdata(List<DTOUser> userlist,string username)
        {
            string sqlstring;
            if (username == String.Empty)
                sqlstring = "select username,psswd,firstname,lastname,lstatus,accountstatus,emailid,phoneno,streetaddress,city,pincode,rolename from userlist, rolelist where rolelist.roleid=userlist.roleid";
            else
                sqlstring = "select username,psswd,firstname,lastname,lstatus,accountstatus,emailid,phoneno,streetaddress,city,pincode,rolename from userlist, rolelist where rolelist.roleid=userlist.roleid and userlist.username='" + username + "'";
            SqlCommand sqlcmd;
            SqlDataReader sqldata;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqldata = sqlcmd.ExecuteReader();
            while (sqldata.Read())
            {
                userlist.Add(new DTOUser
                {
                    username = sqldata[0].ToString(),
                    password = sqldata[1].ToString(),
                    firstname = sqldata[2].ToString(),
                    lastname = sqldata[3].ToString(),
                    loginlogoutstatus = sqldata[4].ToString(),
                    accountstatus = sqldata[5].ToString(),
                    emailid = sqldata[6].ToString(),
                    phoneno = sqldata[7].ToString(),
                    streetaddress = sqldata[8].ToString(),
                    city = sqldata[9].ToString(),
                    pincode = sqldata[10].ToString(),
                    role = sqldata[11].ToString()
                }
                );
                
            }
           // foreach (var i in userlist)
          //  Console.WriteLine(i.username);
            DBclose();
            return userlist;
         }
        
               
        
       
        public List<DTORole> viewroledata(List<DTORole> roles)
        { 
            string sqlstring = "select rolename from rolelist";
            SqlCommand sqlcmd;
            SqlDataReader sqldata;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqldata = sqlcmd.ExecuteReader();
            string roleinfo;
            while (sqldata.Read())
            {   roleinfo=sqldata[0].ToString();
                roles.Add(new DTORole { role = roleinfo });
            }
            DBclose();
            return roles;
        }

        public void insertroledata(List<DTORole> roles)
        {
            string sqlstring = "roleinsert";
            SqlCommand sqlcmd;
            SqlDataReader sqldata;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqlcmd.CommandType = CommandType.StoredProcedure;
            foreach (var r in roles)
            {
                sqlcmd.Parameters.Add(new SqlParameter("@rolename", r.role));
                sqldata = sqlcmd.ExecuteReader();
                sqlcmd.Parameters.Clear();
                sqldata.Close();

            }
            DBclose ();
        }

        public int deleteroledata(string rolename)
        {
            string sqlstring = "roledelete";
            SqlCommand sqlcmd;
           // SqlDataReader sqldata;
            SqlParameter returnparam;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.Add(new SqlParameter("@rolename",rolename));
            returnparam=new SqlParameter("@returnval", SqlDbType.Int);
            returnparam.Direction = ParameterDirection.Output;
            sqlcmd.Parameters.Add(returnparam);
            sqlcmd.ExecuteNonQuery();
            //sqldata = sqlcmd.ExecuteReader();
            //Console.WriteLine(sqldata.ToString());
            int retval = (int)sqlcmd.Parameters["@returnval"].Value;
            DBclose ();
            return retval;
        }

        public int getroleid(string rolename)
        {
            int roleid;
            string sqlstring = "select roleid from rolelist where rolename='" + rolename + "'" ;
            SqlCommand sqlcmd;
            SqlDataReader sqldata;
            sqlcmd = new SqlCommand(sqlstring, DBopen());
            sqldata = sqlcmd.ExecuteReader();
            sqldata.Read();
            if (sqldata.HasRows)
                roleid = Convert.ToInt32(sqldata[0]);
            else
                roleid = 0;
            DBclose();
            return roleid;

        }
               
            
        
    }
}
