using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService
    {

        public EmailService()
        {

        }

        internal void SendEmailConfirmationLink(string token, string? email)
        {
            throw new NotImplementedException();
        }

        internal void SendPasswordResetEmail(string token, string email)
        {
            throw new NotImplementedException();
        }
    }
}
