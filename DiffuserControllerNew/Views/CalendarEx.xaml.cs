using DiffuserControllerNew.Common;
using DiffuserControllerNew.Converter;
using DiffuserControllerNew.Db;
using DiffuserControllerNew.Interface;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiffuserControllerNew.Views
{
    /// <summary>
    /// CalendarEx.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CalendarEx : UserControl
    {
        public static readonly DependencyProperty SelectedDateProperty =
    DependencyProperty.Register(nameof(SelectedDate), typeof(DateTime?),
        typeof(CalendarEx), new PropertyMetadata(null));

        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        DateTime displayDate = DateTime.Now;
        List<TextBlock> lstDays = new List<TextBlock>();
        List<TextBlock> lstYears = new List<TextBlock>();
        private List<Border> lstDayBorders = new List<Border>();
        private Border _selectedDayBorder = null;
        private DateTime _selectedDate = DateTime.MinValue;
        public CalendarEx()
        {
            InitializeComponent();
            InitControl();
            SelectedDate = _selectedDate = 
            displayDate = DateTime.Now;
            SetDate(displayDate);
            SetBorder();
            SelectedDate = DateTime.Now;
        }

        private void InitControl()
        {
            lstDays.Add(D11);
            lstDays.Add(D12);
            lstDays.Add(D13);
            lstDays.Add(D14);
            lstDays.Add(D15);
            lstDays.Add(D16);
            lstDays.Add(D17);
            lstDays.Add(D21);
            lstDays.Add(D22);
            lstDays.Add(D23);
            lstDays.Add(D24);
            lstDays.Add(D25);
            lstDays.Add(D26);
            lstDays.Add(D27);
            lstDays.Add(D31);
            lstDays.Add(D32);
            lstDays.Add(D33);
            lstDays.Add(D34);
            lstDays.Add(D35);
            lstDays.Add(D36);
            lstDays.Add(D37);
            lstDays.Add(D41);
            lstDays.Add(D42);
            lstDays.Add(D43);
            lstDays.Add(D44);
            lstDays.Add(D45);
            lstDays.Add(D46);
            lstDays.Add(D47);
            lstDays.Add(D51);
            lstDays.Add(D52);
            lstDays.Add(D53);
            lstDays.Add(D54);
            lstDays.Add(D55);
            lstDays.Add(D56);
            lstDays.Add(D57);
            lstDays.Add(D61);
            lstDays.Add(D62);
            lstDays.Add(D63);
            lstDays.Add(D64);
            lstDays.Add(D65);
            lstDays.Add(D66);
            lstDays.Add(D67);

            lstYears.Add(Y01);
            lstYears.Add(Y02);
            lstYears.Add(Y03);
            lstYears.Add(Y04);
            lstYears.Add(Y05);
            lstYears.Add(Y06);
            lstYears.Add(Y07);
            lstYears.Add(Y08);
            lstYears.Add(Y09);
            lstYears.Add(Y10);
            lstYears.Add(Y11);
            lstYears.Add(Y12);

            lstDayBorders.Add(BD11);
            lstDayBorders.Add(BD12);
            lstDayBorders.Add(BD13);
            lstDayBorders.Add(BD14);
            lstDayBorders.Add(BD15);
            lstDayBorders.Add(BD16);
            lstDayBorders.Add(BD17);
            lstDayBorders.Add(BD21);
            lstDayBorders.Add(BD22);
            lstDayBorders.Add(BD23);
            lstDayBorders.Add(BD24);
            lstDayBorders.Add(BD25);
            lstDayBorders.Add(BD26);
            lstDayBorders.Add(BD27);
            lstDayBorders.Add(BD31);
            lstDayBorders.Add(BD32);
            lstDayBorders.Add(BD33);
            lstDayBorders.Add(BD34);
            lstDayBorders.Add(BD35);
            lstDayBorders.Add(BD36);
            lstDayBorders.Add(BD37);
            lstDayBorders.Add(BD41);
            lstDayBorders.Add(BD42);
            lstDayBorders.Add(BD43);
            lstDayBorders.Add(BD44);
            lstDayBorders.Add(BD45);
            lstDayBorders.Add(BD46);
            lstDayBorders.Add(BD47);
            lstDayBorders.Add(BD51);
            lstDayBorders.Add(BD52);
            lstDayBorders.Add(BD53);
            lstDayBorders.Add(BD54);
            lstDayBorders.Add(BD55);
            lstDayBorders.Add(BD56);
            lstDayBorders.Add(BD57);
            lstDayBorders.Add(BD61);
            lstDayBorders.Add(BD62);
            lstDayBorders.Add(BD63);
            lstDayBorders.Add(BD64);
            lstDayBorders.Add(BD65);
            lstDayBorders.Add(BD66);
            lstDayBorders.Add(BD67);
        }

        private void SetYear(DateTime dt)
        {
            Grid.SetColumnSpan(BDY, 4); // 7칸으로 변경
            int year = dt.Year - 4;
            foreach (TextBlock x in lstYears)
            {
                x.Text = $"{year}년";
                year++;
            }
            DY.Text = $"{dt.Year - 4} ~ {dt.Year + 7} 년";
            DM.Text = "";
        }

        private void SetMonth(DateTime dt)
        {
            Grid.SetColumnSpan(BDY, 2); // 7칸으로 변경
            DY.Text = $"{dt.Year}년";
            DM.Text = "";
        }

        private void SetDate(DateTime setDt)
        {
            SetMonth(setDt.Year, setDt.Month); 
        }

        private void SetMonth(int year, int month)
        {
            DY.Text = $"{year}년";
            DM.Text = $"{month}월";
            DateTime dt = new DateTime(year, month, 1);
            int d = 0;
            d = d - (int)dt.DayOfWeek;
            dt = dt.AddDays(d);
            foreach (TextBlock x in lstDays)
            {
                x.Text = dt.Day.ToString();
                if (dt.Month == month)
                {
                    //지정된 공휴일인가?
                    //휴일인가?
                    //토요일인가?
                    //그럼 평일이네
                    var date = LocalDbManager.Instance.Dates.FirstOrDefault(x => x.Date == DateOnly.FromDateTime(dt.Date));
                    if (date != null)
                    {
                        x.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(date.Color));
                    }
                    else if (dt.DayOfWeek == DayOfWeek.Sunday)
                        x.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DateColor.Sunday));
                    else if (dt.DayOfWeek == DayOfWeek.Saturday)
                        x.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DateColor.Saturday));
                    else
                        x.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DateColor.Weekday));

                }
                else
                {
                    x.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(DateColor.NoThisMonth));
                }

                if (x.Parent is Border b)
                {
                    if (dt.Date == DateTime.Now.Date)
                    {
                        b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DBEAFE"));
                    }
                    else
                    {
                        b.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    }
                    if (dt.Date == _selectedDate.Date)
                    {
                        b.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1B74C0"));
                        b.BorderThickness = new Thickness(1);
                    }
                    else
                    {
                        b.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                        b.BorderThickness = new Thickness(1);
                    }

                }
                x.Tag = dt;
                dt = dt.AddDays(1);
            }
        }

        private void Next_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PlDay.Visibility == Visibility.Visible)
            {
                displayDate = displayDate.AddMonths(1);
                SetDate(displayDate);
            }
            else if (PlMonth.Visibility == Visibility.Visible)
            {
                displayDate = displayDate.AddYears(1);
                SetMonth(displayDate);
            }
            else if(PlYear.Visibility == Visibility.Visible)
            {
                displayDate = displayDate.AddYears(12);
                SetYear(displayDate);
            }
        }

        private void Previous_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PlDay.Visibility == Visibility.Visible)
            {
                displayDate = displayDate.AddMonths(-1);
                SetDate(displayDate);
            }
            else if (PlMonth.Visibility == Visibility.Visible)
            {
                displayDate = displayDate.AddYears(-1);
                SetMonth(displayDate);
            }
            else if (PlYear.Visibility == Visibility.Visible)
            {
                displayDate = displayDate.AddYears(-12);
                SetYear(displayDate);
            }
        }

        private void Month_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PlDay.Visibility = PlYear.Visibility = Visibility.Hidden;
            PlMonth.Visibility = Visibility.Visible;
            SetMonth(displayDate);
        }

        private void Year_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PlDay.Visibility = PlMonth.Visibility = Visibility.Hidden;
            PlYear.Visibility = Visibility.Visible;
            SetYear(displayDate);
        }

        private void YearDetail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock x = sender as TextBlock;
            if (x == null) return;
            PlYear.Visibility = PlDay.Visibility = Visibility.Hidden;
            PlMonth.Visibility = Visibility.Visible;
            DY.Text = x.Text;
            displayDate = new DateTime(x.Text.Replace("년", "").ToIntEx(), displayDate.Month, 1);
            SetMonth(displayDate);
        }

        private void MonthDetail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock x = sender as TextBlock;
            if (x == null) return;
            PlYear.Visibility = PlMonth.Visibility = Visibility.Hidden;
            PlDay.Visibility = Visibility.Visible;
            displayDate = new DateTime(displayDate.Year, x.Text.Replace("월", "").ToIntEx(), 1);
            SetDate(displayDate);
        }

        private void ToDay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedDate = _selectedDate = DateTime.Now;
            Grid.SetColumnSpan(BDY, 2); // 7칸으로 변경
            PlYear.Visibility = PlMonth.Visibility = Visibility.Hidden;
            PlDay.Visibility = Visibility.Visible;
            displayDate = DateTime.Now;
            SetDate(displayDate);
        }

        private void BDM_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border border && border.Child is TextBlock tb)
            {
                if (!string.IsNullOrEmpty(tb.Text))
                {
                    border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F3F4F6"));
                }
                else
                {
                    border.Cursor = Cursors.Arrow;
                }
            }
        }

        private void BDM_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                border.Background = Brushes.Transparent;
                border.Cursor = Cursors.Hand;
            }
        }

        private void DayBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Child is TextBlock tb)
            {
                _selectedDate = (DateTime)tb.Tag;
                SelectedDate = _selectedDate;
                SetBorder(); 
            }
        }

        private void SetBorder()
        {
            // 모든 테두리 초기화
            foreach (var border in lstDayBorders)
                border.BorderThickness = new Thickness(0);

            foreach (Border b in lstDayBorders)
            {
                if (b.Child is TextBlock tb && tb.Tag is DateTime dt)
                {
                    if (dt.Date == _selectedDate.Date)
                    {
                        b.BorderThickness = new Thickness(1);
                        b.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1B74C0"));
                        _selectedDayBorder = b;
                        break;
                    }
                }
            }
        }
    }
}
