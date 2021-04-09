using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSec
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new Modello();
            SqlParameter[] pars = new SqlParameter[0];
            var utenti = ctx.Database.SqlQuery<utente>("select * from utente", pars).ToList();
            foreach(var u in utenti)
            {
                Console.WriteLine($"nome {u.nome}, cognome {u.cognome}");
            }
            /*
            var str = "Data Source=(local); Initial catalog=appsec;User id=sa;Password=passw0rd;";

            using (var conn = new SqlConnection(str))
            {

                var sql = "select * from utente";
                if (args.Length > 0)
                {
                    sql += $" WHERE nome=@parNome"; 
                }
                else
                {
                    Console.WriteLine("accesso non consentito");
                    return;
                }
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parNome", args[0]);
                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"nome: {reader["nome"]}, cognome: {reader["cognome"]}");
                    }
                    reader.Close();
                }
                catch
                { }
            }
            */
            Console.ReadLine();
            
        }
    }
}
