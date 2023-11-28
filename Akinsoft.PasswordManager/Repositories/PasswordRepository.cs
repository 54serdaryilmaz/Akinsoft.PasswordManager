using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Akinsoft.PasswordManager.Models;
using Dapper;

namespace Akinsoft.PasswordManager.Repositories
{
    public class PasswordRepository
    {
        private SqlConnection connection = new SqlConnection(DbConnectionString.ConnectionString);

        public List<Category> GetListCategory()
        {
            return connection.Query<Category>("SELECT * FROM [Category]  where IsActive=1 order by CategoryName").ToList();
        }
        public Category GetCategoryID(int ID)
        {
            return connection.Query<Category>("SELECT * FROM [Category]  where CategoryID=@key", new { key = ID }).FirstOrDefault();
        }
        public List<Users> GetListUsers()
        {
            return connection.Query<Users>("SELECT * FROM [Users] order by UserName").ToList();
        }
        public List<PasswordRecord> GetListPass(string user)
        {
            return connection.Query<PasswordRecord>("SELECT * FROM [PasswordRecord] WHERE CreatedUser=@key " , new {key=user}).ToList();
        }

        public bool DeletePass(int id, string user)
        {
            connection.Execute("DELETE FROM PasswordRecord  WHERE Id = @key and CreatedUser=@createdUser", new { createdUser = user, key = id });
            return true;
        } 

        public Users GetUserID(int ID)
        {
            return connection.Query<Users>("SELECT * FROM [Users]  where Id=@key", new { key = ID }).FirstOrDefault();
        }
        public bool CategoryUpdate(int id)
        {
            connection.Execute("UPDATE Category SET IsActive=0  WHERE CategoryID = @key", new {key = id });
            return true;
        }
        public bool InsertCategory(Category es)
        {
            var kontrol = connection.Query<Category>("Select * from Category where IsActive=1 and CategoryName=@key", new { key = es.CategoryName }).FirstOrDefault();
            if (kontrol != null)
            {
                return false;
            }
            else
            {
                connection.Execute(@"
                    INSERT INTO Category
                               (CategoryName)
                         VALUES
                               (@str1)",
                               new
                               {
                                   str1 = es.CategoryName
                               });
                return true;


            }
        }
        public bool InsertUser(Users es)
        {

            connection.Execute(@"
                    INSERT INTO Users
                               (UserName,Password)
                         VALUES
                               (@str1,@str2)",
                           new
                           {
                               str1 = es.UserName,
                               str2 = es.Password
                           });
            return true;


        }
        public bool InsertPass(PasswordRecord es)
        {
            try
            {
                connection.Execute(@"
                    INSERT INTO PasswordRecord
                               (Description,Password,Url,Username,CategoryID,CreatedUser)
                         VALUES
                               (@str1,@str2,@str3,@str4,@str5,@str6)",
                           new
                           {
                               str1 = es.Description,
                               str2 = es.Password,
                               str3 = es.Url,
                               str4 = es.Username,
                               str5 = es.CategoryID,
                               str6 = es.CreatedUser

                           });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public string Sifrele(string metin)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(metin);
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += 1;
            }
            return Convert.ToBase64String(bytes);
        }

        public bool LoginCheck(Users mdl)
        {

            var usr = connection.Query<Users>("Select * from Users where UserName=@key and Password=@keypass", new { key = mdl.UserName, keypass = mdl.Password }).FirstOrDefault();
            if (usr != null)
            { return true; }
            else
            {
                return false;
            }

        }


    }
}