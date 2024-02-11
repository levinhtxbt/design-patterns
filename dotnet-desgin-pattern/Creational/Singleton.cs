namespace DesignPatterns.CreationalPattern;

// use the sealed keyword to prevent inheritance
// because inheritance can break the singleton pattern
public sealed class Singleton
{
    // private static instance of the same class
    // it is used to store the instance of the class
    private static Singleton? instance = null;
    
    // lock object to make the thread safe
    // it is used to synchronize the threads
    private static readonly object _lock = new object();
    
    public string InitialValue { get; set; }
    

    // private constructor to restrict the instantiation of the class from outside
    // use private constructor to prevent the creation of the instance of the class from outside
    private Singleton()
    {
        Console.WriteLine("Instantiating inside the private constructor");
    }

    // public static method to return the instance of the class
    // it is used to access the instance of the class from outside
    // it is also used to create the instance of the class if it is not created
    public static Singleton GetInstance(string initialValue = "")
    {
        lock (_lock)
        {
            if (instance == null)
            {
                instance = new Singleton();
                instance.InitialValue = initialValue;
            }
            return instance;
        }
    }

    // public method to print the details
    public void PrintDetails()
    {
        Console.WriteLine(InitialValue);
    }
}