using System.ComponentModel;


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
        BindingList<MainVM> _dataGo = new BindingList<MainVM>();
        public BindingList<MainVM> Items_data
        {
            get { return _dataGo; }
            set
            {
                _dataGo = value;
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
            Items_data.ListChanged += On_List_Changed;         
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

