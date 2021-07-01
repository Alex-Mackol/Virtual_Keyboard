using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Keyboard
{
    public class PanelForKeyboard : Panel
    {
        private const int FIRSTROW = 13;
        private const int ROWCOUNT = 4;


        public Thickness KeyMargin
        {
            get { return (Thickness)GetValue(KeyMarginProperty); }
            set { SetValue(KeyMarginProperty, value); }
        }

        private static readonly DependencyProperty KeyMarginProperty =
           DependencyProperty.Register("KeyMargin", typeof(Thickness), typeof(PanelForKeyboard), new PropertyMetadata(new Thickness(1)));

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0;
            double y = 0;
            double heigth = 0;

            List<FrameworkElement> line;
            for (int i = 0; i < ROWCOUNT; i++)
            {
                line = GetLines(i);

                foreach (FrameworkElement child in line)
                {
                    child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
                    x += child.Width + KeyMargin.Left;
                    heigth = child.Height;
                }
                x = 0;
                y += heigth;
            }                                           
            return base.ArrangeOverride(finalSize);
        }

        private double keyWidthDefoult;
        private double keyHeigthDefoult;
        private double width;
        protected override Size MeasureOverride(Size availableSize)
        {
            List<FrameworkElement> line = GetLines(0);

            keyWidthDefoult = (availableSize.Width - KeyMargin.Left * (line.Count+1)) / line.Count;
            keyHeigthDefoult = (availableSize.Height - KeyMargin.Bottom * (ROWCOUNT+1)) / ROWCOUNT;

            foreach (FrameworkElement child in Children)
            {
                width = (KeyBoardPanel.GetKeyData(child).WidthKoef * keyWidthDefoult) - KeyMargin.Left;

                if(width<0)
                {
                    width = 0;
                }
                if(keyHeigthDefoult < 0)
                {
                    keyHeigthDefoult = 0;
                }
                Size size = new Size(width, keyHeigthDefoult);
                child.Width = width;
                child.Height = keyHeigthDefoult;
                child.Measure(size);
            }
            return base.MeasureOverride(availableSize);
        }

        private List<FrameworkElement> GetLines(int rowCCurrent)
        {
            int row;
            List<FrameworkElement> result = new List<FrameworkElement>();
            foreach (FrameworkElement child in Children)
            {
                row = KeyBoardPanel.GetKeyData(child).IndexRow;
                if(row == rowCCurrent)
                {
                    result.Add(child);
                }
            }
            return result;
        }
    }
}
