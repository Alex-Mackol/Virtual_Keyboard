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
using System.Windows.Media;

namespace Keyboard
{
    [TemplatePart(Name = "PART_Panel", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_PanelNumpad", Type = typeof(ItemsControl))]
    public class KeyBoardPanel : Control
    {
        public static readonly DependencyProperty KeyDataProperty;
        public static readonly DependencyProperty KeyboardLoyautChangedProperty;

        public static readonly DependencyProperty KeyLoyautsProperty;
        public static readonly DependencyProperty AnimationProperty;

        static KeyBoardPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyBoardPanel), new FrameworkPropertyMetadata(typeof(KeyBoardPanel)));

            KeyDataProperty = DependencyProperty.RegisterAttached("KeyData", typeof(KeyModel), typeof(KeyBoardPanel));
            KeyboardLoyautChangedProperty = DependencyProperty.RegisterAttached("KeyboardLoyautChanged", typeof(KeyboardState), typeof(KeyBoardPanel));

            KeyLoyautsProperty = DependencyProperty.Register("KeyLoyauts", typeof(KeyboardState), typeof(KeyBoardPanel), new PropertyMetadata(KeyboardState.All));
            AnimationProperty = DependencyProperty.Register("Animated", typeof(bool), typeof(KeyBoardPanel), new PropertyMetadata(true));

        }


        public static void SetKeyData(DependencyObject element, KeyModel keyModel)
        {
            element.SetValue(KeyDataProperty, keyModel);
        }

        public static KeyModel GetKeyData(DependencyObject element)
        {
            return (KeyModel)element.GetValue(KeyDataProperty);
        }

        public static void SetKeyboardLoyautChanged(DependencyObject element, KeyboardState state)
        {
            element.SetValue(KeyboardLoyautChangedProperty, state);
        }

        public static KeyboardState GetKeyboardLoyautChanged(DependencyObject element)
        {
            return (KeyboardState)element.GetValue(KeyboardLoyautChangedProperty);
        }

        internal void GotFocusEvent(object sender, RoutedEventArgs e)
        {
            if(sender is TextBox)
            {
                if (PanelForKeyboard != null)
                {
                    PanelForKeyboard.Items.Clear();
                }
                if (PanelNumpad != null)
                {
                    PanelNumpad.Items.Clear();
                }
                KeyLoyauts = GetKeyboardLoyautChanged(sender as DependencyObject);
                InvalidateVisual();
            }
            e.Handled = true;
        }


        private const int FIRSTROW = 13;
        private const int SECONDROW = 13;
        private const int THIRDROW = 12;
        private const int FOURTHROW = 13;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PanelForKeyboard = GetTemplateChild("PART_Panel") as ItemsControl;
            PanelNumpad = GetTemplateChild("PART_PanelNumpad") as ItemsControl;


            if (PanelForKeyboard != null && PanelForKeyboard.Items.Count == 0)
            {
                foreach (var item in KeysListButtons)
                {
                    PanelForKeyboard.Items.Add(item);
                }
            }
            if (PanelNumpad != null && PanelNumpad.Items.Count == 0)
            {
                foreach (var item in NumpadListButtons)
                {
                    PanelNumpad.Items.Add(item);
                }
            }
        }

        public bool Animated
        {
            get { return (bool)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        public KeyboardState KeyLoyauts
        {
            get { return (KeyboardState)GetValue(KeyLoyautsProperty); }
            set { SetValue(KeyLoyautsProperty, value); }
        }



        List<string> lang = new List<string>();


        public KeyBoardPanel()
        {
            MakeKeyListData();
            MakeListButtons();
            MakeNumpad();
            EventManager.RegisterClassHandler(typeof(UIElement), GotKeyboardFocusEvent, new RoutedEventHandler(GotFocusEvent));
        }

        ItemsControl PanelForKeyboard;
        ItemsControl PanelNumpad;

        List<KeyModel> KeysListData { get; set; }
        List<UIElement> KeysListButtons { get; set; }

        List<KeyModel> NumpadListData { get; set; }
        List<UIElement> NumpadListButtons { get; set; }

      

        void MakeKeyListData()
        {
            KeysListData = new List<KeyModel>();
            NumpadListData = new List<KeyModel>();

            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_Q) { IndexRow = 0, IndexColumn = 0 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_W) { IndexRow = 0, IndexColumn = 1 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_E) { IndexRow = 0, IndexColumn = 2 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_R) { IndexRow = 0, IndexColumn = 3 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_T) { IndexRow = 0, IndexColumn = 4 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_Y) { IndexRow = 0, IndexColumn = 5 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_U) { IndexRow = 0, IndexColumn = 6 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_I) { IndexRow = 0, IndexColumn = 7 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_O) { IndexRow = 0, IndexColumn = 8 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_P) { IndexRow = 0, IndexColumn = 9 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_4) { IndexRow = 0, IndexColumn = 10 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_6) { IndexRow = 0, IndexColumn = 11 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.BACK) { DisplayName = "Back", IsDefault = true, IndexRow = 0, IndexColumn = 12 });

            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.TAB, DisplayName = "TAB", IsDefault = true, IndexRow = 1, IndexColumn = 0 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_A){ IndexRow = 1, IndexColumn = 1 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_S) { IndexRow = 1, IndexColumn = 2 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_D) { IndexRow = 1, IndexColumn = 3 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_F) { IndexRow = 1, IndexColumn = 4 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_G) { IndexRow = 1, IndexColumn = 5 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_H) { IndexRow = 1, IndexColumn = 6 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_J) { IndexRow = 1, IndexColumn = 7 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_K) { IndexRow = 1, IndexColumn = 8 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_L) { IndexRow = 1, IndexColumn = 9 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_1) { IndexRow = 1, IndexColumn = 10 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_7) { IndexRow = 1, IndexColumn = 11 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.RETURN) { DisplayName = "Enter", IsDefault = true, IndexRow = 1, IndexColumn = 12 });

            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.LSHIFT, DisplayName = "SHIFT", WidthKoef = 2, IsDefault = true, IndexRow = 2, IndexColumn = 0 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_Z) { IndexRow = 2, IndexColumn = 1 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_X) { IndexRow = 2, IndexColumn = 2 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_C) { IndexRow = 2, IndexColumn = 3 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_V) { IndexRow = 2, IndexColumn = 4 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_B) { IndexRow = 2, IndexColumn = 5 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_N) { IndexRow = 2, IndexColumn = 6 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.KEY_M) { IndexRow = 2, IndexColumn = 7 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_COMMA) { IndexRow = 2, IndexColumn = 8 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_PERIOD) { IndexRow = 2, IndexColumn = 9 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_3) { IndexRow = 2, IndexColumn = 10 });
            KeysListData.Add(new KeyModel(VirtualKeyShort.OEM_2) { IndexRow = 2, IndexColumn = 11 });

            KeysListData.Add(new KeyModel() { DisplayName = "&123", IsDefault = true, IndexRow = 3, IndexColumn = 0 });
            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.SPACE, WidthKoef = 9, IndexRow = 3, IndexColumn = 1 });
            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.LEFT, DisplayName = "<", IsDefault = true, IndexRow = 3, IndexColumn = 2 });
            KeysListData.Add(new KeyModel() { VirtualKey = VirtualKeyShort.RIGHT, DisplayName = ">", IsDefault = true, IndexRow = 3, IndexColumn = 3 });
            KeysListData.Add(new KeyModel() { DisplayName = "En", IsDefault = true, IndexRow = 3, IndexColumn = 4 });
        }

        void MakeListButtons()
        {
            KeysListButtons = new List<UIElement>();
            for (int i = 0; i < KeysListData.Count - 1; i++)
            {
                if (i == FIRSTROW + SECONDROW)
                {
                    KeysListButtons.Add(new ToggleButton()
                    {
                        Content = KeysListData[i].DisplayName,
                        FontSize = 16,
                        Focusable = false
                    });
                    ((ToggleButton)KeysListButtons[i]).Click += KeyBoardPanel_Click;
                }
                else
                {
                    KeysListButtons.Add(new RepeatButton()
                    {
                        Content = KeysListData[i].DisplayName,
                        FontSize = 16,
                        Focusable = false
                    });
                    ((RepeatButton)KeysListButtons[i]).Click += KeyBoardPanel_Click;
                }
                KeyBoardPanel.SetKeyData(KeysListButtons[i], KeysListData[i]);
            }

            KeysListButtons.Add(new ComboBox()
            {
                Focusable = false
            });
            KeyBoardPanel.SetKeyData(KeysListButtons[42], KeysListData[42]);
            ((ComboBox)KeysListButtons.Last()).ItemsSource = SetLanguagesForComboBox();

            var hKL = Win32Func.GetKeyboardLayout(0);
            var languageId = (UInt16)((UInt32)hKL & 0xFFFF);
            CultureInfo languageInfo = new CultureInfo(languageId, false);

            for (int i = 0; i < lang.Count; i++)
            {
                if (lang[i].ToString() == languageInfo.ThreeLetterWindowsLanguageName.ToString())
                {
                    ((ComboBox)KeysListButtons.Last()).SelectedIndex = i;
                }
            }
            ((ComboBox)KeysListButtons.Last()).SelectionChanged += KeyBoardPanel_SelectionChanged;

        }


        private void MakeNumpad()
        {
            NumpadListData = new List<KeyModel>();
            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD7) { IndexRow = 0, IndexColumn = 0 });
            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD8) { IndexRow = 0, IndexColumn = 1 });
            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD9) { IndexRow = 0, IndexColumn = 2 });

            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD4) { IndexRow = 1, IndexColumn = 0 });
            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD5) { IndexRow = 1, IndexColumn = 1 });
            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD6) { IndexRow = 1, IndexColumn = 2 });


            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD1) { IndexRow = 2, IndexColumn = 0 });
            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD2) { IndexRow = 2, IndexColumn = 1 });
            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD3) { IndexRow = 2, IndexColumn = 2 });

            NumpadListData.Add(new KeyModel(VirtualKeyShort.NUMPAD0) { IndexRow = 3, IndexColumn = 0, WidthKoef = 2 });
            NumpadListData.Add(new KeyModel(VirtualKeyShort.DECIMAL) { IndexRow = 3, IndexColumn = 1 });


            NumpadListButtons = new List<UIElement>();
            for (int i = 0; i < NumpadListData.Count; i++)
            {
                NumpadListButtons.Add(new RepeatButton()
                {
                    Content = NumpadListData[i].DisplayName,
                    FontSize = 16,
                    Focusable = false
                });
                ((RepeatButton)NumpadListButtons[i]).Click += NumPad_Click1;
                KeyBoardPanel.SetKeyData(NumpadListButtons[i], NumpadListData[i]);
            }
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
                    SwitchLanguage(languageName);

                    for (int i = 0; i < KeysListButtons.Count - 1; i++)
                    {
                        keyModel = KeyBoardPanel.GetKeyData(KeysListButtons[i] as DependencyObject);
                        if (keyModel.DisplayName != null && keyModel.IsDefault == false)
                        {
                            keyModel.DisplayName = keyModel.VKCodeToUnicodeFromCode((uint)keyModel.VirtualKey, languageName);
                            ((ButtonBase)KeysListButtons[i]).Content = keyModel.DisplayName;
                        }
                        KeyBoardPanel.SetKeyData(KeysListButtons[i], keyModel);
                    }
                    break;
                }
            }
        }

        private void SwitchLanguage(IntPtr languageName)
        {
            var hwnd = Win32Func.GetForegroundWindow();
            Win32Func.PostMessage(hwnd, 0x0050, IntPtr.Zero, languageName);
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

            if (sender is ToggleButton)
            {
                Shift_Click(sender);
            }
            Inputs[0].type = 1;
            Inputs[0].U.ki.wVk = keyModel.VirtualKey;
            Inputs[0].U.ki.dwFlags = KEYEVENTF.KEYDOWN;
            Inputs[1].type = 1;
            Inputs[1].U.ki.wVk = keyModel.VirtualKey;
            Inputs[1].U.ki.dwFlags = KEYEVENTF.KEYUP;
            Win32Func.SendInput(2, Inputs, INPUT.Size);
        }

        private void NumPad_Click1(object sender, RoutedEventArgs e)
        {
            INPUT[] Inputs = new INPUT[2];
            KeyModel keyModel = KeyBoardPanel.GetKeyData(sender as DependencyObject);
            Inputs[0].type = 1;
            Inputs[0].U.ki.wVk = keyModel.VirtualKey;
            Inputs[0].U.ki.dwFlags = KEYEVENTF.KEYUP;
            Inputs[1].type = 1;
            Inputs[1].U.ki.wVk = keyModel.VirtualKey;
            Inputs[1].U.ki.dwFlags = KEYEVENTF.KEYDOWN;
            Win32Func.SendInput(2, Inputs, INPUT.Size);
        }

        private void Shift_Click(object sender)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            KeyModel keyModel;
            for (int i = 0; i < KeysListButtons.Count - 1; i++)
            {
                keyModel = KeyBoardPanel.GetKeyData(KeysListButtons[i] as DependencyObject);

                if (keyModel.DisplayName != null)
                {
                    if (Convert.ToBoolean(toggleButton.IsChecked) && keyModel.IsDefault == false)
                    {
                        //keyModel.DisplayName = keyModel.VKCodeToUnicodeShift((uint)keyModel.VirtualKey, Convert.ToBoolean(toggleButton.IsChecked));
                        keyModel.DisplayName = keyModel.DisplayName.ToUpper();
                        
                    }
                    else if (Convert.ToBoolean(toggleButton.IsChecked) == false && keyModel.IsDefault == false)
                    {
                        keyModel.DisplayName = keyModel.DisplayName.ToLower();
                        //keyModel.DisplayName = keyModel.VKCodeToUnicodeShift((uint)keyModel.VirtualKey, Convert.ToBoolean(toggleButton.IsChecked));
                        
                    }
                    ((ButtonBase)KeysListButtons[i]).Content = keyModel.DisplayName;
                }

                KeyBoardPanel.SetKeyData(KeysListButtons[i], keyModel);
            }
        }
    }
}
