using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace PROG1442___Project_1.Models
{
    public class Team : IValidatableObject
    {

        public int ID { get; set; }
        //The name of a team cannot start with any of the Letters X, F or S.
        [Display(Name = "Team Name")]
        [Required(ErrorMessage = "You cannot leave the team name blank.")]
        [StringLength(70, ErrorMessage = "Team name cannot be more than 70 characters long.")]
        [RegularExpression("^[^XFS].*", ErrorMessage = "Team name cannot start with X, F or S.")]
        public string Name { get; set; }

        
        [Required(ErrorMessage = "You cannot leave the Budget blank.")]
        [Range(500.0, 10000.0, ErrorMessage = "Budget must be between $500 and $10,000.")]
        [DataType(DataType.Currency)]
        
        public double Budget { get; set; }

        //league
        [Display(Name = "League")]
        public string LeagueID { get; set; }

        public League League { get; set; }

        //players
        [Display(Name = "Players")]
        public ICollection<Player> Players { get; set; }
        public int PlayerCount { get; internal set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Name[0] == 'X' || Name[0] == 'F' || Name[0] == 'S')
            {
                yield return new ValidationResult("Team names are not allowed to start with the letters X, F, or S.", new[] { "Name" });
            }
        }
    }
}
