using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cassandraJSON;
using Cassandra;

namespace cassandraJSON
{
    class Program
    {


        static void Main(string[] args)
        {


            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("dblp");

            //Row result = session.Execute("SELECT * FROM \"DBLP\"").First();
            //Console.WriteLine("{0}   {1}", result["title"], result["type"]);


            string[] lines = File.ReadAllLines(@"dblp_elastic.json");

            int counter = 1;
            foreach (string line in lines)
            {

                if (counter % 2 == 0)
                {
                    //Console.WriteLine(line);
                    dynamic array = JsonConvert.DeserializeObject(line);
                    Console.WriteLine(array);

                    try
                    {
                       session.Execute("insert into \"DBLP\" JSON '" + array + "';");
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                    
                }
                counter++;

            }



            Console.ReadKey();
        }
    }
}
