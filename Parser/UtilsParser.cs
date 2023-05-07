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
			foreach (var distributionName in distributionNames)
			{
				listOfDistributions.Add(distributionName.Text());
			}
			return listOfDistributions;
		}
		public static async Task<List<string>> GetAllAvailableDistributionValues()
		{
			List<string> listOfDistributions = new List<string>();
			var page = await client.GetAsync(MainLink).Result.Content.ReadAsStringAsync();
			var distributionNames = parser.ParseDocumentAsync(page).Result.QuerySelectorAll("select[name=distribution] > option");
			foreach (var distributionName in distributionNames)
			{
				Console.WriteLine(distributionName.Text());
				listOfDistributions.Add(distributionName.GetAttribute("value")!);
			}
			return listOfDistributions;
		}
		public static async Task DownloadPicture(string name)
		{
			var page = await client.GetAsync(MainLink).Result.Content.ReadAsStringAsync();
			IDocument document = await parser.ParseDocumentAsync(page);
			string? picturePath = document.QuerySelector("td.TablesTitle > div:nth-child(2) > img")!.GetAttribute("src");
			var picture = await client.GetAsync(MainLink + picturePath).Result.Content.ReadAsStreamAsync();
			using (FileStream fileStream = new FileStream($"{Environment.CurrentDirectory}/{name}.png", FileMode.OpenOrCreate))
			{
				await picture.CopyToAsync(fileStream);
			}
		}


	}
}
