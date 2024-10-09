namespace Inventory
{
    using System;
    using System.Data;
    using Microsoft.Data.SqlClient;

    class Program
    {
        static void Main()
        {
            string connectionString = "Server=DESKTOP-9PK656A\\SQLEXPRESS;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True"; // Замените на вашу строку подключения
            int newObjectId;

            AddOrUpdateInventory("Компьютер", 10, "В наличии", out newObjectId, connectionString);

            Console.WriteLine($"Новый/обновленный инвентарный объект имеет ID: {newObjectId}");
        }

        static void AddOrUpdateInventory(string objectName, int objectQuantity, string objectStatus, out int objectId, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("AddOrUpdateInventory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Параметры для процедуры
                    command.Parameters.AddWithValue("@ObjectName", objectName);
                    command.Parameters.AddWithValue("@ObjectQuantity", objectQuantity);
                    command.Parameters.AddWithValue("@ObjectStatus", objectStatus);

                    // Параметр OUTPUT
                    SqlParameter outputIdParam = new SqlParameter("@ObjectID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    // Открытие подключения и выполнение команды
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Получение выходного значения
                    objectId = (int)outputIdParam.Value;
                }
            }
        }
    }

}
