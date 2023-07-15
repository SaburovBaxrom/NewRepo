using System.ComponentModel.DataAnnotations;

namespace PracticeWebApi.Model.DTO
{
    public class AddRegionDto
    {
		[Required]
		[MaxLength(20)]
		[MinLength(3)]
		public string Code { get; set; }
		[Required]
		[MaxLength(50)]
		[MinLength(2)]
		public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
