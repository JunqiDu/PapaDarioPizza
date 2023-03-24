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
    class ContactUses
    {
        public string FeedbackMessage { get; set; }
        public string FeedbackEmail { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddFeedback(string connectionString)
        {
            string AddNewFeedback = "INSERT INTO Feedbacks(FeedbackMessage, FeedbackEmail)values(@FeedbackMessage, " +
                "@FeedbackEmail)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("FeedbackMessage", FeedbackMessage);
                        cmd.Parameters.AddWithValue("FeedbackEmail", FeedbackEmail);
                        cmd.CommandText = AddNewFeedback;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
