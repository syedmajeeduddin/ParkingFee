using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System.Linq;
using System.Globalization;
using ParkingFeeCalculatorAPI;

namespace UnitTesting
{
    [TestClass]
    public class ParkingFeeTestCases
    {
        ParkingFeeCalculator api = new ParkingFeeCalculator();         

        [TestMethod]
        public void EarlyBird_Testing()
        {
            //Check-In at 8 a.m 
            //Check-Out at 4 p.m
            var startDateString = "2019/11/08 08:00";
            var endDateString  = "2019/11/08 16:00";
            //DateTime.ParseExact(startDateString, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture
            var charge = api.CalculateParkingFee( DateTime.Parse(startDateString),
                                                DateTime.Parse(endDateString));

            var rateType = charge.ApplicableRates.FirstOrDefault();

            Assert.AreEqual(charge.TotalCharge, EarlyBirdRates.Rate);
            Assert.AreEqual(charge.ApplicableRates.Count,  1);
            Assert.AreEqual(rateType.Name, RateNames.EarlyBird);


        }

        [TestMethod]
        public void NightRate_Testing()
        {
            //Check-In between  6 pm to 12 am on a BusinessDay
            //Check-Out between 3.30 pm to 11.30pm on next day
            var startDateString = "2019/11/08 19:00";
            var endDateString = "2019/11/09 16:00";

            var charge = api.CalculateParkingFee(DateTime.Parse(startDateString),
                                                DateTime.Parse(endDateString));

            var rateType = charge.ApplicableRates.FirstOrDefault();

            Assert.AreEqual(charge.TotalCharge, NightRates.Rate);
            Assert.AreEqual(charge.ApplicableRates.Count, 1);
            Assert.AreEqual(rateType.Name, RateNames.NightRate);
        }

        [TestMethod]
        public void WeekendRate_Testing()
        {
            var startDateString = "2019/11/09 06:00";
            var endDateString = "2019/11/10 23:00";

            var charge = api.CalculateParkingFee(DateTime.Parse(startDateString),
                                                DateTime.Parse(endDateString));

            var rateType = charge.ApplicableRates.FirstOrDefault();

            Assert.AreEqual(charge.TotalCharge, WeekendRates.Rate);
            Assert.AreEqual(charge.ApplicableRates.Count, 1);
            Assert.AreEqual(rateType.Name, RateNames.WeekendRate);

        }

        [TestMethod]
        public void StandardRate_Testing()
        {
            // 3 hours parking on a Weekday; Standard rates applicable 
            var startDateString = "2019/11/14 09:00";
            var endDateString = "2019/11/14 12:00";

            var charge = api.CalculateParkingFee(DateTime.Parse(startDateString),
                                                DateTime.Parse(endDateString));

            var rateType = charge.ApplicableRates.FirstOrDefault();

            Assert.AreEqual(charge.TotalCharge, 15);
            Assert.AreEqual(charge.ApplicableRates.Count, 1);
            Assert.AreEqual(rateType.Name, RateNames.StandardRate);

        }


        [TestMethod]
        public void StandardAndWeekendRate_Testing()
        {
            // 2 hours parking on a Weekday; Standard rates applicable 
            var startDateString = "2019/11/08 09:00";
            var endDateString = "2019/11/12 02:00";

            var charge = api.CalculateParkingFee(DateTime.Parse(startDateString),
                                                DateTime.Parse(endDateString));
            //returns WeekendRate
            var rateType = charge.ApplicableRates.Skip(1).FirstOrDefault();
            Assert.AreEqual(rateType.Name, RateNames.WeekendRate);

            // 8th : $20; 9th,10th: $10; 11th : $20; 12th : $10 for 2 hours 
            Assert.AreEqual(charge.TotalCharge, 60);

            //Applied Rates will be : Weekday (Fri), Weekend (Sat),Weekend (Sun), Weekday (Mon), WeekDay (Tue)
            Assert.AreEqual(charge.ApplicableRates.Count, 5);
            

        }
    }
}
