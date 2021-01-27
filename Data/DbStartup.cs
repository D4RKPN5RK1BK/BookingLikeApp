﻿using BookingLikeApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingLikeApp.Data
{
    public class DbStartup
    {
        public static async Task InitialiseAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
                await roleManager.CreateAsync(new IdentityRole("admin"));

            if (await roleManager.FindByNameAsync("moderator") == null)
                await roleManager.CreateAsync(new IdentityRole("moderator"));

            if (await userManager.FindByNameAsync("admin") == null)
            {
                User admin = new User()
                {
                    UserName = "admin",
                    Email = "ultrameganagibator228@gmail.com"
                };
                await userManager.CreateAsync(admin, "adminadmin");
                await userManager.AddToRoleAsync(admin, "admin");
            }

            
        }
    }
}