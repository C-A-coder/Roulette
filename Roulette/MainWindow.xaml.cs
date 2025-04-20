using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Roulette
{
    /// <summary>
    /// 🌞schwwarz/rot ev. mit bool? || Even & Odd -- siehe Handbuch
    /// </summary>
    public partial class MainWindow : Window
    {

        private int Einsatz = 0;
        double guthabenPlayer = 1000;
        double guthabenBank = 1000000;
        int zufallszahl = 0;
        string ratezahlStr = "";
        int ratezahl = 0;

        string zahl1Str = "";
        string zahl2Str = "";
        string zahl3Str = "";
        string zahl4Str = "";
        string zahl5Str = "";
        string zahl6Str = "";
        string zahl7Str = "";
        string zahl8Str = "";
        string zahl9Str = "";
        string zahl10Str = "";
        string zahl11Str = "";
        string zahl12Str = "";
        string[] zahlen = new string[36];
        int mengeUnderscore = 0;
        bool checkFarbe = false;
        bool checkRot = false;

        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            //Zufallszahl();
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

        private async void Zufallszahl()
        {
            if (Einsatz <= 0)
            {
                lbl_Infofeld.Content = ("Bitte zuerst Einsatz setzen!");
                return;
            }

            Overlay.Visibility = Visibility.Visible;
            //Geld-Btns-deaktivieren
            //btn_Coint1000.IsEnabled = false;
            //btn_Coint500.IsEnabled = false;
            //btn_Coint100.IsEnabled = false;
            //btn_Coint50.IsEnabled = false;
            //Spannung aufbauen, durch versch. Zahl darstellen
            for (int i = 0; i < 25; i++)
            {
                zufallszahl = rnd.Next(1, 37);
                lbl_Infofeld.Content = "Zufallszahl: " + zufallszahl;
                await Task.Delay(150);
            }
            // Generation finale Zufallszahl
            zufallszahl = rnd.Next(1, 37);
            lbl_Infofeld.Content = "Zufallszahl: " + zufallszahl;
            await Task.Delay(2500);
            //zufallszahl = 1;

            Overlay.Visibility = Visibility.Hidden;
            //Geld-Btns-aktivieren
            //btn_Coint1000.IsEnabled = true;
            //btn_Coint500.IsEnabled = true;
            //btn_Coint100.IsEnabled = true;
            //btn_Coint50.IsEnabled = true;
            Entscheidung();
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
                Einsatz += 1000;
                guthabenPlayer -= 1000;
                lbl_einsatz.Text = Einsatz.ToString();
                lbl_guthaben.Text = guthabenPlayer.ToString();
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
                Einsatz += 500;
                guthabenPlayer -= 500;
                lbl_einsatz.Text = Einsatz.ToString();
                lbl_guthaben.Text = guthabenPlayer.ToString();
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
                Einsatz += 100;
                guthabenPlayer -= 100;
                lbl_einsatz.Text = Einsatz.ToString();
                lbl_guthaben.Text = guthabenPlayer.ToString();
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
                Einsatz += 50;
                guthabenPlayer -= 50;
                lbl_einsatz.Text = Einsatz.ToString();
                lbl_guthaben.Text = guthabenPlayer.ToString();
            }
        }

        //private void Entscheidung() //Entscheidung treffen
        //{
        //    AuswahlZahl();

        //    //Zahl treffen gibt 1:36
        //    zufallszahl = 1;
        //    if (Int32.TryParse(ratezahlStr, out ratezahl)) //Zahlen von 1-36 und falsche
        //    {
        //        if (ratezahl == zufallszahl)
        //        {
        //            lbl_Infofeld.Content = "Jackpot!!! Zahl getroffen. Auszahlung 1:36";
        //            guthabenPlayer += einsatz * 36;
        //            guthabenBank -= einsatz * 36;
        //        }
        //        else if (ratezahl != zufallszahl)
        //        {
        //            lbl_Infofeld.Content = "Falsch! Zahl nicht getroffen";
        //            guthabenPlayer -= einsatz;
        //            guthabenBank += einsatz;
        //        }
        //    }
        //    else //prüfen für größerer Zahlen mit _
        //    {
        //        if (ratezahl == zufallszahl)//Wie komme ich zur richtigen Zahl?
        //        {
        //            lbl_Infofeld.Content = "Jackpot!!! Zahl getroffen. Auszahlung 1:36";
        //            guthabenPlayer += einsatz * 36;
        //            guthabenBank -= einsatz * 36;
        //        }
        //    }

        //    Pleite();
        //}

        private void Entscheidung()
        {
            //AuswahlZahl();
            //Zahl treffen gibt 1:36
            //zufallszahl = 1; //Testwert, um die Logik zu demonstrieren
            //Zufallszahl();
            bool zahlGetroffen = false;

            foreach (string zahlStr in zahlen)
            {
                if (Int32.TryParse(zahlStr, out int gerateneZahl))
                {-
                    if (gerateneZahl == zufallszahl)
                    {
                        zahlGetroffen = true;
                        break; // Sobald Zahl getroffen, Schleife verlassen
                    }
                }
                else if (zahlStr == "")//Farbe
                {
                    switch (zufallszahl)
                    {
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 9:
                        case 12:
                        case 14:
                        case 16:
                        case 18:
                        case 19:
                        case 21:
                        case 23:
                        case 25:
                        case 27:
                        case 30:
                        case 32:
                        case 34:
                        case 36:
                            zahlGetroffen = true;
                            break;
                        default: // Schwarz
                            zahlGetroffen = false;
                            break;
                    }
                }
                else
                {
                    lbl_Infofeld.Content = "Ungültige Eingabe: " + zahlStr;
                    return;
                }
            }

            if (zahlGetroffen)
            {
                if (mengeUnderscore == 1 && checkFarbe == false)
                {
                    guthabenPlayer += Einsatz * 35;
                    guthabenBank -= Einsatz * 35;
                    lbl_Infofeld.Content = $"Jackpot!!! Zahl getroffen. Auszahlung 1:35\n{Einsatz * 35}";
                }
                else
                {
                    if (mengeUnderscore == 2)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:17\n{Einsatz * 17}";
                        guthabenPlayer += Einsatz * 17;
                        guthabenBank -= Einsatz * 17;
                    }
                    else if (mengeUnderscore == 3)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:11\n{Einsatz * 11}";
                        guthabenPlayer += Einsatz * 11;
                        guthabenBank -= Einsatz * 11;
                    }
                    else if (mengeUnderscore == 4)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:8\n{Einsatz * 1}";
                        guthabenPlayer += Einsatz * 8;
                        guthabenBank -= Einsatz * 8;
                    }
                    else if (mengeUnderscore == 6)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:5\n{Einsatz * 5}";
                        guthabenPlayer += Einsatz * 5;
                        guthabenBank -= Einsatz * 5;
                    }
                    else if (mengeUnderscore == 12)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:2\n{Einsatz * 2}";
                        guthabenPlayer += Einsatz * 2;
                        guthabenBank -= Einsatz * 2;
                    }
                    else if (mengeUnderscore == 18 || (checkFarbe && checkRot))//+Farbe
                    {
                        lbl_Infofeld.Content = $"Zahl oder Farbe getroffen! Auszahlung 1:1\n{Einsatz * 1}";
                        guthabenPlayer += Einsatz * 1;
                        guthabenBank -= Einsatz * 1;
                        checkFarbe = false;
                        checkRot = false;
                    }
                    else
                    {
                        lbl_Infofeld.Content = "Nein! Farbe leider nicht getroffen";
                        guthabenPlayer -= Einsatz;
                        guthabenBank += Einsatz;
                        checkFarbe = false;
                        checkRot = false;
                    }
                }
            }
            else
            {
                lbl_Infofeld.Content = "Nein! Zahl nicht getroffen";
                guthabenPlayer -= Einsatz;
                guthabenBank += Einsatz;
            }

            lbl_guthaben.Text = guthabenPlayer.ToString(); // Guthaben aktualisieren
            lbl_einsatz.Text = "0"; // Einsatz zurücksetzen
            Einsatz = 0;

            Pleite();
            //Zufallszahl(); // Neue Zufallszahl für die nächste Runde generieren
        }

        //private async void btn_runde_Click(object sender, RoutedEventArgs e)
        //{
        //    if(Einsatz <= 0)
        //    {
        //        lbl_Infofeld.Content=("Bitte zuerst Einsatz setzen!");
        //        return;
        //    }
        //    //Spannung aufbauen, durch versch. Zahl darstellen
        //    for (int i = 0; i < 25; i++)
        //    {
        //        Zufallszahl();
        //        await Task.Delay(150);
        //    }
        //}

        //Ausgewählte Zahl
        private void AuswahlZahl() //Ki Geschrieben
        {
            zahlen = ratezahlStr.Split('_');
            mengeUnderscore = 1;

            for (int i = 1; i < zahlen.Length; i++)
            {
                mengeUnderscore++;
            }

            if (zahlen.Length >= 1) zahl1Str = zahlen[0];
            if (zahlen.Length >= 2) zahl2Str = zahlen[1];
            if (zahlen.Length >= 3) zahl3Str = zahlen[2];
            if (zahlen.Length >= 4) zahl4Str = zahlen[3];
            if (zahlen.Length >= 5) zahl5Str = zahlen[4];
            if (zahlen.Length >= 6) zahl6Str = zahlen[5];
            if (zahlen.Length >= 7) zahl7Str = zahlen[6];
            if (zahlen.Length >= 8) zahl8Str = zahlen[7];
            if (zahlen.Length >= 9) zahl9Str = zahlen[8];
            if (zahlen.Length >= 10) zahl10Str = zahlen[9];
            if (zahlen.Length >= 11) zahl11Str = zahlen[10];
            if (zahlen.Length >= 12) zahl12Str = zahlen[11];
            if (zahlen.Length >= 13) zahl12Str = zahlen[12];
            if (zahlen.Length >= 14) zahl12Str = zahlen[13];
            if (zahlen.Length >= 15) zahl12Str = zahlen[14];
            if (zahlen.Length >= 16) zahl12Str = zahlen[15];
            if (zahlen.Length >= 17) zahl12Str = zahlen[16];
            if (zahlen.Length >= 18) zahl12Str = zahlen[17];
            if (zahlen.Length >= 19) zahl12Str = zahlen[18];
            if (zahlen.Length >= 20) zahl12Str = zahlen[19];
            if (zahlen.Length >= 21) zahl12Str = zahlen[20];
            if (zahlen.Length >= 22) zahl12Str = zahlen[21];
            if (zahlen.Length >= 23) zahl12Str = zahlen[22];
            if (zahlen.Length >= 24) zahl12Str = zahlen[23];
            if (zahlen.Length >= 25) zahl12Str = zahlen[24];
            if (zahlen.Length >= 26) zahl12Str = zahlen[25];
            if (zahlen.Length >= 27) zahl12Str = zahlen[26];
            if (zahlen.Length >= 28) zahl12Str = zahlen[27];
            if (zahlen.Length >= 29) zahl12Str = zahlen[28];
            if (zahlen.Length >= 30) zahl12Str = zahlen[29];
            if (zahlen.Length >= 31) zahl12Str = zahlen[30];
            if (zahlen.Length >= 32) zahl12Str = zahlen[31];
            if (zahlen.Length >= 33) zahl12Str = zahlen[32];
            if (zahlen.Length >= 34) zahl12Str = zahlen[33];
            if (zahlen.Length >= 35) zahl12Str = zahlen[34];
            if (zahlen.Length >= 36) zahl12Str = zahlen[35];
        }

        //Farbe filtern || rot = true, schwarz = false <-- default = alles andere
        //private bool FarbeFiltern()
        //{
        //    switch (zufallszahl)
        //    {
        //        case 1:
        //        case 3:
        //        case 5:
        //        case 7:
        //        case 9:
        //        case 12:
        //        case 14:
        //        case 16:
        //        case 18:
        //        case 19:
        //        case 21:
        //        case 23:
        //        case 25:
        //        case 27:
        //        case 30:
        //        case 32:
        //        case 34:
        //        case 36:
        //            return checkFarbe = true;
        //        default: return checkFarbe = false;
        //    }
        //}

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "1";
            AuswahlZahl();
            Zufallszahl();
        }

        private void btn_2_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "2";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "3";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "4";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "7";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "10";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "13";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "16";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "19";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "22";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "25";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "28";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "31";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "36";
            AuswahlZahl(); Zufallszahl();
        }

        //--

        //--

        private void btn_34_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "34_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_35_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "31_32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "32_33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_32_34_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "31_32_34_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_33_35_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "32_33_35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "31_34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "32_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_33_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "33_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "28_29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "29_30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_29_31_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "28_29_31_32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_30_32_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "29_30_32_33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "28_31";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "29_32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_30_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "30_33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "25_26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "26_27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_26_28_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "25_26_28_29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_27_29_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "26_27_29_30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "25_28";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "26_29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_27_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "27_30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "22_23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "23_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_23_25_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "22_23_25_26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_24_26_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "23_24_26_27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "22_25";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "23_26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_24_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "24_27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "19_20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "20_21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_20_22_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "19_20_22_23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_21_23_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "20_21_23_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "19_22";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "20_23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_21_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "21_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "16_17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "17_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_17_19_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "16_17_19_20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_18_20_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "17_18_20_21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "16_19";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "17_20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_18_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "18_21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "13_14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "14_15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_14_16_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "13_14_16_17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_15_17_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "14_15_17_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "13_16";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "14_17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_15_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "15_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "10_11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "11_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_11_13_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "10_11_13_14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_12_14_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "11_12_14_15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "10_13";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "11_14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_12_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "12_15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "7_8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "8_9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_8_10_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "7_8_10_11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_9_11_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "8_9_11_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "7_10";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "8_11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_9_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "9_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "4_5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "5_6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_5_7_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "4_5_7_8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_6_8_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "5_6_8_9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "4_7";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "5_8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_6_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "6_9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "1_2";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3_2_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "2_3";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1_2_4_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "1_2_4_5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2_3_5_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "2_3_5_6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "1_4";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "2_5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "3_6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1st12(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "1_2_3_4_5_6_7_8_9_10_11_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2nd12(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "13_14_15_16_17_18_19_20_21_22_23_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3rd12(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "25_26_27_28_29_30_31_32_33_34_35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1_18(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_36(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "19_20_21_22_23_24_25_26_27_28_29_30_31_32_33_34_35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_0_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "0";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_rot_Click(object sender, RoutedEventArgs e)
        {
            checkFarbe = true;
            checkRot = true;
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_schwarz_Click(object sender, RoutedEventArgs e)
        {
            checkFarbe = true;
            checkRot = false;
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3_2_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "3_2_1";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_6_5_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "6_5_4";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_9_8_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "9_8_7";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_12_11_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "12_11_10";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_15_14_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "15_14_13";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_18_17_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "18_17_16";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_21_20_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "21_20_19";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_24_23_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "24_23_22";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_27_26_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "27_26_25";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_30_29_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "30_29_28";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_33_32_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "33_32_31";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_36_35_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "36_35_34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2to1(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "1_4_7_10_13_16_19_22_25_28_31_34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2to2(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "2_5_8_11_14_17_20_23_26_29_32_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2to3(object sender, RoutedEventArgs e)
        {
            ratezahlStr = "3_6_9_12_15_18_21_24_27_30_33_36";
            AuswahlZahl(); Zufallszahl();
        }
    }
}
