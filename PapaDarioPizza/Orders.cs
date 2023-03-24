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
    class Orders:INotifyPropertyChanged
    {
        public int OrderID { get; set; }
        public string PizzaSize { get; set; }
        public string PizzaTopping { get; set; }
        public string Sandwich { get; set; }
        public string Snack { get; set; }
        public string Drinking { get; set; }
        public string OrderEmail { get; set; }
        public double TotalPrice { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddOrder(string connectionString)
        {
            string AddOrder = "INSERT INTO Orders(PizzaSize, PizzaTopping, Sandwich, Snack, Drinking, " +
                "OrderEmail, TotalPrice)values(@PizzaSize, @PizzaTopping, @Sandwich, @Snack, @Drinking, " +
                "@OrderEmail, @TotalPrice)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("PizzaSize", PizzaSize);
                        cmd.Parameters.AddWithValue("PizzaTopping", PizzaTopping);
                        cmd.Parameters.AddWithValue("Sandwich", Sandwich);
                        cmd.Parameters.AddWithValue("Snack", Snack);
                        cmd.Parameters.AddWithValue("Drinking", Drinking);
                        cmd.Parameters.AddWithValue("OrderEmail", OrderEmail);
                        cmd.Parameters.AddWithValue("TotalPrice", TotalPrice);
                        cmd.CommandText = AddOrder;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public string CheckFan(string connectionString)
        {
            const string GetEmailQuery = "Select FanEmail from Fans where FanEmail = @OrderEmail";

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
                            cmd.CommandText = GetEmailQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var o = new Orders();
                                    o.OrderEmail = reader.GetString(0);

                                    result = "Fan";
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
