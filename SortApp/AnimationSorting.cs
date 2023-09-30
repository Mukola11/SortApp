using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;

namespace SortApp
{
    internal class AnimationSorting
    {
        private List<Label> numBlocks;

        // Constructor for the AnimationSorting class
        public AnimationSorting(List<Label> numBlocks) 
        {
            this.numBlocks = numBlocks;
        }

        // Method to swap two labels with animation
        public async Task SwapWithAnimationAsync(int index1, int index2, double speedFactor)
        {
            Label textBlock1 = numBlocks[index1];
            Label textBlock2 = numBlocks[index2];

            double left1 = Canvas.GetLeft(textBlock1);
            double left2 = Canvas.GetLeft(textBlock2);

            double top = Canvas.GetTop(textBlock1);

            // Define animations for the swap
            DoubleAnimation downAnimation = new DoubleAnimation
            {
                To = top + textBlock1.ActualHeight, 
                Duration = TimeSpan.FromSeconds(0.2 * speedFactor),
                EasingFunction = new QuadraticEase()
            };

            DoubleAnimation rightAnimation = new DoubleAnimation
            {
                To = left1, 
                Duration = TimeSpan.FromSeconds(0.2 * speedFactor),
                EasingFunction = new QuadraticEase()
            };

            DoubleAnimation leftAnimation = new DoubleAnimation
            {
                To = left2, 
                Duration = TimeSpan.FromSeconds(0.2 * speedFactor),
                EasingFunction = new QuadraticEase()
            };

            DoubleAnimation upAnimation = new DoubleAnimation
            {
                To = top, 
                Duration = TimeSpan.FromSeconds(0.2 * speedFactor),
                EasingFunction = new QuadraticEase()
            };

            // Perform animations asynchronously
            await AnimateAsync(textBlock2, Canvas.TopProperty, downAnimation);
            await AnimateAsync2(textBlock1, textBlock2, Canvas.LeftProperty, leftAnimation, rightAnimation);
            await AnimateAsync(textBlock2, Canvas.TopProperty, upAnimation);

            // Swap the labels in the list
            numBlocks[index1] = textBlock2;
            numBlocks[index2] = textBlock1;
        }

        // Helper method to perform a single animation asynchronously
        private Task AnimateAsync(UIElement element, DependencyProperty property, DoubleAnimation animation)
        {
            var tcs = new TaskCompletionSource<bool>();
            animation.Completed += (s, e) => tcs.TrySetResult(true);
            element.BeginAnimation(property, animation);
            return tcs.Task;
        }

        // Helper method to perform two animations asynchronously
        private async Task AnimateAsync2(UIElement element1, UIElement element2, DependencyProperty property,
            DoubleAnimation animation1, DoubleAnimation animation2)
        {
            var tcs1 = new TaskCompletionSource<bool>();
            var tcs2 = new TaskCompletionSource<bool>();

            animation1.Completed += (s, e) => tcs1.TrySetResult(true);
            animation2.Completed += (s, e) => tcs2.TrySetResult(true);

            element1.BeginAnimation(property, animation1);
            element2.BeginAnimation(property, animation2);

            await Task.WhenAll(tcs1.Task, tcs2.Task);
        }
    }
}
