using MimeKit;

namespace Graduates_Model.Model
{
    public class Messsage
    {
        public List<MailboxAddress> To {  get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public Messsage(IEnumerable<string> to, string subject, string content)
        {
            //To = new List<MailboxAddress>();
            To = to.Select(email => MailboxAddress.Parse(email)).ToList();
            Subject = subject;
            Content = content;
        }
    }
}
