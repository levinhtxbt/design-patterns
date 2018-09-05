using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.CreationalPattern
{
    #region Sample 1

    /// <summary>
    ///     Fluent Builder
    /// </summary>
    public class BuilderSample1
    {
        public class HtmlElement
        {
            public string Name { get; set; }

            public string Text { get; set; }

            public List<HtmlElement> Elements { get; set; }
                = new List<HtmlElement>();

            private int _indentSize = 2;

            public HtmlElement()
            {
            }

            public HtmlElement(string name, string text)
            {
                Name = name;
                Text = text;
            }

            public string ToStringImp(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', _indentSize * indent);

                sb.AppendLine($"{i}<{Name}>");

                if (!string.IsNullOrEmpty(Text))
                {
                    sb.Append(new string(' ', _indentSize * (indent + 1)));
                    sb.AppendLine(Text);
                }

                foreach (var htmlElement in Elements)
                {
                    sb.Append(htmlElement.ToStringImp(indent + 1));
                }

                sb.AppendLine($"{i}</{Name}>");

                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImp(0);
            }
        }

        public class HtmlBuilder
        {
            private readonly string rootName;

            private HtmlElement root = new HtmlElement();

            public HtmlBuilder(string name)
            {
                rootName = name;
                root.Name = name;
            }

            public HtmlBuilder AddChild(string name, string text)
            {
                var element = new HtmlElement(name, text);
                root.Elements.Add(element);
                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement() { Name = rootName };
            }
        }

        public static void Execute()

        {
            List<string> greeting = new List<string>() { "Hello", "World" };
            var str = new StringBuilder();

            str.AppendLine("<ul>");
            foreach (var word in greeting)
            {
                str.AppendLine("<ul>")
                    .AppendFormat("<li>{0}</li>", word)
                    .AppendLine()
                    .AppendLine("</ul>");
            }
            str.AppendLine("</ul>");

            Console.WriteLine(str);

            var htmlBuilder = new HtmlBuilder("ul");

            htmlBuilder.AddChild("li", "Hello").AddChild("li", "World");

            Console.WriteLine(htmlBuilder.ToString());
        }
    }

    #endregion Sample 1

    #region Sample 2

    /// <summary>
    ///     Fluent Builder Inheritance
    /// </summary>
    public class BuilderSample2
    {
        public class Person
        {
            public string Name { get; set; }

            public string Position { get; set; }

            public DateTime DOB { get; set; }

            public override string ToString()
            {
                return $"Name: {Name} - Position: {Position} - DOB:{DOB.ToShortDateString()}";
            }

            public class Builder : PersonDOBBuilder<Builder>
            {
            }

            public static Builder New => new Builder();
        }

        public abstract class PersonBuilder
        {
            protected Person person = new Person();

            public Person Build()
            {
                return person;
            }
        }

        public class PersonInfoBuilder<SELF> : PersonBuilder where SELF : PersonInfoBuilder<SELF>
        {
            public SELF Called(string name)
            {
                person.Name = name;
                return (SELF)this;
            }
        }

        public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> where SELF : PersonJobBuilder<SELF>
        {
            public SELF WorkAsA(string position)
            {
                person.Position = position;
                return (SELF)this;
            }
        }

        public class PersonDOBBuilder<SELF> : PersonJobBuilder<PersonDOBBuilder<SELF>> where SELF : PersonDOBBuilder<SELF>
        {
            public SELF DOB(DateTime DOB)
            {
                person.DOB = DOB;
                return (SELF)this;
            }
        }

        public static void Execute()
        {
            //var personBuilder = new PersonDOBBuilder<>(); Không thể khai báo builder kiểu này

            var person = Person.New
                .Called("Vinh")
                .WorkAsA("Dev")
                .DOB(DateTime.UtcNow)
                .Build();

            Console.WriteLine(person);
        }
    }

    #endregion Sample 2

    #region Builder pattern

    /// <summary>
    /// MainApp startup class for Real-World
    /// Builder Design Pattern.
    /// </summary>
    public class MainApp

    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        public static void Execute()
        {
            VehicleBuilder builder;

            // Create shop with vehicle builders
            Shop shop = new Shop();

            // Construct and display vehicles
            builder = new ScooterBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new CarBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new MotorCycleBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            // Wait for user

            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Director' class
    /// </summary>
    internal class Shop
    {
        // Builder uses a complex series of steps
        public void Construct(VehicleBuilder vehicleBuilder)
        {
            vehicleBuilder.BuildFrame()
                .BuildEngine()
                .BuildWheels()
                .BuildDoors();
        }
    }

    /// <summary>
    /// The 'Builder' abstract class
    /// </summary>
    internal abstract class VehicleBuilder
    {
        protected Vehicle vehicle;

        // Gets vehicle instance
        public Vehicle Vehicle
        {
            get { return vehicle; }
        }

        // Abstract build methods
        public abstract VehicleBuilder BuildFrame();

        public abstract VehicleBuilder BuildEngine();

        public abstract VehicleBuilder BuildWheels();

        public abstract VehicleBuilder BuildDoors();
    }

    /// <summary>
    /// The 'ConcreteBuilder1' class
    /// </summary>
    internal class MotorCycleBuilder : VehicleBuilder
    {
        public MotorCycleBuilder()
        {
            vehicle = new Vehicle("MotorCycle");
        }

        public override VehicleBuilder BuildFrame()
        {
            vehicle["frame"] = "MotorCycle Frame";
            return this;
        }

        public override VehicleBuilder BuildEngine()
        {
            vehicle["engine"] = "500 cc";
            return this;
        }

        public override VehicleBuilder BuildWheels()
        {
            vehicle["wheels"] = "2";
            return this;
        }

        public override VehicleBuilder BuildDoors()
        {
            vehicle["doors"] = "0";
            return this;
        }
    }

    /// <summary>
    /// The 'ConcreteBuilder2' class
    /// </summary>
    internal class CarBuilder : VehicleBuilder
    {
        public CarBuilder()
        {
            vehicle = new Vehicle("Car");
        }

        public override VehicleBuilder BuildFrame()
        {
            vehicle["frame"] = "Car Frame";
            return this;
        }

        public override VehicleBuilder BuildEngine()
        {
            vehicle["engine"] = "2500 cc";
            return this;
        }

        public override VehicleBuilder BuildWheels()
        {
            vehicle["wheels"] = "4";
            return this;
        }

        public override VehicleBuilder BuildDoors()
        {
            vehicle["doors"] = "4";
            return this;
        }
    }

    /// <summary>
    /// The 'ConcreteBuilder3' class
    /// </summary>
    internal class ScooterBuilder : VehicleBuilder
    {
        public ScooterBuilder()
        {
            vehicle = new Vehicle("Scooter");
        }

        public override VehicleBuilder BuildFrame()
        {
            vehicle["frame"] = "Scooter Frame";
            return this;
        }

        public override VehicleBuilder BuildEngine()
        {
            vehicle["engine"] = "50 cc";
            return this;
        }

        public override VehicleBuilder BuildWheels()
        {
            vehicle["wheels"] = "2";
            return this;
        }

        public override VehicleBuilder BuildDoors()
        {
            vehicle["doors"] = "0";
            return this;
        }
    }

    /// <summary>
    /// The 'Product' class
    /// </summary>
    internal class Vehicle
    {
        private string _vehicleType;

        private Dictionary<string, string> _parts =
            new Dictionary<string, string>();

        // Constructor
        public Vehicle(string vehicleType)
        {
            this._vehicleType = vehicleType;
        }

        // Indexer
        public string this[string key]
        {
            get { return _parts[key]; }
            set { _parts[key] = value; }
        }

        public void Show()
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Vehicle Type: {0}", _vehicleType);
            Console.WriteLine(" Frame : {0}", _parts["frame"]);
            Console.WriteLine(" Engine : {0}", _parts["engine"]);
            Console.WriteLine(" #Wheels: {0}", _parts["wheels"]);
            Console.WriteLine(" #Doors : {0}", _parts["doors"]);
        }
    }

    #endregion Builder pattern
}