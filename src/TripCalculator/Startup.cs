﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TripCalculator.Startup))]

namespace TripCalculator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}