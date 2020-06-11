namespace ScottPlot.Config
{
    public interface ITickSpacingStrategy
    {
        double GetTickSpacing(double low, double high, int maxTickCount);
    }
}
