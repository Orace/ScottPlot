using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ScottPlot.Config;

namespace ScottPlot.Demo.WPF.WpfDemos
{
    /// <summary>
    /// Interaction logic for TickSpacingStrategy.xaml
    /// </summary>
    public partial class TickSpacingStrategy : Window
    {
        Random rand = new Random();
        double[] liveData = DataGen.Sin(100, oscillations: 2, mult: 20);

        public TickSpacingStrategy()
        {
            InitializeComponent();
            wpfPlot1.Configure(middleClickMarginX: 0);

            // plot the data array only once
            wpfPlot1.plt.PlotSignal(liveData);
            wpfPlot1.plt.Axis(y1: -50, y2: 50);
            wpfPlot1.Render();

            // create a timer to modify the data
            DispatcherTimer updateDataTimer = new DispatcherTimer();
            updateDataTimer.Interval = TimeSpan.FromMilliseconds(1);
            updateDataTimer.Tick += UpdateData;
            updateDataTimer.Start();

            // create a timer to update the GUI
            DispatcherTimer renderTimer = new DispatcherTimer();
            renderTimer.Interval = TimeSpan.FromMilliseconds(20);
            renderTimer.Tick += Render;
            renderTimer.Start();
        }

        void UpdateData(object sender, EventArgs e)
        {
            for (int i = 0; i < liveData.Length; i++)
                liveData[i] += rand.NextDouble() - .5;
        }

        void Render(object sender, EventArgs e)
        {
            wpfPlot1.Render(skipIfCurrentlyRendering: true);
        }

        private void XStrategy(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0 || wpfPlot1 == null)
                return;

            switch ((e.AddedItems[0] as ComboBoxItem)?.Content)
            {
                case "OneFive":
                    wpfPlot1.plt.Ticks(tickSpacingStrategyX: OneFiveTickSpacingStrategy.Instance);
                    break;
                default:
                    wpfPlot1.plt.Ticks(tickSpacingStrategyX: DefaultTickSpacingStrategy.Instance);
                    break;
            }

            wpfPlot1.Render();
        }

        private void YStrategy(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0 || wpfPlot1 == null)
                return;

            switch ((e.AddedItems[0] as ComboBoxItem)?.Content)
            {
                case "OneFive":
                    wpfPlot1.plt.Ticks(tickSpacingStrategyY: OneFiveTickSpacingStrategy.Instance);
                    break;
                default:
                    wpfPlot1.plt.Ticks(tickSpacingStrategyY: DefaultTickSpacingStrategy.Instance);
                    break;
            }

            wpfPlot1.Render();
        }
    }
}
