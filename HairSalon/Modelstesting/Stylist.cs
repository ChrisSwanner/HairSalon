using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string _name;
    private int _id;

    public Stylist(string name, int Id = 0)
    {
      _name = name;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int stylistId = rdr.GetInt32(1);
        string stylistName = rdr.GetString(0);
        Stylist newstylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newstylist);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public override bool Equals(System.Object otherstylist)
    {
      if (!(otherstylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newstylist = (Stylist) otherstylist;
        return this.GetId().Equals(newstylist.GetId());
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `stylists` (`name`, `id`) VALUES (@stylistName, @stylistId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@stylistName";
      name.Value = this._name;

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@stylistId";
      id.Value = this._id;

      cmd.Parameters.Add(name);
      cmd.Parameters.Add(id);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Client> GetItems()
    {
      List<Client> allstylistItems = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `clients` WHERE `stylist_id` = @stylist_id;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        string itemRawDate = rdr.GetString(2);
        int itemstylistId = rdr.GetInt32(4);
        Client newItem = new Client(itemDescription, itemRawDate, itemId, itemstylistId);
        newItem.SetDate();
        allstylistItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allstylistItems;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

      var cmdItems = conn.CreateCommand() as MySqlCommand;
      cmdItems.CommandText = @"DELETE FROM clients WHERE stylist_id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);
      cmdItems.Parameters.Add(thisId);

      cmdItems.ExecuteNonQuery();
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from `stylists` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int stylistId = 0;
      string stylistName = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(1);
        stylistName = rdr.GetString(0);
      }

      Stylist foundstylist = new Stylist(stylistName, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundstylist;
    }

    public List<Client> SortAsc()
    {
      List<Client> sortedList = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM `clients` WHERE `stylist_id` = @thisId ORDER BY -`formatted_date` DESC;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = this.GetId();
      cmd.Parameters.Add(thisId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        string itemRawDate = rdr.GetString(2);
        int stylistId = rdr.GetInt32(4);

        Client newItem = new Client(itemDescription, itemRawDate, itemId, stylistId);
        newItem.SetDate();
        sortedList.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return sortedList;
    }

    public List<Client> SortDesc()
    {
      List<Client> sortedList = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM `clients` WHERE `stylist_id` = @thisId ORDER BY `formatted_date` DESC;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = this.GetId();
      cmd.Parameters.Add(thisId);

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        string itemRawDate = rdr.GetString(2);
        int stylistId = rdr.GetInt32(4);

        Client newItem = new Client(itemDescription, itemRawDate, itemId, stylistId);
        newItem.SetDate();
        sortedList.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return sortedList;
    }
  }
}
