namespace TestEmailService;

[TestClass]
public class  UnitTestEmailService
{
    [TestMethod]
    public async Task TestEmailService()
    {
        // Setup:  Create a folio charge and the email service
        FolioCharge folioCharge = new FolioCharge();
        folioCharge.Amount = 100;
        EmailService emailService = new EmailService();
        string guestName = "John Doe";

        // Test:  Send the email
        var result = await(emailService.SendFolioChargeSuccessEmail(folioCharge, guestName));

        // Assertion:  The email should be sent successfully
        Assert.IsTrue(result);
    }
}