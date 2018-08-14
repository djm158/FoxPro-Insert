using System.Data;
using System.Data.OleDb;

namespace InserterLibrary
{
    public class FoxproWrapper
    {
        private string connectionString;

        public FoxproWrapper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetAllData(string table)
        {
            var queryText = $"select * from {table}";
            var data = new DataTable();

            using (OleDbConnection connectionHandler = new OleDbConnection(connectionString))
            {
                var da = new OleDbDataAdapter();
                var query = new OleDbCommand(queryText, connectionHandler);
                connectionHandler.Open();
                da.SelectCommand = query;

                da.Fill(data);
                connectionHandler.Close();

            }

            return data;
        }

        public void InsertRecord(string table, string category)
        {
            using (OleDbConnection connectionHandler = new OleDbConnection(connectionString))
            {
                var insertText = $"insert into {table} (catname) values (?)";
                var insertCommand = new OleDbCommand(insertText, connectionHandler);

                insertCommand.Parameters.Add("category", OleDbType.Char).Value = category;

                connectionHandler.Open();
                insertCommand.ExecuteNonQuery();
                connectionHandler.Close();
            }
        }

        public void InsertCustomer(string name)
        {
            using (OleDbConnection connectionHandler = new OleDbConnection(connectionString))
            {
                var insertText = $"insert into customers (firstname) values (?)";
                var insertCommand = new OleDbCommand(insertText, connectionHandler);

                insertCommand.Parameters.Add("firstname", OleDbType.Char).Value = name;

                connectionHandler.Open();
                insertCommand.ExecuteNonQuery();
                connectionHandler.Close();
            }
        }
    }
}
