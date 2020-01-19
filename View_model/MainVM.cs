using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;


namespace Winding
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
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
                View_change();
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
                View_change();
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
                View_change();
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
                View_change();
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
                View_change();
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
                View_change();
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
                View_change();
            }
        }       
        

       

        private static string[] _coord;
        public string[] coord
        {
            get
            {
                
                Wire_type some_wire = new Wire_type(Convert.ToDouble(Area_width.Replace(".", ",")), Field_quantity, 
                                                    a, b, Z, Type_of_wire);
                return _coord = some_wire.Paint(InfoGo.data);

            }
            set
            {
            }
        }

       

        public static double _height;
        public double Height
        {
            get
            {
                Wire_type some_wire = new Wire_type(Convert.ToDouble(Area_width.Replace(".", ",")), Field_quantity,
                                                    a, b, Z, Type_of_wire);              
                return _height = some_wire.Find_height();
            }
            set
            {
            }
        }

        public static string _Field_coord;
        public string Field_coord
        {
            get
            {
                Field some_field = new Field(Convert.ToDouble(Area_width.Replace(".", ",")), Field_quantity);
                return _Field_coord=some_field.Paint_field();
            }
        }

        public void View_change()
        {
            MessageBox.Show("Я тут");
            OnPropertyChanged("coord");
            OnPropertyChanged("Height");
            OnPropertyChanged("Field_coord");
        }
    }
}
