using Models;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UserCrud
    {
        private readonly string connStr;

        public UserCrud(string connectionString)
        {
            connStr = connectionString;
        }

        public int UsersCounter()
        {
            int result = 0;
            SqlConnection sqlConnection = new SqlConnection(connStr);

            sqlConnection.Open();

            string query = @"SELECT count(*) 
                                FROM [PocketDB].[dbo].[Users]";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            result = int.Parse(sqlCommand.ExecuteScalar().ToString());
            sqlConnection.Close();

            return result;
        }

        public List<UserEntity> DisplayAllUsers()
        {
            List<UserEntity> result = new List<UserEntity>();
            UserEntity user = new UserEntity();
            SqlConnection sqlConnection = new SqlConnection(connStr);

            sqlConnection.Open();

            string query = @"SELECT TOP (1000) [Id]
                                  ,[FirstName]
                                  ,[LastName]
                                  ,[Email]
                                  ,[Mobile]
                                  ,[DateofBirth]
                                  ,[CreatedDate]
                                  ,[CreatedBy]
                                  ,[ModifiedDate]
                                  ,[ModifiedBy]
                                  ,[IsDeleted]
                              FROM [PocketDB].[dbo].[Users]";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                user.Id = reader.GetInt32(0);
                user.FirstName = reader.GetString(1);
                user.LastName = reader.GetString(2);
                user.Email = reader.GetString(3);
                user.Mobile = reader.GetString(4);
                user.DateofBirth = reader.GetDateTime(5);
                user.CreatedDate = reader.GetDateTime(6);
                user.CreatedBy = reader.GetString(7);
                user.ModifiedDate = NullableDate(reader, 8);
                user.ModifiedBy = NullableString(reader, 9);
                user.IsDeleted = reader.GetBoolean(10);

                result.Add(user);
            }
            reader.Close();
            sqlConnection.Close();

            return result;
        }

        public int InsertUser(User user)
        {
            int result = 0;
            SqlConnection sqlConnection = new SqlConnection(connStr);

            sqlConnection.Open();

            string query = @"INSERT INTO [dbo].[Users]
                       ([FirstName]
                       ,[LastName]
                       ,[Email]
                       ,[Mobile]
                       ,[DateofBirth]
                       ,[CreatedDate]
                       ,[CreatedBy]
                       ,[IsDeleted])
                 VALUES
                       (@FirstName, @LastName, @Email, @Mobile, @DateofBirth,@CreatedDate, @CreatedBy, @IsDeleted)";

            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
                sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                sqlCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = user.Mobile;
                sqlCommand.Parameters.Add("@DateofBirth", SqlDbType.NVarChar).Value = user.DateofBirth;
                sqlCommand.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.UtcNow;
                sqlCommand.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = "operator";
                sqlCommand.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                 try
                {
                    result = sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException se)
                {
                    Console.WriteLine("Was exception " + se.Message);
                }
                catch (Exception e)
                {

                    Console.WriteLine("Was exception " + e.Message);
                }
            }
            sqlConnection.Close();

            return result;
        }
        public int DeleteUser(int id)
        {
            int result;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "Delete from Users where Id =" + id;
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int UpdateUserMobile(int userId)
        {
            int result;

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "Update Users set Mobile = '777' where Id =" + userId;
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        private DateTime? NullableDate(SqlDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            else
            {
                return reader.GetDateTime(index);
            }
        }
        private string NullableString(SqlDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
            {
                return null;
            }
            else
            {
                return reader.GetString(index);
            }
        }
    }
}
