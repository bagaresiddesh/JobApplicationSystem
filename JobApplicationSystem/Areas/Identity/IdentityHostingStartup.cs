﻿using System;
using JobApplicationSystem.Areas.Identity.Data;
using JobApplicationSystem.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(JobApplicationSystem.Areas.Identity.IdentityHostingStartup))]
namespace JobApplicationSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<JobApplicationSystemContext>(options =>
                   options.UseSqlServer(
                   context.Configuration.GetConnectionString("JobApplicationSystemContextConnection")));

                services.AddDefaultIdentity<JobApplicationSystemUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<JobApplicationSystemContext>();
               
            });
        }
    }
}