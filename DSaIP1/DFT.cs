using System.Collections.Generic;
using System.Numerics;

namespace DSaIP1;

internal class DFT
{
    public static Complex[] ProcessDFT(Complex[] input)
    {
        int N = input.Length;
        Complex[] output = new Complex[N];
        for (int k = 0; k < N; k++)
        {
            Complex sum = Complex.Zero;
            for (int n = 0; n < N; n++)
            {
                double angle = -2 * Math.PI * k * n / N;
                sum += input[n] * Complex.Exp(new Complex(0, angle));
            }
            output[k] = new Complex(Math.Round(sum.Real, 2), Math.Round(sum.Imaginary, 2));
        }
        return output;
    }

    public static double[] ProcessIDFT(Complex[] input)
    {
        int N = input.Length;
        double[] output = new double[N];
        for (int n = 0; n < N; n++)
        {
            Complex sum = Complex.Zero;
            for (int k = 0; k < N; k++)
            {
                double angle = 2 * Math.PI * k * n / N;
                sum += input[k] * Complex.Exp(new Complex(0, angle));
            }
            output[n] = Math.Round(sum.Real / N, 2);
        }
        return output;
    }

    public static Complex[] GetAmplitude(Complex[] input)
    {
        int N = input.Length;
        Complex[] output = new Complex[N];
        for (int n = 0; n < N; n++)
        {
            double real = Math.Abs(input[n].Real);
            double imaginary = Math.Abs(input[n].Imaginary);
            output[n] = new Complex(real, imaginary);
        }
        return output;
    }

    public static List<double> GetPhase(Complex[] input)
    {
        int N = input.Length;
        List<double> output = [];
        for (int n = 0; n < N; n++)
        {
            output.Add(Math.Round(Math.Atan(input[n].Imaginary / input[n].Real), 2));
        }
        return output;
    }
}