using System;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace Roblox_User_Searcher
{
    class Program
    {
        static void Main(string[] args)
        {

            string get(string uri)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }

            Console.WriteLine("Please enter a userid to search.");
            string toFind = Console.ReadLine();

            int test = 0;
            bool isValid = int.TryParse(toFind, out test);

            if (!isValid)
            {
                Console.WriteLine("Invalid ID.");
                Thread.Sleep(3000);
                System.Environment.Exit(1);
            }

            string url = @"https://users.roblox.com/v1/users/" + toFind;
            dynamic data = JsonConvert.DeserializeObject(get(url));
            Console.WriteLine("Name: " + data.name);
            Console.WriteLine("User description: " + data.description);
            Console.WriteLine("Creation date " + data.created);
            Console.WriteLine("Banned: " + data.isBanned);
            Console.ReadLine();
        }
    }
}
