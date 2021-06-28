using Keyboard.Enums;
using Keyboard.Function;
using Keyboard.Struct;
using Keyboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Keyboard
{
    [TemplatePart(Name = "PART_Stackpanel", Type = typeof(StackPanel))]
    public class KeyBoardPanel : Control
    {
        public static readonly DependencyProperty KeyDataProperty;

        static KeyBoardPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyBoardPanel), new FrameworkPropertyMetadata(typeof(KeyBoardPanel)));

            KeyDataProperty = DependencyProperty.RegisterAttached("KeyDataProperty", typeof(KeyModel), typeof(KeyBoardPanel));
        }

        public static void SetKeyData(DependencyObject element, KeyModel keyModel)
        {
            element.SetValue(KeyDataProperty, keyModel);
        }

        public static KeyModel GetKeyData(DependencyObject element)
        {
            return (KeyModel)element.GetValue(KeyDataProperty);
        }

        private const int FIRSTROW = 13;
        private const int SECONDROW = 13;
        private const int THIRDROW = 12;
        private const int FOURTHROW = 13;

        public Thickness KeyMargin
        {
            get { return (Thickness)GetValue(KeyMarginProperty); }
            set { SetValue(KeyMarginProperty, value); }
        }

        public static readonly DependencyProperty KeyMarginProperty =
            DependencyProperty.Register("KeyMargin", typeof(Thickness), typeof(KeyBoardPanel), new PropertyMetadata(new Thickness(1)));

        List<string> lang = new List<string>();
        private UInt16 _currentLang;

        private double _widthTouchKeyboard = 830;

        public double WidthTouchKeyboard
        {
            get { return _widthTouchKeyboard; }
            set { _widthTouchKeyboard = value; }

        }

        private double _defoultWidth = 60;

        public double DefoultWidth
        {
            get { return _defoultWidth; }
            set { _defoultWidth = value; }

        }

        private double _defoultHeigth = 60;

        public double DefoultHeigth
        {
            get { return _defoultHeigth; }
            set { _defoultHeigth = value; }

        }

        private bool _shiftFlag = false;

        protected bool ShiftFlag
        {
            get { return _shiftFlag; }
            set { _shiftFlag = value; }
        }

        public KeyBoardPanel()
        {
            MakeKeyListData();
            MakeListButtons();
            MakeStackPanels();
        }

        StackPanel BaseStackPanel;
        private StackPanel _line1;
        private StackPanel _line2;
        private StackPanel _line3;
        private StackPanel _line4;


        //ObservableCollection<ObservableCollection<KeyModel>> KeysListData { get; set; }
        //ObservableCollection<ObservableCollection<RepeatButton>> KeysListButtons { get; set; }

        List<KeyModel> KeysListData { get; set; }
        List<UIElement> KeysListButtons { get; set; }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            if (GetTemplateChild("PART_Stackpanel") is StackPanel baseStackPanel)
            {
                BaseStackPanel = baseStackPanel;
                BaseStackPanel.Children.Add(_line1);
                BaseStackPanel.Children.Add(_line2);
                BaseStackPanel.Children.Add(_line3);
                BaseStackPanel.Children.Add(_line4);

            }
        }

        void MakeKeyListData()
        {
            //    KeysListData = new ObservableCollection<ObservableCollection<KeyModel>>();
            //    KeysListData.Add(new ObservableCollection<KeyModel>());
            //    KeysListData.Add(new ObservableCollection<KeyModel>());
            //    KeysListData.Add(new ObservableCollection<KeyModel>());
            //    KeysListData.Add(new ObservableCollection<KeyModel>());

            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_Q));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_W));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_E));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_R));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_T));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_Y));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_U));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_I));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_O));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.KEY_P));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.OEM_4));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.OEM_6));
            //    KeysListData[0].Add(new KeyModel(VirtualKeyShort.BACK));

            //    KeysListData[1].Add(new KeyModel() { VirtualKey = VirtualKeyShort.TAB, DisplayName = "TAB" });
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_A));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_S));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_D));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_F));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_G));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_H));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_J));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_K));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.KEY_L));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.OEM_1));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.OEM_3));
            //    KeysListData[1].Add(new KeyModel(VirtualKeyShort.RETURN));

            //    KeysListData[2].Add(new KeyModel() { VirtualKey = VirtualKeyShort.CAPITAL, DisplayName = "SHIFT", WidthKoef = 2.1});
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.KEY_Z));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.KEY_X));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.KEY_C));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.KEY_V));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.KEY_B));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.KEY_N));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.KEY_M));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.OEM_COMMA));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.OEM_PERIOD));
            //    KeysListData[2].Add(new KeyModel(VirtualKeyShort.OEM_2));
            //    KeysListData[2].Add(new KeyModel() { DisplayName = "/" });

            //    KeysListData[3].Add(new KeyModel() { DisplayName = "&123"});
            //    KeysListData[3].Add(new KeyModel() { VirtualKey = VirtualKeyShort.SPACE, WidthKoef = 9.8 });
            //    KeysListData[3].Add(new KeyModel() { DisplayName = "<" });
            //    KeysListData[3].Add(new KeyModel() { DisplayName = ">" });
            //    KeysListData[3].Add(new KeyModel() { DisplayName = "En" });


            KeysListData = new List<KeyModel>();

            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_Q));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_W));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_E));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_R));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_T));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_Y));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_U));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_I));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_O));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_P));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_4));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_6));
            KeysListData.Add(new KeyModel(VirtualKeyShort.BACK) { DisplayName = "Back", IsDefault = true });

            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.TAB, DisplayName = "TAB", IsDefault = true });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_A));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_S));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_D));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_F));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_G));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_H));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_J));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_K));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_L));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_1));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_7));
            KeysListData.Add(new KeyModel(VirtualKeyShort.RETURN) { DisplayName = "Enter", IsDefault = true });

            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.CAPITAL, DisplayName = "SHIFT", WidthKoef = 2.04, IsDefault = true });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_Z));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_X));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_C));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_V));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_B));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_N));
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_M));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_COMMA));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_PERIOD));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_3));
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_2));

            KeysListData.Add(new KeyModel() { DisplayName = "&123", IsDefault = true });
            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.SPACE, WidthKoef = 9.26 });
            KeysListData.Add(new KeyModel() { DisplayName = "<", IsDefault = true });
            KeysListData.Add(new KeyModel() { DisplayName = ">", IsDefault = true });
            KeysListData.Add(new KeyModel() { DisplayName = "En", IsDefault = true });
        }


        void MakeListButtons()
        {
            KeysListButtons = new List<UIElement>();
            int columnButton = 0;
            for (int i = 0; i < KeysListData.Count - 1; i++)
            {
                if (i == FIRSTROW + SECONDROW)
                {
                    KeysListButtons.Add(new ToggleButton()
                    {
                        Margin = KeyMargin,
                        Content = KeysListData[i].DisplayName,
                        Width = DefoultWidth * KeysListData[i].WidthKoef,
                        Height = DefoultHeigth * KeysListData[i].HeigthKoef,
                        Focusable = false
                    });
                    ((ToggleButton)KeysListButtons[i]).Click += KeyBoardPanel_Click;
                }
                else
                {
                    KeysListButtons.Add(new RepeatButton()
                    {
                        Margin = KeyMargin,
                        Content = KeysListData[i].DisplayName,
                        Width = DefoultWidth * KeysListData[i].WidthKoef,
                        Height = DefoultHeigth * KeysListData[i].HeigthKoef,
                        Focusable = false
                    });
                    ((RepeatButton)KeysListButtons[i]).Click += KeyBoardPanel_Click;
                }
                KeyBoardPanel.SetKeyData(KeysListButtons[i], KeysListData[i]);
            }

            //for (int i = 0; i < 13; i++)
            //{
            //    KeysListButtons.Add(new RepeatButton()
            //    {
            //        Margin = KeyMargin,
            //        Content = KeysListData[i].DisplayName,
            //        Width = DefoultWidth * KeysListData[i].WidthKoef,
            //        Height = DefoultHeigth * KeysListData[i].HeigthKoef,
            //        Focusable = false
            //    });
            //    KeyBoardPanel.SetKeyData(KeysListButtons[i], KeysListData[i]);
            //    ((RepeatButton)KeysListButtons[i]).Click += KeyBoardPanel_Click;
            //}

            //for (int i = 13; i < 26; i++)
            //{
            //    KeysListButtons.Add(new RepeatButton()
            //    {
            //        Margin = KeyMargin,
            //        Content = KeysListData[i].DisplayName,
            //        Width = DefoultWidth * KeysListData[i].WidthKoef,
            //        Height = DefoultHeigth * KeysListData[i].HeigthKoef,
            //        Focusable = false
            //    });
            //    KeyBoardPanel.SetKeyData(KeysListButtons[i], KeysListData[i]);
            //    ((RepeatButton)KeysListButtons[i]).Click += KeyBoardPanel_Click;
            //}

            //KeysListButtons.Add(new ToggleButton()
            //{
            //    Margin = KeyMargin,
            //    Content = KeysListData[26].DisplayName,
            //    Width = DefoultWidth * KeysListData[26].WidthKoef,
            //    Height = DefoultHeigth * KeysListData[26].HeigthKoef,
            //    Focusable = false
            //});
            //KeyBoardPanel.SetKeyData(KeysListButtons[26], KeysListData[26]);
            //((ToggleButton)KeysListButtons[26]).Click += KeyBoardPanel_Click;

            //for (int i = 27; i < 38; i++)
            //{
            //    KeysListButtons.Add(new RepeatButton()
            //    {
            //        Margin = KeyMargin,
            //        Content = KeysListData[i].DisplayName,
            //        Width = DefoultWidth * KeysListData[i].WidthKoef,
            //        Height = DefoultHeigth * KeysListData[i].HeigthKoef,
            //        Focusable = false
            //    });
            //    KeyBoardPanel.SetKeyData(KeysListButtons[i], KeysListData[i]);
            //    ((RepeatButton)KeysListButtons[i]).Click += KeyBoardPanel_Click;
            //}

            //KeysListButtons.Add(new Button()
            //{
            //    Margin = KeyMargin,
            //    Content = KeysListData[38].DisplayName,
            //    Width = DefoultWidth * KeysListData[38].WidthKoef,
            //    Height = DefoultHeigth * KeysListData[38].HeigthKoef,
            //    Focusable = false
            //});
            //KeyBoardPanel.SetKeyData(KeysListButtons[38], KeysListData[38]);

            //KeysListButtons.Add(new RepeatButton()
            //{
            //    Margin = KeyMargin,
            //    Content = KeysListData[39].DisplayName,
            //    Width = DefoultWidth * KeysListData[39].WidthKoef,
            //    Height = DefoultHeigth * KeysListData[39].HeigthKoef,
            //    Focusable = false
            //});
            //KeyBoardPanel.SetKeyData(KeysListButtons[39], KeysListData[39]);
            //((RepeatButton)KeysListButtons[39]).Click += KeyBoardPanel_Click;

            //for (int i = 40; i < 42; i++)
            //{
            //    KeysListButtons.Add(new RepeatButton()
            //    {
            //        Margin = KeyMargin,
            //        Content = KeysListData[i].DisplayName,
            //        Width = DefoultWidth * KeysListData[i].WidthKoef,
            //        Height = DefoultHeigth * KeysListData[i].HeigthKoef,
            //        Focusable = false
            //    });
            //    KeyBoardPanel.SetKeyData(KeysListButtons[i], KeysListData[i]);
            //}

            KeysListButtons.Add(new ComboBox()
            {
                Margin = KeyMargin,
                Width = DefoultWidth * KeysListData[42].WidthKoef,
                Height = DefoultHeigth * KeysListData[42].HeigthKoef,
                Focusable = false
            });
            ((ComboBox)KeysListButtons.Last()).ItemsSource = SetLanguagesForComboBox();

            var hKL = Win32Func.GetKeyboardLayout(0);
            var languageId = (UInt16)((UInt32)hKL & 0xFFFF);
            CultureInfo languageInfo = new CultureInfo(languageId, false);

            for (int i = 0; i < lang.Count; i++)
            {
                if (lang[i].ToString() == languageInfo.ThreeLetterWindowsLanguageName.ToString())
                {
                    ((ComboBox)KeysListButtons.Last()).SelectedIndex = i;
                    _currentLang = languageId;
                }
            }
            ((ComboBox)KeysListButtons.Last()).SelectionChanged += KeyBoardPanel_SelectionChanged;
        }

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        private void KeyBoardPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            uint nElements = Win32Func.GetKeyboardLayoutList(0, null);
            IntPtr[] ids = new IntPtr[nElements];
            Win32Func.GetKeyboardLayoutList(ids.Length, ids);
            KeyModel keyModel;
            foreach (var languageName in ids)
            {
                var languageId = (UInt16)((UInt32)languageName & 0xFFFF);
                CultureInfo languageInfo = new CultureInfo(languageId, false);

                if (lang[((ComboBox)KeysListButtons.Last()).SelectedIndex] == languageInfo.ThreeLetterWindowsLanguageName.ToString())
                {
                    Win32Func.LoadKeyboardLayout(languageInfo.IetfLanguageTag.ToString(), 1);
                    for (int i = 0; i < KeysListButtons.Count - 1; i++)
                    {
                        keyModel = KeyBoardPanel.GetKeyData(KeysListButtons[i] as DependencyObject);
                        if (keyModel.DisplayName != null && keyModel.IsDefault == false)
                        {
                            keyModel.DisplayName = keyModel.VKCodeToUnicodeFromCode((uint)keyModel.VirtualKey,languageName);
                            ((ButtonBase)KeysListButtons[i]).Content = keyModel.DisplayName;
                        }
                        KeyBoardPanel.SetKeyData(KeysListButtons[i], keyModel);
                    }

                    break;
                }
            }
        }


        private List<ComboBoxItem> SetLanguagesForComboBox()
        {
            List<ComboBoxItem> languages = new List<ComboBoxItem>();

            uint nElements = Win32Func.GetKeyboardLayoutList(0, null);
            IntPtr[] ids = new IntPtr[nElements];
            Win32Func.GetKeyboardLayoutList(ids.Length, ids);

            foreach (var languageName in ids)
            {
                var languageId = (UInt16)((UInt32)languageName & 0xFFFF);
                CultureInfo languageInfo = new CultureInfo(languageId, false);

                ComboBoxItem comboBoxItem = new ComboBoxItem();
                lang.Add(languageInfo.ThreeLetterWindowsLanguageName);
                comboBoxItem.Content = languageInfo.ThreeLetterWindowsLanguageName.ToString();
                comboBoxItem.Tag = languageId;

                languages.Add(comboBoxItem);
            }

            return languages;
        }



        private void KeyBoardPanel_Click(object sender, RoutedEventArgs e)
        {
            INPUT[] Inputs = new INPUT[2];
            KeyModel keyModel = KeyBoardPanel.GetKeyData(sender as DependencyObject);
            INPUT Input = new INPUT();

            if (sender is ToggleButton)
            {
                Shift_Click(sender);
            }
                Inputs[0].type = 1;
                Inputs[0].U.ki.wVk = keyModel.VirtualKey;
                Inputs[0].U.ki.dwFlags = KEYEVENTF.KEYUP;
                Inputs[1].type = 1;
                Inputs[1].U.ki.wVk = keyModel.VirtualKey;
                Inputs[1].U.ki.dwFlags = KEYEVENTF.KEYDOWN;
                //Inputs[0] = Input;
                Win32Func.SendInput(2, Inputs, INPUT.Size);
        }

        private void Shift_Click(object sender)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            KeyModel keyModel;
            if (Convert.ToBoolean(toggleButton.IsChecked))
            {
                for (int i = 0; i < KeysListButtons.Count - 1; i++)
                {
                    keyModel = KeyBoardPanel.GetKeyData(KeysListButtons[i] as DependencyObject);
                    if (keyModel.DisplayName != null)
                    {
                        keyModel.DisplayName = keyModel.DisplayName.ToUpper();
                        ((ButtonBase)KeysListButtons[i]).Content = keyModel.DisplayName;
                    }
                    KeyBoardPanel.SetKeyData(KeysListButtons[i], keyModel);
                }
            }
            else
            {
                for (int i = 0; i < KeysListButtons.Count - 1; i++)
                {
                    keyModel = KeyBoardPanel.GetKeyData(KeysListButtons[i] as DependencyObject);
                    if (keyModel.DisplayName != null)
                    {
                        keyModel.DisplayName = keyModel.DisplayName.ToLower();
                        ((ButtonBase)KeysListButtons[i]).Content = keyModel.DisplayName;
                    }
                    KeyBoardPanel.SetKeyData(KeysListButtons[i], keyModel);
                }
            }
        }

        void MakeStackPanels()
        {
            _line1 = new StackPanel();
            _line2 = new StackPanel();
            _line3 = new StackPanel();
            _line4 = new StackPanel();
            _line1.Orientation = Orientation.Horizontal;
            _line2.Orientation = Orientation.Horizontal;
            _line3.Orientation = Orientation.Horizontal;
            _line4.Orientation = Orientation.Horizontal;
            //_line1.Width = 800;
            //_line2.Width = 800;
            //_line3.Width = 800;
            //_line4.Width = 800;

            //_line1.Height = 800;
            //_line2.Height = 800;
            //_line3.Height = 800;
            //_line4.Height = 800;


            for (int i = 0; i < KeysListButtons.Count; i++)
            {
                if (i >= 0 && i < 13)
                {
                    _line1.Children.Add(KeysListButtons[i]);
                }

                else if (i >= 13 && i < 26)
                {
                    _line2.Children.Add(KeysListButtons[i]);
                }

                else if (i >= 26 && i < 38)
                {
                    _line3.Children.Add(KeysListButtons[i]);
                }

                else
                {
                    _line4.Children.Add(KeysListButtons[i]);
                }
            }
        }
    }
}
