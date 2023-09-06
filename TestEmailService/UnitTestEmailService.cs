namespace TestEmailService;

[TestClass]
public class  UnitTestEmailService
{
    [TestMethod]
    public async Task TestEmailService()
    {
        // Setup
        FolioCharge folioCharge = new FolioCharge();
        folioCharge.Amount = 100;
        EmailService emailService = new EmailService();
        string guestName = "John Doe";

        // Test
        var result = await(emailService.SendFolioChargeSuccessEmail(folioCharge, guestName));

        // Assertion
        Assert.IsTrue(result);
 
    }
}