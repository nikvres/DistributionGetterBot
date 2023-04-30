using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace DistributionGetterBot.Parser
{
	public static class UtilsParser
	{
		private static readonly HttpClient client = new HttpClient();
		private static readonly HtmlParser parser = new HtmlParser();
		private static readonly string MainLink = "https://distrowatch.com/table.php?distribution=3cx";
		public static async Task<List<string>> GetAllAvailableDistributions()
		{
			List<string> listOfDistributions = new List<string>();
			var page = await client.GetAsync(MainLink).Result.Content.ReadAsStringAsync();
			var distributionNames = parser.ParseDocumentAsync(page).Result.QuerySelectorAll("select[name=distribution] > option");
			foreach (var item in distributionNames)
			{
				listOfDistributions.Add(item.Text());
			}
			return listOfDistributions;
		}
		public static async Task<List<string>> GetAllAvailableDistributionValues()
		{
			List<string> listOfDistributions = new List<string>();
			var page = await client.GetAsync(MainLink).Result.Content.ReadAsStringAsync();
			var distributionNames = parser.ParseDocumentAsync(page).Result.QuerySelectorAll("select[name=distribution] > option");
			foreach (var item in distributionNames)
			{
				Console.WriteLine(item.Text());
				listOfDistributions.Add(item.GetAttribute("value")!);
			}
			return listOfDistributions;
		}
	}
}
