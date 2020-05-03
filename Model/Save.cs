using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Xml.Serialization;

namespace Winding
{
    [Serializable]
    public class Save
    {
        /// <summary>
        /// Тип выбранного провода
        /// </summary>
        public string Type_of_wire { get;  set; }
        /// <summary>
        /// Меньшая сторона элементарного проводника
        /// </summary>
        public double a { get;  set; }
        /// <summary>
        /// Большая сторона элементарного проводника
        /// </summary>
        public double b { get;  set; }
        /// <summary>
        /// Размер изоляции на две стороны
        /// </summary>
        public double Z { get;  set; }
        /// <summary>
        /// Число элементарок
        /// </summary>
        public int N { get; set; }
        /// <summary>
        /// Напраалнеие намотки
        /// </summary>
        public string Winding_direction { get;  set; }
        /// <summary>
        /// коэффициент усадки
        /// </summary>
        public double Paper_koef { get;  set; }
        /// <summary>
        /// Количество полей
        /// </summary>
        public int Field_quantity { get; set; }       
        /// <summary>
        /// Щирина центрального канала
        /// </summary>
        public double Center_ch { get;  set; }
        /// <summary>
        /// Данные по ходам в обмотке верхней части
        /// </summary>        
        public BindingList<MainDataGo> data_Go_up { get;  set; } = new BindingList<MainDataGo>();
        /// <summary>
        /// Данные по ходам в обмотке нижней части
        /// </summary>        
        public BindingList<MainDataGo> data_Go_down { get;  set; } = new BindingList<MainDataGo>();

        public Save()
        {
        }

       
      
        [NonSerialized] XmlSerializer formatter = new XmlSerializer(typeof(Save));
        public void Serialized_data(string Type_of_wire, double a, double b, double Z, string Winding_direction,
                    double Paper_koef, int Field_quantity, double Center_ch, BindingList<MainDataGo> data_Go_up,
                    BindingList<MainDataGo> data_Go_down, int N, string Saving_file_name)
        {
            this.Type_of_wire = Type_of_wire;
            this.a = a;
            this.b = b;
            this.Z = Z;
            this.Winding_direction = Winding_direction;
            this.Paper_koef = Paper_koef;
            this.Field_quantity = Field_quantity;
            this.Center_ch = Center_ch;
            this.data_Go_up = data_Go_up;
            this.data_Go_down = data_Go_down;
            this.N = N;
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(Saving_file_name, FileMode.Create))
            {
                try { formatter.Serialize(fs, this); }
                catch (Exception ex)
                { 
                    MessageBox.Show(ex.Message); 
                }                               
            }
            
        }
        public Save Deserialized_data(string file_name)
        {
            using (FileStream fs = new FileStream(file_name, FileMode.Open))
            {
                
                Save old_data = (Save)formatter.Deserialize(fs);
                return old_data;             
            }
        }        
    }
}
