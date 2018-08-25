using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID
{
    public class LiskovSubstitutionPrinciple
    {
        public class Rectangle
        {
            private double _width;

            private double _height;

            public virtual double Width
            {
                set => _width = value;
            }

            public virtual double Height
            {
                set => _height = value;
            }

            public double Area()
            {
                return _width * _height;
            }
        }

        public class Square : Rectangle
        {
            public override double Width
            {
                set { base.Width = base.Height = value; }
            }

            public override double Height
            {
                set { base.Height = base.Width = value; }
            }
        }

        public void Excute()
        {
            Console.Write("Rectangle: ");
            Rectangle r = new Rectangle();
            g(r);
            Console.WriteLine($"area {r.Area()}");

            Console.Write("Square -> ");
            Rectangle square = new Square();
            g(square);
            Console.WriteLine($"area {square.Area()}");
        }

        public void UpdateDimesion(Rectangle r)
        {
            r.Width = 25;
            r.Height = 10;
        }

        public void g(Rectangle r)
        {
            r.Width = 5;
            r.Height = 4;
            if (r.Area() != 20)
                throw new Exception("Bad area!");
        }
    }
}
