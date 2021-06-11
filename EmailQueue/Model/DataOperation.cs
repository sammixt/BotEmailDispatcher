using EmailQueue.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EmailQueue.Model
{
    public class DataOperation : IDataOperation
    {
        //Get Pending Item
        //Set Status to Processing
        //Update Status To Sent if Successfull or Retry (if Failed and Update Exception Table)
        //for retry, if difference between date logged and current date is greated than 24hrs, flag as failed
        private string connstr;
        IApplicationConfiguration config;
        public DataOperation(IApplicationConfiguration _config)
        {
            config = _config;
            setconnstring();
        }

        private void setconnstring()
        {
            connstr = new SqlConnectionStringBuilder()
            {
                IntegratedSecurity = true,
                DataSource = config.DatabaseIp,
                InitialCatalog = config.Database,
                ConnectTimeout = 1
            }.ConnectionString;
        }

        public List<EmailMessageQueue> GetPendingMessage(string hostName)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var entity = connection.GetList<EmailMessageQueue>($"where Resources = '{hostName}' and (Status = 'PENDING' or Status = 'RETRY') order by QueueTime desc");
                return entity.ToList();
            }
        }

        public void UpdateStatus(EmailMessageQueue model)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                connection.Update(model);
            }
        }

        public List<EmailAttachment> GetAttachments(string mailId)
        {
            using (IDbConnection connection = new SqlConnection(connstr))
            {
                var entity = connection.GetList<EmailAttachment>(new { MailID = mailId });
                return entity.ToList();
            }
        }
    }
}
