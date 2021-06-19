using Keyboard.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.ViewModels
{
    class KeyModel : INotifyPropertyChanged
    {
        private double _width;
        private double _heigth;
        private VirtualKeyShort _virtualKey;
        private char _displayName;

        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        public double Heigth
        {
            get => _heigth;
            set => _heigth = value;
        }

        public VirtualKeyShort VirtualKey
        {
            get => _virtualKey;
            set => _virtualKey = value; 
        }

        public char DisplayName
        {
            get => _displayName;
            private set
            {
                if (Convert.ToChar(VirtualKey) != _displayName)
                {
                    _displayName = Convert.ToChar(VirtualKey);
                    OnPropertyChanged("DisplayName");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
