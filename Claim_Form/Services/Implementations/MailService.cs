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
       
        public async Task<string> SendMailByEmpCode(string empCode, Guid ClaimID)
        {
            //  Get Employee
            var emp = await _context.Employees
                .FirstOrDefaultAsync(x => x.EmpCode == empCode);
            if (emp == null)
                throw new Exception("Employee not found");
            //  Get Latest Claim
            var claim = await _context.RecentClaims.FirstOrDefaultAsync(c => c.RecentClaimId == ClaimID);
            // . Build Email
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
            // Call SMTP Method
            await SendEmail(to, subject, body);
            //  Return response
            return $"Mail sent successfully to {to}";
        }
        //  METHOD (Inside Same Service)

        public async Task<string> AdminAction(string empCode, Guid ClaimID)
        {
            
            var emp = await _context.Employees
                .FirstOrDefaultAsync(x => x.EmpCode == empCode);
            if (emp == null)
                throw new Exception("Employee not found");
            //  Get Latest Claim
            var claim = await _context.RecentClaims.FirstOrDefaultAsync(c => c.RecentClaimId == ClaimID);
            //  Build Email
            string to = "ext_SKannan2@nordex-online.com";
            string subject = "New Expense Claim Submitted";
            string body = $@"
<h3>Expense Claim Action Taken by Admin {claim.Status}</h3>
<p><b>Employee:</b> {emp.Name} ({emp.EmpCode})</p>
<p><b>Claim Type:</b> {claim.Type}</p>
<p><b>Total Amount:</b> ₹{claim.Amount}</p>


       ";
            // Call SMTP Method
            await SendEmail(to, subject, body);
            //  Return response
            return $"Mail sent successfully to {to}";
        }
        //  (Inside Same Service
        private async Task SendEmail(string to, string subject, string body)
        {
            try
            {
                var email = _config["EmailSettings:Email"];
                var password = _config["EmailSettings:Password"];
                var host = _config["EmailSettings:Host"];
                var port = int.Parse(_config["EmailSettings:Port"]);

                using var smtp = new SmtpClient(host, port)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(email, password),
                    Timeout = 15000 // ✅ important
                };

                using var message = new MailMessage(email, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // ✅ LOG EVERYTHING
                throw new Exception($"SMTP FAILED: {ex}");
            }
        }

    }
}
