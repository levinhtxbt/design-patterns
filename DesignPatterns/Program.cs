using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.SOLID;

namespace DesignPatterns
{


    class Program
    {
        static void Main(string[] args)
        {

            var openClosedPrinciple = new OpenClosedPrinciple.AppyPrinciple();
            openClosedPrinciple.Execute();


            Console.ReadKey();

            
        }


    }

    
}

