using ConwaysLife.Models;
using ConwaysLife.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ConwaysLife
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private Task task;
        private CancellationTokenSource cts;
        public MainWindow()
        {
            InitializeComponent();
            var b = new Board();
            cellControl.DataContext = b;
        }
        
        private void RandomButtonClick(object sender, RoutedEventArgs e)
        {
            var b= cellControl.DataContext as Board;
            b.SetCellsLifeRandom();
        }
        private void PauseButtonClick(object sender, RoutedEventArgs e)
        {
            if(!(task?.IsCanceled??true))
            {
                cts.Cancel();
                task = null;
            }
        }
        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            var b = cellControl.DataContext as Board;
            if(task==null)
            {
                cts = new CancellationTokenSource();
                task = Task.Run(() =>
                {
                    try
                    {
                        while (true)
                        {
                            Dispatcher.Invoke(() =>
                            {

                                b.SetCellsLifeNext();

                            });
                            Task.Delay(20).Wait();
                            cts.Token.ThrowIfCancellationRequested();
                        }
                    }
                    catch (OperationCanceledException)
                    {

                        return;
                    }


                }, cts.Token);
            }
          
        }

        private void ContentControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            var cell = frameworkElement.DataContext as Cell;
            cell.IsAlive = !cell.IsAlive; 
        }

        private void ContentControl_MouseEnter(object sender, MouseEventArgs e)
        {
           
            if (e.LeftButton== MouseButtonState.Pressed)
            {
                var frameworkElement = sender as FrameworkElement;
                var cell = frameworkElement.DataContext as Cell;
                cell.IsAlive = !cell.IsAlive;
            }    
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            var b = cellControl.DataContext as Board;
            b.SetCellsLifeDead();
        }
    }
}
