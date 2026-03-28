using Claim_Form.Data;
using Claim_Form.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace Claim_Form.Services.Implementations
{
    public class MailService:IMailService
    {
       

        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        public MailService(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }
        // 🔥 MAIN METHOD (Called from Controller)
        public async Task<string> SendMailByEmpCode(string empCode, Guid ClaimID)
        {
            // ✅ 1. Get Employee
            var emp = await _context.Employees
                .FirstOrDefaultAsync(x => x.EmpCode == empCode);
            if (emp == null)
                throw new Exception("Employee not found");
            // ✅ 2. Get Latest Claim
            var claim = await _context.RecentClaims.FirstOrDefaultAsync(c => c.RecentClaimId == ClaimID);
            // ✅ 3. Build Email
            string to = "ext_SKannan2@nordex-online.com";
            string subject = "New Expense Claim Submitted";
            string body = $@"
<h3>Expense Claim Notification</h3>
<p><b>Employee:</b> {emp.Name} ({emp.EmpCode})</p>
<p><b>Claim Type:</b> {claim.Type}</p>
<p><b>Total Amount:</b> ₹{claim.Amount}</p>
<p><b>Status:</b> {claim.Status}</p>
<p><b>Date:</b> {claim.Date}</p>
       ";
            // 🔥 4. Call SMTP Method
            await SendEmail(to, subject, body);
            // ✅ 5. Return response
            return $"Mail sent successfully to {to}";
        }
        // 🔥 SMTP METHOD (Inside Same Service)
        private async Task SendEmail(string to, string subject, string body)
        {
            var email = _config["EmailSettings:Email"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:Host"];
            var port = int.Parse(_config["EmailSettings:Port"]);
            using (var smtp = new SmtpClient(host, port))
            {
                smtp.Credentials = new NetworkCredential(email, password);
                smtp.EnableSsl = true;
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(email);
                    message.To.Add(to);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    await smtp.SendMailAsync(message); // ✅ SMTP send
                }
            }
        }
    }
}
