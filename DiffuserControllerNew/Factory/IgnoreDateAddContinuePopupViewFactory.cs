using DiffuserControllerNew.Interface;
using DiffuserControllerNew.Message;
using DiffuserControllerNew.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Factory
{
    public class IgnoreDateAddContinuePopupViewFactory : IIgnoreDateAddContinuePopupViewFactory
    {
        private readonly IServiceProvider _provider;

        public IgnoreDateAddContinuePopupViewFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IgnoreDateAddContinuePopupView IgnoreDateAddContinuePopupView(ScheduleMessageData scheduleMessageData)
        {
            var view = _provider.GetRequiredService<IgnoreDateAddContinuePopupView>();
            view.SetInitData(scheduleMessageData);
            return view;
        }
    }
}
