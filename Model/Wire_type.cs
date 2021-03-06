﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Winding;

namespace Winding
{
    [Serializable]
    class Wire_type : Build_area
    {
        /// <summary>
        /// Тип выбранного провода
        /// </summary>
        public string Type_of_wire { get; private set; }
        /// <summary>
        /// Меньшая сторона элементарного проводника
        /// </summary>
        public double a { get; private set; }
        /// <summary>
        /// Большая сторона элементарного проводника
        /// </summary>
        public double b { get; private set; }
        /// <summary>
        /// Размер изоляции на две стороны
        /// </summary>
        public double Z { get; private set; }
        /// <summary>
        /// Напраалнеие намотки
        /// </summary>
        public string Winding_direction { get; private set; }
        /// <summary>
        /// Число элементарок
        /// </summary>
        public double N { get; private set; }
        /// <summary>
        /// коэффициент усадки
        /// </summary>
        public double Paper_koef { get; private set; }
        /// <summary>
        /// Координата x наибольшего витка
        /// </summary>
        public double Down_border_x { get; private set; }
        /// <summary>
        /// Координата y наименьшая
        /// </summary>
        public double min_y_coord { get; private set; } = 0;
        /// <summary>
        /// Координата y нибольшая
        /// </summary>
        public double max_y_coord { get; private set; } = 0;
        /// <summary>
        /// Координаты для отрисовки
        /// </summary>
        private string winding_coord { get; set; } = "";
        /// <summary>
        /// Данные ходов
        /// </summary>
        private BindingList<MainDataGo> data;        
        /// <summary>
        /// первый виток да/нет
        /// </summary>
        bool first = true;
        /// <summary>
        /// координата y витка первого хода
        /// </summary>
        public double first_y { get; private set; }

        
        public Wire_type(double Area_width, int Field_quantity,
                         double a, double b, double Z, string Type_of_wire,string Winding_direction, 
                         double Paper_koef, BindingList<MainDataGo> data, int N) : base(Area_width, Field_quantity)
        {         
            this.a = a;
            this.b = b;
            this.Z = Z;
            this.Winding_direction = Winding_direction;
            this.Type_of_wire = Type_of_wire;
            this.Paper_koef = Paper_koef;
            this.data = data;
            this.N = N;
            
        }

        public double Begin_coord_down_winding(BindingList<MainDataGo> data_up, BindingList<MainDataGo> data_down, double x_coord_of_max_y)
        {
            double n_part = 0;
            try
            {
                if (Winding_direction == "Левое")
                {
                    n_part = (data_down[0].FieldSetting*Field_width-Field_width/2+Left_border- x_coord_of_max_y) /(Field_width);
                }
                if (Winding_direction == "Правое")
                {
                    n_part = (x_coord_of_max_y - (data_down[0].FieldSetting*Field_width-Field_width/2+Left_border))/(Field_width);
                }
            }
            catch
            {
                return 0;
            }

            double height_turn = Calculate_turn_height();// высота витка/провода 
            if (data_down[0].FieldSetting * Field_width - Field_width / 2 + Left_border > x_coord_of_max_y && Winding_direction == "Правое"
                || data_down[0].FieldSetting * Field_width - Field_width / 2 + Left_border < x_coord_of_max_y && Winding_direction == "Левое")
            {
                return Top_border + (n_part * (((data_down.Count - 1) * height_turn) + height_turn) / Field_quantity);
            }
            else
            {
                return Top_border + (n_part * (((data_down.Count - 1) * height_turn) + height_turn) / Field_quantity) - height_turn;
            }
        }

        /// <summary>
        /// Тестовый метод
        /// </summary>     
        /// <returns>Возвращает массив строк, где каждый n-тый массив отдельный ход </returns>
        public string[] Paint(double height_up_winding=0)
        {
            int Number_go = data.Count;
            string[] all_coord = new string[Number_go];
            double n_part = 0;
            
            for (int i = 0; i < Number_go; i++)
            {
                if (i > 0)
                {
                    if (Winding_direction == "Левое")
                    {
                        n_part = data[i].FieldSetting - data[0].FieldSetting;
                    }
                    if (Winding_direction == "Правое")
                    {
                        n_part = data[0].FieldSetting - data[i].FieldSetting ;
                    }
                }
                all_coord[i] = Paint_one_go(Number_go, data[i].FieldSetting, data[i].NumberTurnsInGo, data[i].CurrentGo, 
                                            n_part, height_up_winding);
            }
                return all_coord;
        }

        private string Paint_one_go(int Number_go, int Field_numeric, double Number_turns, int fact_go,
                                     double n_part, double height_up_winding)
        {
            /*Как мотать обмотку (левая):
                             x1y1------>--x2y2
            x4y4-------------xmym---------x3y3
            x5y5-------------x6y6
            */

            /*Как мотать обмотку (правая):
            x2y2------<------x1y1
            x3y3-------------xmym---------x4y4
                             x6y6---------x5y5
            */
            double height_turn = Calculate_turn_height();// высота витка 
            

            double y1 = Top_border+ (n_part * (((Number_go - 1) * height_turn) + height_turn) / Field_quantity)
                        + fact_go * height_turn + height_up_winding;//смещение хода витка   
            if (first)
            {
                first_y = y1;
                first = false;
            }
            double x1 = Left_border + Field_width * Field_numeric - Field_width / 2;// поле захода обмотки
            double height_inclien = 0;//высота наклона
            if (Winding_direction == "Левое")
            {
                height_inclien = (((Number_go - 1) * height_turn)+ height_turn) * (Area_width - Right_border - x1) / (Area_width - Right_border - Left_border);
            }
            if (Winding_direction == "Правое")
            {
                height_inclien = (((Number_go - 1) * height_turn) + height_turn) * (Left_border - x1) / (Left_border - (Area_width - Right_border));
            }

            for (int turn = 0; turn < Math.Floor(Number_turns); turn++)//отрисовка целых витков
            {
                double x2 = 0;
                if (Winding_direction == "Левое") { x2 = Area_width - Right_border; }
                if (Winding_direction == "Правое") { x2 = Left_border; }
                double y2 = y1 + height_inclien;
                double x3 = 0;
                if (Winding_direction == "Левое") { x3 = Area_width - Right_border; }
                if (Winding_direction == "Правое") { x3 = Left_border; }
                double y3 = y2 + height_turn;
                double xm = x1;
                double ym = y1 + height_turn;
                double x4 = 0;
                if (Winding_direction == "Левое") { x4 = Left_border; }
                if (Winding_direction == "Правое") { x4 = Area_width - Right_border; }
                double y4 = y2;
                double x5 = 0;
                if (Winding_direction == "Левое") { x5 = Left_border; }
                if (Winding_direction == "Правое") { x5 = Area_width - Right_border; }
                double y5 = y4 + height_turn;
                double x6 = x1;
                double y6 = 2 * height_turn + y1;
                double[] coord = {
                                     x1,y1,
                                     x2, y2,
                                     x3, y3,
                                     xm, ym,
                                     x1, y1,
                                     x2, y2,
                                     x2,0,
                                     x4,0,
                                     x4, y4,
                                     xm, ym+((Number_go-1)*height_turn),
                                     x6, y6+((Number_go-1)*height_turn),
                                     x5 ,y5,
                                     x4, y4,
                                     x5 ,y5,
                                     x6 ,y6+((Number_go-1)*height_turn)
                                     };
                double Down_border_tmp = y6 + ((Number_go - 1) * height_turn);//для высоты обмотки                
                if (Down_border_tmp > Down_border)
                {
                    Down_border_x=x6;
                    Down_border = Down_border_tmp;
                }//для высоты обмотки
                
                winding_coord = Add_coord(coord, turn, true);
                y1 = y1 + height_turn * (Number_go);
                //отрисовка дробных витков
                double not_int_turn = Number_turns - Math.Floor(Number_turns); //часть дроб-го витка
                if (Number_turns - Math.Floor(Number_turns) != 0 && turn == Math.Floor(Number_turns) - 1)
                {
                    if (x6 + (x2 - x4) * not_int_turn > x2 && Winding_direction == "Левое" || x6 + (x2 - x4) * not_int_turn < x2 && Winding_direction == "Правое")//переход на след-щий уровень
                    {
                        double[] part_coord = {
                                     x3,y3+height_turn+((Number_go-1)*height_turn),
                                     x3,y3+((Number_go-1)*height_turn),
                                     x6,y6-height_turn+((Number_go-1)*height_turn),
                                     x6,y6+((Number_go-1)*height_turn),
                                     x5,y5,
                                     x5,y5+height_turn+(((Number_go-1)*height_turn)),
                                     x5 +((x2 - x5) * not_int_turn-(x3-x6)),
                                     y5 +height_turn+height_inclien*((x2 - x5) * not_int_turn - (x3 - x6))/(x2-x1)+(((Number_go-1)*height_turn)),
                                     x5 +((x2 - x5) * not_int_turn-(x3-x6)),
                                     y5+height_inclien*((x2 - x5) * not_int_turn - (x3 - x6))/(x2-x1)+(((Number_go-1)*height_turn)),
                                     x5,y5+((Number_go-1)*height_turn),
                                     x5,y5+((Number_go-1)*height_turn)+height_turn,
                                     x5 +((x2 - x5) * not_int_turn-(x3-x6)),
                                     y5 +height_turn+height_inclien*((x2 - x5) * not_int_turn - (x3 - x6))/(x2-x1)+(((Number_go-1)*height_turn))
                                                };                        
                        Down_border_tmp = y5 + height_turn + height_inclien * ((x2 - x5) * not_int_turn - (x3 - x6)) / (x2 - x1) + (((Number_go - 1) * height_turn));
                        if (Down_border_tmp > Down_border)
                        {
                            Down_border = Down_border_tmp;
                            Down_border_x = x5 + ((x2 - x5) * not_int_turn - (x3 - x6));
                        }//для высоты обмотки
                        winding_coord = Add_coord(part_coord, turn, false);
                    }
                    else // перехода на следующий уровень нет
                    {
                        double[] part_coord = {
                                    x6+(x2 - x5) * not_int_turn,
                                    y5 +height_turn+height_inclien*((x2 - x5) * not_int_turn - (x3 - x6))/(x2-x1)+((Number_go-1)*height_turn),
                                    x6+(x2 - x5) * not_int_turn,
                                    y5 +height_inclien*((x2 - x5) * not_int_turn - (x3 - x6))/(x2-x1)+((Number_go-1)*height_turn),
                                    x6,
                                    ym+((Number_go-1)*height_turn)
                                                  };                       
                        Down_border_tmp = y5 + height_turn + height_inclien * ((x2 - x5) * not_int_turn - (x3 - x6)) / (x2 - x1) + ((Number_go - 1) * height_turn);
                        
                        if (Down_border_tmp > Down_border)
                        {
                            Down_border_x = x6 + (x2 - x5) * not_int_turn;
                            Down_border = Down_border_tmp;
                        }//для высоты обмотки
                        winding_coord = Add_coord(part_coord, turn, false);
                    }
                }
            }
            return winding_coord;
        }

        private string Add_coord(double[]x_or_y, int turn, bool int_turn)
        {
            if (min_y_coord == 0)
            {
                min_y_coord = x_or_y[1];
            }

            for (int i=1;i<x_or_y.Length;i+=2) 
            {                               
                if (x_or_y[i] < min_y_coord && x_or_y[i]!=0)
                {
                   min_y_coord = x_or_y[i];
                }
                if (x_or_y[i] >= max_y_coord)
                {
                    max_y_coord = x_or_y[i];
                }
            }
            for (int i=0;i<x_or_y.Length;i++ )
            {                
                string _x_or_y = Convert.ToString(x_or_y[i]).Replace(",",".");
                if (i == 0 && turn==0 && int_turn==true)
                {
                    winding_coord = _x_or_y;                    
                }                
                else
                {
                    winding_coord += "," + _x_or_y;                    
                }
            }     
            return winding_coord;
        }

        public  double Find_height()
        {
            if (winding_coord == "")
            {                
                return 0;
            }
            return max_y_coord - min_y_coord;
        }

        public double Calculate_turn_height()
        {
            if (Type_of_wire == "ПТБ" && b>0)
            {
                double strip = 0.2;
                if (N <= 11)
                {
                    strip = 0;
                }
                return (b + 0.15) * 2 + strip * Paper_koef + Z* Paper_koef;
            }         

            if (Type_of_wire == "ПБУ" && b > 0)
            {
                return b+Z* Paper_koef;
            }

            if (Type_of_wire == "ПБП" && b > 0)
            {
                return b+0.34 + Z* Paper_koef;
            }
            return 0;
        }       
    }
}