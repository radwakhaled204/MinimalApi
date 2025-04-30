namespace MinimalApi.Modeles
{
    public class Product
    {
        public int Id { get; set; }
        //To Avoid Non-nullable Warning
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
