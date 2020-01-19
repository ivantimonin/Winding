using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Winding
{
    public class InfoGo
    {


        MainVM obj = new MainVM();
        /// <summary>
        /// Текущий ход
        /// </summary>
        private int currentGo;
        public int CurrentGo
        {
            get { return currentGo; }
            set
            {
                
                currentGo = value;
            
                obj.View_change();
            }           
        }

        /// <summary>
        /// Число витков в выбранном ходе
        /// </summary>
        private double numberTurnsInGo;
        public double NumberTurnsInGo
        {
            
            get { return numberTurnsInGo; }
            set {
                
                numberTurnsInGo = value;
               
                obj.View_change();
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
              
                obj.View_change();
            }
        }
        public static ObservableCollection<InfoGo> data=new ObservableCollection<InfoGo>();   

        
    }
}
