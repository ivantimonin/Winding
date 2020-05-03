using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winding
{
    [Serializable]
    abstract class Build_area
    {
        /// <summary>
        /// Левый край области построения (отступ)
        /// </summary>
        protected double Left_border { get; set; } = 20;

        /// <summary>
        /// Правый край области построения (отступ)
        /// </summary>
        protected double Right_border { get; private set; } = 50;

        /// <summary>
        /// Верхний край области построения (отступ)
        /// </summary>
        protected double Top_border { get; set; } =0;

        /// <summary>
        /// Нижний край области построения (отступ)
        /// </summary>
        protected double Down_border { get; set; } =0;

        /// <summary>
        /// Ширина области построения
        /// </summary>
        protected double Area_width { get; set; }

      
        /// <summary>
        /// Количество полей
        /// </summary>
        protected int Field_quantity { get; set; }

        /// <summary>
        /// Ширина поля
        /// </summary>
        protected double Field_width { get; set; }



        protected Build_area(double Area_width, int Field_quantity)
        {                
            this.Area_width = Area_width;
            this.Field_quantity = Field_quantity;
            Calculate_field_width();
        }

        protected void Calculate_field_width()
        {
            Field_width=(Area_width-Left_border-Right_border) / Field_quantity;          
        }
    }
}
