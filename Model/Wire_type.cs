using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winding
{
    class Wire_type
    {
        public string Name { get; private set;}// тип выбранного провода
        public double a_size { get; private set; }//меньшая сторона элементарного провода
        public double b_size { get; private set; }//большая сторона элементарного провода
        public double isolation_size { get; private set; }//размер изоляции на две стороны
        public double number_turns { get; private set; } = 3;//числов витков
        public Wire_type()
        {           
                   
        }
        public string Paint(double a, double b, double number_turns, double Z,
                            int Field_numeric, double left_border, double right_border)
        {
            double pole_size=(right_border - left_border)/26;
            left_border += pole_size;
            right_border -= pole_size;
            double height_turn = Z+b;// высота витка 
            double x1 = pole_size*Field_numeric+ pole_size/2;//будет задавать пользователь( поле захода обмотки)
            double y1 = 150;//координата начала отрисовки по y
            double height_inclien = height_turn*(right_border-x1)/(right_border- left_border);//высота наклона
            string up_winding_coord="";
            for (int turn = 0; turn < Math.Floor(number_turns); turn++)//отрисовка целых витков
            {                
                double x2 = right_border;
                double y2 = y1 + height_inclien;
                double x3 = right_border;
                double y3 = y2 + height_turn;
                double x4 = left_border;
                double y4 = y2;
                double x5 = left_border;
                double y5 = y4 + height_turn;
                double x6 = x1;
                double y6 = 2*height_turn+y1;
                double[] coord = { x1, y1, x2, y2, x3, y3, x4, y4, x5, y5, x6, y6};
                up_winding_coord = Add_coord(coord, up_winding_coord, turn);
                y1 = y1+ height_turn;
                //отрисовка дробных витков
                if (number_turns - Math.Floor(number_turns) != 0 && turn == Math.Floor(number_turns) - 1)
                {
                    double not_int_turn = number_turns - Math.Floor(number_turns); //часть дроб-го витка
                    if (x6 + (x2 - x4) * not_int_turn > x2)//переход на след-щий уровень
                    {
                        
                        double[] part_coord = {//хорошо бы убрать
                            x3,y3+height_turn,
                            x3,y3,
                            x6,y6-height_turn,
                            x6,y6,
                            x5,y5,
                            x5,y5+height_turn,
                            x5 +((x2 - x5) * not_int_turn-(x3-x6)),y5+height_turn+height_inclien*
                            ((x2 - x5) * not_int_turn - (x3 - x6))/(x2-x1)};
                            
                        up_winding_coord = Add_coord(part_coord, up_winding_coord, turn);
                    }        
                }                    
            }           
            

            return up_winding_coord;
        }
        private string Add_coord(double[]x_or_y, string up_winding_coord, int turn)
        {
            for (int i=0;i<x_or_y.Length;i++ )
            {                
                string _x_or_y = Convert.ToString(x_or_y[i]).Replace(",", ".");
                if (i == 0 && turn==0)
                {
                    up_winding_coord = _x_or_y;
                }                
                else
                {
                    up_winding_coord += "," + _x_or_y;
                }
            }     
            return up_winding_coord;
        }
    }
}
