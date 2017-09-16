using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShauliBlog.Models;

namespace ShauliBlog.DAL {
    public class BlogInitializer : DropCreateDatabaseIfModelChanges<BlogContext> {

        protected override void Seed(BlogContext context) {
            var fans = new List<Fan> {
                new Fan(1, "Liron", "Elbaz", new DateTime(1992,5,9), Gender.Male, 9),
                new Fan(2, "Yakir", "Nadav", new DateTime(1992,8,23), Gender.Male, 5)
            };
            
            var comments = new List<Comment> {
                new Comment(1, "Comment 1", "Liron", "https://Liron.com", "first comment ever", 1),
                new Comment(2, "Comment 2", "Yakir", "https://Yakir.com", "Second comment ever", 1),
                new Comment(3, "Comment 3", "Igor", "https://Igor.com", "Third comment ever", 2)
            };

            var posts = new List<Post> {
                new Post(1, "Post 1", "Liron", "https://Liron.com", DateTime.Today, "First post ever", "", "", comments.FindAll(x => x.ID < 3)),
                new Post(2, "Post 2", "Yakir", "https://Yakir.com", DateTime.Today.AddDays(-2), "Second post ever", "", "", comments.FindAll(x => x.ID == 3))
            };
            
            fans.ForEach(f => context.Fans.Add(f));
            comments.ForEach(c => context.Comments.Add(c));
            posts.ForEach(p => context.Posts.Add(p));

            context.SaveChanges();
        }
    }
}