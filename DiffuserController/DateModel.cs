using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiffuserController
{
    public class DateModel
    {
        [JsonIgnore]
        public bool IsSelected { get; set; } = false;

        public DateOnly Date { get; set; }
        public string Message { get; set; }
        public int year
        {
            get
            {
                return this.Date.Year;
            }
        }
        public int month
        {
            get
            {
                return this.Date.Month;
            }
        }
        public int day
        {
            get
            {
                return this.Date.Day;
            }
        }
        public string DoW
        {
            get
            {
                string result = "";
                switch (this.Date.DayOfWeek)
                {
                    case DayOfWeek.Sunday: result = "일"; break;
                    case DayOfWeek.Monday: result = "월"; break;
                    case DayOfWeek.Tuesday: result = "화"; break;
                    case DayOfWeek.Wednesday: result = "수"; break;
                    case DayOfWeek.Thursday: result = "목"; break;
                    case DayOfWeek.Friday: result = "금"; break;
                    case DayOfWeek.Saturday: result = "토"; break;
                }
                return result;
            }
        }
    }
}
