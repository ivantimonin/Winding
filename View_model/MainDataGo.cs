using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Winding
{
    [Serializable]
    public class MainDataGo: INotifyPropertyChanged
    {
        /// <summary>
        /// Поле захода витка
        /// </summary>
        private int fieldSetting;
        public int FieldSetting
        {
            get { return fieldSetting; }
            set
            {
                if (value == 0)
                {
                    fieldSetting = 1;
                }                
                else
                {
                    fieldSetting = value;
                }
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Текущий ход
        /// </summary>
        public int currentGo;
        public int CurrentGo
        {
            get { return currentGo; }
            set
            {
                currentGo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Число витков в выбранном ходе
        /// </summary>
        private double numberTurnsInGo;
        public double NumberTurnsInGo
        {

            get { return numberTurnsInGo; }
            set
            {
                numberTurnsInGo = value;
                OnPropertyChanged();
            }
        }

        public MainDataGo()
        {
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
