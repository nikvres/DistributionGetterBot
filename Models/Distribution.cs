using System.ComponentModel.DataAnnotations;

namespace DistributionGetterBot.Models
{
	public class Distribution
	{
		[Key]
		public int IdDistribution { get; set; }
		public string? NameDistribution { get; set; }
		public string? ValueDistribution { get; set; }
		public string? DescriptionDistribution { get; set; }
		public string? OSName { get; set; }
		public string? BasedOnDistribution { get; set; }
		public string? OriginDistribution { get; set; }
		public string? ArchitectureDistribution { get; set; }
		public string? CategoryDistribution { get; set; }
		public string? StatusDistribution { get; set; }
		public string? PopularityDistribution { get; set; }
		public string? DesktopDistribution { get; set; }
		public FileStream? PictureDistribution { get; set; }

		public string GetFields() =>
			$"{NameDistribution}\n" +
			$"{DescriptionDistribution}\n" +
			$"Based on: {BasedOnDistribution}\n" +
			$"Origin: {OriginDistribution}" +
			$"Architecture: {ArchitectureDistribution}\n" +
			$"Category: {CategoryDistribution}\n" +
			$"Desktops: {DesktopDistribution}\n" +
			$"{PopularityDistribution}";
	}
}
