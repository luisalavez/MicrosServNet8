﻿namespace Web.Utility
{
    public class SD
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        public const string RoleAdmin = "Admin";

        public const string RoleCustomer = "CUSTOMER";

        public const string TokenCookie = "JWTToken";
        public enum ApyType
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }
}
