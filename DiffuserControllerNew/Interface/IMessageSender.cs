using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffuserControllerNew.Interface
{
    public enum Sender
    {
        BtnLogin,
        BtnLoginForce,
        BtnCancelDuplicate,
        BtnClose,
        BtnUserInfoRun,
        BtnUserInfoCancel,
        BtnUserCreate,
        BtnUserUpdate,
        BtnUserDelete,
        UserCreateSuccess,
        UserUpdateSuccess,
        None,
        BtnSessionStay,
        BtnSignOut,
        BtnNotiClose,
        BtnReSignIn,
    }

    public interface IMessageSender
    {
        public Sender Sender { get; set; }
    }
}
