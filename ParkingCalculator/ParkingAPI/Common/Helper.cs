using System;
using System.Collections.Generic;
using System.Linq;


namespace Common
{
    public class Helper
    {

        public static bool IsBusinessDay(DateTime refDate)
        {
            DateTime current = refDate;

            if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday)
            {
                return true;
            }
            return false;

        }      


        public static double CalculateStandardRate(double hours)
        {
            if (hours > 0 && hours <= 1)
            {
                return  BusinessDayRates.OneHourRate;
            }
            else if (hours > 1 && hours <= 2)
            {
                return BusinessDayRates.TwoHourRate;
            }
            else if (hours > 2 && hours <= 3)
            {
                return BusinessDayRates.ThreeHourRate;
            }
            else if (hours > 3)
            {
                return BusinessDayRates.DayRate;
            }

            return 0;

        }

        public static double CalculateWeekendRate()
        {
            return WeekendRates.Rate;
        }

        /// <summary>
        /// Early Bird is only for weekdays 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static bool IsEarlyBirdTime(DateTime start , DateTime end)
        {
            if (end.Date.Subtract(start.Date).Days <= 0 && IsBusinessDay(start))
            {

                // 6 to 9:30
                var eligibleStartTime = start.Date.AddHours(6);
                var eligibleStartEndTime = start.Date.AddHours(9.5);

                if(start >= eligibleStartTime && start <= eligibleStartEndTime)
                {
                    // 3.30 pm to 11.30pm
                    var eligibleEndStartTime = start.Date.AddHours(15.5);
                    var eligibleEndTime = start.Date.AddHours(23.5);

                    if(end >= eligibleEndStartTime && end <= eligibleEndTime)
                        return true;
                }

            }
            return false;
        }

        public static bool IsNightRateTime(DateTime start, DateTime end)
        {
            if (end.Date.Subtract(start.Date).Days == 1 && IsBusinessDay(start))
            {                
                // 18 to 24
                var eligibleStartTime = start.Date.AddHours(18);
                var eligibleStartEndTime = start.Date.AddDays(1).AddSeconds(-1);

                if (start >= eligibleStartTime && start <= eligibleStartEndTime)
                {
                    // 15.30 pm to 23.30pm
                    var eligibleEndStartTime = end.Date.AddHours(15.5);
                    var eligibleEndTime = end.Date.AddHours(23.5);

                    if (end >= eligibleEndStartTime && end <= eligibleEndTime)
                        return true;
                }

            }
            return false;
        }

       public static IEnumerable<DateType> GetDatesBetween(DateTime start, DateTime end)
        {
            var lstAllDates = GetDateRange(start, end);

            if (lstAllDates != null && lstAllDates.Count() > 0)
                return lstAllDates.Where(e => (e.DateEntity > start.Date) && (e.DateEntity < end.Date)).Select(e => e);

             return null;
        }


        public static IEnumerable<DateType> GetDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            while (startDate < endDate)
            {                
                yield return new DateType() { DateEntity = startDate.Date, IsBusinessDay = IsBusinessDay(startDate.Date) } ;
                startDate = startDate.AddDays(1).Date;
            }
        }


        

    }
}
