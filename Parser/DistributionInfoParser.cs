using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace DistributionGetterBot.Parser
{
	public class DistributionInfoParser
	{
		private readonly HtmlParser parser = new HtmlParser();
		private readonly HttpClient client = new HttpClient();
		private IHtmlDocument document;

		public async Task ParsePage(string url)
		{
			var page = await client.GetAsync(url).Result.Content.ReadAsStringAsync();
			document = await parser.ParseDocumentAsync(page);
		}

		public Dictionary<string, string> GetInformationDistribution()
		{
			Dictionary<string, string> dictionaryDistributionInformation = new Dictionary<string, string>();
			var distributionDescription = document.QuerySelectorAll("td.TablesTitle > div > img[align=left] ~ ul > li");
			foreach (var item in distributionDescription)
			{
				string textOfElement = item.Text();
				dictionaryDistributionInformation.Add(
					textOfElement.Substring(0, textOfElement.IndexOf(":")), textOfElement.Substring(textOfElement.IndexOf(":") + 2));
			}
			var info = document.QuerySelector("td.TablesTitle > div:nth-child(2)")!.Text().Split("\n", StringSplitOptions.RemoveEmptyEntries)[4];
			dictionaryDistributionInformation.Add("Description", info);
			dictionaryDistributionInformation.Add("Value", GetDistributionNameWithoutSpace());
			return dictionaryDistributionInformation;
		}
		public string GetDistributionName()
		{
			return document.QuerySelector("td.TablesTitle > div > h1")!.Text();
		}
		public string GetDistributionNameWithoutSpace()
		{
			return document.QuerySelector("select[name = distribution] > option[selected]")!.GetAttribute("value")!;
		}
	}
}
