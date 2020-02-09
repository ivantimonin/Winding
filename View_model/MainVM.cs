using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.Specialized;
using Winding.View_model;


namespace Winding
{
    public class MainVM : INotifyPropertyChanged
    {

        /// <summary>
        /// меньшая сторона провода   
        /// </summary>
        private static double _a;
        public double a
        {
            get { return _a; }
            set
            {
                _a = value;
                UpdateCalcul();
            }
        }

        /// <summary>
        /// большая сторона провода
        /// </summary>
        private static double _b;
        public double b
        {
            get { return _b; }
            set
            {
                _b = value;
                UpdateCalcul();
            }
        }

        /// <summary>
        ///изоляция провода
        /// </summary>
        private static double _Z;
        public double Z
        {
            get { return _Z; }
            set
            {
                _Z = value;
                UpdateCalcul();
            }
        }

        /// <summary>
        /// Тип провода
        /// </summary>
        private static string _type_of_wire;
        public string Type_of_wire
        {
            get { return _type_of_wire; }
            set
            {
               
                _type_of_wire = value;
                UpdateCalcul();
            }
        }

        /// <summary>
        /// Направление намотки
        /// </summary>
        private static string _winding_direction="Левое";
        public string Winding_direction
        {
            get { return _winding_direction; }
            set
            {       
                _winding_direction = value;
                UpdateCalcul();
            }
        }
        /// <summary>
        /// Правая граница, рисуемой пазвертки
        /// </summary>
        private static string _Area_width = "900";
        public string Area_width
        {
            get { return _Area_width; }
            set
            {
                _Area_width = value;
                UpdateCalcul();
            }
        }


        private string[] _coord;
        public string[] coord
        {
            get
            {
                return _coord;
            }
            set
            {
                _coord = value;
                OnPropertyChanged();
            }
        }

        public static double _height;
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public static string _Field_coord;
        public string Field_coord
        {
            get
            {
                return _Field_coord;
            }
            set
            {
                _Field_coord = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Количество элементарных проводников
        ///  </summary>
        private static int _N;
        public int N
        {
            get { return _N; }
            set
            {
                _N = value;
                UpdateCalcul();
            }
        }


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

        public static ObservableCollection<MainVM> _dataGo=new ObservableCollection<MainVM>();
        public ObservableCollection<MainVM> Items_data
        {
            get { return _dataGo; }
            set
            {
                _dataGo = value;
                //OnPropertyChanged();
               // UpdateCalcul();
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
                //UpdateCalcul();
                // OnPropertyChanged();
                
                Items_data.Add(new MainVM() { });               
                Items_data.RemoveAt(Items_data.Count - 1);
               
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
                Items_data.Add(new MainVM() { });
                Items_data.RemoveAt(Items_data.Count - 1);
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
                //UpdateCalcul();
               
                Items_data.Add(new MainVM() { });
                Items_data.RemoveAt(Items_data.Count - 1);
            }
        }

       

        public MainVM()
        {
           
            Items_data = _dataGo;
            Items_data.CollectionChanged += On_CollectionChanged;
            UpdateCalcul();
     
        }




        public void UpdateCalcul()
        {

            Wire_type some_wire = new Wire_type(Convert.ToDouble(Area_width.Replace(".", ",")), Field_quantity,
                                                a, b, Z, Type_of_wire, Winding_direction);
            coord = some_wire.Paint(_dataGo);
            Height = some_wire.Find_height();
            Field some_field = new Field(Convert.ToDouble(Area_width.Replace(".", ",")), Field_quantity);
            Field_coord = some_field.Paint_field();
          

        }

        public event PropertyChangedEventHandler PropertyChanged;
        





        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private  void On_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
                        
            switch (e.Action)
            {   

                case NotifyCollectionChangedAction.Add:
                    //MessageBox.Show("добав элемента");
                    UpdateCalcul();
                    break;

                case NotifyCollectionChangedAction.Remove:
                    //MessageBox.Show("удал элемента");
                    UpdateCalcul();
                    break;
            }
            OnPropertyChanged();
        }
    }
}
