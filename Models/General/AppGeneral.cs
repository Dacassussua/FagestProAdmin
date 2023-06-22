using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FagestProAdmin.Models.General
{
    public static class AppGeneral
    {
        public static string PathIdentityUrl = @"http://dukaidentity.somee.com";
        public static string PathLicenceUrl = @"http://fagestproadmin.somee.com";
        public static LoginResponse loginResponse { get; set; }
        public static UserModel CurrentUserModel { get; set; }
    }
}