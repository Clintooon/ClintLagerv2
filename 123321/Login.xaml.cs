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
using System.Windows.Shapes;

namespace _123321
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    /// <code>string user = "admin"; string passwd = "zaq1234";</code>
    ///Ustawienie wartości zmiennych, jako danych do poprawnego zalogowania
    public partial class Login : Window
    {
        public
            string user = "admin";
            string passwd = "zaq1234";

        /// <summary>
        /// Inicjalizacja Formularza Logowania
        /// </summary>
        /// <code>InitializeComponent();</code> Inicjalizacja
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda odpowiadająca za działanie przycisku Log In i poprawne zalogowanie się użytkownika.
        /// Gdy dane są błędne wyświetla się stosowny komunikat.
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event Executed wskazuje na zdarzenie, które jest kierowane i wykonane</param>
        /// <code>if (LoginTextBox.Text == user && PassTextBox.Text == paswd)</code> Warunek do poprawnego zalogowania
        /// <code>this.Close; MainWindow mw = new MainWindow(); mw.Show</code>Zamknięcie logowania, utworzenie obiektu głównej klasy programu, otwarcie głównego okna programu
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == user && PassTextBox.Text == passwd)
            {
                this.Close();
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else
                MessageBox.Show("Niepoprawne Dane Logowania!");
        }

        /// <summary>
        /// Metoda odpowiadająca za działanie przycisku Exit czyli wyjście z programu konkretniej z okna logowania.
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event Executed wskazuje na zdarzenie, które jest kierowane i wykonane</param>
        /// <code>this.Close()</code>Zdarzenie odpowiadające za wyłączenie programu na etapie logowania
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
