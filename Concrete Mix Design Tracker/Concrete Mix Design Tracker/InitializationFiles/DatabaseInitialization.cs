using System;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Concrete_Mix_Design_Tracker
{
    public partial class Main
    {
        /**/
        String str;
        SqlConnection myConn = new SqlConnection ("Server=localhost;Integrated security=SSPI;database=master");
        private void InitializeDatabase()
        {
            str = "CREATE DATABASE MixDatabase ON PRIMARY" +
                "(NMME = MixDatabase_Data, " +
                "FILENAME = 'C:\\MixDatabaseData.mdf', " +
                "SIZE = 2MB, MAXSIZE = 10 MB, FILEGROWTH = 10%)" +
                "LOG ON (NAME = MixDatabaseLog.ldf', " +
                "SIZE = 1MB, " +
                "FILEGROWTH = 10%)";
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("DataBase is Created Successfully");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Mix Design Tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == System.Data.ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

        }
        /**/
    }
}