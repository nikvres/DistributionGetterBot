using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace Parser
{
	public static class UtilsParser
	{
		private static readonly HtmlParser parser = new HtmlParser();
		private static readonly string page = "https://distrowatch.com/table.php?distribution=3cx";
		public static List<string> GetAllAvailableDistributions()
		{
			List<string> listOfDistributions = new List<string>();
			var distributionNames = parser.ParseDocumentAsync(page).Result.QuerySelectorAll("select[name=distribution] > option");
			foreach (var item in distributionNames)
			{
				listOfDistributions.Add(item.Text());
			}
			return listOfDistributions;
		}
		public static List<string> GetAllAvailableDistributionValues()
		{
			List<string> listOfDistributions = new List<string>();
			var distributionNames = parser.ParseDocumentAsync(page).Result.QuerySelectorAll("select[name=distribution] > option");
			foreach (var item in distributionNames)
			{
				listOfDistributions.Add(item.GetAttribute("value")!);
			}
			return listOfDistributions;
		}
	}
}
