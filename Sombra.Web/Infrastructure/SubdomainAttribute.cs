﻿using System;

namespace Sombra.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SubdomainAttribute : Attribute
    {
        public string Redirect { get; set; }
    }
}
