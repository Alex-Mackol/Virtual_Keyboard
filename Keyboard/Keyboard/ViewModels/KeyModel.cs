using Keyboard.Enums;
using Keyboard.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.ViewModels
{
    public class KeyModel : INotifyPropertyChanged
    {
        private double _widthKoef = 1;
        private double _heigthKoef = 1;
        private int _indexRow;
        private int _indexColumn;
        private VirtualKeyShort _virtualKey;
        private string _displayName;
        private bool _isDefault = false;

        public KeyModel()
        {

        }

        public KeyModel(VirtualKeyShort virtualKey)
        {
            _virtualKey = virtualKey;
            _displayName = VKCodeToUnicode((uint)virtualKey);
        }

        public KeyModel(VirtualKeyShort virtualKey, string name)
        {
            _virtualKey = virtualKey;
            _displayName = name;
        }

        public double WidthKoef
        {
            get => _widthKoef;
            set
            {
                _widthKoef = value;
                OnPropertyChanged(nameof(WidthKoef));
            }
        }

        public double HeigthKoef
        {
            get => _heigthKoef;
            set => _heigthKoef = value;
        }

        public int IndexRow
        {
            get => _indexRow;
            set => _indexRow = value;
        }

        public int IndexColumn
        {
            get => _indexColumn;
            set => _indexColumn = value;
        }

        public bool IsDefault
        {
            get => _isDefault;
            set => _isDefault = value;
        }

        public VirtualKeyShort VirtualKey
        {
            get => _virtualKey;
            set => _virtualKey = value; 
        }

        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (value!= _displayName)
                {
                    _displayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        public string VKCodeToUnicode(uint VKCode)
        {
           StringBuilder sbString = new StringBuilder();

            byte[] bKeyState = new byte[256];
            bool bKeyStateStatus = Win32Func.GetKeyboardState(bKeyState);
            if (!bKeyStateStatus)
                return "";
            uint lScanCode = Win32Func.MapVirtualKey(VKCode, 0);
            IntPtr HKL = Win32Func.GetKeyboardLayout(0);

            Win32Func.ToUnicodeEx(VKCode, lScanCode, bKeyState, sbString, (int)5, (uint)0, HKL);
            return sbString.ToString();
        }


        public string VKCodeToUnicodeFromCode(uint VKCode, IntPtr HKL)
        {
            StringBuilder sbString = new StringBuilder();

            byte[] bKeyState = new byte[256];
            bool bKeyStateStatus = Win32Func.GetKeyboardState(bKeyState);
            if (!bKeyStateStatus)
                return "";
            uint lScanCode = Win32Func.MapVirtualKey(VKCode, 0);

            Win32Func.ToUnicodeEx(VKCode, lScanCode, bKeyState, sbString, (int)5, (uint)0, HKL);
            return sbString.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
