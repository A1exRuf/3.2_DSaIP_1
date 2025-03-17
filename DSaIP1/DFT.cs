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

    public static Complex[] GenerateComplexSineWave(int periods)
    {
        const int length = 32;
        Complex[] signal = new Complex[length];

        for (int i = 0; i < length; i++)
        {
            double angle = 2 * Math.PI * periods * i / length;
            signal[i] = new Complex(Math.Sin(angle), 0.0);
        }

        return signal;
    }

    public static Complex[] GenerateComplexSquareWave(int periods)
    {
        const int length = 32;
        Complex[] signal = new Complex[length];
        int samplesPerHalfPeriod = length / (periods * 2);

        for (int i = 0; i < length; i++)
        {
            int phase = (i / samplesPerHalfPeriod) % 2;
            double value = phase == 0 ? 1.0 : -1.0;
            signal[i] = new Complex(value, 0.0);
        }

        return signal;
    }

    public static Complex[] GenerateComplexUnitStep(int offset)
    {
        const int length = 32;
        Complex[] signal = new Complex[length];

        for (int i = 0; i < length; i++)
        {
            double value = i >= offset ? 1.0 : 0.0;
            signal[i] = new Complex(value, 0.0);
        }

        return signal;
    }

    public static Complex[] ZeroDCComponent(Complex[] spectrum)
    {
        Complex[] modified = (Complex[])spectrum.Clone();
        modified[0] = Complex.Zero;
        return modified;
    }

    public static Complex[] ZeroFirstTwoHarmonics(Complex[] spectrum)
    {
        Complex[] modified = (Complex[])spectrum.Clone();
        if (modified.Length > 1) modified[1] = Complex.Zero;
        if (modified.Length > 2) modified[2] = Complex.Zero;
        return modified;
    }

    public static Complex[] ShiftSpectrum(Complex[] spectrum, int shift)
    {
        int N = spectrum.Length;
        Complex[] shifted = new Complex[N];

        for (int i = 0; i < N; i++)
        {
            int newPos = (i + shift) % N;
            if (newPos < 0) newPos += N;
            shifted[newPos] = spectrum[i];
        }

        return shifted;
    }

}