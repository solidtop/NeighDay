namespace NeighDay.Server.Utils
{
    public class ColorGenerator
    {
        public static string GenerateDisplayColor()
        {
            var random = new Random();
            double hue = random.NextDouble() * 360;
            double saturation = 0.6 + random.NextDouble() * 0.4;
            double lightness = 0.5 + random.NextDouble() * 0.2;

            return HSLToHex(hue, saturation, lightness);
        }

        private static string HSLToHex(double hue, double saturation, double lightness)
        {
            double chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            double huePrime = hue / 60;
            double x = chroma * (1 - Math.Abs(huePrime % 2 - 1));

            double r1, g1, b1;
            if (huePrime >= 0 && huePrime < 1)
            {
                (r1, g1, b1) = (chroma, x, 0);
            }
            else if (huePrime >= 1 && huePrime < 2)
            {
                (r1, g1, b1) = (x, chroma, 0);
            }
            else if (huePrime >= 2 && huePrime < 3)
            {
                (r1, g1, b1) = (0, chroma, x);
            }
            else if (huePrime >= 3 && huePrime < 4)
            {
                (r1, g1, b1) = (0, x, chroma);
            }
            else if (huePrime >= 4 && huePrime < 5)
            {
                (r1, g1, b1) = (x, 0, chroma);
            }
            else
            {
                (r1, g1, b1) = (chroma, 0, x);
            }

            double m = lightness - chroma / 2;

            int r = (int)((r1 + m) * 255);
            int g = (int)((g1 + m) * 255);
            int b = (int)((b1 + m) * 255);

            return $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}
