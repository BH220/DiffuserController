using DiffuserControllerNew.Message;
using DiffuserControllerNew.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Interface
{
    public interface IIgnoreDateAddContinuePopupViewFactory
    {
        IgnoreDateAddContinuePopupView IgnoreDateAddContinuePopupView(ScheduleMessageData scheduleMessageData);
    }
}
