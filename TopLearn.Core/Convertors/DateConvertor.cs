using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.Core.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            persianCalendar.GetDayOfMonth(date);

            return $"{persianCalendar.GetYear(date)}/" +
                $"{persianCalendar.GetMonth(date).ToString("00")}/" +
                $"{persianCalendar.GetDayOfMonth(date).ToString("00")}";
        }

        public static DateTime ToMiladi(this string shamsiDate)
        {
            string[] date = shamsiDate.Split('/');
            return new DateTime(int.Parse(date[0]),
                int.Parse(date[1]),
                int.Parse(date[2]),
                new PersianCalendar());
        }
    }
}
