using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMailSender
{
    public class SimpleEmailSender
    {
        public async Task SendMailAsync()
        {
            try
            {
                // Send mail to the signed-in user
                // Get the user for their email address
                //var user = await GraphHelper.GetUserAsync();

                //var userEmail = user?.Mail ?? user?.UserPrincipalName;

                //if (string.IsNullOrEmpty(userEmail))
                //{
                //    Console.WriteLine("Couldn't get your email address, canceling...");
                //    return;
                //}

                await GraphHelper.SendMailAsync("Testing Microsoft Graph", "Hello world!", "yuanliang@yulian.onmicrosoft.com");

                // Console.WriteLine("Mail sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending mail: {ex.Message}");
            }
        }
    }
}
