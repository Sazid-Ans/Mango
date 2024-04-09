namespace Mango.Services.ShoppingCartApi.Model.Dto
{
    public class CartDto
    {
        public CartHeaderDto? CartHeader { get; set; }
        public IEnumerable<cartDetailsDto>? CartDetails { get; set; }
    }
}
