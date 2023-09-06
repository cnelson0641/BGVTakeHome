using System;
using System.Threading.Tasks;
using RoomCharges.Data;
using RestSharp;
using RestSharp.Authenticators;


namespace RoomCharges.Services
{
    public class EmailService{
        public async Task<bool> SendFolioChargeSuccessEmail(FolioCharge folioCharge, string guestName){
			// Get api key and endpoint from settings
			String apiKey = Environment.GetEnvironmentVariable("MAILGUN_API_KEY");
			String endpoint = "sandbox2247aa0598a74c0e82b71c774aa388fe.mailgun.org";
			String from = "BGV Admin <bgvadmin@" + endpoint + ">";
			String to = "cnelson0641@gmail.com";
			String subject = "Folio Charge Success";
			String text = "Dear " + guestName + ",\n\n" +
				"Your folio charge of " + folioCharge.Amount + " has been successfully charged to your room.\n\n" +
				"Thank you for staying with us.\n\n" +
				"Best regards,\n" +
				"BGV Admin";

			// Create rest client, options, and request
			RestClientOptions clientOptions = new RestClientOptions();
            RestRequest request = new RestRequest ();
			clientOptions.BaseUrl = new Uri("https://api.mailgun.net/v3");
			clientOptions.Authenticator = new HttpBasicAuthenticator("api", apiKey);
			request.AddParameter ("domain", endpoint, ParameterType.UrlSegment);
			request.Resource = endpoint + "/messages";
			request.AddParameter ("from", from);
			request.AddParameter ("to", to);
			request.AddParameter ("subject", subject);
			request.AddParameter ("text", text);
			request.Method = Method.Post;
			RestClient client = new RestClient(clientOptions);

			// Execute request on rest client
			RestResponse result = await client.ExecuteAsync(request);

			// If not successful print error message
			if (!result.IsSuccessful) {
				Console.WriteLine ("Error: " + result.StatusCode + " " + result.Content);
			}

			// Return if request was successful
			return result.IsSuccessful;
        }
    }
}