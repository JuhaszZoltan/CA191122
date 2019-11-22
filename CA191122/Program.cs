using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CA191122
{
    class Program
    {
        static MySqlConnection conn = new MySqlConnection(
            "Server=winsql.vereb.dc;" +
            "Database=diak90;" +
            "Uid=diak90;" +
            "Pwd=2JtORd;");
        static void Main(string[] args)
        {
            //Teszt();
            //Feltolt();
            //Kiir();
            Drop();
            Console.ReadKey();
        }

        private static void Drop()
        {
            conn.Open();
            var adapter = new MySqlDataAdapter();
            adapter.DeleteCommand = new MySqlCommand("DROP TABLE filmek", conn);
            adapter.DeleteCommand.ExecuteNonQuery();
            conn.Close();
        }

        private static void Kiir()
        {
            conn.Open();

            var sql = new MySqlCommand("SELECT * FROM filmek", conn);
            var reader = sql.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}\t{reader[4]}");
            }

            conn.Close();
        }

        private static void Feltolt()
        {
            var sr = new StreamReader("lopott_adatok.txt", Encoding.UTF8);
            conn.Open();
            string[] os = sr.ReadLine().Split('\t');
            var sql = $"INSERT INTO filmek ({os[1]}, {os[2]}, {os[3]}, {os[4]}) VALUES ";
            
            while (!sr.EndOfStream)
            {
                var t = sr.ReadLine().Split('\t');
                sql += $"('{t[1].Replace("\'", "\'\'")}', '{t[2]}', {t[3]}, {t[4]}), ";
            }

            sql = sql.TrimEnd(',', ' ') + ';';

            //Console.WriteLine(sql);

            var adapter = new MySqlDataAdapter();
            adapter.InsertCommand = new MySqlCommand(sql, conn);
            adapter.InsertCommand.ExecuteNonQuery();

            conn.Close();
        }

        private static void Teszt()
        {
            conn.Open();

            var sql = new MySqlCommand("SHOW tables", conn);
            var reader = sql.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }

            conn.Close();
        }
    }
}
