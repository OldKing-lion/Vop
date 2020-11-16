﻿using System;

namespace Vop.Api.FluentException
{
    public class ApiException : ApiExceptionBase
    {
        public override int? Code { get; set; } = 4000;
        public ApiException() : base() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}