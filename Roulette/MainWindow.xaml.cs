using Black_Jack;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Roulette
{
    /// <summary>
    /// 🌞testlauf
    /// </summary>
    public partial class MainWindow : Window
    {

        private int Einsatz_ = 0;
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
        int mengeUnderscore = 0;
        bool checkFarbe = false;
        bool checkRot = false;
        public string pfad = null;
        bool checkMusik = true;

        Random rnd = new Random();
        MediaPlayer player = null;
        Erfolgsfenster erfolgsfenster = null;

        public MainWindow()
        {
            InitializeComponent();
            txtb_guthaben.Text = guthabenPlayer_.ToString();
            StartMediaPlayer();
            erfolgsfenster = new Erfolgsfenster(this);

            pfad = Path.Combine(Environment.CurrentDirectory, "VerlaufSpezifisch.txt");
            File.Delete(pfad);
            //pfad = Path.Combine(Environment.CurrentDirectory, "KartenverlaufAllgemein.txt");
            File.Delete(pfad);
            pfad = Path.Combine(Environment.CurrentDirectory, "Geldfluss.txt");
            File.Delete(pfad);
            player.Volume = 0.2;
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
        //////////

        //public int einsatz
        //{
        //    get { return Einsatz_; }
        //    set
        //    {
        //        //Hier wird der Mindesteinsatz von 50 € festgelegt, nicht der Button!
        //        if (value < 50)
        //        {
        //            lbl_Infofeld.Content=("Mindesteinsatz ist: 50 €");
        //        }
        //        else
        //        {
        //            Einsatz_ = value;
        //        }
        //    }
        //}

        private async void Zufallszahl()
        {
            if (Einsatz_ <= 0)
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
                zufallszahl_ = rnd.Next(0, 37);
                lbl_Infofeld.Content = "Zufallszahl: " + zufallszahl_;
                await Task.Delay(150);
            }
            // Generation finale Zufallszahl
            zufallszahl_ = rnd.Next(0, 37);
            lbl_Infofeld.Content = "Zufallszahl: " + zufallszahl_;
            await Task.Delay(2500);
            //zufallszahl_ = 25;

            Overlay.Visibility = Visibility.Hidden;
            Entscheidung();
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
                Einsatz_ += 1000;
                guthabenPlayer_ -= 1000;
                guthabenBank_ += Einsatz_;
                lbl_einsatz.Text = Einsatz_.ToString();
                txtb_guthaben.Text = guthabenPlayer_.ToString();
                Protokoll("VerlaufSpezifisch", "Einsatz: ", Einsatz_);
            }
        }

        private void btn_Coint500_Click(object sender, RoutedEventArgs e)
        {
            if (guthabenPlayer_ - 500 < 0)
            {
                lbl_Infofeld.Content = ("Fehler: Guthaben zu niedrig!");
                Protokoll("VerlaufSpezifisch.txt", "Einsatz: ", Einsatz_);
            }
            else
            {
                Einsatz_ += 500;
                guthabenPlayer_ -= 500;
                guthabenBank_ += Einsatz_;
                lbl_einsatz.Text = Einsatz_.ToString();
                txtb_guthaben.Text = guthabenPlayer_.ToString();
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
                Einsatz_ += 100;
                guthabenPlayer_ -= 100;
                guthabenBank_ += Einsatz_;
                lbl_einsatz.Text = Einsatz_.ToString();
                txtb_guthaben.Text = guthabenPlayer_.ToString();
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
                Einsatz_ += 50;
                guthabenPlayer_ -= 50;
                guthabenBank_ += Einsatz_;
                lbl_einsatz.Text = Einsatz_.ToString();
                txtb_guthaben.Text = guthabenPlayer_.ToString();
                Protokoll("VerlaufSpezifisch", "Einsatz: ", Einsatz_);
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

            //Speicherung von Max-Einsatz
            if (Einsatz_ > guthabenPlayerMax_)
            {
                guthabenPlayerMax_ = Einsatz_;
            }

            foreach (string zahlStr in zahlen)
            {
                if (Int32.TryParse(zahlStr, out int gerateneZahl))
                {
                    if (gerateneZahl == zufallszahl_)
                    {
                        zahlGetroffen = true;
                        break; // Sobald Zahl getroffen, Schleife verlassen
                    }
                    else if (checkFarbe == true)
                    {
                        zahlGetroffen = true;
                        break;
                    }
                }
                else if (checkFarbe || zahlStr == "")//Farbe
                {
                    zahlGetroffen = true;
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
                    guthabenPlayer_ += Einsatz_ + Einsatz_ * 35;//inkl. Rückzahlung Einsatz
                    guthabenBank_ -= Einsatz_ * 35;
                    lbl_Infofeld.Content = $"Jackpot!!! Zahl getroffen. Auszahlung 1:35\n{Einsatz_ * 35}";
                    Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_*35);
                }
                else
                {
                    if (mengeUnderscore == 2 && !checkFarbe)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:17\n{Einsatz_ * 17}";
                        Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_ * 17);
                        guthabenPlayer_ += Einsatz_ + Einsatz_ * 17;//inkl. Rückzahlung Einsatz
                        guthabenBank_ -= Einsatz_ * 17;
                    }
                    else if (mengeUnderscore == 3 && !checkFarbe)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:11\n{Einsatz_ * 11}";
                        Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_ * 11);
                        guthabenPlayer_ += Einsatz_ + Einsatz_ * 11;//inkl. Rückzahlung Einsatz
                        guthabenBank_ -= Einsatz_ * 11;
                    }
                    else if (mengeUnderscore == 4 && !checkFarbe)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:8\n{Einsatz_ * 1}";
                        Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_ * 1);
                        guthabenPlayer_ += Einsatz_ + Einsatz_ * 8;//inkl. Rückzahlung Einsatz
                        guthabenBank_ -= Einsatz_ * 8;
                    }
                    else if (mengeUnderscore == 6 && !checkFarbe)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:5\n{Einsatz_ * 5}";
                        Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_ * 5);
                        guthabenPlayer_ += Einsatz_ + Einsatz_ * 5;//inkl. Rückzahlung Einsatz
                        guthabenBank_ -= Einsatz_ * 5;
                    }
                    else if (mengeUnderscore == 12 && checkFarbe == false)
                    {
                        lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:2\n{Einsatz_ * 2}";
                        Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_ * 2);
                        guthabenPlayer_ += Einsatz_ + Einsatz_ * 2;//inkl. Rückzahlung Einsatz
                        guthabenBank_ -= Einsatz_ * 2;
                    }
                    //else if (mengeUnderscore == 18 || (checkFarbe && checkRot))//+Farbe
                    //{
                    //    lbl_Infofeld.Content = $"Zahl oder Farbe getroffen! Auszahlung 1:1\n{Einsatz * 1}";
                    //    guthabenPlayer += Einsatz * 1;
                    //    guthabenBank -= Einsatz * 1;
                    //    checkFarbe = false;
                    //    checkRot = false;
                    //}
                    else if ((mengeUnderscore == 18 || mengeUnderscore == 19) && (!checkFarbe))//+Farbe+Un/Gerade
                    {
                        bool zahlGefunden = false;
                        foreach (string zahlStr in zahlen)
                        {
                            if (int.TryParse(zahlStr, out int gewaehlteZahl) && gewaehlteZahl == zufallszahl_)
                            {
                                zahlGefunden = true;
                                break;
                            }
                        }

                        if (zahlGefunden && !checkFarbe)
                        {
                            switch (mengeUnderscore)
                            {
                                case 19:
                                    lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:1\n{Einsatz_ * 1}";
                                    Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_ * 1);
                                    guthabenPlayer_ += Einsatz_ * 1;
                                    guthabenBank_ -= Einsatz_ * 1;
                                    Einsatz_ = 0;
                                    lbl_einsatz.Text = Einsatz_.ToString(); // Einsatz zurücksetzen
                                    txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
                                    return;
                                case 18:
                                    lbl_Infofeld.Content = $"Zahl getroffen! Auszahlung 1:1\n{Einsatz_ * 1}";
                                    Protokoll("VerlaufSpezifisch", "Gewonnen! +", Einsatz_ * 1);
                                    guthabenPlayer_ += Einsatz_ * 1;
                                    guthabenBank_ -= Einsatz_ * 1;
                                    Einsatz_ = 0;
                                    lbl_einsatz.Text = Einsatz_.ToString(); // Einsatz zurücksetzen
                                    txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
                                    return;
                            }
                        }
                        else
                        {
                            lbl_Infofeld.Content = $"Leider nicht getroffen.";
                            Protokoll("VerlaufSpezifisch", "Verloren! Zufallszahl: ", zufallszahl_);
                            //guthabenPlayer_ -= Einsatz_; wurde breits abgezogen bei btn_zahl
                            guthabenBank_ += Einsatz_;
                        }
                    }
                    else if (checkRot)
                    {
                        switch (zufallszahl_)//rot
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
                                lbl_Infofeld.Content = $"Farbe getroffen! Auszahlung 1:1\n{Einsatz_ * 1}";
                                Protokoll("VerlaufSpezifisch", "Gewonnen! Farbe: +", Einsatz_ * 1);
                                guthabenPlayer_ += Einsatz_ * 2;
                                guthabenBank_ -= Einsatz_ * 1; //inkl. Rückzahlung Einsatz
                                txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
                                Einsatz_ = 0;
                                lbl_einsatz.Text = Einsatz_.ToString(); // Einsatz zurücksetzen
                                checkFarbe = false;
                                checkRot = false;
                                return;
                            default:
                                lbl_Infofeld.Content = "Nein! Farbe leider nicht getroffen";
                                Protokoll("VerlaufSpezifisch", "Verloren! Zufallszahlfarbe: ", zufallszahl_);
                                //guthabenPlayer_ -= Einsatz_ * 1;//Abzug Einsatz
                                txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
                                guthabenBank_ += Einsatz_ * 1;
                                Einsatz_ = 0;
                                lbl_einsatz.Text = Einsatz_.ToString(); // Einsatz zurücksetzen
                                checkFarbe = false;
                                checkRot = false;
                                return;
                        }
                    }
                    else if (!checkRot)
                    {
                        switch (zufallszahl_)//schwarz
                        {
                            case 2:
                            case 4:
                            case 6:
                            case 8:
                            case 10:
                            case 11:
                            case 13:
                            case 15:
                            case 17:
                            case 20:
                            case 22:
                            case 24:
                            case 26:
                            case 28:
                            case 29:
                            case 31:
                            case 33:
                            case 35:
                                lbl_Infofeld.Content = $"Farbe getroffen! Auszahlung 1:1\n{Einsatz_ * 1}";
                                Protokoll("VerlaufSpezifisch", "Gewonnen! Farbe: +", Einsatz_ * 1);
                                guthabenPlayer_ += Einsatz_ * 2;//inkl. Rückzahlung Einsatz
                                txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
                                guthabenBank_ -= Einsatz_ * 1;
                                Einsatz_ = 0;
                                lbl_einsatz.Text = Einsatz_.ToString(); // Einsatz zurücksetzen
                                checkFarbe = false;
                                checkRot = false;
                                break;
                            default:
                                lbl_Infofeld.Content = "Nein! Farbe leider nicht getroffen";
                                Protokoll("VerlaufSpezifisch", "Verloren! Zufallszahlfarbe: ", zufallszahl_);
                                //guthabenPlayer_ -= Einsatz_ * 1;//Abzug Einsatz
                                txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
                                guthabenBank_ += Einsatz_ * 1;
                                Einsatz_ = 0;
                                lbl_einsatz.Text = Einsatz_.ToString(); // Einsatz zurücksetzen
                                checkFarbe = false;
                                checkRot = false;
                                break;

                        }
                    }
                    else
                    {
                        lbl_Infofeld.Content = $"Nein! Zahl nicht getroffen: {zufallszahl_}";
                        Protokoll("VerlaufSpezifisch", "Verloren! Zufallszahl: ", zufallszahl_);
                        //lbl_Infofeld.Content = "Nein! Farbe leider nicht getroffen";
                        //guthabenPlayer_ -= Einsatz;
                        //guthabenBank += Einsatz;
                        checkFarbe = false;
                        checkRot = false;
                    }
                }

                //Zufallszahl(); // Neue Zufallszahl für die nächste Runde generieren
            }
            else
            {
                lbl_Infofeld.Content = $"Nein! Zahl nicht getroffen: {zufallszahl_}";
                Protokoll("VerlaufSpezifisch", "Verloren! Zufallszahl: ", zufallszahl_);
                //guthabenPlayer_ -= Einsatz;
                //guthabenBank += Einsatz;
                checkFarbe = false;
                checkRot = false;
            }

            txtb_guthaben.Text = guthabenPlayer_.ToString(); // Guthaben aktualisieren
            lbl_einsatz.Text = "0"; // Einsatz zurücksetzen
            Einsatz_ = 0;
            Pleite();
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

        //private void hoverEffekt(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    if(sender is Button geklickterButton)
        //    {
        //        string buttonName = geklickterButton.Name;

        //        //switch (buttonName)
        //        //{
        //        //    case "dreiReihe1":
        //        //        dreiReihe1.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe2":
        //        //        dreiReihe2.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe3":
        //        //        dreiReihe3.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe4":
        //        //        dreiReihe4.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe5":
        //        //        dreiReihe5.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe6":
        //        //        dreiReihe6.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe7":
        //        //        dreiReihe7.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe8":
        //        //        dreiReihe8.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe9":
        //        //        dreiReihe9.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe10":
        //        //        dreiReihe10.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe11":
        //        //        dreiReihe11.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //    case "dreiReihe12":
        //        //        dreiReihe12.Background = Brushes.BlanchedAlmond;
        //        //        break;
        //        //}
        //    }
        //}

        //private void leaveEffekt(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    if (sender is Button geklickterButton)
        //    {
        //        string buttonName = geklickterButton.Name;

        //        switch (buttonName)
        //        {
        //            case "dreiReihe1":
        //                dreiReihe1.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe2":
        //                dreiReihe2.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe3":
        //                dreiReihe3.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe4":
        //                dreiReihe4.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe5":
        //                dreiReihe5.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe6":
        //                dreiReihe6.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe7":
        //                dreiReihe7.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe8":
        //                dreiReihe8.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe9":
        //                dreiReihe9.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe10":
        //                dreiReihe10.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe11":
        //                dreiReihe11.Background = Brushes.Transparent;
        //                break;
        //            case "dreiReihe12":
        //                dreiReihe12.Background = Brushes.Transparent;
        //                break;
        //        }
        //    }
        //}

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1";
            AuswahlZahl();
            Zufallszahl();
        }

        private void btn_2_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "36";
            AuswahlZahl(); Zufallszahl();
        }

        //--

        //--

        private void btn_34_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "34_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_35_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31_32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32_33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_32_34_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31_32_34_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_33_35_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32_33_35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_31_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "31_34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_32_35_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "32_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_33_36_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "33_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28_29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29_30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_29_31_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28_29_31_32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_30_32_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29_30_32_33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_28_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "28_31";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_29_32_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "29_32";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_30_33_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "30_33";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26_27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_26_28_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_26_28_29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_27_29_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26_27_29_30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_25_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_28";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_26_29_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "26_29";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_27_30_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "27_30";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22_23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_23_25_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22_23_25_26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_24_26_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23_24_26_27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_22_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "22_25";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_23_26_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "23_26";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_24_27_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "24_27";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20_21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_20_22_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_20_22_23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_21_23_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20_21_23_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_22";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_20_23_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "20_23";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_21_24_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "21_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16_17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_17_19_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16_17_19_20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_18_20_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17_18_20_21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_16_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "16_19";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_17_20_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "17_20";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_18_21_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "18_21";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14_15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_14_16_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_14_16_17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_15_17_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14_15_17_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_13_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_16";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_14_17_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "14_17";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_15_18_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "15_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10_11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_11_13_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10_11_13_14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_12_14_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11_12_14_15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_10_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "10_13";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_11_14_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "11_14";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_12_15_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "12_15";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7_8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8_9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_8_10_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7_8_10_11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_9_11_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8_9_11_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_7_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "7_10";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_8_11_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "8_11";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_9_12_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "9_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4_5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5_6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_5_7_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4_5_7_8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_6_8_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5_6_8_9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_4_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "4_7";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_5_8_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "5_8";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_6_9_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "6_9";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_6_3_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3_6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2_1_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3_2_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_3";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1_2_4_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2_4_5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2_3_5_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_3_5_6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_4";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2_5_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_5";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3_6_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3_6";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1st12(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2_3_4_5_6_7_8_9_10_11_12";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2nd12(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "13_14_15_16_17_18_19_20_21_22_23_24";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_3rd12(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "25_26_27_28_29_30_31_32_33_34_35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_1_18(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_19_36(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "19_20_21_22_23_24_25_26_27_28_29_30_31_32_33_34_35_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_0_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "0";
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
            ratezahlStr_ = "3_2_1";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_6_5_4_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "6_5_4";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_9_8_7_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "9_8_7";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_12_11_10_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "12_11_10";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_15_14_13_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "15_14_13";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_18_17_16_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "18_17_16";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_21_20_19_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "21_20_19";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_24_23_22_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "24_23_22";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_27_26_25_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "27_26_25";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_30_29_28_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "30_29_28";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_33_32_31_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "33_32_31";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_36_35_34_Click(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "36_35_34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2to1(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "1_4_7_10_13_16_19_22_25_28_31_34";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2to2(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_5_8_11_14_17_20_23_26_29_32_35";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_2to3(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "3_6_9_12_15_18_21_24_27_30_33_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_Even(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "2_4_6_8_10_12_14_16_18_20_22_24_26_28_30_32_34_36";
            AuswahlZahl(); Zufallszahl();
        }

        private void btn_Odd(object sender, RoutedEventArgs e)
        {
            ratezahlStr_ = "0_1_3_5_7_9_11_13_15_17_19_21_23_25_27_29_31_33_35";
            AuswahlZahl(); Zufallszahl();
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
