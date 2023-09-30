using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SortApp
{
    internal class FillBlock
    {
        private Canvas container; // The canvas where labels will be placed
        private List<Label> labelList; // List to keep track of the labels
        private Random random = new Random();

        // Constructor for the FillBlock class
        public FillBlock(Canvas container, List<Label> labelList) 
        {
            this.container = container;
            this.labelList = labelList;
        }

        // Method to fill the canvas with blocks
        public void FillBlocks (int count)
        {
            container.Children.Clear(); 
            labelList.Clear();

            // Calculate the size of each label based on the canvas dimensions
            double labelSize = (container.ActualHeight + container.ActualWidth)*0.044 ;
            for (int i = 0; i < count; i++)
            {
                // Create a new label
                Label newLabel = new Label
                {
                    Content = random.Next(1, 101).ToString(), 
                    Width = labelSize, 
                    Height = labelSize, 
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Background = Brushes.MediumPurple,
                    BorderThickness = new Thickness(3),
                    FontSize = (container.ActualHeight + container.ActualWidth)*0.015
                };

                // Position the label within the canvas 
                Canvas.SetLeft(newLabel, container.Children.Count*labelSize);
                Canvas.SetTop(newLabel, labelSize / 2 - labelSize / 2);

                container.Children.Add(newLabel); 
                labelList.Add(newLabel);
            }
        }
    }
}
