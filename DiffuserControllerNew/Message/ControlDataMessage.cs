using DiffuserControllerNew.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Message
{
    public class ControlDataMessage : IMessageSender
    {
        public Sender Sender { get; set; }
        public object Args { get; set; }
    }
}
