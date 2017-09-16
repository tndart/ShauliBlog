using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ShauliBlog.Models;


namespace ShauliBlog.DAL {
    public class BlogContext : DbContext {
        
        /*
        public BlogContext() : base("BlogContext") {
            //Database.SetInitializer<BlogContext>(new DropCreateDatabaseIfModelChanges<BlogContext>());
            Database.SetInitializer<BlogContext>(new CreateDatabaseIfNotExists<BlogContext>());

        }*/

        public DbSet<Fan> Fans { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}