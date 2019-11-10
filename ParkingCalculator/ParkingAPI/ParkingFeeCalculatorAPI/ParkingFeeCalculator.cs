using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;
namespace ParkingFeeCalculatorAPI
{
    public class ParkingFeeCalculator : IParkingCalculation
    {
        Charges _charges = null;


        /// <summary>
        ///  Calculates the Parking Fee and returns a Charges complex object to 
        ///  encapsulate the multiple Rates that may have been used 
        ///  while generating total charge for parking 
        ///  e.g. The total parking fee may be combination of (Standard Rate + Night Rate)
        /// </summary>
        /// <param name="startDate"> start Date </param>
        /// <param name="endDate"> end date </param>
        /// <returns> Return a Charges Object that will contain the Rate objects containing the Rate Name and the charges against that Rate</returns>
        public Charges CalculateParkingFee(DateTime startDate , DateTime endDate)
        {
            try { 
            
            
            if (startDate < endDate)
            {
                if (_charges == null)
                    _charges = new Charges(startDate, endDate);
               
                // Early Bird Rate applicable only when the 
                // check in and check out happens on same day
                // of a weekday on the given times
                if (Helper.IsEarlyBirdTime(startDate, endDate))
                {
                    _charges.CalculateEarlyBirdRate();
                }
                // Night Rate applicable only when the 
                // check in happens at night on Weekday and check out happens on 
                //subsequent day on the given time
                else if (Helper.IsNightRateTime(startDate, endDate))
                {
                    _charges.CalculateNighRate();
                }
                else
                {
                    //All other times when parking is for more than one 
                    //day or where multiple rates are applicable based on check-in and check-out time
                    _charges.CalculateStandardAndWeekendRates();
                }

            }
            else
            {
                throw new Exception($"Start Date : {startDate} can not be greater than End Date : {endDate} ");
            }

                return _charges;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
