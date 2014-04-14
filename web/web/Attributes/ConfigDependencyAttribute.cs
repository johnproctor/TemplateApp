using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ConfigDependencyAttribute : Attribute
    {
    }
}