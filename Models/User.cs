namespace SolarSystems.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int accessLevel { get; set; }

        public User(long id, string name, int accessLevel)
        {
            this.Id = Id;
            this.Name = name;
            this.accessLevel = accessLevel;
        }

        public User() { }
    }
}
