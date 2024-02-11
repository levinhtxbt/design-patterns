using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SOLID
{
    // don't apply 
    public class OpenClosedPrinciple
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Huge
        }

        public class Product
        {
            public string Name { get; set; }

            public Color Color { get; set; }

            public Size Size { get; set; }

            public decimal Price { get; set; }

            public Product(string name, Color color, Size size, decimal price)
            {
                Name = name;
                Color = color;
                Size = size;
                Price = price;
            }
        }

        public class DontApplyPrinciple
        {
            public class ProductFilter
            {
                public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
                {
                    foreach (var product in products)
                        if (product.Color == color)
                            yield return product;
                }

                public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
                {
                    foreach (var product in products)
                        if (product.Size == size)
                            yield return product;
                }
            }

            public void Execute()
            {
                var products = new List<Product>
                {
                    new Product("House", Color.Blue, Size.Large, 150),
                    new Product("House", Color.Red, Size.Large, 100),
                    new Product("Tree", Color.Green, Size.Medium, 5),
                    new Product("Building", Color.Red, Size.Huge, 500)
                };

                var pf = new ProductFilter();
                var newProducts = pf.FilterByColor(products, Color.Red);

                Console.WriteLine("Product (old)");
                PrintProduct(products);
                Console.WriteLine("Product (new)");
                PrintProduct(newProducts);
            }

        }

        public class AppyPrinciple
        {
            public interface ISpecification<T>
            {
                bool IsSatisfiedBy(T t);
            }

            public interface IFilter<T>
            {
                IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
            }

            public class ProductFilter : IFilter<Product>
            {
                public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
                {
                    foreach (var product in items)
                        if (spec.IsSatisfiedBy(product))
                            yield return product;
                }
            }

            public class ColorSpecification : ISpecification<Product>
            {
                private readonly Color _color;
                public ColorSpecification(Color color)
                {
                    _color = color;
                }

                public bool IsSatisfiedBy(Product t)
                {
                    return _color == t.Color;
                }
            }

            public class SizeSpecification : ISpecification<Product>
            {
                private readonly Size _size;

                public SizeSpecification(Size size)
                {
                    _size = size;
                }

                public bool IsSatisfiedBy(Product t)
                {
                    return _size == t.Size;
                }
            }

            public class PriceSpecification : ISpecification<Product>
            {
                private readonly decimal _minPrice;
                private readonly decimal _maxPrice;

                public PriceSpecification(decimal minPrice, decimal maxPrice)
                {
                    _minPrice = minPrice;
                    _maxPrice = maxPrice;
                }

                public bool IsSatisfiedBy(Product t)
                {
                    return _minPrice <= t.Price && t.Price <= _maxPrice;
                }
            }

            public class AndSpericification : ISpecification<Product>
            {
                private readonly IEnumerable<ISpecification<Product>> _specifications;

                public AndSpericification(IEnumerable<ISpecification<Product>> specifications)
                {
                    _specifications = specifications ?? throw new ArgumentNullException();
                }

                public bool IsSatisfiedBy(Product t)
                {
                    return _specifications.All(x => x.IsSatisfiedBy(t));
                }
            }

            public class OrSpericification : ISpecification<Product>
            {
                private readonly IEnumerable<ISpecification<Product>> _specifications;

                public OrSpericification(IEnumerable<ISpecification<Product>> specifications)
                {
                    _specifications = specifications ?? throw new ArgumentNullException();
                }

                public bool IsSatisfiedBy(Product t)
                {
                    return _specifications.Any(x => x.IsSatisfiedBy(t));
                }
            }

            public void Execute()
            {
                var products = new List<Product>
                {
                    new Product("House", Color.Blue, Size.Large, 150),
                    new Product("House", Color.Red, Size.Large, 100),
                    new Product("Tree", Color.Green, Size.Medium, 5),
                    new Product("Building", Color.Red, Size.Huge, 500)
                };
                Console.WriteLine("Product (old)");
                PrintProduct(products);

                Console.WriteLine("==============================================");
                Console.WriteLine("=          Single condition filter           =");
                Console.WriteLine("==============================================");
                var pf = new ProductFilter();
                var redColorFilter = new ColorSpecification(Color.Red);
                var newProducts = pf.Filter(products, redColorFilter);

                Console.WriteLine("Product (new)");
                PrintProduct(newProducts);

                Console.WriteLine("==============================================");
                Console.WriteLine("=         Multiple condition filter          =");
                Console.WriteLine("==============================================");

                var listOfFilter = new List<ISpecification<Product>>()
                {
                    new PriceSpecification(0, 150),
                    new ColorSpecification(Color.Green)
                };
                var andFilter = new OrSpericification(listOfFilter);
                var andFilterProducts = pf.Filter(products, andFilter);

                Console.WriteLine("Product (new)");
                PrintProduct(andFilterProducts);
            }
        }

        private static void PrintProduct(IEnumerable<Product> products)
        {
            foreach (var product in products)
                Console.WriteLine($"\t{product.Name} - Color: {product.Color} - Size: {product.Size} - Price: {product.Price}");
        }
    }

}
