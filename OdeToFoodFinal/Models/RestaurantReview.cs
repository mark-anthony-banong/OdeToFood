using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFoodFinal.Models
{

    public class MaxWordsAttribute : ValidationAttribute
    {
        public MaxWordsAttribute(int maxWords)
            : base("{0} has too many words.")
        {
            _maxWords = maxWords;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > _maxWords)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }

        private readonly int _maxWords;
    }

    public class RestaurantReview
    {
        public int Id { get; set; }
        //[Range(1,10, ErrorMessage="warning message here") ]
        [Range(1,10)]
        [Required]
        public int Rating { get; set; }
        //validation annotation
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }
        //display annotation
        [Display(Name="Reviewed by")]
        [DisplayFormat(NullDisplayText="anonymous")]
        [MaxWords(1)]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; } //point back to the restaurant that this review is associated with

        //other way to set up validation attribute
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Rating < 2 && ReviewerName.ToLower().StartsWith("scott"))
            {
                yield return new ValidationResult("sorry, scott you cant do this");
            }
        }

    }
}