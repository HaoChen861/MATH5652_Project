using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace WpfApp5
{
    public class MainViewModel
    {
        public PlotModel MModel { get; set; }

        public MainViewModel(List<double> ArrivalTimeList, List<int> ArrivalGroupList, int CIU, int CIM)
        {
            MModel = new PlotModel()
            {
                LegendPosition = LegendPosition.RightBottom,
                LegendBorder = OxyColors.Black,
                LegendTitle = "Legend",
                

                
            };
            //Number of people infected
            int Number = ArrivalTimeList.Count();
            //Series Unmask
            var SU = new ScatterSeries()
            {
                MarkerType = MarkerType.Diamond,
                MarkerStrokeThickness = 0,
                Title = "Number of Unmasked People Infected"
                
            };
            //Series Masked
            var SM = new ScatterSeries()
            {
                MarkerType = MarkerType.Diamond,
                MarkerStrokeThickness = 0,
                Title = "Number of Masked People Infected"

            };



            for (int i =0; i < Number; i++)
            {
                if (ArrivalGroupList[i] == 0)
                {
                    CIU += 1;
                    SU.Points.Add(new ScatterPoint(ArrivalTimeList[i], CIU));

                }
                else
                {
                    CIM += 1;
                    SM.Points.Add(new ScatterPoint(ArrivalTimeList[i], CIM));
                }
            }

            MModel.Series.Add(SU);
            MModel.Series.Add(SM);

        }


    }
}
