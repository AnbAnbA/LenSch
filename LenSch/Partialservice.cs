using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenSch
{
    public partial class Service
    {
        public string CostAndTime 
        {
            get
            {
                if (Discount != null)
                {
                    return Convert.ToDouble(Convert.ToDouble(Cost) - (Convert.ToDouble(Cost)*Convert.ToDouble(Discount))) + " рублей за " + sentomin + " минут";
                }
                else 
                {
                    return Convert.ToDouble(Cost) + " рублей за " + sentomin + " минут";
                }
                    
            }
        }
        public int sentomin 
        {
            get 
            {
                return (DurationInSeconds/60);
            }
        }
        public string discount 
        {
            get 
            {
                if (Discount != null) 
                {
                    return "* скидка " + (Discount * 100) + "% ";
                }
                return "";
            }
        }
    }
}
