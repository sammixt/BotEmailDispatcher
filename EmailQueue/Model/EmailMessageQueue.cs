using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue.Model
{
    [Table("EmailMessageQueue")]
    public class EmailMessageQueue
    {
        [Key]
        public string MailID { get; set; }
        public string Sender { get; set; }
        public string Receipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public string Status { get; set; }
        public int TryCount { get; set; }
        public string Error { get; set; }
        public Nullable<DateTime> QueueTime { get; set; }
        public Nullable<DateTime> ProcessedTime { get; set; }
        public string Resources { get; set; }
    }
}
