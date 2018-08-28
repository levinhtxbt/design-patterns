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

            //var openClosedPrinciple = new OpenClosedPrinciple.AppyPrinciple();
            //openClosedPrinciple.Execute();

            //var liskovSubstitutionPrinciple = new LiskovSubstitutionPrinciple();
            //liskovSubstitutionPrinciple.Excute();

            DependencyInversionPrinciple.Research.Run();



            Console.ReadKey();

            
        }


    }

    
}


