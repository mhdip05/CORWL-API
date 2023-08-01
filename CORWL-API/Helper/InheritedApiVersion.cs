using Microsoft.AspNetCore.Mvc;

namespace CORWL_API.Helper
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class InheritedApiVersion : ApiVersionAttribute
    {
        public InheritedApiVersion(string version) : base(version)
        {
        }
    }
}
