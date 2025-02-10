using System.Numerics;

namespace DSaIP1;

class Program
{
    static void Main()
    {
        Complex[] data = new Complex[]
        {
            1, -1, 4, 2, -2, 3, 5, -3,
            6, -4, 7, -5, 8, -6, 9, -7,
            10, -8, 11, -9, 12, -10, 13, -11,
            14, -12, 15, -13, 16, -14, 17, -15
        };

        Complex[] spectrum = DFT.ProcessDFT(data);
        Console.WriteLine("ДПФ:");
        for (int k = 0; k < spectrum.Length; k++)
        {
            Console.WriteLine($"S[{k}] = {spectrum[k]}");
        }

        double[] restored = DFT.ProcessIDFT(spectrum);
        Console.WriteLine("\nОДПФ:");
        for (int n = 0; n < restored.Length; n++)
        {
            Console.WriteLine($"S[{n}] = {restored[n]}");
        }
    }
}