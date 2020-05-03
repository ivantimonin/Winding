using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Winding;



namespace Winding
{    
    public partial class MainVM//данные по каждому ходу
    {
        /// <summary>
        /// Количество полей
        /// </summary>
        private int _Field_quantity=1;
        public int Field_quantity
        {
            get { return _Field_quantity; }
            set
            {
                if (value > 50)
                {
                    MessageBox.Show("Максимальное количество полей не должно превышать 50");
                    _Field_quantity = 50;
                }
                else if (value <= 0)
                {
                    _Field_quantity = 1;
                }
                else
                {
                    _Field_quantity = value;
                }
                OnPropertyChanged();
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
                OnPropertyChanged();
                UpdateCalcul();
            }
        }

        /// <summary>
        /// Направление намотки
        /// </summary>
        private string _winding_direction="Правое";
        public string Winding_direction
        {
            get { return _winding_direction; }
            set
            {
                if (value == "Левое" || value == "Правое")
                {
                    _winding_direction = value;
                    OnPropertyChanged();
                    UpdateCalcul();
                }               
            }
        }
        /// <summary>
        /// Данные по каждому ходу верхней части
        /// </summary>
        private  BindingList<MainDataGo> _Items_data_up = new BindingList<MainDataGo>();
        public BindingList<MainDataGo> Items_data_up
        {
            get { return _Items_data_up; }
            set
            {
                _Items_data_up = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }
        /// <summary>
        /// Данные по каждому ходу нижней части
        /// </summary>
        private  BindingList<MainDataGo> _Items_data_down = new BindingList<MainDataGo>();
        public BindingList<MainDataGo> Items_data_down
        {
            get
            {
                return _Items_data_down;
            }
            set
            {
                _Items_data_down = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }       
        
       
        public MainVM()
        {           
            Items_data_up.ListChanged += On_List_Changed_up;
            Items_data_down.ListChanged += On_List_Changed_down;
        }

        private void On_List_Changed_up(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case (ListChangedType.ItemAdded):                   
                    Items_data_up[Items_data_up.Count-1].NumberTurnsInGo = 1;
                    Items_data_up[Items_data_up.Count-1].CurrentGo = Items_data_up.Count;
                    Items_data_up[Items_data_up.Count-1].FieldSetting =Field_quantity/2;                    
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


        private void On_List_Changed_down(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case (ListChangedType.ItemAdded):                    
                    Items_data_down[Items_data_down.Count - 1].NumberTurnsInGo = 1;
                    Items_data_down[Items_data_down.Count - 1].CurrentGo = Items_data_down.Count;
                    Items_data_down[Items_data_down.Count-1].FieldSetting = Field_quantity/2;
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

