using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    public class Simulation
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

        public List<double> ArrivalTimeList = new List<double>();

        public List<int> ArrivalGroupList = new List<int>();

        public bool isMasked { get; set; }

        public double ProbInf { get; set; }

        public double MeanInfUnmask { get; set; }

        public double MeanInfMask { get; set; }

        public double Variance { get; set; }

        Random UniRand = new Random();

        public Simulation(int NT, int aNumberUnMasked, int aNumberMasked, int aCurrentInfUnMasked, int aCurrentInfMasked, double aMaxClock, double aRate, double aProbWithMask,bool aIsMask)
        {
            NumberUnMasked = aNumberUnMasked;
            NumberMasked = aNumberMasked;

            MaxNumber = aNumberUnMasked+ aNumberMasked;
            MaxClock = aMaxClock;
            Rate = aRate;
            ProbWithMask = aProbWithMask/100;
            isMasked = aIsMask;
            MeanInfMask = 0;
            MeanInfUnmask = 0;
            ProbInf = 0;
            Variance = 0;

            for (int i =0; i < NT; i++)
            {
                Clock = 0;
                CurrentInfUnMasked = aCurrentInfUnMasked;
                CurrentInfMasked = aCurrentInfMasked;
                ArrivalTimeList.Clear();
                ArrivalGroupList.Clear();

                while (Clock < MaxClock && (CurrentInfMasked + CurrentInfUnMasked) < MaxNumber)
                {
                    Spreads();
                }

                if (isMasked)
                {
                    ProbInf += CurrentInfMasked / NumberMasked;
                    Variance += Math.Pow(CurrentInfMasked / NumberMasked,2);
                }
                else
                {
                    ProbInf += CurrentInfUnMasked / NumberUnMasked;
                    Variance += Math.Pow(CurrentInfUnMasked / NumberUnMasked,2);
                }

                MeanInfMask += Convert.ToDouble(CurrentInfMasked);
                MeanInfUnmask += Convert.ToDouble(CurrentInfUnMasked);

            }


            MeanInfMask /= NT;
            MeanInfUnmask /= NT;
            Variance = (Variance - ProbInf * ProbInf / NT) / (NT - 1);
            ProbInf /= NT;
            
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

        /// <summary>
        /// Model Spread
        /// Get two random exp number based on current infected people
        /// If the masked infection comes first, a masked person is infected
        /// Otherwise, an unmasked person is infected
        /// if the current infected is equal to number of masked then the random exp number becomes positive infinity
        /// 
        /// </summary>
        private void Spreads()
        {
            double NextMask;
            double NextUnMask;

            if (CurrentInfMasked == NumberMasked)
            {
                NextMask = double.PositiveInfinity;
            }
            else
            {
                NextMask = GenerateMaskedArrival();
            }
            
            if (CurrentInfUnMasked == NumberUnMasked)
            {
                NextUnMask = double.PositiveInfinity;
            }
            else
            {
                NextUnMask = GenerateUnMaskedArrival();
            }
            

            if (NextMask < NextUnMask)
            {
                HandleMaskedArrival(NextMask);
            }
            else
            {
                HandleUnMaskedArrival(NextUnMask);
            }

        }

        /// <summary>
        /// when an unmasked person is infected
        /// </summary>
        /// <param name="NextUnMask"></param>
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

        /// <summary>
        /// when a masked person is infected
        /// </summary>
        /// <param name="NextMask"></param>
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

        /// <summary>
        /// Use min{exp(lambda),exp(mu)}~exp(lambda+mu)
        /// Masked people have lower rate of transmission
        /// if the probability of getting infected is 0.1, the mean time is timed by 10
        /// </summary>
        /// <returns></returns>
        private double GenerateUnMaskedArrival()
        {
            double MeanUnMasked;
            if (CurrentInfUnMasked == 0)
            {
                MeanUnMasked = 0;
            }
            else
            {
                MeanUnMasked = Rate / CurrentInfUnMasked;
            }

            double MeanMasked;
            if (CurrentInfMasked == 0)
            {
                MeanMasked = 0;
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
                MeanUnMasked = 0;
            }
            else
            {
                MeanUnMasked = Rate / CurrentInfUnMasked;
            }

            double MeanMasked;
            if (CurrentInfMasked == 0)
            {
                MeanMasked = 0;
            }
            else
            {
                MeanMasked = Rate / ProbWithMask / CurrentInfMasked;
            }

            return GetExpRand((MeanUnMasked + MeanMasked)/ProbWithMask);
        }


    }

    public class Simulation<T> : Simulation
    {
        public Simulation(int NT, int aNumberUnMasked, int aNumberMasked, int aCurrentInfUnMasked, int aCurrentInfMasked, double aMaxClock, double aRate, double aProbWithMask, bool aIsMask)
            : base(NT, aNumberUnMasked, aNumberMasked, aCurrentInfUnMasked, aCurrentInfMasked, aMaxClock, aRate, aProbWithMask, aIsMask)
        {

        }
    }
}
