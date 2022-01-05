using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class V1Attribute : ApiVersionAttribute
    {
        public V1Attribute() : base(Version) { }

        public static ApiVersion Version { get; } = new ApiVersion(1, 0);
    }
}
