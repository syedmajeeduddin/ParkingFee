using System;
using System.Collections.Generic;
using System.Linq;


namespace Common
{
    public class BusinessDayRates
    {

        public const double OneHourRate = 5.0;
        public const double TwoHourRate = 10.0;
        public const double ThreeHourRate = 15.0;
        public const double DayRate = 20.0;
    }

    public class WeekendRates
    {
        public const double Rate = 10.0; 
    }

    public class EarlyBirdRates
    {
        public const double Rate = 13.0;
    }

    public class NightRates
    {
        public const double Rate = 6.50;
    }
   

    public class RateNames
    {
        public const string EarlyBird = "Early Bird";
        public const string NightRate = "Night Rate";
        public const string WeekendRate = "Weekend Rate";
        public const string StandardRate = "Standard Rate";
    }

    public class DateType
    {
        public DateTime DateEntity { get; set; }

        public bool IsBusinessDay { get; set; }
    }


    



}
