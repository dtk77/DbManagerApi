using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject
{
    public abstract record ProductForManipulationDto
    {
        [Required(ErrorMessage = "Product Name is a required field")]
        [MaxLength(255, ErrorMessage = "Maximum lenght for Name is 255 characters")]
        public string? Name { get; init; }

        public string? Description { get; init; }
    }
}
