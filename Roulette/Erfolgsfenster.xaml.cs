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

namespace Roulette
{
    /// <summary>
    /// Interaktionslogik für Erfolgsfenster.xaml
    /// </summary>
    public partial class Erfolgsfenster : Page
    {
        public Erfolgsfenster()
        {
            InitializeComponent();
            Abbruch();
        }

        private void Abbruch()
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string filePath = null;

            //if (game_.guthabenPlayer_ > 1000) //Ursprungswert
            //{
            //    lbl_ende.Foreground = Brushes.Green;
            //    lbl_ende.Content = $"Erfolgreich mit {game_.guthabenPlayer_} und einem Gewinn von {game_.guthabenPlayer_ - 1000} aufgehört!\nHöchsteinsatz war {game_.guthabenPlayerMax_}";

            //    //___________

            //    filePath = System.IO.Path.Combine(currentDirectory, "Audio/victory.mp3");
            //    mw.ChangeSong(filePath);

            //    //___________
            //}
            //else if (game_.guthabenPlayer_ == 1000)
            //{
            //    lbl_ende.Foreground = Brushes.DarkBlue;
            //    lbl_ende.Content = "Sie haben neutral aufgehört!\nNicht wirklich gewonnen, aber auch nichts verloren!";


            //    filePath = System.IO.Path.Combine(currentDirectory, "Audio/Forever.mp3");
            //    mw.ChangeSong(filePath);
            //}
            //else
            //{
            //    lbl_ende.Foreground = Brushes.Red;
            //    lbl_ende.Content = $"Leider mit {game_.guthabenPlayer_} und einem Verlust von {1000 - game_.guthabenPlayer_} aufgehört! ";


            //    currentDirectory = System.IO.Directory.GetCurrentDirectory();
            //    filePath = System.IO.Path.Combine(currentDirectory, "Audio/SchindlerSoundtrack.mp3");
            //    mw.ChangeSong(filePath);
            //    //___________
            //}
        }

        private void btn_schließen_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
