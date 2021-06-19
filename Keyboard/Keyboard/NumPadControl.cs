using Keyboard.Enums;
using Keyboard.Function;
using Keyboard.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keyboard
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Keyboard"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Keyboard;assembly=Keyboard"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = "MAIN_Grid", Type = typeof(Grid))]
    public class NumPadControl : Control
    {
        static NumPadControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumPadControl), new FrameworkPropertyMetadata(typeof(NumPadControl)));
        }

        public NumPadControl()
        {
            BuildNumpad();
        }

        Grid BaseGrid;
        private Grid _numpadGrid;
        private static int NAMPAD_ROW = 4;
        private static int NAMPAD_cOLUMN = 3;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if(GetTemplateChild("MAIN_Grid") is Grid baseGrid)
            {
                BaseGrid = baseGrid;
                BaseGrid.Children.Add(_numpadGrid);
            }
        }

        private void BuildNumpad()
        {
            _numpadGrid = new Grid();
            BuildGrid(_numpadGrid, NAMPAD_ROW, NAMPAD_cOLUMN);
            uint virtualNumber = 0x31;

            for (int i = NAMPAD_ROW-2; i >= 0; i--)
            {
                for (int j = 0; j < NAMPAD_cOLUMN; j++)
                {
                    RepeatButton repeatButton = new RepeatButton()
                    {
                        Content = Convert.ToChar(virtualNumber),
                        DataContext = (VirtualKeyShort)virtualNumber,
                        Margin = new Thickness(3),
                        Focusable = false
                    };

                    repeatButton.Click += RepeatButton_Click;
                    Grid.SetRow(repeatButton,i);
                    Grid.SetColumn(repeatButton, j);
                    _numpadGrid.Children.Add(repeatButton);
                    virtualNumber++;
                }
            }

            RepeatButton repeatButton1 = new RepeatButton()
            {
                Content = Convert.ToChar(0x30),
                DataContext = (VirtualKeyShort)(0x30),
                Margin = new Thickness(3),
                Focusable = false
            };
            repeatButton1.Click += RepeatButton_Click;
            Grid.SetRow(repeatButton1, NAMPAD_ROW-1);
            Grid.SetColumnSpan(repeatButton1, 2);
            _numpadGrid.Children.Add(repeatButton1);

            RepeatButton repeatButton2 = new RepeatButton()
            {
                Content = ".",
                DataContext = VirtualKeyShort.OEM_PERIOD,
                Margin = new Thickness(3),
                Focusable = false
            };

            repeatButton2.Click += RepeatButton_Click;
            Grid.SetRow(repeatButton2, NAMPAD_ROW - 1);
            Grid.SetColumn(repeatButton2, NAMPAD_cOLUMN-1);
            _numpadGrid.Children.Add(repeatButton2);
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            RepeatButton button = sender as RepeatButton;
            INPUT[] Inputs = new INPUT[1];
            INPUT Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wVk = (VirtualKeyShort)button.DataContext;
            Inputs[0] = Input;
            Win32Func.SendInput(1, Inputs, INPUT.Size);
        }

        private void BuildGrid(Grid grid,int row, int column)
        {
            for (int i = 0; i < row; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < column; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
    }
}
