using Microsoft.Data.SqlClient;
using PetroPrice_MVC.Models;

namespace PetroPrice_MVC.Services
{
    public class PetrolStationsDAO : IPetrolStationService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PetrolPrice-MVC;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public virtual int Create(PetrolStation petrolStation)
        {
            int newIdNUmber = -1;
            string sqlStatement = "INSERT INTO [dbo].[PetroPrice-MVC] (Name, Address, Price) VALUES (@Name, @Address, @Price)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", petrolStation.Name);
                command.Parameters.AddWithValue("@Address", petrolStation.Address);
                command.Parameters.AddWithValue("@Price", petrolStation.Price);

                try
                {
                    connection.Open();
                    newIdNUmber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return newIdNUmber;

        }

        public virtual int Delete(int Id)
        {
            int newIdNUmber = -1;
            string sqlStatement = "DELETE FROM [dbo].[PetroPrice-MVC] WHERE Id=@Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", Id);

                try
                {
                    connection.Open();
                    newIdNUmber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return newIdNUmber;
        }

        public virtual List<PetrolStation> GetAllPetrolStations()
        {
            List<PetrolStation> foundPetrolStations = new List<PetrolStation>();
            string sqlStatement = "SELECT * FROM [dbo].[PetroPrice-MVC]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundPetrolStations.Add(new PetrolStation { Id = (int)reader[0], Name = (string)reader[1], Address = (string)reader[2], Price = (decimal)reader[3] });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return foundPetrolStations;
        }

        public virtual PetrolStation GetPetrolStationById(int id)
        {
            PetrolStation foundPetrolStation = null;
            string sqlStatement = "SELECT * FROM [dbo].[PetroPrice-MVC] WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundPetrolStation = new PetrolStation { Id = (int)reader[0], Name = (string)reader[1], Address = (string)reader[2], Price = (Decimal)reader[3] };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return foundPetrolStation;
        }

        public List<PetrolStation> SearchPetrolStations(string searchTerm)
        {
            List<PetrolStation> foundPetrolStations = new List<PetrolStation>();
            string sqlStatement = "SELECT * FROM [dbo].[PetroPrice-MVC] WHERE Name LIKE @Name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", '%' + searchTerm + '%');
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundPetrolStations.Add(new PetrolStation { Id = (int)reader[0], Name = (string)reader[1], Address = (string)reader[2], Price = (decimal)reader[3] });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return foundPetrolStations;
        }

        public virtual int Update(PetrolStation petrolStation)
        {
            int newIdNUmber = -1;
            string sqlStatement = "UPDATE [dbo].[PetroPrice-MVC] SET Name = @Name, Address = @Address, Price=@Price WHERE Id=@Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@Name", petrolStation.Name);
                command.Parameters.AddWithValue("@Address", petrolStation.Address);
                command.Parameters.AddWithValue("@Price", petrolStation.Price);
                command.Parameters.AddWithValue("@Id", petrolStation.Id);

                try
                {
                    connection.Open();
                    newIdNUmber = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return newIdNUmber;
        }
    }
}
