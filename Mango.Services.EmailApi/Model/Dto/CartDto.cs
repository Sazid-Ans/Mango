namespace Mango.Services.EmailApi.Model.Dto
{
    public class CartDto
    {
        public CartHeaderDto? CartHeader { get; set; }
        public IEnumerable<cartDetailsDto>? CartDetails { get; set; }
    }
}
