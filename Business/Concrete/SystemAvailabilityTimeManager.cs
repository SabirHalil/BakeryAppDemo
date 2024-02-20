using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;


namespace Business.Concrete
{
    public class SystemAvailabilityTimeManager : ISystemAvailabilityTimeService
    {
        ISystemAvailabilityTimeDal _systemAvailabilityTimeDal;
        public SystemAvailabilityTimeManager(ISystemAvailabilityTimeDal systemAvailabilityTime) {

            _systemAvailabilityTimeDal = systemAvailabilityTime;
        }
        public SystemAvailabilityTime GetSystemAvailabilityTime()
        {
          return  _systemAvailabilityTimeDal.Get(s => s.Id == 1);
        }

        public void UpdateSystemAvailabilityTime(SystemAvailabilityTime systemAvailabilityTime)
        {
         _systemAvailabilityTimeDal.Update(systemAvailabilityTime);
        }
    }
}
