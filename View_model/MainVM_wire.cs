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
    }
}

