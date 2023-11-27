namespace CRUD_Class.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public List<BookTag> BookTag { get; set; }
    }
}
