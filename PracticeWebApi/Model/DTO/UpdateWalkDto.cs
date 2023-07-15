using System.ComponentModel.DataAnnotations;

namespace PracticeWebApi.Model.DTO
{
	public class UpdateWalkDto
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(300)]
		public string Description { get; set; }
		[Required]
		public double LengthInKm { get; set; }
		public string? WalkImageUrl { get; set; }
		public Guid DifficultyId { get; set; }
		public Guid RegionId { get; set; }

	}
}
