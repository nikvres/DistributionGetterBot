using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using DistributionGetterBot.Models;

namespace DistributionGetterBot.Parser
{
	public class DistributionInfoParser
	{
		private const string MainLink = "https://distrowatch.com/table.php?distribution=";
		private readonly HtmlParser parser = new HtmlParser();
		private readonly HttpClient client = new HttpClient();
		private IHtmlDocument document;

		public async Task<Distribution> ParseInformationAboutDistribution(string name)
		{
			var page = await client.GetAsync(MainLink + name).Result.Content.ReadAsStringAsync();
			document = await parser.ParseDocumentAsync(page);
			return GetDistributionFullInformation();
		}

		private string GetDistributionName()
		{
			return document.QuerySelector("td.TablesTitle > div > h1")!.Text();
		}

		private string GetDistributionNameWithoutSpace()
		{
			return document.QuerySelector("select[name = distribution] > option[selected]")!.GetAttribute("value")!;
		}

		private Distribution GetDistributionFullInformation()
		{
			var distributionInformation = GetInformationDictionary();
			return new Distribution
			{
				NameDistribution = GetDistributionName(),
				BasedOnDistribution = distributionInformation["Based on"],
				OSName = distributionInformation["OS Type"],
				ValueDistribution = distributionInformation["Value"],
				OriginDistribution = distributionInformation["Origin"],
				ArchitectureDistribution = distributionInformation["Architecture"],
				DesktopDistribution = distributionInformation["Desktop"],
				CategoryDistribution = distributionInformation["Category"],
				StatusDistribution = distributionInformation["Status"],
				PopularityDistribution = distributionInformation["Popularity"],
				DescriptionDistribution = distributionInformation["Description"],
			};
		}

		private Dictionary<string, string> GetInformationDictionary()
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
	}
}
