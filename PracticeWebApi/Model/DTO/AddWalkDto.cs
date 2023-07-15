using System.ComponentModel.DataAnnotations;

namespace PracticeWebApi.Model.DTO
{
	public class AddWalkDto
	{
		[Required]
		[MaxLength(100)]
		[MinLength(2)]
		public string Name { get; set; }

		[Required]
		[MaxLength(300)]
		public string Description { get; set; }
		public double LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }
		[Required]
		public Guid DifficultyId { get; set; }
		[Required]
		public Guid RegionId { get; set; }
	}
}
