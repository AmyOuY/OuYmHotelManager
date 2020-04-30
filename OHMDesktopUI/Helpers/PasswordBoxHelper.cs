using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OHMDesktopUI.Helpers
{
    public class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundedPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, OnBoundPasswordChanged));


        public static string GetBoundPassword(DependencyObject d)
        {
            var box = d as PasswordBox;

            if (box != null)
            {
                box.PasswordChanged -= PasswordChanged;
                box.PasswordChanged += PasswordChanged;
            }

            return (string)d.GetValue(BoundPasswordProperty);
        }


        public static void SetBoundPassword(DependencyObject d, string value)
        {
            if (string.Equals(GetBoundPassword(d), value))
            {
                return;
            }

            d.SetValue(BoundPasswordProperty, value);
        }


        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as PasswordBox;

            if (box == null)
            {
                return;
            }

            box.Password = GetBoundPassword(d);
        } 



        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox password = sender as PasswordBox;

            SetBoundPassword(password, password.Password);

            password.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(password, new Object[] { password.Password.Length, 0 });
        }
    }
}
