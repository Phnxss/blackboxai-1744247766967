using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NeronSettings
{
    public partial class MainWindow : Window
    {
        private readonly Random random = new Random();
        private readonly List<UIElement> particles = new List<UIElement>();
        private readonly DispatcherTimer particleTimer;
        private const int MaxParticles = 50;

        public MainWindow()
        {
            InitializeComponent();
            
            // Initialize particle system
            particleTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };
            particleTimer.Tick += ParticleTimer_Tick;
            particleTimer.Start();
        }

        private void ParticleTimer_Tick(object sender, EventArgs e)
        {
            // Create new particles
            if (particles.Count < MaxParticles && random.NextDouble() < 0.3)
            {
                CreateParticle();
            }

            // Update existing particles
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                var particle = particles[i];
                var transform = (TranslateTransform)((particle as Ellipse)?.RenderTransform);
                if (transform != null)
                {
                    transform.Y -= 1;
                    transform.X += (random.NextDouble() - 0.5) * 2;

                    // Remove particles that have moved off screen
                    if (transform.Y < -10)
                    {
                        ContentArea.Children.Remove(particle);
                        particles.RemoveAt(i);
                    }
                }
            }
        }

        private void CreateParticle()
        {
            var particle = new Ellipse
            {
                Width = random.Next(3, 6),
                Height = random.Next(3, 6),
                Fill = new SolidColorBrush(Color.FromArgb(
                    (byte)random.Next(40, 80),
                    124, 92, 255)),
                RenderTransform = new TranslateTransform
                {
                    X = random.Next(0, (int)ContentArea.ActualWidth),
                    Y = ContentArea.ActualHeight + 10
                }
            };

            particles.Add(particle);
            ContentArea.Children.Add(particle);
            Panel.SetZIndex(particle, -1);
        }

        private void AnimateContentTransition()
        {
            var animation = new DoubleAnimation
            {
                From = 20,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            ContentTransform.BeginAnimation(TranslateTransform.XProperty, animation);

            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            ContentArea.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void OnBasicSettingsClick(object sender, RoutedEventArgs e)
        {
            BasicSettingsPanel.Visibility = Visibility.Visible;
            AnimateContentTransition();
        }

        private void OnAccessibilityClick(object sender, RoutedEventArgs e)
        {
            // Placeholder for Accessibility panel
            AnimateContentTransition();
        }

        private void OnDisplaySettingsClick(object sender, RoutedEventArgs e)
        {
            // Placeholder for Display Settings panel
            AnimateContentTransition();
        }

        private void OnProfileManagementClick(object sender, RoutedEventArgs e)
        {
            // Placeholder for Profile Management panel
            AnimateContentTransition();
        }

        private void OnAdvancedParametersClick(object sender, RoutedEventArgs e)
        {
            // Placeholder for Advanced Parameters panel
            AnimateContentTransition();
        }

        private void OnDataSyncClick(object sender, RoutedEventArgs e)
        {
            // Placeholder for Data Sync panel
            AnimateContentTransition();
        }
    }
}
