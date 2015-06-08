using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointment.Web.Helper
{
    public class DateUtil
    {
        public static DateTime GetBusinessDay(DateTime today, int addValue)
        {
            #region Sanity Checks
            if ((addValue != -1) && (addValue != 1))
                throw new ArgumentOutOfRangeException("addValue must be -1 or 1");
            #endregion

            if (addValue > 0)
                return NextBusinessDay(today);
            else
                return DateUtil.PreviousBusinessDay(today);
        }

        /// <summary>
        /// return the previous business date of the date specified.
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public static DateTime PreviousBusinessDay(DateTime today)
        {
            DateTime result;
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    result = today.AddDays(-2);
                    break;

                case DayOfWeek.Monday:
                    result = today.AddDays(-3);
                    break;

                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    result = today.AddDays(-1);
                    break;

                case DayOfWeek.Saturday:
                    result = today.AddDays(-1);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("DayOfWeek=" + today.DayOfWeek);
            }
            return ScreenHolidays(result, -1);
        }

        /// <summary>
        /// return the next business date of the date specified.
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public static DateTime NextBusinessDay(DateTime today)
        {
            DateTime result;
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                    result = today.AddDays(1);
                    break;

                case DayOfWeek.Friday:
                    result = today.AddDays(3);
                    break;

                case DayOfWeek.Saturday:
                    result = today.AddDays(2);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("DayOfWeek=" + today.DayOfWeek);
            }
            return ScreenHolidays(result, 1);
        }


        /// <summary>
        /// return the mm/dd string of the date specified.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string MonthDay(DateTime time)
        {
            return String.Format("{0:00}/{1:00}", time.Month, time.Day);
        }


        /// <summary>
        /// screen for holidays 
        /// (simple mode)
        /// </summary>
        /// <param name="result"></param>
        /// <param name="addValue"></param>
        /// <returns></returns>
        public static DateTime ScreenHolidays(DateTime result, int addValue)
        {
            #region Sanity Checks
            if ((addValue != -1) && (addValue != 1))
                throw new ArgumentOutOfRangeException("addValue must be -1 or 1");
            #endregion

            // holidays on fixed date
            switch (MonthDay(result))
            {
                case "01/01":  // Happy New Year
                case "01/26":  // Independent Day
                case "04/03":  // Public Holiday
                case "04/06":  // Public Holiday
                case "06/08":  // Public Holiday
                case "10/05":  // Public Holiday
                case "12/25":  // Christmas
                case "12/28":  // Public Holiday
                    return GetBusinessDay(result, addValue);
                default:
                    return result;
            }
        }
    }
}
