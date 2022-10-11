namespace EF_CodeFirst_CRUD.DAL.Entities
{
    public class Country
    {
        public Country()
        {
            this.Cities = new List<City>();
        }
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        // Navigation property
        public List<City> Cities { get; set; }
    }
}
