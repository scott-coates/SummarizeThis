using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Summarizer.Phone
{
    public class PhoneSlider : Slider
    {
        public PhoneSlider()
        {
            SizeChanged += new SizeChangedEventHandler(PhoneSlider_SizeChanged);
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Value = Math.Round(newValue);
        }

        void PhoneSlider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width > 0 && e.NewSize.Height > 0)
            {
                Rect clipRect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
                if (Orientation == Orientation.Horizontal)
                {
                    clipRect.X -= 12;
                    clipRect.Width += 24;
                    //object margin = Resources["PhoneHorizontalMargin"];
                    //if (margin != null)
                    //{
                    //    Margin = (Thickness)margin;
                    //}
                }
                else
                {
                    clipRect.Y -= 12;
                    clipRect.Height += 24;
                    //object margin = Resources["PhoneVerticalMargin"];
                    //if (margin != null)
                    //{
                    //    Margin = (Thickness)margin;
                    //}
                }

                this.Clip = new RectangleGeometry() { Rect = clipRect };
            }
        }
    }
}
