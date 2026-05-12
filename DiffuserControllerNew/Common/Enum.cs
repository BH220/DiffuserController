using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Common
{
    public enum ActionType
    {
        Interval,
        Schedule
    }

    public enum DateTypes
    {
        Weekday,
        Saturday,
        Sunday,
        Holiday,
        SpecifiedDate,
    }
}
