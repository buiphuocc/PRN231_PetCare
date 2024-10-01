using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Infrastructure.ViewModels.OrderDTO;
namespace Infrastructure.Utils
{
    public class SendMail
    {
        public static string GenerateRandomCodeWithExpiration(string token, int minutesToExpire)
        {
            Random random = new Random();
            StringBuilder codeWithExpiration = new StringBuilder();

            List<char> digitChars = token.Where(char.IsDigit).ToList();

            for (int i = 0; i < 6; i++)
            {
                char randomDigit = digitChars[random.Next(0, digitChars.Count)];
                codeWithExpiration.Append(randomDigit);
            }

            DateTime expirationTime = DateTime.Now.AddMinutes(minutesToExpire);
            string code = codeWithExpiration.ToString();

            return code;
        }
        public static async Task<bool> SendResetPass(IMemoryCache cache, string toEmail, string code, bool showExpirationTime)
        {
            var userName = "Bbone";
            var emailFrom = "minhnkase173338@fpt.edu.vn";
            var password = "imdf ujmi glgg exaj";

            var subjet = "Reset Password Confirmation";

            if (!showExpirationTime)
            {
                code = new string(code.Take(6).ToArray());
            }
            var body = $"Please enter this code to reset your password: {code}";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subjet;
            message.Body = new TextPart("html")
            {
                Text = body
            };
            // Lưu mã OTP vào cache
            string key = $"{toEmail}_OTP";
            cache.Set(key, code, TimeSpan.FromMinutes(1));
            // Thêm logic xóa key OTP khi hết hạn
            Task.Delay(TimeSpan.FromMinutes(1)).ContinueWith(_ =>
            {
                cache.Remove(key);
            });

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                //authenticate account email
                client.Authenticate(emailFrom, password);

                try
                {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public static async Task<bool> SendConfirmationEmail(
            string toEmail,
            string confirmationLink
        )
        {
            var userName = "Bbone";
            var emailFrom = "minhnkase173338@fpt.edu.vn";
            var password = "imdf ujmi glgg exaj";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Confirmation your email to login";
            message.Body = new TextPart("html")
            {
                Text =
                    @"
        <html>
            <head>
                <style>
                    body {
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        margin: 0;
                        font-family: Arial, sans-serif;
                    }
                    .content {
                        text-align: center;
                    }
                    .button {
                        display: inline-block;
                        padding: 10px 20px;
                        background-color: #000;
                        color: #ffffff;
                        text-decoration: none;
                        border-radius: 5px;
                        font-size: 16px;
                    }
                </style>
            </head>
            <body>
                <div class='content'>
                    <p>Please click the button below to confirm your email:</p>                    
                      <a class='button' href='"
                    + confirmationLink
                    + "'>Confirm Email</a>"
                    + @"
                </div>
            </body>
        </html>
    "
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                //authenticate account email
                client.Authenticate(emailFrom, password);

                try
                {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public static async Task<bool> SendRegistrationSuccessEmail(string toEmail)
        {
            var userName = "Bbone";
            var emailFrom = "minhnkase173338@fpt.edu.vn";
            var password = "imdf ujmi glgg exaj";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Registration Successful";
            message.Body = new TextPart("html")
            {
                Text =
                    @"
    <html>
        <head>
            <style>
                body {
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh;
                    margin: 0;
                    font-family: Arial, sans-serif;
                }
                .content {
                    text-align: center;
                }
            </style>
        </head>
        <body>
            <div class='content'>
                <p>Your registration has been confirmed successfully. Welcome to BBone!</p>
            </div>
        </body>
    </html>
"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                //authenticate account email
                client.Authenticate(emailFrom, password);

                try
                {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public static async Task<bool> SendOrderPaymentSuccessEmail(ShowOrderSuccessEmailDTO orderEmailDto, string toEmail)
        {
            var userName = "Bbone";
            var emailFrom = "minhnkase173338@fpt.edu.vn";
            var password = "imdf ujmi glgg exaj";


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(userName, emailFrom));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Order Payment Successful";

            var orderItemsHtml = string.Join("", orderEmailDto.orderitems.Select(item => $@"
        <tr>
            <td>{item.productname}</td>
            <td>{item.Quantity}</td>
            <td>{item.Price:C}</td>
            <td>{item.totalprice:C}</td>
        </tr>
    "));

            message.Body = new TextPart("html")
            {
                Text = $@"
<html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 0;
                display: flex;
                justify-content: center;
                align-items: center;
                height: 100vh;
            }}
            .container {{
                width: 80%;
                margin: auto;
            }}
            .content {{
                text-align: center;
            }}
            table {{
                width: 100%;
                border-collapse: collapse;
            }}
            th, td {{
                padding: 10px;
                border: 1px solid #ddd;
                text-align: left;
            }}
            th {{
                background-color: #f2f2f2;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='content'>
                <h1>Thank you for your purchase, {orderEmailDto.username}!</h1>
                <p>Your payment for order ID {orderEmailDto.orderid} has been confirmed successfully on {orderEmailDto.paymentdate:MMMM dd, yyyy}.</p>
                <h2>Order Details</h2>
                <table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Quantity</th>
                            <th>Price</th>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tbody>
                        {orderItemsHtml}
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan='3' style='text-align:right'><strong>Total Price:</strong></td>
                            <td>{orderEmailDto.totalprice:C}</td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </body>
</html>"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(emailFrom, password);

                try
                {
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    return true;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }



    }
}
