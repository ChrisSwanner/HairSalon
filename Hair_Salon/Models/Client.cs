using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _name;
        private int _id;
        private int _stylistId;

        public Client(string name, int Id = 0, int stylistId = 0)
        {
            _name = name;
            _id = Id;
            _stylistId = stylistId;
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

        public string GetName() {return _name;}

        public int GetId() {return _id;}

        public int GetStylistId() {return _stylistId;}

        public void SetStylistId(int id)
        {
            _stylistId = id;
        }

        public static List<Client> GetAllClients()
        {
            List<Client> allClients = new List<Client>{};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              string clientName = rdr.GetString(1);
              string clientLastName = rdr.GetString(2);
              int clientId = rdr.GetInt32(0);
              int clientStylistId = rdr.GetInt32(4);
              Client newClient = new Client(clientName, clientLastName, clientId, clientStylistId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
            return allClients;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `clients` ('name') VALUES (@Name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@Name";
            name.Value = this._name;

            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public override bool Equals(System.Object otherClient)
        {
            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client) otherClient;
                bool idEquality = (this.GetId() == newClient.GetId());
                bool nameEquality = (this.GetName() == newClient.GetName());
                bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());

                return (idEquality && nameEquality && stylistIdEquality);
            }
        }



    }
}
