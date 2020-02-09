using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winding
{
    class Field: Build_area
    {       
        private string field_coord="";
        private double y_up = 10;//верхний край области построения
        private double y_down = 3000;//пока так, но потом это должно тоже считаться 

        public Field(double Area_width, double Field_quantity) : base(Area_width, Field_quantity) { }       

        public string Paint_field()
        {           
            //координаты области построения
            Add_coord(Left_border, y_up);
            Add_coord(Left_border, y_down);
            Add_coord(Area_width-Right_border, y_down);
            Add_coord(Area_width-Right_border, y_up);
            Add_coord(Left_border, y_up);

            for (int i = 0; i <= Field_quantity; i++)
            {
                Add_coord(Left_border + i * Field_width, y_up);
                Add_coord(Left_border + i * Field_width, y_down);
                Swap(ref y_up, ref y_down);               
            }            
            return field_coord;
        }

        private void Add_coord(double x, double y)
        {
            if (field_coord.Length == 0)
            {
                field_coord += Convert.ToString(x).Replace(",", ".");
                field_coord +=","+Convert.ToString(y).Replace(",", ".");
            }
            else
            {
                field_coord +="," + Convert.ToString(x).Replace(",", ".");
                field_coord +="," + Convert.ToString(y).Replace(",", ".");
            }
        }

        private void Swap(ref double var1, ref double var2)
        {
            double tmp = var1;
            var1 = var2;
            var2 = tmp;
        }

    }
}
