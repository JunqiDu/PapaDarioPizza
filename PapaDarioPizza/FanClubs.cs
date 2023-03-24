using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PapaDarioPizza
{
    class FanClubs : INotifyPropertyChanged
    {
        public int FanID { get; set; }
        public string FanEmail { get; set; }
        public string FanPassword { get; set; }
        public string FanNickname { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //add new fan
        public void AddFan(string connectionString)
        {
            string AddNewFan = "INSERT INTO Fans(FanEmail, FanPassword, FanNickname)values(@FanEmail, " +
                "@FanPassword, @FanNickname)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("FanEmail", FanEmail);
                        cmd.Parameters.AddWithValue("FanPassword", FanPassword);
                        cmd.Parameters.AddWithValue("FanNickname", FanNickname);
                        cmd.CommandText = AddNewFan;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        //login fan
        public string Login(string connectionString)
        {
            const string GetLoginQuery = "select FanEmail from Fans where FanEmail = @FanEmail";

            string result = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetLoginQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var f = new FanClubs();
                                    f.FanEmail = reader.GetString(0);

                                    result = "Welcome back !";
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }
    }
}