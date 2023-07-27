using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;


namespace DBproject
{
    public class DataBase
    {
        private string connectString = @"DATA SOURCE=localhost:1521/orcl;TNS_ADMIN=C:\Users\Administrator\Oracle\network\admin;PERSIST SECURITY INFO=True;USER ID=FJH;Password=123456";
        OracleConnection con;
        //静态类声明
        public static DataBase oracleCon;
        //构造函数
        public DataBase()
        {
            con = new OracleConnection(connectString);
            con.Open();
            Console.WriteLine("Oracle 连接成功\n");
        }

        //用户注册
        public string RegisterUser( string phonenumber,string password, string address, string type)
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                try
                {
                    DateTime now = DateTime.Now;
                    cmd.CommandText = "INSERT INTO USERS(USER_PHONE,USER_PASSWORD,USER_ADDRESS,USER_STATE,USER_BALANCE,USER_REGTIME,USER_TYPE,USER_ID)" +
                        " VALUES(" + phonenumber + " , '" + password + "','" + address +
                        "',0 ,0 ,to_date('" + now.ToString() + "','yyyy/MM/dd HH24:mi:ss')," + type + ",USERIDSEQUENCE.NEXTVAL)";
                    Console.WriteLine(cmd.CommandText);
                                      
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("注册成功");
                    string newID="0000000";
                    using (OracleCommand cmdd = con.CreateCommand())
                    {
                        try
                        {
                            cmdd.CommandText = "SELECT USER_ID FROM USERS WHERE USER_PHONE=" + phonenumber;
                            OracleDataReader reader = cmdd.ExecuteReader();
                            while (reader.Read())
                            {
                                newID = reader.GetString(0);
                                break;
                            }
                            reader.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("返回新ID异常" + ex.Message);
                            return "返回新ID异常" + ex.Message;
                        }

                    }
                        return newID;
                }
                catch (OracleException ex)
                {
                    Console.WriteLine("数据库操作异常：" + ex.Message);
                    return "数据库操作异常：" + ex.Message;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("其他异常：" + ex.Message);
                    return "其他异常：" + ex.Message;
                }
            }
        }

        //商店注册
        public string RegiserSto(string ID,string name,string introduction, string imageURL)
        {
            string message = "";
            using (OracleCommand cmd =con.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "SELECT USER_TYPE FROM USERS WHERE USER_ID=" + ID;
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        message = "商家ID不存在";
                        reader.Dispose();
                        return message;
                    }
                    else
                    {
                        if (reader.GetString(0) != "1")
                        {
                            message = "该用户不是商家";
                            reader.Dispose();
                            return message;
                        }
                    }
                    cmd.CommandText = "INSERT INTO STORE(STO_ID,STO_NAME,STO_INTRODUCTION,STO_LICENSEIMG,STO_STATE) " +
                        "VALUES(" + ID + ",'" + name + "','" + introduction + "','" + imageURL + "',1)";
                    Console.WriteLine(cmd.CommandText);
                    cmd.ExecuteNonQuery();
                    message = "success";
                }
                catch (OracleException ex)
                {
                    Console.WriteLine("数据库操作异常：" + ex.Message);
                    message = "数据库操作异常：" + ex.Message;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    message = "其他错误：" + ex.Message;
                }
                return message;
            }
 
        }

        //ID登录
        public string IDLogin(string ID,string password)
        {
            string flag = "用户不存在";
            using (OracleCommand cmd = con.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "SELECT USER_PASSWORD,USER_TYPE FROM USERS WHERE USER_ID=" + ID;
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    { 
                        if ((string)reader.GetString(0).Trim() == password)
                        {
                            Console.WriteLine((string)reader.GetString(0));
                            flag = reader.GetString(1);
                            Console.WriteLine("登录成功,用户类别为" + flag);
                            reader.Dispose();
                            return flag;
                        }
                    }
                    reader.Dispose();

                    cmd.CommandText = "SELECT USER_ID FROM USERS WHERE USER_ID=" + ID;
                    OracleDataReader reader_id = cmd.ExecuteReader();
                    while (reader_id.Read())
                    {
                        if (ID == reader_id.GetString(0))
                            flag = "密码错误";
                    }
                    reader_id.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    flag = ex.Message;
                }

            }
            return flag;
        }

        //电话号码登录
        public string PhonenumberLogin(string phonenumber,string password)
        {

            string flag = "用户不存在";
            using (OracleCommand cmd = con.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "SELECT USER_PASSWORD,USER_TYPE FROM USERS WHERE USER_PHONE=" + phonenumber;
                    OracleDataReader reader = cmd.ExecuteReader();
                    cmd.CommandText = "SELECT USER_PHONE FROM USERS WHERE USER_PHONE=" + phonenumber;
                    OracleDataReader reader_id = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(0).Trim() == password)
                        {
                            Console.WriteLine((string)reader.GetString(0));
                            flag = reader.GetString(1);
                            Console.WriteLine("登录成功,用户类别为" + flag);
                            reader.Dispose();
                            reader_id.Dispose();
                            return flag;
                        }
                    }
                    while (reader_id.Read())
                    {
                        if (phonenumber == reader_id.GetString(0))
                            flag = "密码错误";
                    }
                    reader.Dispose();
                    reader_id.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    flag = ex.Message;
                }
            }
            return flag;
        }

        //通过电话号码重置密码
        public string ResetPassword(string phonenumber,string password)
        {
            string message = "";
            using (OracleCommand cmd = con.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "SELECT USER_PASSWORD FROM USERS WHERE USER_PHONE=" + phonenumber;
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        message = "用户电话号码不存在！";
                        reader.Dispose();
                        return message;
                    }
                    else
                    {
                        if (reader.GetString(0).Trim() == password)
                        {
                            message = "新密码不能与旧密码相同";
                            reader.Dispose();
                            return message;
                        }
                    }
                    reader.Dispose();
                    cmd.CommandText = "UPDATE USERS SET USER_PASSWORD = '" + password + "' WHERE USER_PHONE=" + phonenumber;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(cmd.CommandText);
                    message = "success";
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    message = "其他错误：" + ex.Message;
                }
                return message;
            }
        }
    }
}
