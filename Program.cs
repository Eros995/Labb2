
using Labb2;
// Eros Alem Labb2
class program
{
    static void Main(string[] args)
    {
        string filename = "Level1.txt";
        if(args.Length > 0) filename = args[0];

        try
        {
            GameLoop.Run(filename);
        }
        catch (Exception ex)
        {
            Console.CursorVisible = true;
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
