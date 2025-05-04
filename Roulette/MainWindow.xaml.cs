using Black_Jack;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Roulette
{
    /// <summary>
    /// "Entscheidung" GPT fertig stellen || Prüfen
    /// </summary>
    public partial class MainWindow : Window
    {

        private int Einsatz_ = 0;
        private int GesGewinn_ = 0;
        private int GesEinsatz_ = 0;
        public double guthabenPlayer_ = 1000;
        double guthabenBank_ = 1000000;
        int zufallszahl_ = 0;
        string ratezahlStr_ = "";
        int ratezahl_ = 0;
        public double guthabenPlayerMax_ { get; private set; }//Max guthaben

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
        Dictionary<string, int> auswahlZahlen = new Dictionary<string, int>(); //Dic, einmal menge "_" und Zahl(en) selber speichern
        Dictionary<string, int> einsatzProAuswahl = new Dictionary<string, int>();
        int mengeUnderscore = 0;
        bool checkFarbe = false;
        bool checkRot = false;
        public string pfad = null;
        bool checkMusik = false;

        Random rnd = new Random();
        MediaPlayer player = null;
        Erfolgsfenster erfolgsfenster = null;

        public MainWindow()
        {
            InitializeComponent();

            StartMediaPlayer();
            erfolgsfenster = new Erfolgsfenster(this);

            pfad = Path.Combine(Environment.CurrentDirectory, "VerlaufSpezifisch.txt");
            File.Delete(pfad);
            //pfad = Path.Combine(Environment.CurrentDirectory, "KartenverlaufAllgemein.txt");
            File.Delete(pfad);
            pfad = Path.Combine(Environment.CurrentDirectory, "Geldfluss.txt");
            File.Delete(pfad);
            player.Volume = 0.2;
            MessageBox.Show("Viel Spaß! Beachte, Einsatz wird NICHT zurückbezahlt, wenn investiert, aber auf KEINE ZAHl gesetzt wird.\nEbenfalls kann man bei mehreren Sätzen den Einsatz nur erhöhen - nicht niedriger als Erster Satz!");
            Timer();
        }

        ////////// vereinen und entfernen durch bj

        private void StartMediaPlayer()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string filePath = System.IO.Path.Combine(currentDirectory, "Audio/violin.m4a");
            if (System.IO.File.Exists(filePath))
            {
                //MessageBox.Show("Datei gefunden!");
            }
            else
            {
                MessageBox.Show("Datei nicht gefunden!");
            }
            try
            {
                player = new MediaPlayer();
                player.Open(new Uri("Audio/violin.m4a", UriKind.Relative));
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Abspielen der Audiodatei: " + ex.Message);
            }
        }

        public void ChangeSong(string path)
        {
            if (System.IO.File.Exists(path))
            {
                //MessageBox.Show("Datei gefunden!");
            }
            else
            {
                MessageBox.Show("Datei nicht gefunden!");
            }
            try
            {
                player.Open(new Uri(path, UriKind.Relative));
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Abspielen der Audiodatei: " + ex.Message);
            }
        }

        private async void Zufallszahl()
        {
            if (Einsatz_ <= 0)
            {
                lbl_Infofeld.Content = ("Bitte Einsatz setzen!");
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
                zufallszahl_ = rnd.Next(0, 37);
                lbl_Infofeld.Content = "Zufallszahl: " + zufallszahl_;
                await Task.Delay(150);
            }
            // Generation finale Zufallszahl
            zufallszahl_ = rnd.Next(0, 37);
            lbl_Infofeld.Content = "Zufallszahl: " + zufallszahl_;
            //await Task.Delay(5000);
            //zufallszahl_ = 17;

            Overlay.Visibility = Visibility.Hidden;


            Entscheidung();

            //erst aufrufen, wenn (10 sek) alles gesetzt ist --> ev. eigene Methode: nach 10 sek wird aufgerufen bis spin, dann erneut...diese dann bei jedem Feld einfügen
            foreach (string durchlaufZahlen in auswahlZahlen.Keys)
            {
                Debug.WriteLine(durchlaufZahlen);
            }

            auswahlZahlen.Clear();
        }

        private async void Timer()
        {
            while (true)
            {
                //Timer für 10 sek
                for (int i = 15; i > 0; i--)
                {
                    lbl_Infofeld_Delay.Content = "Noch " + i + " Sekunden bis zum Spin!";
                    await Task.Delay(1000);
                }
                lbl_Infofeld_Delay.Content = "Spin!";
                Zufallszahl();
                //Delay nach zufallszahl

                //berechnet mit 25*Delay(1500) --> Zufallszahlsdauer zur Entscheidung
                await Task.Delay(4000);
            }
        }

        private void Pleite()
        {
            if (guthabenPlayer_ <= 0)
            {
                MessageBox.Show("Kein Guthaben mehr!\n" +
                    "Wird beendet!");

                this.Content = erfolgsfenster;
                erfolgsfenster.Abbruch();
            }
        }

        private void btn_Coint1000_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer_ - 1000 < 0)
            {
                lbl_Infofeld.Content = ("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                Einsatz_ = 1000;
                lbl_Infofeld.Content = $"Einsatzbetrag auf {Einsatz_} gesetzt.";

                Protokoll("VerlaufSpezifisch", "Einsatz: ", Einsatz_);
            }
        }

        private void btn_Coint500_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer_ - 500 < 0)
            {
                lbl_Infofeld.Content = ("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                Einsatz_ = 500;
                lbl_Infofeld.Content = $"Einsatzbetrag auf {Einsatz_} gesetzt.";

                Protokoll("VerlaufSpezifisch", "Einsatz: ", Einsatz_);
            }
        }

        private void btn_Coint100_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer_ - 100 < 0)
            {
                lbl_Infofeld.Content = ("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                Einsatz_ = 100;
                lbl_Infofeld.Content = $"Einsatzbetrag auf {Einsatz_} gesetzt.";

                Protokoll("VerlaufSpezifisch", "Einsatz: ", Einsatz_);
            }
        }

        private void btn_Coint50_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer_ - 50 < 0)
            {
                lbl_Infofeld.Content = ("Fehler: Guthaben zu niedrig!");
            }
            else
            {
                Einsatz_ = 50;
                lbl_Infofeld.Content = $"Einsatzbetrag auf {Einsatz_} gesetzt.";
                //Einsatz_ += 50;
                //GesEinsatz_ += 50;
                //guthabenPlayer_ -= 50;
                //guthabenBank_ += Einsatz_;
                //lbl_einsatz.Text = GesEinsatz_.ToString(); txtb_guthaben.Text = guthabenPlayer_.ToString();

                Protokoll("VerlaufSpezifisch", "Einsatz: ", Einsatz_);
            }
        }

        private void Entscheidung()
        {
            bool zahlGetroffen = false;
            int gerateneZahl = 0;

            // Speicherung von Max-Einsatz
            if (Einsatz_ > guthabenPlayerMax_)
            {
                guthabenPlayerMax_ = Einsatz_;
            }

            // Überprüfung der Farbwette
            if (checkFarbe)
            {
                if (zufallszahl_ == 0) // Ausnahme: 0 hat keine Farbe
                {
                    lbl_Infofeld.Content += "\nFarbe nicht getroffen (Zufallszahl: 0)";
                    Protokoll("VerlaufSpezifisch", "Verloren! Zufallszahlfarbe: ", zufallszahl_);
                    guthabenBank_ += Einsatz_;
                }
                else
                {
                    bool farbeGetroffen = false;

                    if (checkRot)
                    {
                        int[] roteZahlen = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
                        for (int i = 0; i < roteZahlen.Length; i++)
                        {
                            if (zufallszahl_ == roteZahlen[i])
                            {
                                farbeGetroffen = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        int[] schwarzeZahlen = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
                        for (int i = 0; i < schwarzeZahlen.Length; i++)
                        {
                            if (zufallszahl_ == schwarzeZahlen[i])
                            {
                                farbeGetroffen = true;
                                break;
                            }
                        }
                    }

                    if (farbeGetroffen)
                    {
                        lbl_Infofeld.Content += $"\nFarbe getroffen! Auszahlung 1:1 --> {Einsatz_}";
                        Protokoll("VerlaufSpezifisch", "Gewonnen! Farbe: +", Einsatz_);
                        guthabenPlayer_ += Einsatz_ * 2; // inkl. Rückzahlung Einsatz
                        guthabenBank_ -= Einsatz_;
                        txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
                    }
                    else
                    {
                        lbl_Infofeld.Content += "\nFarbe nicht getroffen";
                        Protokoll("VerlaufSpezifisch", "Verloren! Zufallszahlfarbe: ", zufallszahl_);
                        guthabenBank_ += Einsatz_;
                    }
                }
            }

            // Überprüfung der Zahlenauswahl
            foreach (var auswahl in auswahlZahlen)
            {
                string zahlStr = auswahl.Key;
                int mengeUnderscore = auswahl.Value;

                if (!einsatzProAuswahl.ContainsKey(zahlStr))
                {
                    continue;
                }

                int einsatz = einsatzProAuswahl[zahlStr];
                string[] einzelneZahlen = zahlStr.Split('_');
                bool gruppenTreffer = false;

                for (int i = 0; i < einzelneZahlen.Length; i++)
                {
                    if (Int32.TryParse(einzelneZahlen[i], out gerateneZahl) && gerateneZahl == zufallszahl_)
                    {
                        zahlGetroffen = true;

                        int auszahlungsMultiplikator = 1;

                        if (mengeUnderscore == 1) auszahlungsMultiplikator = 35;
                        else if (mengeUnderscore == 2) auszahlungsMultiplikator = 17;
                        else if (mengeUnderscore == 3) auszahlungsMultiplikator = 11;
                        else if (mengeUnderscore == 4) auszahlungsMultiplikator = 8;
                        else if (mengeUnderscore == 6) auszahlungsMultiplikator = 5;
                        else if (mengeUnderscore == 12) auszahlungsMultiplikator = 2;

                        int gewinn = einsatz * auszahlungsMultiplikator;
                        lbl_Infofeld.Content += $"\nTreffer bei {zahlStr}:\nAuszahlung 1:{auszahlungsMultiplikator} --> {gewinn}";
                        Protokoll("VerlaufSpezifisch", $"Gewonnen bei {zahlStr}! +", gewinn);

                        guthabenPlayer_ += gewinn + einsatz;
                        guthabenBank_ -= gewinn;

                        gruppenTreffer = true;
                        break; // Nur den ersten Treffer pro Auswahl berücksichtigen
                    }
                }

                if (!gruppenTreffer)
                {
                    lbl_Infofeld.Content += $"\nKein Treffer bei {zahlStr}.";
                    Protokoll("VerlaufSpezifisch", $"Verloren bei {zahlStr}. Zufallszahl: ", zufallszahl_);
                }
            }

            // Guthaben und Einsätze zurücksetzen
            lbl_einsatz.Text = "0";
            GesGewinn_ = 0;
            GesEinsatz_ = 0;
            Einsatz_ = 0;
            txtb_guthaben.Text = guthabenPlayer_.ToString();
            einsatzProAuswahl.Clear();
            auswahlZahlen.Clear();
            checkFarbe = false;
            checkRot = false;

            // Buttons wieder aktivieren
            AktiviereAlleButtons();

            Pleite();
        }

        //Ausgewählte Zahl
        private void AuswahlZahl()
        {
            //Teilt die EIngaben nach _ und "Zählt"
            zahlen = ratezahlStr_.Split('_');
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

        private void Test()
        {
            if (Einsatz_ <= 0)
            {
                lbl_Infofeld.Content = "Bitte zuerst einen Einsatzbetrag wählen!";
                return;
            }

            if (guthabenPlayer_ < Einsatz_)
            {
                lbl_Infofeld.Content = "Nicht ausreichend Guthaben!";
                return;
            }

            if (auswahlZahlen.ContainsKey(ratezahlStr_))
            {
                auswahlZahlen[ratezahlStr_] += Einsatz_;
            }
            else
            {
                auswahlZahlen.Add(ratezahlStr_, mengeUnderscore);
            }

            if (einsatzProAuswahl.ContainsKey(ratezahlStr_))
            {
                einsatzProAuswahl[ratezahlStr_] += Einsatz_;
            }
            else
            {
                einsatzProAuswahl.Add(ratezahlStr_, Einsatz_);
            }

            guthabenPlayer_ -= Einsatz_;
            guthabenBank_ += Einsatz_;

            lbl_Infofeld.Content = $"Einsatz auf {ratezahlStr_}: {einsatzProAuswahl[ratezahlStr_]}";

            int gesamtEinsatz = 0;
            foreach (int einsatz in einsatzProAuswahl.Values)
            {
                gesamtEinsatz += einsatz;
            }
            lbl_einsatz.Text = gesamtEinsatz.ToString();
            txtb_guthaben.Text = guthabenPlayer_.ToString();

            Protokoll("VerlaufSpezifisch", $"Einsatz auf {ratezahlStr_}: ", Einsatz_);
        }

        private void AktiviereAlleButtons()
        {
            AktiviereButtonsInContainer(this);
        }

        private void AktiviereButtonsInContainer(DependencyObject container)// KI
        {
            int childCount = VisualTreeHelper.GetChildrenCount(container);

            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(container, i);

                if (child is Button button)
                {
                    button.IsEnabled = true;
                }
                else if (child is DependencyObject dependencyObject)
                {
                    AktiviereButtonsInContainer(dependencyObject);
                }
            }
        }

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1";
            AuswahlZahl();


            Test(); ((Button)sender).IsEnabled = false;//ki
        }

        private void btn_2_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "6";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "9";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "12";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "15";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "18";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "21";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "24";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "27";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "30";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "33";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "34";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "35";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "36";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        //--

        //--

        private void btn_34_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "34_35";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_35_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "35_36";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_31_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31_32";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_32_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32_33";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_31_32_34_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31_32_34_35";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_32_33_35_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32_33_35_36";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_31_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31_34";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_32_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32_35";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_33_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "33_36";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_28_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28_29";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_29_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29_30";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_28_29_31_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28_29_31_32";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_29_30_32_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29_30_32_33";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_28_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28_31";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_29_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29_32";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_30_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "30_33";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_25_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_26";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_26_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26_27";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_25_26_28_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_26_28_29";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_26_27_29_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26_27_29_30";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_25_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_28";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_26_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26_29";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_27_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "27_30";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_22_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22_23";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_23_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23_24";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_22_23_25_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22_23_25_26";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_23_24_26_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23_24_26_27";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_22_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22_25";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_23_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23_26";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_24_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "24_27";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_19_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_20";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_20_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20_21";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_19_20_22_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_20_22_23";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_20_21_23_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20_21_23_24";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_19_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_22";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_20_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20_23";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_21_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "21_24";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_16_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16_17";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_17_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17_18";

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_16_17_19_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16_17_19_20";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_17_18_20_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17_18_20_21";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_16_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16_19";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_17_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17_20";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_18_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "18_21";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_13_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_14";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_14_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14_15";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_13_14_16_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_14_16_17";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_14_15_17_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14_15_17_18";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_13_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_16";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_14_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14_17";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_15_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "15_18";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_10_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10_11";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_11_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11_12";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_10_11_13_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10_11_13_14";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_11_12_14_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11_12_14_15";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_10_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10_13";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_11_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11_14";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_12_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "12_15";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_7_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7_8";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_8_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8_9";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_7_8_10_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7_8_10_11";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_8_9_11_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8_9_11_12";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_7_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7_10";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_8_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8_11";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_9_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "9_12";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_4_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4_5";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_5_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5_6";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_4_5_7_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4_5_7_8";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_5_6_8_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5_6_8_9";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_4_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4_7";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_5_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5_8";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_6_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "6_9";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_6_3_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3_6";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_2_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_3_2_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_3";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_1_2_4_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2_4_5";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_2_3_5_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_3_5_6";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_1_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_4";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_2_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_5";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_3_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3_6";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_1st12(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2_3_4_5_6_7_8_9_10_11_12";
            AuswahlZahl();

            Test(); ((Button)sender).IsEnabled = false;
        }
        //private void btn_1st12(object sender, RoutedEventArgs e)
        //{
        //    ratezahlStr_ = "1_2_3_4_5_6_7_8_9_10_11_12";
        //    AuswahlZahl();

        //    AuswahlZahl();//doppelte vermeiden
        //    if (auswahlZahlen.ContainsKey(ratezahlStr_))
        //    {
        //        mengeUnderscore = 1;

        //        GesEinsatz_ += Einsatz_;
        //        guthabenPlayer_ -= Einsatz_;


        //        guthabenBank_ += Einsatz_;
        //        lbl_einsatz.Text = GesEinsatz_.ToString(); txtb_guthaben.Text = guthabenPlayer_.ToString();

        //        Protokoll("VerlaufSpezifisch", "Einsatz: ", Einsatz_);
        //    }
        //    else
        //    {
        //        auswahlZahlen.Add(ratezahlStr_, mengeUnderscore);
        //    }
        //}

        private void btn_2nd12(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_14_15_16_17_18_19_20_21_22_23_24";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_3rd12(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_26_27_28_29_30_31_32_33_34_35_36";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_1_18(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18";
            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_19_36(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_20_21_22_23_24_25_26_27_28_29_30_31_32_33_34_35_36";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_0_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "0";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_rot_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "rot";
            checkFarbe = true;
            checkRot = true;

            AuswahlZahl();//doppelte vermeiden
            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_schwarz_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "schwarz";
            checkFarbe = true;
            checkRot = false;

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_3_2_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3_2_1";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_6_5_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "6_5_4";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_9_8_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "9_8_7";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_12_11_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "12_11_10";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_15_14_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "15_14_13";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_18_17_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "18_17_16";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_21_20_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "21_20_19";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_24_23_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "24_23_22";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_27_26_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "27_26_25";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_30_29_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "30_29_28";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_33_32_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "33_32_31";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_36_35_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "36_35_34";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_2to1(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_4_7_10_13_16_19_22_25_28_31_34";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_2to2(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_5_8_11_14_17_20_23_26_29_32_35";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_2to3(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3_6_9_12_15_18_21_24_27_30_33_36";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_Even(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_4_6_8_10_12_14_16_18_20_22_24_26_28_30_32_34_36";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_Odd(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "0_1_3_5_7_9_11_13_15_17_19_21_23_25_27_29_31_33_35";

            AuswahlZahl();//doppelte vermeiden

            Test(); ((Button)sender).IsEnabled = false;
        }

        private void btn_beenden_Click(object sender, RoutedEventArgs e)
        {
            this.Content = erfolgsfenster;
            erfolgsfenster.Abbruch();
            //erfolgsfenster_BJ.lbl_maxEinsatz.Content = "Maximaler Einsatz betrug: " + game_.guthabenPlayerMax_;
        }

        private void btn_regel_Click(object sender, RoutedEventArgs e)
        {
            Regelwerke regelwerke = new Regelwerke();
            regelwerke.Show();
        }

        private void btn_Protokoll_Click(object sender, RoutedEventArgs e)
        {
            string ordnerpfad = Path.GetDirectoryName(pfad);

            try
            {
                Process.Start("explorer.exe", ordnerpfad);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Öffnen des Ordners!\n" + ex.Message);
            }
        }

        public void Protokoll(string dateiname, string status, double gewinn)
        {
            //Prolog: Gezogene Karte mitschreiben
            pfad = Path.Combine(Environment.CurrentDirectory, dateiname + ".txt");

            StreamWriter sw = null;
            FileStream fs = null;

            try
            {
                //Dateien/Internet/ex. Quelle immer Try|Catch-Block
                if (File.Exists(pfad))
                {
                    fs = new FileStream(pfad, FileMode.Append);
                }
                else
                {
                    fs = new FileStream(pfad, FileMode.Create);
                }
                //Schreib-/Leseköpfe
                sw = new StreamWriter(fs);

                //Speichert im "Puffer", bis Flush (gespült wird)
                sw.WriteLine($"{status}{gewinn}");

                //"Schreiben ins Dokument"
                sw.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private void btn_MusikRegelung_Click(object sender, RoutedEventArgs e)
        {
            if (!checkMusik)
            {
                btn_musikregeler.ImageSource = new BitmapImage(new Uri($"./Bilder_Roulette/mute.png", UriKind.Relative));
                player.Volume = 0;
                checkMusik = true;
            }
            else
            {
                btn_musikregeler.ImageSource = new BitmapImage(new Uri($"./Bilder_Roulette/laut.png", UriKind.Relative));
                player.Volume = 0.2;
                checkMusik = false;
            }
        }
    }
}
