using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTK_HT_Nhom4
{
    internal class Connector
    {
        private SqlConnection strCon;

        public Connector() 
        {
            strCon = new SqlConnection(@"Data Source=LASCENT;Initial Catalog=PTTK_HT;User ID=sa;Password=Hieu@2003;");
        }
        public SqlConnection getConnection
        {
            get { return strCon; }
        }


        // open the connection
        public void openConnection()
        {
            try
            {
                if (strCon.State == ConnectionState.Closed)
                {
                    strCon.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening connection: " + ex.Message);
            
                // Handle the exception as needed
            }
        }

        // close the connection
        public void closeConnection()
        {
            try
            {
                if (strCon.State == ConnectionState.Open)
                {
                    strCon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error closing connection: " + ex.Message);
                // Handle the exception as needed
            }
        }

        // Implement IDisposable to properly release resources
        public void Dispose()
        {
            closeConnection();
            strCon.Dispose();
        }
    }
}
