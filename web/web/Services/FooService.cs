﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Services
{
    public interface IFooService 
    {
        void Do();
    }
    
    public class FooService : IFooService
    {
        public void Do()
        { }
    }
}