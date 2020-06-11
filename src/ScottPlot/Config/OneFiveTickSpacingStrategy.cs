using System;
using System.Collections.Generic;
using System.Linq;

namespace ScottPlot.Config
{
    public class OneFiveTickSpacingStrategy : ITickSpacingStrategy
    {
        public static OneFiveTickSpacingStrategy Instance { get; } = new OneFiveTickSpacingStrategy();

        OneFiveTickSpacingStrategy()
        {
        }

        public double GetTickSpacing(double low, double high, int maxTickCount)
        {
            double range = high - low;
            int exponent = (int)Math.Log10(range);
            List<double> tickSpacings = new List<double> {Math.Pow(10, exponent)};
            tickSpacings.Add(tickSpacings.Last());
            tickSpacings.Add(tickSpacings.Last());

            int divisions = 0;
            double[] divBy = {2, 5}; // dividing from 10 yields 5 and 1.

            while (true)
            {
                tickSpacings.Add(tickSpacings.Last() / divBy[divisions++ % divBy.Length]);
                int tickCount = (int)(range / tickSpacings.Last());
                if ((tickCount > maxTickCount) || (tickSpacings.Count > 1000))
                    break;
            }

            return tickSpacings[tickSpacings.Count - 3];
        }
    }
}
