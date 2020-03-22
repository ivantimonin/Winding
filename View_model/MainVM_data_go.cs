using System.ComponentModel;
using System.Windows;

namespace Winding
{
    public partial class MainVM//данные по каждому ходу
    {
        /// <summary>
        /// Количество полей
        /// </summary>
        private static int _Field_quantity = 24;
        public int Field_quantity
        {
            get { return _Field_quantity; }
            set
            {
                if (value > 50)
                {
                    _Field_quantity = 50;
                }
                else if (value < 0)
                {
                    _Field_quantity = 0;
                }
                else
                {
                    _Field_quantity = value;
                }
                UpdateCalcul();
            }
        }

        ///<summary>
        ///центральный канал
        ///</summary>
        private double _center_chenel;
        public double Center_chenel
        {
            get { return _center_chenel; }
            set
            {               
                _center_chenel = value;
                UpdateCalcul();
            }
        }

        /// <summary>
        /// Направление намотки
        /// </summary>
        private string _winding_direction = "Левое";
        public string Winding_direction
        {
            get { return _winding_direction; }
            set
            {
                _winding_direction = value;
                UpdateCalcul();
            }
        }
        BindingList<MainVM> _dataGo_up = new BindingList<MainVM>();
        public BindingList<MainVM> Items_data_up
        {
            get { return _dataGo_up; }
            set
            {
                _dataGo_up = value;
                UpdateCalcul();
            }
        }

        BindingList<MainVM> _dataGo_down = new BindingList<MainVM>();
        public BindingList<MainVM> Items_data_down
        {
            get { return _dataGo_down; }
            set
            {
                _dataGo_down = value;
                UpdateCalcul();
            }
        }


        /// <summary>
        /// Поле захода витка
        /// </summary>
        private int fieldSetting;
        public int FieldSetting
        {
            get { return fieldSetting; }
            set
            {
                fieldSetting = value;
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
        
        public MainVM()
        {            
            Items_data_up.ListChanged += On_List_Changed;
            Items_data_down.ListChanged += On_List_Changed;
        }

        private void On_List_Changed(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case (ListChangedType.ItemAdded):
                    UpdateCalcul();
                    break;
                case (ListChangedType.ItemChanged):
                    UpdateCalcul();
                    break;
                case (ListChangedType.ItemDeleted):
                    UpdateCalcul();
                    break;
            }
        }
    }
}

