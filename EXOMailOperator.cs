using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXOMailOperatorViaGraph.Service;

namespace EXOMailOperatorViaGraph
{
    public class EXOMailOperator
    {
        public async Task SendMailAsync()
        {
            try
            {
                await GraphHelper.SendMailAsync("Testing Microsoft Graph", "Hello world!", "yuanliang@yulian.onmicrosoft.com");

                // Console.WriteLine("Mail sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending mail: {ex.Message}");
            }
        }

        public async Task CopyMailAsync()
        {
            try
            {
                string messageId = "AAMkAGQ4NGY4ZGU5LWIzNWQtNDFiYi1hZjM5LWIzYzMxNzhjNGVkYwBGAAAAAABi6UOHkfIJTq+dJwoo6gaQBwDoHOF8RpqCTJPIZd3Xqq6SAAAAAAEJAADoHOF8RpqCTJPIZd3Xqq6SAAUY4QLgAAA=";
                string destinationId = "AQMkAGQ4NGY4ZGU5LWIzNWQtNDFiYgAtYWYzOS1iM2MzMTc4YzRlZGMALgAAA2LpQ4eR8glOr50nCijqBpABAOgc4XxGmoJMk8hl3deqrpIAAAIBDAAAAA==";
                await GraphHelper.CopyMailAsync(messageId, destinationId);

                // Console.WriteLine("Mail sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending mail: {ex.Message}");
            }
        }
    }
}
