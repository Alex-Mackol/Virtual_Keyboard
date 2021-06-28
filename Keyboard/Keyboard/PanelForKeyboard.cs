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
        
        static PanelForKeyboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyBoardPanel), new FrameworkPropertyMetadata(typeof(KeyBoardPanel)));
        }



        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach(UIElement child in InternalChildren)
            {
                double x = 50;
                double y = 50;

                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
            }
            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size panelDesiredSize = new Size();
            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                panelDesiredSize = child.DesiredSize;
            }
            return base.MeasureOverride(availableSize);
        }
    }
}
