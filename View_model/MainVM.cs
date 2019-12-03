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

        private double _a;//меньшая сторона провода        
        public double a
        {
            get { return _a; }
            set
            {
                _a = value;
                View_change();
            }
        }

        private double _b;//большая сторона провода
        public double b
        {
            get { return _b; }
            set
            {
                _b = value;
                View_change();
            }
        }

        private double _Z;//изоляция провода
        public double Z
        {
            get { return _Z; }
            set
            {
                _Z = value;
                View_change();
            }
        }

        private double _number_turns;//число витков        
        public double number_turns
        {
            get { return _number_turns; }
            set
            {
                if (value > 300)
                {
                    value = 300;
                }
                if (value < 0)
                {
                    value = 0;
                }
                else
                {
                    _number_turns = value;

                }
                View_change();
            }
        }

        private string _type_of_wire;//тип провода     
        public string type_of_wire
        {
            get { return _type_of_wire; }
            set
            {
                _type_of_wire = value;
                View_change();
            }
        }

        private string _right_bord = "900";
        public string right_bord
        {
            get { return _right_bord; }
            set
            {
                _right_bord = value;
                View_change();
            }
        }


        private int _N;//кол-во элементарок     
        public int N
        {
            get { return _N; }
            set
            {
                MessageBox.Show($"Переменная=");
                _N = value;
                View_change();
            }
        }

        private int _Field_numeric = 12;//поле захода витка    
        public int Field_numeric
        {
            get { return _Field_numeric; }
            set
            {
                if (value > 24)
                {
                    value = 24;
                }
                if (value < 1)
                {
                    value = 1;
                }
                _Field_numeric = value;
                View_change();
            }
        }

        private double left_border = 0;

        Wire_type some_wire = new Wire_type();
        private string _coord;
        public string coord
        {
            get
            {
                return _coord = some_wire.Paint(a, b, number_turns, Z, Field_numeric, left_border, 
                    
                    Convert.ToDouble(right_bord.Replace(".", ",")));
            }
            set
            {
            }
        }
        public double _height;
        public double Height
        {
            get
            {
                return _height = some_wire.Find_height();
            }            
        }        
        
        private void View_change()
        {
            OnPropertyChanged("coord");
            OnPropertyChanged("Height");
        }
    }
}
