using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace HospitalManagementSystem
{
    public class DatabaseManager
    {
        private string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=HospitalManagementSystem;Integrated Security=true;";
        public bool ValidateUser(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password",
                    connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public DataTable GetPatients()
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Patients ORDER BY CreatedDate DESC", connection);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetDoctors()
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Doctors", connection);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        public DataTable GetAppointments()
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM vw_AppointmentDetails", connection);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
    }
}