using System.ComponentModel.DataAnnotations;

namespace DistributionGetterBot.Models
{
	public class Distribution
	{
		[Key]
		public int id_distribution { get; set; }
		public string? name_distribution { get; set; }
		public string? value_distribution { get; set; }
		public string? description_distribution { get; set; }
		public string? os_name { get; set; }
		public string? based_on_distribution { get; set; }
		public string? origin_distribution { get; set; }
		public string? architecture_distribution { get; set; }
		public string? category_distribution { get; set; }
		public string? status_distribution { get; set; }
		public string? popularity_distribution { get; set; }
		public string? desktop_distribution { get; set; }
	}
}
