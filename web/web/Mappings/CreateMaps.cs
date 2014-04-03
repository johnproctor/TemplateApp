using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;

namespace Web.Mappings
{
    public static class CreateMaps
    {
        //Maps view models to EF entities for validation and to flatten object graphs
        //May want to break out into seperate files if this gets lengthy
        public static void Create() 
        {
            Mapper.CreateMap<SomeOtherManageUserModel, ManageUserViewModel>();
        }
    }
}