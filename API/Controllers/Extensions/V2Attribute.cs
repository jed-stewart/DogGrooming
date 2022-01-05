using System;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable 1591
namespace API.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class V2Attribute : ApiVersionAttribute
    {
        public V2Attribute() : base(Version) { }

        public static ApiVersion Version { get; } = new ApiVersion(2, 0);
    }
}
