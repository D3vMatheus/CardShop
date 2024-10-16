using CardShop.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Newtonsoft.Json.Serialization;

namespace CardShop.DTOs
{
    public class ProductDTOUpdateRequest : IValidatableObject
    {
        [Required]
        [Range(1, 9999, ErrorMessage = "Product has to be between 1 and 9999")]
        public float storage { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(CreatedAt.Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("Date has to be greater the actual date",
                    new[] { nameof(this.CreatedAt) });
            }
        }
    }
}
