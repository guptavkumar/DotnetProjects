using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace WpfTestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Added Comment
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listbox1.Items.Clear(); 
            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 9000000; i++)
            {
                listbox1.Items.Add("List Item "+i);
            }
            stopWatch.Stop();

            lblThreadResult.Content = "Finished in " + (double)stopWatch.ElapsedMilliseconds / 1000 + " Seconds";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");
            listbox1.Items.Clear();
            var stopWatch = Stopwatch.StartNew();

            ThreadStart startThread = new ThreadStart(() =>
                {
                    for (long i = 0; i < 900000000; i++)
                    {
                        listbox1.Items.Add("List Item " + i);
                    }
                    stopWatch.Stop();

                    lblThreadResult.Content = "Finished in " + (double)stopWatch.ElapsedMilliseconds / 1000 + " Seconds";
                });
            Thread t = new Thread(startThread);
            t.Start();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            listbox1.Items.Clear();
            Stopwatch stpWatch = Stopwatch.StartNew();
            //ThreadStart job = new ThreadStart(delegate()
            //{
            //    {
                    for (int i = 0; i < 200000; i++)
                    {
                        listbox1.Dispatcher.Invoke(DispatcherPriority.SystemIdle, new Action(delegate()
                        {
                            listbox1.Items.Add("List item " + i);
                        }));
                    }
            //    };
            //});
            //stpWatch.Stop();
                    listbox1.Dispatcher.Invoke(DispatcherPriority.SystemIdle, new Action(delegate()
               {
               lblThreadResult.Content="Finishid in "+stpWatch.ElapsedMilliseconds/1000+" Seconds";}));
           //Thread th = new Thread(job);
        //   th.Start();
        }

      
    }
}
