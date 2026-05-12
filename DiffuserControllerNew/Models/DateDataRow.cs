using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Models
{
    public partial class DateDataRow: ObservableObject
    {
        [ObservableProperty] private bool _isSelected;

        public DateModel Data { get; set; }

        // 바인딩 편의용 프로퍼티
        public string Year => Data.Date.Year.ToString();
        public string Month => Data.Date.Month.ToString("00");
        public string Day => Data.Date.Day.ToString("00");
            public string DoW
            {
                get
                {
                    string result = "";
                    switch (Data.Date.DayOfWeek)
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
        public string Color => Data.Color;

        public DateDataRow(DateModel data)
        {
            Data = data;
        }
    }
}
