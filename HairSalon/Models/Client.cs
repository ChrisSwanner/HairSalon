using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private string _description;
    private int _id;
    private int _stylistId;
    private string _rawDate;
    private DateTime? _formattedDate;

    public Client (string description, string rawDate, int Id = 0, int stylistId = 0)
    {
      _description = description;
      _stylistId = stylistId;
      _id = Id;
      _rawDate = rawDate;
      _formattedDate = new DateTime();
    }

    public DateTime? GetFormattedDate()
    {
      return _formattedDate;
    }

    public string GetRawDate()
    {
      return _rawDate;
    }

    public void SetDate()
    {
      if (_rawDate != null && _rawDate != "")
      {
        string[] dateArray = _rawDate.Split('-');
        List<int> intDateList = new List<int>{};
        foreach (string num in dateArray)
        {
          intDateList.Add(Int32.Parse(num));
        }
        _formattedDate = new DateTime(intDateList[0], intDateList[1], intDateList[2]);
      }
      else
      {
        _formattedDate = null;
      }

    }

    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetstylistId()
    {
      return _stylistId;
    }

    public void SetCatId(int id)
    {
      _stylistId = id;
    }

    public static List<Client> GetAll()
    {
      List<Client> allItems = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        string itemRawDate = rdr.GetString(2);
        int stylistId = rdr.GetInt32(4);
        Client newItem = new Client(itemDescription, itemRawDate, itemId, stylistId);
        newItem.SetDate();
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      this.SetDate();
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `clients` (`description`,  `raw_date`, `formatted_date`, `stylist_id`) VALUES (@ItemDescription, @RawDate, @FormattedDate, @stylistId);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this._description;

      MySqlParameter rawDate = new MySqlParameter();
      rawDate.ParameterName = "@RawDate";
      rawDate.Value = this._rawDate;

      MySqlParameter formattedDate = new MySqlParameter();
      formattedDate.ParameterName = "@FormattedDate";
      formattedDate.Value = this._formattedDate;

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = this._stylistId;

      cmd.Parameters.Add(description);
      cmd.Parameters.Add(rawDate);
      cmd.Parameters.Add(formattedDate);
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Client))
      {
        return false;
      }
      else
      {
        Client newItem = (Client) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        bool stylistEquality = (this.GetstylistId() == newItem.GetstylistId());
        return (idEquality && descriptionEquality && stylistEquality);
      }
    }
    // public static void ClearAll()
    // {
    //   _instances.Clear();
    // }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `clients` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      string itemRawDate = "";
      int itemstylistId = 0;

      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
        itemRawDate = rdr.GetString(2);
        itemstylistId = rdr.GetInt32(4);
      }

      Client foundItem = new Client(itemDescription, itemRawDate, itemId, itemstylistId);
      foundItem.SetDate();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundItem;
    }

    public void Edit(string newDescription, string newDate)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET description = @newDescription, raw_date = @newDate WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      MySqlParameter date = new MySqlParameter();
      date.ParameterName = "@newDate";
      date.Value = newDate;
      cmd.Parameters.Add(date);

      cmd.ExecuteNonQuery();
      _description = newDescription;

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
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = _id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


  }
}
