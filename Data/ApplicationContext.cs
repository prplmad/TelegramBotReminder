using System;
using Microsoft.EntityFrameworkCore;
using DataStore.Entities;


namespace DataStore
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<RemindEntity> Reminds { get; set; }
        public DbSet<StateEntity> States { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
