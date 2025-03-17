using System.Numerics;

namespace DSaIP1;

class Program
{
    static void Main()
    {
        Complex[] data = new Complex[]
        {
            1, -1, 4, 2, -2, 3
        };

        while (true)
        {
            Console.WriteLine("1. ДПФ И ОДПФ\n" +
                "2. Синус\n" +
                "3. Прямоугольная последовательность меандрового типа\n" +
                "4. Cмещенный единичный скачок\n" +
                "5. Демонстрация модификаций спектра\n");

            string option = Console.ReadLine();
            switch(option)
            {
                case "1":
                    ProcessDFTAndIDFT(data);
                    break;

                case "2":
                    GenerateAndAnalyzeSignal("Синус", DFT.GenerateComplexSineWave(2));
                    break;

                case "3":
                    GenerateAndAnalyzeSignal("Прямоугольная последовательность меандрового типа", DFT.GenerateComplexSquareWave(4));
                    break;

                case "4":
                    GenerateAndAnalyzeSignal("Cмещенный единичный скачок", DFT.GenerateComplexUnitStep(10));
                    break;

                case "5":
                    DemonstrateSpectrumModifications();
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;

            }
        }
    }

    private static void ProcessDFTAndIDFT(Complex[] data)
    {
        Complex[] spectrum = DFT.ProcessDFT(data);
        Console.WriteLine("ДПФ:");
        for (int n = 0; n < spectrum.Length; n++)
        {
            Console.WriteLine($"S[{n}] = {spectrum[n]}");
        }

        double[] restored = DFT.ProcessIDFT(spectrum);
        Console.WriteLine("\nОДПФ:");
        for (int n = 0; n < restored.Length; n++)
        {
            Console.WriteLine($"S[{n}] = {restored[n]}");
        }

        Complex[] amplitude = DFT.GetAmplitude(spectrum);
        Console.WriteLine("\nАмплитуда:");
        for (int n = 0; n < amplitude.Length; n++)
        {
            Console.WriteLine($"S[{n}] = {amplitude[n]}");
        }

        List<double> phase = DFT.GetPhase(spectrum);
        Console.WriteLine("\nФаза:");
        for (int n = 0; n < amplitude.Length; n++)
        {
            Console.WriteLine($"S[{n}] = {phase[n]}");
        }
    }

    private static void GenerateAndAnalyzeSignal(string signalName, Complex[] signal)
    {
        Console.WriteLine($"{signalName}:");
        PrintComplexArray(signal);

        Complex[] amplitude = DFT.GetAmplitude(signal);
        Console.WriteLine("\nАмплитуда:");
        PrintComplexArray(amplitude);

        List<double> phase = DFT.GetPhase(signal);
        Console.WriteLine("\nФаза:");
        PrintDoubleList(phase);
    }

    private static void DemonstrateSpectrumModifications()
    {
        var signals = new[]
        {
            new {
                Name = "Синус (2 периода)",
                Signal = DFT.GenerateComplexSineWave(2)
            },
            new {
                Name = "Меандр (4 периода)",
                Signal = DFT.GenerateComplexSquareWave(4)
            },
            new {
                Name = "Скачок (offset=10)",
                Signal = DFT.GenerateComplexUnitStep(10)
            }
        };

        foreach (var signal in signals)
        {
            Console.WriteLine($"\n=== {signal.Name} ===");

            Complex[] spectrum = DFT.ProcessDFT(signal.Signal);

            var modifications = new[]
            {
                new {
                    Name = "Исходный сигнал",
                    Spectrum = spectrum
                },
                new {
                    Name = "Без постоянной составляющей",
                    Spectrum = DFT.ZeroDCComponent(spectrum)
                },
                new {
                    Name = "Без первых двух гармоник",
                    Spectrum = DFT.ZeroFirstTwoHarmonics(spectrum)
                },
                new {
                    Name = "Сдвиг спектра на 3",
                    Spectrum = DFT.ShiftSpectrum(spectrum, 3)
                }
            };

            foreach (var mod in modifications)
            {
                Console.WriteLine($"\n{mod.Name}:");
                double[] restored = DFT.ProcessIDFT(mod.Spectrum);
                Console.WriteLine(string.Join(" ",
                    restored.Select(x => Math.Round(x, 2).ToString("F2"))));
            }
        }
    }

    private static void PrintComplexArray(Complex[] array)
    {
        for (int n = 0; n < array.Length; n++)
        {
            Console.WriteLine($"S[{n}] = {array[n]}");
        }
    }

    private static void PrintDoubleList(List<double> list)
    {
        for (int n = 0; n < list.Count; n++)
        {
            Console.WriteLine($"S[{n}] = {list[n]}");
        }
    }
}