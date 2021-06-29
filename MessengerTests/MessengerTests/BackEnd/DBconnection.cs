using System;
using Npgsql;
using System.Data;

namespace TestC.BackEndClass
{
    public class DBconnection
    {
        private NpgsqlConnection nc;

        public string ConectionString { get; private set; }


        public DBconnection(string connectionString)
        {
            ConectionString = connectionString;
            nc = new NpgsqlConnection(ConectionString);
        }

        public void Open()
        {
            nc.Open();

            if (nc.FullState == ConnectionState.Broken || nc.FullState == ConnectionState.Closed)
            {
                Console.WriteLine("crash...");
                Console.Read();
            }

            Console.WriteLine("OK!");
        }
    }
}
