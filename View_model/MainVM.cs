using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;


namespace Winding
{
    [Serializable]
    public partial class MainVM : INotifyPropertyChanged
    {
        Save data_input = new Save();//объект сохранения данных
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*";
                            openFileDialog.ShowDialog();
                            Save Saved_data = data_input.Deserialized_data(openFileDialog.FileName);
                            Type_of_wire = Saved_data.Type_of_wire;
                            a = Saved_data.a;
                            b = Saved_data.b;
                            Z = Saved_data.Z;
                            Winding_direction = Saved_data.Winding_direction;
                            Paper_koef = Saved_data.Paper_koef;
                            Field_quantity = Saved_data.Field_quantity;
                            Center_chenel = Saved_data.Center_ch;
                            Items_data_up = Saved_data.data_Go_up;
                            Items_data_down = Saved_data.data_Go_down;
                            N = Saved_data.N;
                            Items_data_up.ListChanged += On_List_Changed_up;
                            Items_data_down.ListChanged += On_List_Changed_down;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }));
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(obj =>
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Text files (*.xml)|*.xml|All files (*.*)|*.*";
                        saveFileDialog.ShowDialog();
                        //Сохранение исходных данных
                        try
                        {
                            data_input.Serialized_data(Type_of_wire, a, b, Z, Winding_direction, Paper_koef, Field_quantity,
                                                  Center_chenel, Items_data_up, Items_data_down, N, saveFileDialog.FileName);
                            MessageBox.Show("Сохранено");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"{ex.Message}");
                        }                       
                       
                    }));
            }
        }

        public string _Window_state;
        public string Window_state
        {
            get { return _Window_state; }
            set
            {
                _Window_state = value;
                if (value == "Maximized")
                {
                    Area_width = SystemParameters.WorkArea.Width + 14;
                }
            }
        }

        /// <summary>
        /// Правая граница, рисуемой развертки
        /// </summary>
        private double _Area_width;
        public double Area_width
        {
            get { return _Area_width; }
            set
            {
                _Area_width = value - ColumnData;
                OnPropertyChanged();
                UpdateCalcul();
            }
        }
        /// <summary>
        /// Ширина столбца данных
        /// </summary>
        public double _ColumnData;
        public double ColumnData
        {
            get
            {
                return _ColumnData;
            }
            set
            {
                _ColumnData = value;
            }
        }

        private string[] _coord_up, _coord_down;
        /// <summary>
        /// Координаты верхней части обмотки
        /// </summary>
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

        /// <summary>
        /// Координаты нижней части обмотки
        /// </summary>
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

        public string _up;
        /// <summary>
        /// Координаты нижней части обмотки
        /// </summary>
        public string up
        {
            get
            {
                return _up;
            }
            set
            {
                _up = value;
                OnPropertyChanged();

            }
        }

        public string _down;
        /// <summary>
        /// Координаты нижней части обмотки
        /// </summary>
        public string down
        {
            get
            {
                return _down;
            }
            set
            {
                _down = value;
                OnPropertyChanged();
            }
        }


        private double _height_turn;
        /// <summary>
        /// Высота витка
        /// </summary>
        public double Height_turn
        {
            get
            {
                return _height_turn;
            }
            set
            {
                _height_turn = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Высота обмотки
        /// </summary>
        public double _height_winding;
        public double Height
        {
            get
            {
                return _height_winding;
            }
            set
            {
                _height_winding = value;
                OnPropertyChanged();
            }
        }

        public static string _Field_coord;
        /// <summary>
        /// Координаты полей
        /// </summary>
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
                //размкетка полей
                Field some_field = new Field(Area_width, Field_quantity);
                Field_coord = some_field.Paint_field();

                // верхняя часть обмотки
                Wire_type up_part_winding = new Wire_type(Area_width, Field_quantity, a, b, Z, Type_of_wire,
                                                          Winding_direction, Paper_koef, Items_data_up, N);
                Height_turn = up_part_winding.Calculate_turn_height() * Items_data_up.Count;
                coord_up = up_part_winding.Paint();
                if (up_part_winding.min_y_coord < up_part_winding.first_y)
                {
                    coord_up = up_part_winding.Paint(up_part_winding.first_y - up_part_winding.min_y_coord);
                }
                double height_up = up_part_winding.Find_height();

                // up = ( "100,0,50,"+(Convert.ToString(up_part_winding.min_y_coord)).Replace(",", "."));
                Height = height_up;

                double x_coord_of_max_y = up_part_winding.Down_border_x;

                //нижняя часть обмотки
                Wire_type down_part_winding = new Wire_type(Area_width, Field_quantity, a, b, Z, Type_of_wire,
                                                            Winding_direction, Paper_koef, Items_data_down, N);

                coord_down = down_part_winding.Paint(down_part_winding.Begin_coord_down_winding(Items_data_up, Items_data_down, x_coord_of_max_y)
                                         + Center_chenel + Height - up_part_winding.first_y + up_part_winding.min_y_coord);
                double height_down = down_part_winding.Find_height();
                //down = ("100,0,50," + (Convert.ToString(down_part_winding.max_y_coord)).Replace(",", "."));
                if (Items_data_down.Count > 0)
                {
                    Height = down_part_winding.max_y_coord - up_part_winding.min_y_coord;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
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
