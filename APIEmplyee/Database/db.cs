using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


    public class db
    {
        public const string ConnectionString = "Data Source=SIDHARTH-LAPTOP\\SQLEXPRESS;" +
        "Initial Catalog=employ;Integrated Security=True";

        public void SaveEmployeeData(string Id, string Name, string email, string phone, string dob, bool gender, string pos)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO employeetable2 (Id,Name,email,phone,dob,gender,pos) " +
                "VALUES (@Id,@Name,@email,@phone,@dob,@gender,@pos)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@dob", dob);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@pos", pos);

                    command.ExecuteNonQuery();
                }
            }
        }

    public employee RetrieveEmployeeById(string employeeId)
    {
        employee result = null;

        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            string selectQuery = "SELECT Id, Name, email, phone, dob, gender, pos FROM employeetable2 WHERE Id = @EmployeeId";

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new employee
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            email = reader.GetString(2),
                            phone = reader.GetString(3),
                            dob = reader.GetString(4),
                            gender = reader.GetBoolean(5),
                            pos = reader.GetString(6)
                        };
                    }
                }
            }
        }

        return result;
    }



    public void UpdateEmployee(string Id, employee request)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE employeetable2 SET Name=@Name, email=@email, phone=@phone, dob=@dob, gender=@gender, pos=@pos " +
                    "WHERE Id=@Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Name", request.Name);
                    command.Parameters.AddWithValue("@email", request.email);
                    command.Parameters.AddWithValue("@phone", request.phone);
                    command.Parameters.AddWithValue("@dob", request.dob);
                    command.Parameters.AddWithValue("@gender", request.gender);
                    command.Parameters.AddWithValue("@pos", request.pos); ;

                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteEmployeeData(string Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM employeetable2 WHERE Id=@Id";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }



