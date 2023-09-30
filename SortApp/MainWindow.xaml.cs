using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Xml.Linq;

namespace SortApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FillBlock fillBlock;
        private Sorter sorter;
        private static List<Label> numBlocks = new List<Label>();
        private double speedFactor;
        private bool isSortingPaused;
        private bool firstStart = true;

        public MainWindow()
        {
            InitializeComponent();

            fillBlock = new FillBlock(canvas, numBlocks);

            sorter = new Sorter(numBlocks, speedFactor);

            sortingSelection.SelectedIndex = 0;

        }

        // Event handler for the number of elements slider value change
        private void sliderNam_of_Elem_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int sliderValue = (int)sliderNam_of_Elem.Value;

            namOfElem.Text = sliderValue.ToString();
            if (canvas != null)
            {
                fillBlock.FillBlocks(sliderValue);
                sorter.SortingChange(true);
                firstStart = true; 
            }
        }

        // Event handler for the speed slider value change
        private void sliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedFactor = (4 - sliderSpeed.Value) * 0.75;

            if (sorter != null)
            {
                sorter.СhangeDuration(speedFactor);
                
            }
        }

        // Event handler for the "Play" button click
        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            if (isSortingPaused)
            {
                isSortingPaused = false;
                sorter.SortingPaused(isSortingPaused);
                sorter.SortingChange(false);
                if (sortingSelection.SelectedIndex == 0)
                {
                    sorter.BubbleSort();
                }
                if (sortingSelection.SelectedIndex == 1)
                {
                    sorter.CombSort();
                }
                               
            }
            else if (firstStart)
            {          
                sorter.SortingChange(false);
                firstStart = false;
                if (sortingSelection.SelectedIndex == 0)
                {
                    sorter.BubbleSort();
                }
                if (sortingSelection.SelectedIndex == 1)
                {
                    sorter.CombSort();
                }
            }
        }

        // Event handler for the "Pause" button click
        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            if (!isSortingPaused)
            {
                isSortingPaused = true;
                sorter.SortingPaused(isSortingPaused);
            }     
        }

        // Event handler for the "Build" button click
        private void Button_Click_build(object sender, RoutedEventArgs e)
        {
            fillBlock.FillBlocks((int)sliderNam_of_Elem.Value);
            sorter.SortingChange(true);
            firstStart = true;
        }
    }
}
