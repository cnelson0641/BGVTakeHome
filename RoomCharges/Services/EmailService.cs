using System;
using System.Threading.Tasks;
using RoomCharges.Data;
using RestSharp;
using RestSharp.Authenticators;


namespace RoomCharges.Services
{
    public class EmailService{
        public async Task<bool> SendFolioChargeSuccessEmail(FolioCharge folioCharge, string guestName){
			// Get api key, endpoint, and email parameters
			String apiKey   = getMailgunApiKey();
			String endpoint = "sandbox2247aa0598a74c0e82b71c774aa388fe.mailgun.org";
			String from     = "BGV Admin <bgvadmin@" + endpoint + ">";
			String to       = "cnelson0641@gmail.com";
			String subject  = "Folio Charge Success";
			String text     = "Dear " + guestName + ",\n\n" +
				"Your folio charge of " + folioCharge.Amount + " has been successfully charged to your room.\n\n" +
				"Thank you for staying with us.\n\n" +
				"Best regards,\n" +
				"BGV Admin";

            // Create client options obj
            RestClientOptions clientOptions = new()
            {
                BaseUrl = new Uri("https://api.mailgun.net/v3"),
                Authenticator = new HttpBasicAuthenticator("api", apiKey)
            };

            // Create request obj
            RestRequest request = new();
			request.AddParameter ("domain", endpoint, ParameterType.UrlSegment);
			request.Resource = endpoint + "/messages";
			request.AddParameter ("from", from);
			request.AddParameter ("to", to);
			request.AddParameter ("subject", subject);
			request.AddParameter ("text", text);
			request.Method = Method.Post;

			// Create client obj and send request
			RestClient client = new(clientOptions);
			RestResponse result = await client.ExecuteAsync(request);

			// If not successful print error message to console
			if (!result.IsSuccessful) {
				Console.WriteLine ("Error sending email: " + result.StatusCode + " " + result.Content);
			}

			// Return if request was successful
			return result.IsSuccessful;
        }

		public String getMailgunApiKey(){
			// Get api key from environment variable
			String apiKey = Environment.GetEnvironmentVariable("MAILGUN_API_KEY");

			// If api key is null, fail
			if (apiKey == null) {
				throw new Exception("MAILGUN_API_KEY environment variable not set");
			}

			// Return api key
			return apiKey;
		}
    }
}