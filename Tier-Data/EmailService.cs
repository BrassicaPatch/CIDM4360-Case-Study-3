namespace Mailroom.DataTier;

using Azure;
using Azure.Communication.Email;
using Mailroom.DataTier.Models;

public static class EmailService {

    static string sender = "DoNotReply@877d3c43-8068-43a8-a5be-fe94f3318395.azurecomm.net";
    static string serviceConnectionString =  "endpoint=https://chrisweek10commservice.communication.azure.com/;accesskey=+A/G1goNGBkLoD9vH/R1r/mlNHkWnqMMm6rQMm+/ob8oRzL4SJokojkJijxooo7XkiscWE+dYP8Oz63h2iYjzQ==";

    public static async void SendPackageNotificationEmail(Resident resident, Package package) {
        EmailClient emailClient = new EmailClient(serviceConnectionString);
        var subject = $"Package Pick-Up for {resident.FullName}";
        var htmlContent = $@"
                    <html>
                        <body>
                            <h1 style=color:red>Package Pickup</h1>
                            <h4>Tracking Number: {package.TrackingNumber}</h4>
                            <p>{resident.FullName}, you have a package available for pickup.</p>
                        </body>
                    </html>";

        EmailSendOperation emailSendOperation = await emailClient.SendAsync(
            Azure.WaitUntil.Started,
            sender,
            resident.Email,
            subject,
            htmlContent
        );
    }
}