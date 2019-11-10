using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    
    public static class RateTypes
    {

        private static List<RateType> _rateTypes;

        public readonly static RateType EarlyBird = new RateType() { Name = RateNames.EarlyBird, Charge = 13.0 };
        public readonly static RateType NightRate = new RateType() { Name = RateNames.NightRate, Charge = 6.50 };
        public readonly static RateType WeekendRate = new RateType() { Name = RateNames.WeekendRate, Charge = 10.0 };
        public readonly static RateType HourlyRate = new RateType() { Name = RateNames.StandardRate,Charge = 0.0 };

        public static RateType GetByName(string name)
        {
            if (_rateTypes != null)
                return _rateTypes.Find(a => a.Name == name);
            else
                _rateTypes = new List<RateType>();

            _rateTypes.Add(EarlyBird);
            _rateTypes.Add(NightRate);
            _rateTypes.Add(WeekendRate);
            _rateTypes.Add(HourlyRate);
            return _rateTypes.Find(a => a.Name == name);
        }

    }
}
