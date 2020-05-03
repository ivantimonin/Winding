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


namespace Winding
{    
    public partial class MainVM//данные провода
    {     
        /// <summary>
        /// меньшая сторона провода   
        /// </summary>
        private  double _a;
        public double a
        {
            get { return _a; }
            set
            {
                _a = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }

        /// <summary>
        /// большая сторона провода
        /// </summary>
        private  double _b;
        public double b
        {
            get { return _b; }
            set
            {
                _b = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }

        /// <summary>
        ///изоляция провода
        /// </summary>
        private  double _Z;
        public double Z
        {
            get { return _Z; }
            set
            {
                _Z = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }
                     
        /// <summary>
        /// Тип провода
        /// </summary>
        private  string _type_of_wire;
        public string Type_of_wire
        {
            get { return _type_of_wire; }
            set
            {
                _type_of_wire = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }

        /// <summary>
        /// Количество элементарных проводников
        ///  </summary>
        private  int _N;
        public int N
        {
            get { return _N; }
            set
            {                
                _N = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }

        /// <summary>
        /// Кэффициент усадки 
        ///  </summary>
        private  double _paper_koef=0.9;
        public double Paper_koef
        {
            get { return _paper_koef; }
            set
            {
                _paper_koef = value;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }
    }
}

