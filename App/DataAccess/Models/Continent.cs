namespace DataAccess.Models
{
    public class Continent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public Map Map { get; set; }
    }
}
