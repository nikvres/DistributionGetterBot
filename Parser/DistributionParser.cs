using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using DistributionGetterBot.Models;

namespace DistributionGetterBot.Parser
{
	public class DistributionParser
	{
		private const string MainLink = "https://distrowatch.com/";

		private readonly HtmlParser parser = new HtmlParser();
		private readonly HttpClient client = new HttpClient();

		private IHtmlDocument document;

		public async Task<Distribution> ParseInformationAboutDistribution(string name)
		{
			var page = await client.GetAsync(MainLink + "table.php?distribution=" + name).Result.Content.ReadAsStringAsync();
			document = await parser.ParseDocumentAsync(page);
			return GetDistribution();
		}
		private Distribution GetDistribution()
		{
			var distributionInformation = GetInformationDictionary();
			return new Distribution
			{
				NameDistribution = distributionInformation["Name"],
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
				PictureDistribution = distributionInformation["PictureDistribution"]
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
			dictionaryDistributionInformation.Add("Name", GetDistributionName());
			dictionaryDistributionInformation.Add("Description", GetDistributionDescription());
			dictionaryDistributionInformation.Add("Value", GetDistributionNameWithoutSpace());
			dictionaryDistributionInformation.Add("PictureDistribution", GetPictureDistributionDestination(GetDistributionNameWithoutSpace()));
			return dictionaryDistributionInformation;
		}
		private string GetPictureDistributionDestination(string name)
		{
			foreach (string fileName in Directory.GetFiles(Environment.CurrentDirectory + "\\img"))
			{
				if (fileName.Equals(Environment.CurrentDirectory + "\\img\\" + name + ".png"))
				{
					return fileName;
				}
			}
			return string.Empty;
		}
		private string GetDistributionDescription() =>
			document.QuerySelector("td.TablesTitle > div:nth-child(2)")!.Text().Split("\n", StringSplitOptions.RemoveEmptyEntries)[4];
		private string GetDistributionName() =>
			document.QuerySelector("td.TablesTitle > div > h1")!.Text();
		private string GetDistributionNameWithoutSpace() =>
			document.QuerySelector("select[name = distribution] > option[selected]")!.GetAttribute("value")!;

	}
}
