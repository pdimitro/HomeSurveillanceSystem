using CameraProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraProject.Logic
{
    public class LoginValidation
    {
        private string _username;
        private string _password;

        public string errText { get; private set; }

        public LoginValidation(string username, string pass) { _username = username; _password = pass; errText = string.Empty; }

        public bool isUserInputValid()
        {
            bool retResult = true;

            if( false == isUserNameValid() )
            {
                retResult = false;
                errText = "Моля въведете потребителско име с дължина поне 5 символа";
            }
            else if(false == isPasswordValid() )
            {
                retResult = false;
                errText = "Моля въведете парола с дължина поне 5 символа";
            }
            else
            {
                UserTypeClass var = new UserTypeClass();
                bool result = var.isAuthorized(_username, _password);

                if( false != result )
                {
                    //Enable control..
                }
                else
                {
                    retResult = false;
                    errText = "Въведения потребител няма администраторски достъп";
                }
            }

            return retResult;
        }

        private bool isUserNameValid()
        {
            return (_username.Length >= 5);
        }

        private bool isPasswordValid()
        {
            return (_password.Length >= 5);
        }
    }
}
