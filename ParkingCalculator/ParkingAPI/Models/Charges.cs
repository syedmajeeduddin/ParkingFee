using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Charges
    {
        List<RateType> _applicableRates;

        public Charges(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;

        }

        /// <summary>
        /// List of Rates that may be applicable if a user 
        /// parks for multiple days 
        /// </summary>
        public List<RateType> ApplicableRates
        {
            get
            {
                if (_applicableRates == null)
                    _applicableRates = new List<RateType>();

                return _applicableRates;
            }
            set { _applicableRates = value; }

        }


        public double TotalCharge { get { return ApplicableRates.Sum(e => e.Charge); }  }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        
        public void CalculateStandardAndWeekendRates()
        {
            var dateRanges = Helper.GetDatesBetween(StartDate, EndDate);
            RateType rateTemplate;

            //start and end date are same 
            if (dateRanges.Count() == 0)
            {
                //check if it is a Business Day/ WeekDay
                var isBusinessDay = Helper.IsBusinessDay(StartDate.Date);

                if (isBusinessDay)
                {
                    rateTemplate = GetRateTemplate(RateNames.StandardRate, Helper.CalculateStandardRate(EndDate.Subtract(StartDate).TotalHours));
                    ApplicableRates.Add(rateTemplate);                   
                }
                else
                {
                    //If Weekend 
                    rateTemplate = GetRateTemplate(RateNames.WeekendRate, Helper.CalculateWeekendRate());                    
                    ApplicableRates.Add(rateTemplate);
                }

            }
            else
            {

                if (Helper.IsBusinessDay(StartDate))
                {
                    var fee = Helper.CalculateStandardRate((StartDate.Date.AddDays(1).AddSeconds(-1)).Subtract(StartDate).TotalHours);
                    rateTemplate = GetRateTemplate(RateNames.StandardRate, fee); 
                    ApplicableRates.Add(rateTemplate);
                }
                else
                {
                    rateTemplate = GetRateTemplate(RateNames.WeekendRate, Helper.CalculateWeekendRate());
                    ApplicableRates.Add(rateTemplate);
                }


                foreach (var dt in dateRanges)
                {
                    rateTemplate = null;
                    if (dt.IsBusinessDay)
                    {
                        rateTemplate = GetRateTemplate(RateNames.StandardRate, BusinessDayRates.DayRate);
                        ApplicableRates.Add(rateTemplate);
                    }                        
                    else
                    {
                        var previousDay = dt.DateEntity.AddDays(-1);
                        if (previousDay >= StartDate.Date)
                        {
                            //don't charge if the previous day was Sat/Non business day
                            // you can only charge once 10 dollars for the whole weekend (sat+sun)
                            var fee =  !Helper.IsBusinessDay(previousDay) ? 0 : WeekendRates.Rate;
                            rateTemplate = GetRateTemplate(RateNames.WeekendRate, fee);
                            ApplicableRates.Add(rateTemplate);
                        }

                    }


                }

                if (Helper.IsBusinessDay(EndDate))
                {
                    var fee =  Helper.CalculateStandardRate(EndDate.Subtract(EndDate.Date).TotalHours);
                    rateTemplate = GetRateTemplate(RateNames.StandardRate, fee);
                    ApplicableRates.Add(rateTemplate);
                }
                else
                {
                    var previousDay = EndDate.Date.AddDays(-1);
                    if (previousDay >= StartDate.Date)
                    {
                        //don't charge if the previous day was Sat/Non business day
                        // you can only charge once 10 dollars for the whole weekend (sat+sun)
                        var fee = !Helper.IsBusinessDay(previousDay) ? 0 : WeekendRates.Rate;
                        rateTemplate = GetRateTemplate(RateNames.WeekendRate, fee);
                        ApplicableRates.Add(rateTemplate);
                    }

                }


            }


        }
        
        public void CalculateEarlyBirdRate()
        {           
            ApplicableRates.Add(GetRateTemplate(RateNames.EarlyBird, EarlyBirdRates.Rate));
        }

        public void CalculateNighRate()
        {
            ApplicableRates.Add(GetRateTemplate(RateNames.NightRate, NightRates.Rate));
        }


        private RateType GetRateTemplate(string rateName, double charge)
        {
            if (!string.IsNullOrEmpty(rateName))
            {
                var rateTemplate = RateTypes.GetByName(rateName);
                rateTemplate.Charge = charge;
                return new RateType() { Charge = charge, EntryTime = rateTemplate.EntryTime, ExitTime = rateTemplate.ExitTime, Name = rateTemplate.Name };

            }
            return null;
        }

    }
}
