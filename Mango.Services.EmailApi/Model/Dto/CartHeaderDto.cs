namespace Mango.Services.EmailApi.Model.Dto
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double CartTotal { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName
        {
            get { return $"{FirstName} {LastName}"; }
            set { FullName = value; }
        }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
