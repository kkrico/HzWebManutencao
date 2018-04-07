using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Net;

namespace RelatoriosReportServer
{
    [Serializable]
    public class MyCredentials : IReportServerCredentials
    {
        private string m_userName;
        private string m_password;

        public MyCredentials(string userName, string password)
        {
            m_userName = userName;
            m_password = password;
        }

        public WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(m_userName, m_password); }
        }
        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }
    }
}