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

            if (!NT_s || !NUP_s || !NMP_s || !CIU_s || !CIM_s || !DF_s || !MIT_s || !PIM_s || NTVal <= 0 || NUPVal <= 0 || NMPVal <= 0 || CIUVal < 0 || CIMVal < 0 || DFVal <= 0 || MITVal <= 0 || PIMVal <= 0 || PIMVal > 100)
            {
                MessageBox.Show("You have entered (an) invalid value(s).", "Error");
            }
            else if (NMPVal ==0 && isMasked.IsChecked.Value)
            {
                MessageBox.Show("You entered 0 masked people, yet yourself is masked.", "Error");
            }
            else if (NUPVal == 0 && !isMasked.IsChecked.Value)
            {
                MessageBox.Show("You entered 0 unmasked people, yet yourself is unmasked.", "Error");
            }
            else
            {
                SimulationList.Add( new Simulation(NTVal, NUPVal, NMPVal, CIUVal, CIMVal, DFVal, MITVal, PIMVal, isMasked.IsChecked.Value));

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
    }
}
