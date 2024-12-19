
namespace namasdev.Apps.Entidades
{
    public class AspNetRole
    {
        public const string ADMINISTRADOR = "Administrador";
        public const string VENDEDOR = "Vendedor";

        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
