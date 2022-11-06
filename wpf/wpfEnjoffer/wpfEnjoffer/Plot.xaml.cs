using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScottPlot;

namespace wpfEnjoffer
{
    /// <summary>
    /// Interaction logic for Plot.xaml
    /// </summary>
    public partial class Plot : Page
    {
        public Plot()
        {
            InitializeComponent();
            var plt = new ScottPlot.Plot(600, 400);

            // create sample data
            double[] valuesA = { 1, 2, 3, 2, 1, 2, 1 };
            double[] valuesB = { 3, 3, 2, 1, 3, 2, 1 };

            // to simulate stacking B on A, shift B up by A
            double[] valuesB2 = new double[valuesB.Length];
            for (int i = 0; i < valuesB.Length; i++)
                valuesB2[i] = valuesA[i] + valuesB[i];

            // plot the bar charts in reverse order (highest first)
            plt.AddBar(valuesB2);
            plt.AddBar(valuesA);

            // adjust axis limits so there is no padding below the bar graph
            plt.SetAxisLimits(yMin: 0);

            plt.SaveFig("bar_stacked.png");
        }
    }
}
