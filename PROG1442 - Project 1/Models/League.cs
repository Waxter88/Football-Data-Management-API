using System.Text.Json.Serialization;

namespace PROG1442___Project_1.Models
{
    public class League
    {
        //id - primary key unique two letter code
        
        public string ID { get; set; }
        public string Name { get; set; }

        //team
        public ICollection<Team> Teams { get; set; }
        public int TeamCount { get; internal set; }
    }
}
