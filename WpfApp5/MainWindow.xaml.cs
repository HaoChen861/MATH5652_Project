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

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Result.ItemsSource = SimulationList;
        }

        List<Simulation> SimulationList = new List<Simulation>();

        private void Cal_Click(object sender, RoutedEventArgs e)
        {
            bool NT_s = int.TryParse(NTBox.Text, out int NTVal);
            bool NUP_s = int.TryParse(NUP.Text, out int NUPVal);
            bool NMP_s = int.TryParse(NMP.Text, out int NMPVal);
            bool CIU_s = int.TryParse(CIU.Text, out int CIUVal);
            bool CIM_s = int.TryParse(CIM.Text, out int CIMVal);
            bool DF_s = double.TryParse(DF.Text, out double DFVal);
            bool MIT_s = double.TryParse(MIT.Text, out double MITVal);
            bool PIM_s = double.TryParse(PIM.Text, out double PIMVal);

            if (!NT_s || !NUP_s || !NMP_s || !CIU_s || !CIM_s || !DF_s || !MIT_s || !PIM_s || NTVal <= 0 || NUPVal < 0 || NMPVal < 0 || CIUVal < 0 || CIMVal < 0 || DFVal <= 0 || MITVal <= 0 || PIMVal <= 0 || PIMVal > 100)
            {
                MessageBox.Show("You have entered (an) invalid value(s).", "Error");
            }
            else if(NUPVal == 0 && NMPVal == 0)
            {
                MessageBox.Show("There must be people on a plane.", "Error");
            }
            else if (NMPVal ==0 && isMasked.IsChecked.Value)
            {
                MessageBox.Show("You entered 0 masked people, yet yourself is masked.", "Error");
            }
            else if (NUPVal == 0 && !isMasked.IsChecked.Value)
            {
                MessageBox.Show("You entered 0 unmasked people, yet yourself is unmasked.", "Error");
            }
            else if (CIUVal > NUPVal)
            {
                MessageBox.Show("The number of infected unmasked people exceeds the number of unmasked people.", "Error");
            }
            else if (CIMVal > NMPVal)
            {
                MessageBox.Show("The number of infected masked people exceeds the number of masked people.", "Error");
            }
            else
            {
                SimulationList.Add( new Simulation(NTVal, NUPVal, NMPVal, CIUVal, CIMVal, DFVal, MITVal, PIMVal, isMasked.IsChecked.Value));
                MainViewModel ViewModel = new MainViewModel(SimulationList[SimulationList.Count - 1].ArrivalTimeList, SimulationList[SimulationList.Count - 1].ArrivalGroupList, CIUVal, CIMVal);
                SimPlot.DataContext = ViewModel;
                Result.Items.Refresh();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            NTBox.Clear();
            NUP.Clear();
            NMP.Clear();
            CIU.Clear();
            CIM.Clear();
            DF.Clear();
            MIT.Clear();
            PIM.Clear();
            isMasked.IsChecked = false;
            Result.Items.Clear();
        }

        private void NUP_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool NUP_s = int.TryParse(NUP.Text, out int NUPVal);
            
            if (!NUP_s)
            {
                NUP.Background = Brushes.Pink;
            }
            else
            {
                NUP.Background = Brushes.White;
            }
        }

        private void NMP_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool NMP_s = int.TryParse(NMP.Text, out int NMPVal);

            if (!NMP_s)
            {
                NMP.Background = Brushes.Pink;
            }
            else
            {
                NMP.Background = Brushes.White;
            }
        }

        private void CIU_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool CIU_s = int.TryParse(CIU.Text, out int CIUVal);

            if (!CIU_s)
            {
                CIU.Background = Brushes.Pink;
            }
            else
            {
                CIU.Background = Brushes.White;
            }
        }

        private void CIM_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool CIM_s = int.TryParse(CIM.Text, out int CIMVal);

            if (!CIM_s)
            {
                CIM.Background = Brushes.Pink;
            }
            else
            {
                CIM.Background = Brushes.White;
            }
        }

        private void DF_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool DF_s = int.TryParse(DF.Text, out int DFVal);

            if (!DF_s)
            {
                DF.Background = Brushes.Pink;
            }
            else
            {
                DF.Background = Brushes.White;
            }
        }

        private void MIT_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool MIT_s = int.TryParse(MIT.Text, out int MITVal);

            if (!MIT_s)
            {
                MIT.Background = Brushes.Pink;
            }
            else
            {
                MIT.Background = Brushes.White;
            }
        }

        private void PIM_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool PIM_s = int.TryParse(PIM.Text, out int PIMVal);

            if (!PIM_s)
            {
                PIM.Background = Brushes.Pink;
            }
            else
            {
                PIM.Background = Brushes.White;
            }
        }

        private void NTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool NTBox_s = int.TryParse(NTBox.Text, out int NTBoxVal);

            if (!NTBox_s)
            {
                NTBox.Background = Brushes.Pink;
            }
            else
            {
                NTBox.Background = Brushes.White;
            }
        }
    }
}
