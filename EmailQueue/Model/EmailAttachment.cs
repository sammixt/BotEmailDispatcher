using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailQueue.Model
{
    [Table("EmailAttachment")]
    public class EmailAttachment
    {
        [Key]
        public int AttachmentID { get; set; }
        public string MailID { get; set; }
        public string AttachmentPath { get; set; }
    }
}
