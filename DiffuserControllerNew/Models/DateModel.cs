using DiffuserControllerNew.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Models
{
    public class DateModel
    {
        public DateOnly Date { get; set; }
        public DateTypes DateType { get; set; }
        public string Color
        {
            get
            {
                switch(DateType)
                {
                    case DateTypes.Holiday: return DateColor.Holiday;
                    case DateTypes.Sunday: return DateColor.Sunday;
                    case DateTypes.Saturday: return DateColor.Saturday;
                    default: return DateColor.Weekday;
                }
                
            }
        }
        public string Message { get; set; }
    }
}
