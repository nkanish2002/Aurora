using Aurora.Models.Derived;
using Aurora.Models.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Aurora.Entity.AuroraContext()) {
                var data = context.Companies.ToList();
                Print(data, ConsoleColor.Yellow);
            }
        }
        
        static void Print(object obj, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            var jsonop = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            Console.Write("JSON processed... Write to file? (Y/N)");
            var input = Console.ReadLine();
            if (input.ToUpper() == "Y")
            {
                using (var F = new FileStream("output.json", FileMode.Create, FileAccess.ReadWrite))
                {
                    using (var S = new StreamWriter(F)) {
                        S.Write(jsonop);
                    }
                }
            }
            else {
                Console.WriteLine(jsonop);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
