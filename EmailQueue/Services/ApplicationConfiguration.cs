using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue.Services
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        
        

        private string smtp_server_one = ConfigurationManager.AppSettings["smtp_server_one"];
        private string smtp_server_two = ConfigurationManager.AppSettings["smtp_server_two"];
        private string smtp_server_three = ConfigurationManager.AppSettings["smtp_server_three"];
        private string smtp_username = ConfigurationManager.AppSettings["smtp_username"];
        private string smtp_pass = ConfigurationManager.AppSettings["smtp_password"];
        private string dbIp = ConfigurationManager.AppSettings["dbip"];
        private string db = ConfigurationManager.AppSettings["db"];
        private string port = ConfigurationManager.AppSettings["port"];

        public string SmtpServerOne
        {
            get
            {
                return smtp_server_one;
            }
        }

        public string SmtpServerTwo
        {
            get
            {
                return smtp_server_two;
            }
        }

        public string SmtpServerThree
        {
            get
            {
                return smtp_server_three;
            }
        }

        public string SmtpUserName
        {
            get
            {
                return smtp_username;
            }
        }

        public string SmtpPassword
        {
            get
            {
                return smtp_pass;
            }
        }

        public string DatabaseIp
        {
            get
            {
                return dbIp;
            }
        }

        public string Database
        {
            get
            {
                return db;
            }
        }

        public int Port
        {
            get
            {
                return Convert.ToInt32(port);
            }
        }
    }
}
