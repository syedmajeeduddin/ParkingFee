﻿using ParkingFeeCalculatorAPI;
using System;


/*********************************************************************************************************************************************************************
 *  Early Bird Rate : Applicable only when the Check-In (between 6:00 AM to 9:00 AM) and Check-Out (between 3:30 PM to 11:30 PM) happens on the same day
 *  Night Rate : Applicable only when the Check-In (between 6:00 PM to midnight (weekdays) ) and Check-Out (between 3:30 PM to 11:30 PM) happens on the next day
 *  Weekend Rate : Charged $10 irrespective of the number of hours parked on Weekend. If parked on both Sat and Sunday; charges will be $10 even though parked for 2 days of weekedn
 *  StandDard Rate : Rate is applicable on WeekDays. If check-in happens on EarlyBird Time but check-out happens before or after Early Bird Check-Out time then 
 *                   Standard Rates are applicable. Same is applicable for Night Rates too
 * 
 * 
 * ******************************************************************************************************************************************************************/

namespace ParkingCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            DateTime start;
            DateTime end;

            Console.WriteLine("Enter a Start date (Format: YYYY/MM/DD HH:MM): ");
           
            if (DateTime.TryParse(Console.ReadLine(), out start))
            {
                Console.WriteLine("The day of the week is: " + start.DayOfWeek);
            }
            else
            {
                Console.WriteLine("You have entered an incorrect start value.");
                Console.ReadLine();
            }

            Console.WriteLine("Enter a End date (Format: YYYY/MM/DD HH:MM): ");

            if (DateTime.TryParse(Console.ReadLine(), out end))
            {
                Console.WriteLine("The day of the week is: " + end.DayOfWeek);
            }
            else
            {
                Console.WriteLine("You have entered an incorrect Enddate value.");
                Console.ReadLine();
            }

           

            if (start < end )
            {            
              
                Console.WriteLine("Start Date " + start.ToLongDateString() + " Time " + start.ToShortTimeString());
                Console.WriteLine("End Date " + end.ToLongDateString() + " Time " + end.ToShortTimeString());

                var Api = new ParkingFeeCalculator();
                var charges =  Api.CalculateParkingFee(start, end);

                //TODO : NEED TO DISPLAY Applied CHARGE RATES Which is a property in Charges 
                Console.WriteLine($"Total Charges : {charges.TotalCharge.ToString()}");

            }
            else
            {
                Console.WriteLine($"Start Date : {start} can not be greater than End Date : {end} ");
            }

            Console.ReadLine();


        }
    }
}
