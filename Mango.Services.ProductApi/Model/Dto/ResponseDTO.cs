﻿using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductApi.Model.Dto
{
    public class ResponseDTO
    {
        public object Result { get; set; }
        public bool isSuccess { get; set; } = true;
        public string? message { get; set; } = "";

        public ResponseDTO(object Result , bool isSuccess, string message)
        {
            this.Result = Result;
            this.isSuccess = isSuccess;
            this.message = message;
        }

        public ResponseDTO()
        {
        }
    }
}