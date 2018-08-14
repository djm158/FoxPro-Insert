using InserterLibrary;
using System;
using System.Data;
using System.IO;

namespace Inserter
{
    class Program
    {
        static void Main(string[] args)
        {
            var foxProPrefix = "Provider=VFPOLEDB.1;Data Source=";
            // change this
            var dbPath = @"C:\Users\dmcgrath\source\repos\FoxPro-Insert\Foxpro-Insert-Repro\Database\main.dbc";
            Console.WriteLine(dbPath);

            var categoriesGood = "categoryauto";
            var categoriesBad = "categorynewid";

            var conString = $"{foxProPrefix}{dbPath}";

            Console.WriteLine(conString);

            FoxproWrapper wrapper = new FoxproWrapper(conString);

            // insert a record into auto incrementing table
            wrapper.InsertRecord(categoriesGood, GetRandomString());

            // get all the categories
            var categories = wrapper.GetAllData(categoriesGood);
            PrintTable(categories);

            wrapper.InsertCustomer("dmcgrath");
            var customers = wrapper.GetAllData("customers");
            PrintTable(customers);
            //wrapper.InsertRecord(categoriesBad, GetRandomString());

            // pause execution
            Console.ReadLine();
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
