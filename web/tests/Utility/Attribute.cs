using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.AutoEF;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using System.Data.Entity;
using global::Model;

namespace Tests.Utility
{


    //https://github.com/alexfoxgill/AutoFixture.AutoEntityFramework
    public class AutoEFAttribute : AutoDataAttribute
    {
        public AutoEFAttribute()
            : base(new Fixture()
                .Customize(new EntityCustomization(new DbContextEntityTypesProvider(typeof(Model1Container)))))
        { }
    }
}
