using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;

namespace Winding
{
    public partial class MainVM : INotifyPropertyChanged
    {
        public string _Window_state;
        public string Window_state
        {
            get { return _Window_state; }
            set
            {                
                _Window_state = value;
                if (value == "Maximized")
                {
                    Area_width = Convert.ToString(SystemParameters.WorkArea.Width+14);
                }
            }
        }
        
        /// <summary>
        /// Правая граница, рисуемой развертки
        /// </summary>
        public string _Area_width;
        public string Area_width
        {
            get { return _Area_width; }
            set
            {               
                _Area_width = value;
                 UpdateCalcul();                
            }
        }
        /// <summary>
        /// Ширина столбца данных
        /// </summary>
        public string _ColumnData="300";
        public string ColumnData
        {
            get
            {
                return _ColumnData;
            }
            set
            {
                _ColumnData = value;
                UpdateCalcul();
            }
        }

        private string[] _coord_up, _coord_down;
        public string[] coord_up
        {
            get
            {
                return _coord_up;
            }
            set
            {
                _coord_up = value;
                OnPropertyChanged();
            }
        }
        public string[] coord_down
        {
            get
            {
                return _coord_down;
            }
            set
            {
                _coord_down = value;
                OnPropertyChanged();
            }
        }




        public static double _height_up, _height_down;
        public double Height_up
        {
            get
            {
                return _height_up;
            }
            set
            {
                _height_up = value;
                OnPropertyChanged();
            }
        }
        public double Height_down
        {
            get
            {
                return _height_down;
            }
            set
            {
                _height_down = value;
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

        public void UpdateCalcul()
        {
            try
            {
                Wire_type some_wire = new Wire_type(Convert.ToDouble(Area_width.Replace(".", ",")) -
                                                    Convert.ToDouble(ColumnData.Replace(".", ",")),
                                                    Field_quantity,
                                                    a, b, Z, Type_of_wire, Winding_direction);
                
               
                coord_up = some_wire.Paint(_dataGo_up);
                Height_up = some_wire.Find_height();
                coord_down = some_wire.Paint(_dataGo_down, Height_up+ _center_chenel);
                Height_down= some_wire.Find_height();

                Field some_field = new Field(Convert.ToDouble(Area_width.Replace(".", ","))-
                                             Convert.ToDouble(ColumnData.Replace(".", ",")),
                                             Field_quantity);
                Field_coord = some_field.Paint_field();
            }
            catch
            {
            }
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
