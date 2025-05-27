using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

public class DatabaseHelper
{
  private string connectionString;

  public DatabaseHelper()
  {
    connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
  }

  public DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
  {
    DataTable dataTable = new DataTable();

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        if (parameters != null)
        {
          command.Parameters.AddRange(parameters);
        }

        try
        {
          connection.Open();
          MySqlDataAdapter adapter = new MySqlDataAdapter(command);
          adapter.Fill(dataTable);
        }
        catch (Exception ex)
        {
          // Handle or log exception
          throw;
        }
      }
    }

    return dataTable;
  }

  public int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
  {
    int result = 0;

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      using (MySqlCommand command = new MySqlCommand(query, connection))
      {
        if (parameters != null)
        {
          command.Parameters.AddRange(parameters);
        }

        try
        {
          connection.Open();
          result = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          // Handle or log exception
          throw;
        }
      }
    }

    return result;
  }
}