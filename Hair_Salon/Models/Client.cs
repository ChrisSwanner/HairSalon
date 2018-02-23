using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private int _id;
        private int _stylistId;

        public Client(string firstName, string lastName, string phoneNumber, int Id = 0, int stylistId = 0)
        {
            _firstName = firstName;
            _lastName = lastName;
            _phoneNumber = phoneNumber;
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

        public string GetFirstName() {return _firstName;}

        public string GetLastName() {return _lastName;}

        public string GetPhoneNumber() {return _phoneNumber;}

        public int GetId() {return _id;}

        public int GetStylistId() {return _stylistId;}
    }
}
