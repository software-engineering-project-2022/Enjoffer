namespace ConsoleApp
{
    class Program
    {

        static void Main()
        {
            var manipulate = new ManipulationDB("Host=localhost;Username=postgres;Password=yari2233;Database=enjoffer");

            while (true)
            {
                try
                {
                    Console.WriteLine("1. Insert data tables");
                    Console.WriteLine("2. Print data");
                    Console.WriteLine("3. Delete data");
                    Console.WriteLine("\n");

                    Console.Write("Enter number of task:\t");

                    string num = Console.ReadLine();

                    if (num == "1")
                    {
                        Console.Write("Enter number of records:\t");
                        int n = int.Parse(Console.ReadLine());
                        manipulate.TablesInsertion(n);
                        Console.WriteLine("Insertion is done successfully\n");
                    }
                    else if (num == "2")
                    {
                        manipulate.Print();
                    }
                    else if (num == "3")
                    {
                        manipulate.TablesDeletion();
                        Console.WriteLine("Deletion is done successfully\n");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}