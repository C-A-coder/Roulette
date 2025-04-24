using Roulette;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Roulette
{
    /// <summary>
    /// </summary>
    public partial class Erfolgsfenster : Page
    {
        MainWindow mw_ = null;
        public Erfolgsfenster(MainWindow mw)
        {
            InitializeComponent();
            mw_ = mw;
        }

        public void Abbruch()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string filePath = null;
            mw_.Protokoll("VerlaufSpezifisch", "Beendet mit: ", mw_.guthabenPlayer_);

            if (mw_.guthabenPlayer_ > 1000) //Ursprungswert
            {
                lbl_ende.Foreground = Brushes.Green;
                lbl_ende.Content = $"Erfolgreich mit {mw_.guthabenPlayer_} und einem Gewinn von {mw_.guthabenPlayer_ - 1000} aufgehört!\n\t\tHöchsteinsatz war {mw_.guthabenPlayerMax_}";

                //___________

                filePath = System.IO.Path.Combine(currentDirectory, "Audio/victory.mp3");
                mw_.ChangeSong(filePath);

                //___________
            }
            else if (mw_.guthabenPlayer_ == 1000)
            {
                lbl_ende.Foreground = Brushes.DarkBlue;
                lbl_ende.Content = $"Sie haben neutral aufgehört!\nNicht wirklich gewonnen, aber auch nichts verloren! \n\t\tHöchsteinsatz war {mw_.guthabenPlayerMax_}";

                filePath = System.IO.Path.Combine(currentDirectory, "Audio/Forever.mp3");
                mw_.ChangeSong(filePath);
            }
            else
            {
                lbl_ende.Foreground = Brushes.Red;
                lbl_ende.Content = $"Leider mit {mw_.guthabenPlayer_} und einem Verlust von {1000 - mw_.guthabenPlayer_} aufgehört! \n\t\tHöchsteinsatz war {mw_.guthabenPlayerMax_}";


                currentDirectory = System.IO.Directory.GetCurrentDirectory();
                filePath = System.IO.Path.Combine(currentDirectory, "Audio/SchindlerSoundtrack.mp3");
                mw_.ChangeSong(filePath);
                //___________
            }
        }

        private void btn_schließen_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("versprochenes Protokoll unter: \n\n" + game_.pfad);
            Application.Current.Shutdown();
        }
    }
}
