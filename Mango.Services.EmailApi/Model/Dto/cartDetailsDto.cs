//using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.EmailApi.Model.Dto
{
    public class cartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}
