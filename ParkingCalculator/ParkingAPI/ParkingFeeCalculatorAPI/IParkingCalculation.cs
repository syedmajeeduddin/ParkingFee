using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingFeeCalculatorAPI
{
    interface IParkingCalculation
    {

         Charges CalculateParkingFee(DateTime startDate, DateTime endDate);
    }
        
}
