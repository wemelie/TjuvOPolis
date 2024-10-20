using static TjuvOPolis1.City;

namespace TjuvOPolis1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            City city = new City(15, 20, 25); 
            city.StartSimulation();
        }
        public void DrawSquare()
            
        {
            int width = 100; 
            int height = 25;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        Console.Write("|"); // Rita kanten
                    }
                    else
                    {
                        Console.Write("  "); // Insidan
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
  
        

    


