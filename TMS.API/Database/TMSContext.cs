using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models.Model;

namespace TMS.API.Database
{
    public class TMSContext : DbContext
    {
        public TMSContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        //entities
        public DbSet<User> Users { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<BreakTime> BreakTimes { get; set; }
        public DbSet<LeaveTime> LeaveTimes { get; set; }
    }
}
