using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace _123321
{
    /// <summary>
    /// Przestrzeń nazw programu.
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         NorthwindEntities2 context = new NorthwindEntities2();
        CollectionViewSource prodViewSource;

        /// <summary>
        /// Załadowanie Głównego okna programu.
        /// </summary>
        /// <code>Login lg = new Login()</code>utworzenie nowego obiektu Login do otwarcia Okna logowania
        /// <code>lg.Show()</code>Wyświetlenie formularza logowania
        /// <code>this.Hide()</code>Ukrycie głównego okna
        public MainWindow()
        {
            InitializeComponent();
            prodViewSource = ((CollectionViewSource)(FindResource("productsViewSource")));
            DataContext = this;

            this.Hide();
            Login lg = new Login();
            lg.Show();

        }

        /// <summary>
        /// Odnalezienie źródła danych i wyświetlenie go.
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event wskazuje na zdarzenie, które jest kierowane</param>
        /// <code>cotext.Products.Load()</code>Załadowanie bazy produktów
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource productsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("productsViewSource")));
            context.Products.Load();
            prodViewSource.Source = context.Products.Local;
        }

        /// <summary>
        /// Metoda przenosząca nas do ostatniego elementu bazy danych po wciśnięci przycisku Last.
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event Executed wskazuje na zdarzenie, które jest kierowane i wykonane</param>
        /// <code>prodViewSource.View.MoveCurrentToLast()</code>Przesunięcie do ostatniego elementu bazy danych
        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            prodViewSource.View.MoveCurrentToLast();
        }

        /// <summary>
        /// Metoda przenosząca nas do następnego elementu bazy danych po wciśnięciu przycisku Next.
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event Executed wskazuje na zdarzenie, które jest kierowane i wykonane</param>
        /// <code>prodViewSource.View.MoveCurrentToNext()</code>Przesunięcie do Następnego elementu bazy danych
        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            prodViewSource.View.MoveCurrentToNext();
        }

        /// <summary>
        /// Metoda odwołująca się do przycisku Add, czyści TextBoxy i umożliwia wprowadzanie danych
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event Executed wskazuje na zdarzenie, które jest kierowane i wykonane</param>
        /// <code>foreach (var child in productsGrid.Children)
        ///          {
              ///  var tb = child as TextBox;
              ///  if (tb != null)
              ///  {
               ///     tb.Text = "";
              ///  }
            ///}</code>Pętla, która sprawdza obecność elemntu w głównej bazie
            private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            productsGrid.Visibility = Visibility.Visible;
            foreach (var child in productsGrid.Children)
            {
                var tb = child as TextBox;
                if (tb != null)
                {
                    tb.Text = "";
                }
            }
        }

        /// <summary>
        /// Metoda odpowiadająca za wprowadzenie zmian po wciśnięciu przycisku Update. 
        /// Metoda Sprawdza TextBoxy, konwertuje w nich dane na odpowiednio , tekstowy-> tekstowy , liczbowy-> liczbowy. 
        /// Metoda sprawdza poprawność wprowadzanych danych.
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event Executed wskazuje na zdarzenie, które jest kierowane i wykonane</param>
        /// <code>Products newProducts = new Products()</code>Utworzenie nowego obiektu klasy Products
        /// <code>try
        ///    {
        ///        if (productsGrid.IsVisible)
        ///        {
        ///            Products newProducts = new Products()
        ///            {
        ///                ProductName = productNameTextBox.Text,
        ///                UnitPrice = Int32.Parse(unitPriceTextBox.Text),
       ///                 UnitsInStock = Int16.Parse(unitsInStockTextBox.Text),
        ///                UnitsOnOrder = Int16.Parse(unitsOnOrderTextBox.Text),
        ///                ReorderLevel = Int16.Parse(reorderLevelTextBox.Text),
        ///            };
        ///        }
        ///     }
        ///    catch
        ///    {
        ///        MessageBox.Show(message, title, button, Image);
        ///    }</code> Pętla która sprawdza poprawność wprowadzanych danych
        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

            string message = "Zły format wprowadzonych danych!\n" + "Sprawdź zaznaczone na Czerwono Pole!";
            string title = "BŁĄD 01";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage Image = MessageBoxImage.Error;

            try
            {
                if (productsGrid.IsVisible)
                {
                    Products newProducts = new Products()
                    {
                        ProductName = productNameTextBox.Text,
                        UnitPrice = Int32.Parse(unitPriceTextBox.Text),
                        UnitsInStock = Int16.Parse(unitsInStockTextBox.Text),
                        UnitsOnOrder = Int16.Parse(unitsOnOrderTextBox.Text),
                        ReorderLevel = Int16.Parse(reorderLevelTextBox.Text),
                    };
                }
            }
            catch
            {
                MessageBox.Show(message,title,button,Image);
            }
            context.SaveChanges();
            prodViewSource.View.Refresh();
        }

        /// <summary>
        /// Metoda Anulująca wpisywane dane by w bazie danych nie zaszły zmiany.
        /// </summary>
        /// <param name="sender">Parametr sender zawiera odniesienie do kontrolki/obiektu, który wywołał zdarzenie</param>
        /// <param name="e">Routed Event Executed wskazuje na zdarzenie, które jest kierowane i wykonane</param>
        /// <code>productNameTextBox.Text = " "</code>Fragment czyszczący dany TextBox
        private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            productNameTextBox.Text = " ";
            reorderLevelTextBox.Text = " ";
            unitPriceTextBox.Text = " ";
            unitsInStockTextBox.Text = " ";
            unitsOnOrderTextBox.Text = " ";

            prodViewSource.View.Refresh();
            productsGrid.Visibility = Visibility.Collapsed;
            productsGrid.Visibility = Visibility.Visible;
        }
    }
}
