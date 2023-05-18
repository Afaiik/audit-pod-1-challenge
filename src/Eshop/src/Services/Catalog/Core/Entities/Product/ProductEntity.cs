namespace Core.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string Description { get; set; } = default!;

        public int AvailableStock { get; set; }

        public float Price { get; set; }

    }
}
