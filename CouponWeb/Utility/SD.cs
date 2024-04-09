namespace MangoWeb.Utility
{
    public class SD
    {
        public static string CouponAPIbase { get; set; }
        public static string AuthAPIbase { get; set; }
        public static string ProductAPIbase { get; set; }
        public static string ShoppingCartAPIbase { get; set; }

        public static string OrderAPIbase { get; set; }

        public const string RoleAdmin = "Admin";
        public const string RoleCustomer = "Customer";
        public const string TokenKey = "JwtToken";
        public enum ApiType
        {
            Get,
            Put,
            Post,
            Delete
        };

        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
