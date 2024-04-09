﻿using MangoWeb.Service.Iservice;
using MangoWeb.Utility;
using Newtonsoft.Json.Linq;

namespace MangoWeb.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenKey);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hastoken =  _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenKey, out token);
            return hastoken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenKey, token);
        }
    }
}