using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Roulette
{
    /// <summary>
    /// 🌞ratezahl einlesen
    /// </summary>
    public partial class MainWindow : Window
    {

        private int Einsatz = 0;
        double guthabenPlayer = 1000;
        double guthabenBank = 1000000;
        int zufallszahl = 0;
        int ratezahl = 0;

        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Zufallszahl();
        }

        public int einsatz
        {
            get { return Einsatz; }
            set
            {
                //Hier wird der Mindesteinsatz von 50 € festgelegt, nicht der Button!
                if (value < 50)
                {
                    MessageBox.Show("Mindesteinsatz ist: 50 €");
                }
                else
                {
                    Einsatz = value;
                }
            }
        }

        private void Zufallszahl()
        {
            zufallszahl = rnd.Next(1, 37);
            lbl_Infofeld.Content = "Zufallszahl: " + zufallszahl;
        }

        private void Pleite()
        {
            if (guthabenPlayer <= 0)
            {
                MessageBox.Show("Kein Guthaben mehr!\n" +
                    "Wird beendet!");
                Erfolgsfenster erfolgsfenster = new Erfolgsfenster();
            }
        }

        private void btn_Coint1000_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer - 1000 < 0)
            {
                MessageBox.Show("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                einsatz += 1000;
                guthabenPlayer -= 1000;
                lbl_einsatz.Content = einsatz;
                lbl_guthaben.Content = guthabenPlayer;
            }
        }

        private void btn_Coint500_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer - 500 < 0)
            {
                MessageBox.Show("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                einsatz += 500;
                guthabenPlayer -= 500;
                lbl_einsatz.Content = einsatz;
                lbl_guthaben.Content = guthabenPlayer;
            }
        }

        private void btn_Coint100_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer - 100 < 0)
            {
                MessageBox.Show("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                einsatz += 100;
                guthabenPlayer -= 100;
                lbl_einsatz.Content = einsatz;
                lbl_guthaben.Content = guthabenPlayer;
            }
        }

        private void btn_Coint50_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer - 50 < 0)
            {
                MessageBox.Show("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                einsatz += 50;
                guthabenPlayer -= 50;
                lbl_einsatz.Content = einsatz;
                lbl_guthaben.Content = guthabenPlayer;
            }
        }

        private void Entscheidung() //Entscheidung treffen
        {
            //AuswahlZahl(ratezahlStr);//

            //Zahl treffen gibt 1:36
            zufallszahl = 1;
            if (ratezahl == zufallszahl)
            {
                lbl_Infofeld.Content = "Jackpot!!! Zahl getroffen. Auszahlung 1:36";
                guthabenPlayer += einsatz * 36;
                guthabenBank -= einsatz * 36;
            }
            else if (ratezahl != zufallszahl)
            {
                lbl_Infofeld.Content = "Falsch! Zahl nicht getroffen";
                guthabenPlayer -= einsatz;
                guthabenBank += einsatz;
            }

            Pleite();
        }

        private async void btn_runde_Click(object sender, RoutedEventArgs e)
        {
            //Spannung aufbauen, durch versch. Zahl darstellen
            for (int i = 0; i < 25; i++)
            {
                Zufallszahl();
                await Task.Delay(150);
            }
        }

        //Ausgewählte Zahl
        private void AuswahlZahl(string ratezahlStr) //Ki Geschrieben
        {
            string[] zahlen = ratezahlStr.Split('_');

            string zahl1 = "";
            string zahl2 = "";
            string zahl3 = "";
            string zahl4 = "";
            string zahl5 = "";
            string zahl6 = "";
            string zahl7 = "";
            string zahl8 = "";
            string zahl9 = "";
            string zahl10 = "";
            string zahl11 = "";
            string zahl12 = "";

            if (zahlen.Length >= 1) zahl1 = zahlen[0];
            if (zahlen.Length >= 2) zahl2 = zahlen[1];
            if (zahlen.Length >= 3) zahl3 = zahlen[2];
            if (zahlen.Length >= 4) zahl4 = zahlen[3];
            if (zahlen.Length >= 5) zahl5 = zahlen[4];
            if (zahlen.Length >= 6) zahl6 = zahlen[5];
            if (zahlen.Length >= 7) zahl7 = zahlen[6];
            if (zahlen.Length >= 8) zahl8 = zahlen[7];
            if (zahlen.Length >= 9) zahl9 = zahlen[8];
            if (zahlen.Length >= 10) zahl10 = zahlen[9];
            if (zahlen.Length >= 11) zahl11 = zahlen[10];
            if (zahlen.Length >= 12) zahl12 = zahlen[11];
        }

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("1");
            Entscheidung();
        }

        private void btn_2_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("2");
        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("3");
        }

        private void btn_4_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("4");
        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("5");
        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("6");
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("7");
        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("8");
        }

        private void btn_9_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("9");
        }

        private void btn_10_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("10");
        }

        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("11");
        }

        private void btn_12_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("12");
        }

        private void btn_13_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("13");
        }

        private void btn_14_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("14");
        }

        private void btn_15_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("15");
        }

        private void btn_16_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("16");
        }

        private void btn_17_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("17");
        }

        private void btn_18_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("18");
        }

        private void btn_19_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("19");
        }

        private void btn_20_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("20");
        }

        private void btn_21_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("21");
        }

        private void btn_22_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("22");
        }

        private void btn_23_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("23");
        }

        private void btn_24_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("24");
        }

        private void btn_25_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("25");
        }

        private void btn_26_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("26");
        }

        private void btn_27_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("27");
        }

        private void btn_28_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("28");
        }

        private void btn_29_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("29");
        }

        private void btn_30_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("30");
        }

        private void btn_31_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("31");
        }

        private void btn_32_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("32");
        }

        private void btn_33_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("33");
        }

        private void btn_34_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("34");
        }

        private void btn_35_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("35");
        }

        private void btn_36_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("36");
        }

        //--

        //--

        private void btn_34_35_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("34_35");
        }

        private void btn_35_36_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("35_36");
        }

        private void btn_31_32_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("31_32");
        }

        private void btn_32_33_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("32_33");
        }

        private void btn_31_32_34_35_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("31_32_34_35");
        }

        private void btn_32_33_35_36_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("32_33_35_36");
        }

        private void btn_31_34_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("31_34");
        }

        private void btn_32_35_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("32_35");
        }

        private void btn_33_36_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("33_36");
        }

        private void btn_28_29_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("28_29");
        }

        private void btn_29_30_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("29_30");
        }

        private void btn_28_29_31_32_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("28_29_31_32");
        }

        private void btn_29_30_32_33_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("29_30_32_33");
        }

        private void btn_28_31_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("28_31");
        }

        private void btn_29_32_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("29_32");
        }

        private void btn_30_33_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("30_33");
        }

        private void btn_25_26_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("25_26");
        }

        private void btn_26_27_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("26_27");
        }

        private void btn_25_26_28_29_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("25_26_28_29");
        }

        private void btn_26_27_29_30_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("26_27_29_30");
        }

        private void btn_25_28_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("25_28");
        }

        private void btn_26_29_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("26_29");
        }

        private void btn_27_30_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("27_30");
        }

        private void btn_22_23_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("22_23");
        }

        private void btn_23_24_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("23_24");
        }

        private void btn_22_23_25_26_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("22_23_25_26");
        }

        private void btn_23_24_26_27_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("23_24_26_27");
        }

        private void btn_22_25_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("22_25");
        }

        private void btn_23_26_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("23_26");
        }

        private void btn_24_27_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("24_27");
        }

        private void btn_19_20_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("19_20");
        }

        private void btn_20_21_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("20_21");
        }

        private void btn_19_20_22_23_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("19_20_22_23");
        }

        private void btn_20_21_23_24_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("20_21_23_24");
        }

        private void btn_19_22_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("19_22");
        }

        private void btn_20_23_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("20_23");
        }

        private void btn_21_24_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("21_24");
        }

        private void btn_16_17_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("16_17");
        }

        private void btn_17_18_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("17_18");
        }

        private void btn_16_17_19_20_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("16_17_19_20");
        }

        private void btn_17_18_20_21_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("17_18_20_21");
        }

        private void btn_16_19_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("16_19");
        }

        private void btn_17_20_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("17_20");
        }

        private void btn_18_21_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("18_21");
        }

        private void btn_13_14_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("13_14");
        }

        private void btn_14_15_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("14_15");
        }

        private void btn_13_14_16_17_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("13_14_16_17");
        }

        private void btn_14_15_17_18_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("14_15_17_18");
        }

        private void btn_13_16_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("13_16");
        }

        private void btn_14_17_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("14_17");
        }

        private void btn_15_18_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("15_18");
        }

        private void btn_10_11_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("10_11");
        }

        private void btn_11_12_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("11_12");
        }

        private void btn_10_11_13_14_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("10_11_13_14");
        }

        private void btn_11_12_14_15_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("11_12_14_15");
        }

        private void btn_10_13_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("10_13");
        }

        private void btn_11_14_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("11_14");
        }

        private void btn_12_15_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("12_15");
        }

        private void btn_7_8_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("7_8");
        }

        private void btn_8_9_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("8_9");
        }

        private void btn_7_8_10_11_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("7_8_10_11");
        }

        private void btn_8_9_11_12_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("8_9_11_12");
        }

        private void btn_7_10_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("7_10");
        }

        private void btn_8_11_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("8_11");
        }

        private void btn_9_12_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("9_12");
        }

        private void btn_4_5_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("4_5");
        }

        private void btn_5_6_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("5_6");
        }

        private void btn_4_5_7_8_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("4_5_7_8");
        }

        private void btn_5_6_8_9_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("5_6_8_9");
        }

        private void btn_4_7_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("4_7");
        }

        private void btn_5_8_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("5_8");
        }

        private void btn_6_9_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("6_9");
        }

        private void btn_2_1_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("1_2");
        }

        private void btn_3_2_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("2_3");
        }

        private void btn_1_2_3_4_5_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("1_2_3_4_5");
        }

        private void btn_2_3_5_6_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("2_3_5_6");
        }

        private void btn_1_4_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("1_4");
        }

        private void btn_2_5_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("2_5");
        }

        private void btn_3_6_Click(object sender, RoutedEventArgs e)
        {
            AuswahlZahl("3_6");
        }

        private void btn_1st12(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_2st12(object sender, RoutedEventArgs e)
        {

        }

        private void btn_3st12(object sender, RoutedEventArgs e)
        {

        }

        private void btn_1_18(object sender, RoutedEventArgs e)
        {

        }

        private void btn_19_36(object sender, RoutedEventArgs e)
        {

        }

        private void btn_0_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
