using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SortApp
{
    internal class Sorter
    {
        private AnimationSorting animationSorting;
        private List<Label> numBlocks;
        private double speedFactor; // Speed factor for sorting animations
        private int currentI = 0; // Current 'i' index for sorting progress
        private int currentJ = 0; // Current 'j' index for sorting progress
        private bool isSortingChange = false; // Flag to indicate if sorting algorithm should stop
        private bool isSortingPaused = false; // Flag to indicate if sorting is paused

        // Constructor for the Sorter class
        public Sorter(List<Label> numBlocks, double speedFactor)
        {
            this.numBlocks = numBlocks;
            this.speedFactor = speedFactor;
            animationSorting = new AnimationSorting(numBlocks);
        }

        // Method to pause or resume sorting
        public void SortingPaused(bool isSortingPaused)
        {
            this.isSortingPaused = isSortingPaused;
        }

        // Method to change the speed factor of sorting animations
        public void SortingChange(bool isSortingChange)
        {
            this.isSortingChange = isSortingChange;
        }

        // Method to change the duration of sorting animations
        public void СhangeDuration(double speedFactor)
        {
            this.speedFactor = speedFactor;
        }

        // Method to perform Bubble Sort with animations
        public async void BubbleSort()
        {
            int n = numBlocks.Count;
            for (int i = currentI; i < n - 1; i++)
            {
                for (int j = currentJ; j < n - i - 1; j++)
                {
                    if (isSortingChange)
                    {
                        return;
                    }
                    if (isSortingPaused)
                    {
                        currentI = i;
                        currentJ = j;
                        return; 
                    }
                    double val1 = Convert.ToDouble(numBlocks[j].Content);
                    double val2 = Convert.ToDouble(numBlocks[j + 1].Content);
                    if (val1 > val2)
                    {
                        numBlocks[j].Background = Brushes.HotPink;
                        await Task.Delay(200);
                        numBlocks[j + 1].Background = Brushes.HotPink;
                        await animationSorting.SwapWithAnimationAsync(j, j + 1, speedFactor);      
                        await Task.Delay((int)(speedFactor * 800 - 500));
                        numBlocks[j].Background = Brushes.MediumPurple;
                        numBlocks[j + 1].Background = Brushes.MediumPurple;
                    }
                }
            }
            currentI = 0;
            currentJ = 0;
        }

        // Method to perform Comb Sort with animations
        public async void CombSort()
        {
            int n = numBlocks.Count;
            int gap = currentJ > 0 ? currentJ : n;
            bool swapped = true;

            while (gap > 1 || swapped)
            {
                if (isSortingChange)
                {
                    return;
                }
                if (isSortingPaused)
                {
                    currentJ = gap;
                    return;
                }
                gap = (gap * 10) / 13;
                if (gap < 1)
                    gap = 1;
                swapped = false;

                for (int i = 0; i < n - gap; i++)
                {
                    if (isSortingChange)
                    {
                        return;
                    }
                    if (isSortingPaused)
                    {
                        currentI = i;
                        currentJ = gap;
                        return;
                    }
                    int j = i + gap;
                    double val1 = Convert.ToDouble(numBlocks[i].Content);
                    double val2 = Convert.ToDouble(numBlocks[j].Content);
                    if (val1 > val2)
                    {
                        numBlocks[i].Background = Brushes.HotPink;
                        await Task.Delay(200);
                        numBlocks[j].Background = Brushes.HotPink;
                        await animationSorting.SwapWithAnimationAsync(i, j, speedFactor);
                        await Task.Delay((int)(speedFactor * 800 - 500));
                        numBlocks[i].Background = Brushes.MediumPurple;
                        numBlocks[j].Background = Brushes.MediumPurple;
                        swapped = true;
                    }
                }
            }
            currentI = 0;
            currentJ = 0;
        }  
    }
}








