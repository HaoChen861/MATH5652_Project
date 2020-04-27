using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    class Simulation
    {
        public int NumberUnMasked { get; set; }

        public int NumberMasked { get; set; }

        public int CurrentInfUnMasked;

        public int CurrentInfMasked;

        public double Clock;

        public int MaxNumber { get; set; }

        public double MaxClock { get; set; }

        public double Rate { get; set; }

        public double ProbWithMask { get; set; }

        public List<double> ArrivalTimeList { get; set; }

        public List<int> ArrivalGroupList { get; set; }

        public bool isMasked { get; set; }

        public double ProbInf { get; set; }

        Random UniRand = new Random();

        public Simulation(int NT, int aNumberUnMasked, int aNumberMasked, int aCurrentInfUnMasked, int aCurrentInfMasked, int aMaxNumber, double aMaxClock, double aRate, double aProbWithMask,bool aIsMask)
        {
            NumberUnMasked = aNumberUnMasked;
            NumberMasked = aNumberMasked;

            MaxNumber = aMaxNumber;
            MaxClock = aMaxClock;
            Rate = aRate;
            ProbWithMask = aProbWithMask;
            isMasked = aIsMask;

            ProbInf = 0;

            for (int i =0; i < NT; i++)
            {
                Clock = 0;
                CurrentInfUnMasked = aCurrentInfUnMasked;
                CurrentInfMasked = aCurrentInfMasked;
                while (Clock < MaxClock && (CurrentInfMasked + CurrentInfUnMasked) < MaxNumber)
                {
                    Spreads();
                }

                if (isMasked)
                {
                    ProbInf += CurrentInfMasked / NumberMasked;
                }
                else
                {
                    ProbInf += CurrentInfUnMasked / NumberUnMasked;
                }

            }

            ProbInf = ProbInf / NT;
        }


        /// <summary>
        /// Note: Generate based on mean, not lambda
        /// </summary>
        /// <param name="mean"></param>
        /// <returns></returns>
        public double GetExpRand(double mean)
        {
            return - mean * Math.Log(1 - UniRand.NextDouble());
        }


        private void Spreads()
        {
            double NextMask = GenerateMaskedArrival();
            double NextUnMask = GenerateUnMaskedArrival();

            if (NextMask < NextUnMask)
            {
                HandleMaskedArrival(NextMask);
            }
            else
            {
                HandleUnMaskedArrival(NextUnMask);
            }

        }

        private void HandleUnMaskedArrival(double NextUnMask)
        {
           if (Clock < MaxClock &  CurrentInfUnMasked < NumberUnMasked)
            {
                CurrentInfUnMasked += 1;
                Clock += NextUnMask;
                ArrivalTimeList.Add(Clock);
                ArrivalGroupList.Add(0);
            }
        }

        private void HandleMaskedArrival(double NextMask)
        {
            if (Clock < MaxClock & CurrentInfMasked < NumberMasked)
            {
                CurrentInfMasked += 1;
                Clock += NextMask;
                ArrivalTimeList.Add(Clock);
                ArrivalGroupList.Add(1);
            }
        }


        private double GenerateUnMaskedArrival()
        {
            double MeanUnMasked;
            if (CurrentInfUnMasked == 0)
            {
                MeanUnMasked = double.PositiveInfinity;
            }
            else
            {
                MeanUnMasked = Rate / CurrentInfUnMasked;
            }

            double MeanMasked;
            if (CurrentInfMasked == 0)
            {
                MeanMasked = double.PositiveInfinity;
            }
            else
            {
                MeanMasked = Rate / ProbWithMask / CurrentInfMasked;
            }
            
            return GetExpRand(MeanUnMasked+MeanMasked);
        }

        private double GenerateMaskedArrival()
        {
            double MeanUnMasked;
            if (CurrentInfUnMasked == 0)
            {
                MeanUnMasked = double.PositiveInfinity;
            }
            else
            {
                MeanUnMasked = Rate / CurrentInfUnMasked;
            }

            double MeanMasked;
            if (CurrentInfMasked == 0)
            {
                MeanMasked = double.PositiveInfinity;
            }
            else
            {
                MeanMasked = Rate / ProbWithMask / CurrentInfMasked;
            }

            return GetExpRand((MeanUnMasked + MeanMasked)/ProbWithMask);
        }


    }
}
