using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WMIP.Data.Models;

namespace WMIP.Data
{
    public class WmipDbContext : IdentityDbContext<User>
    {
        public WmipDbContext(DbContextOptions<WmipDbContext> options)
            : base(options)
        {
        }
    }
}
