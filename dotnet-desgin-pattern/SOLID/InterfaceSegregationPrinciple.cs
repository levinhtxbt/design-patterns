using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID
{
    public class InterfaceSegregationPrinciple
    {
        public interface IMachine
        {
            void Print();

            void Scan();

            void Fax();
        }

        public class MultipleFunctionPrinter : IMachine
        {
            public void Print()
            {
                // print
            }

            public void Scan()
            {
                // scan
            }

            public void Fax()
            {
                // fax
            }
        }

        public class OldFashionedPrinter : IMachine
        {
            public void Print()
            {
                // print
            }

            public void Scan()
            {
                // scan
            }

            public void Fax()
            {
                throw new NotImplementedException();
            }
        }

        public interface IPrinter
        {
            void Print();
        }

        public interface IScanner
        {
            void Scan();
        }

        public class Photocopier : IPrinter, IScanner
        {
            public void Print()
            {
                // print
            }

            public void Scan()
            {
                // scan
            }
        }
    }
}
