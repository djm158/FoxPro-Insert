using InserterLibrary;
using System;
using System.Data;
using System.IO;

namespace Inserter
{
    class Program
    {
        //todo change this to your path
        static readonly string DatabasePath = @"C:\Users\dmcgrath\source\repos\FoxPro-Insert\Foxpro-Insert-Repro\Database\main.dbc";
        static string ConnectionString;

        static readonly string TableCategoriesWorking = "categoryauto";
        static readonly string TableCategoriesFailing = "categorynewid";

        static void Main(string[] args)
        {
            BuildConnectionString();

            // uses stored proc for primary key
            GetAndPrintCategories();

            // testing stored proc. last name of customer will default to "HELLO"
            CallStoredProc();

            // this works
            InsertCategoryIntoWorkingTable();

            // this will fail
            InsertCategoryIntoFailingTable();

            // pause execution
            Console.ReadLine();
        }

        private static void InsertCategoryIntoFailingTable()
        {
            var wrapper = CreateFoxProWrapper(ConnectionString);

            wrapper.InsertRecord(TableCategoriesFailing, GetRandomString());
        }

        private static void CallStoredProc()
        {
            var wrapper = CreateFoxProWrapper(ConnectionString);
            wrapper.InsertCustomer("dmcgrath");
            var customers = wrapper.GetAllData("customers");
            PrintTable(customers);
        }

        private static void InsertCategoryIntoWorkingTable()
        {
            var wrapper = CreateFoxProWrapper(ConnectionString);

            // insert a record into auto incrementing table
            wrapper.InsertRecord(TableCategoriesWorking, GetRandomString());
        }

        private static void GetAndPrintCategories()
        {
            // get all the categories
            var wrapper2 = CreateFoxProWrapper(ConnectionString);
            var categories = wrapper2.GetAllData(TableCategoriesWorking);
            PrintTable(categories);
        }

        private static FoxProWrapper CreateFoxProWrapper(string conString)
        {
            // instantiate wrapper
            return new FoxProWrapper(conString);
        }

        private static void BuildConnectionString()
        {
            var foxProPrefix = "Provider=VFPOLEDB.1;Data Source=";

            // printing path for debugging
            Console.WriteLine(DatabasePath);

            var conString = $"{foxProPrefix}{DatabasePath}";

            // again print for debuggin
            Console.WriteLine(conString);

            ConnectionString = conString;
        }

        // use Path.GetRandomFileName() to generate a random string
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        public static void PrintTable(DataTable table)
        {
            // write out column names
            foreach (DataColumn col in table.Columns)
            {
                Console.Write("{0,-14}", col.ColumnName);
            }

            Console.WriteLine();

            // write out data
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    Console.Write("{0,-14}", row[col]);
                }
                Console.WriteLine();
            }
        }
    }
}
