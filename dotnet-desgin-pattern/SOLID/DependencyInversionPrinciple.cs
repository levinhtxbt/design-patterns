using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID
{
    public class DependencyInversionPrinciple
    {
        public enum Relationship
        {
            Parent,
            Child,
            Sibling
        }

        public class Person
        {
            public string Name { get; set; }
            // other info
            public Person(string name)
            {
                Name = name;
            }
        }

        // Khai báo 1 interface với phương thức tìm children 
        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        // Kế thừa interface IRelationshipBrowser và implement phương thức FindAllChildrenOf
        public class Relationships : IRelationshipBrowser
        {
            private List<(Person, Relationship, Person)> relations = 
                new List<(Person, Relationship, Person)>();

            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            public List<(Person, Relationship, Person)> Relations => relations;

            // Implement phương thức của interface. 
            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                foreach (var item in relations
                    .Where(x => x.Item1.Name.Equals("John") &&
                                x.Item2 == Relationship.Parent))
                {
                    yield return item.Item3;
                }
            }
        }

        
        public class Research
        {
            //public Research(Relationships relationships)
            //{
            //    foreach (var item in relationships.Relations
            //        .Where(x => x.Item1.Name.Equals("John") && 
            //                    x.Item2 == Relationship.Parent))
            //    {
            //        Console.WriteLine($"John has a child called {item.Item3.Name}");

            //    }
            //}

            // Thay vì truyền vào một class cụ thể thì chúng ta sẽ truyền vào 1 interface (abstractions)
            public Research(IRelationshipBrowser relationshipBrowser)
            {
                foreach (var children in relationshipBrowser.FindAllChildrenOf("John"))
                {
                    Console.WriteLine($"John has a child called {children.Name}");
                }
            }
            
            public static void Run()
            {
                var parent = new Person("John");
                var child1 = new Person("Chris");
                var child2 = new Person("Steve");

                var relationships = new Relationships();
                relationships.AddParentAndChild(parent, child1);
                relationships.AddParentAndChild(parent, child2);

                var research = new Research(relationships);
            }            
        }
    }
}
