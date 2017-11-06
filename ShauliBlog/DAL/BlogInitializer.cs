using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShauliBlog.Models;

namespace ShauliBlog.DAL {
    public class BlogInitializer : DropCreateDatabaseAlways<BlogContext> {

        protected override void Seed(BlogContext context) {

            var tags = new List<Tag> {
               new Tag { ID = 1, Name="Sport", Posts=null } ,
               new Tag { ID = 2, Name="Romantic", Posts=null } ,
               new Tag { ID = 3, Name="Entertainment", Posts=null }
           };

            var fans = new List<Fan> {
                new Fan(1, "Liron", "Elbaz", new DateTime(1992,5,9), Gender.Male, 9, "liron", "123456", false,"Israel"),
                new Fan(2, "Yakir", "Nadav", new DateTime(1992,8,23), Gender.Male, 5, "yakir", "123456", false,"PA"),
                new Fan(3, "Mike", "Laury", new DateTime(1980,8,2), Gender.Male, 7, "admin", "admin", true,"Jorden"),
            };

            var comments = new List<Comment> {
                new Comment(1, "Comment 1", "Yakir", "https://Liron.com", DateTime.Today, "first comment ever", 1),
                new Comment(2, "Comment 2", "Yakir", "https://Yakir.com", DateTime.Today, "Second comment ever", 1),
                new Comment(3, "Comment 3", "Liron", "https://Igor.com", DateTime.Today, "Third comment ever", 2),
                new Comment(4, "Comment 4", "Liron", "https://Yakir.com", DateTime.Today, "Fourth comment ever", 3),
                new Comment(5, "Comment 5", "Shemtova", "https://Igor.com", DateTime.Today, "fifth comment ever", 4),
                new Comment(6, "Comment 6", "Shemtova", "https://Igor.com", DateTime.Today, "sixth comment ever", 5)

            };

            var posts = new List<Post> {
                new Post(1, "Post 1", "https://Liron.com", DateTime.Today, "First post ever", "images/flower.png", "", comments.FindAll(x => x.ID < 3), new List<Tag> { tags[0], tags[1] },1),
                new Post(2, "Post 2", "https://Yakir.com", DateTime.Today.AddDays(-2), "Second post ever", "", "", comments.FindAll(x => x.ID == 3), new List<Tag> { tags[0], tags[2] },2),
                new Post(3, "Post 3", "https://Yakir.com", DateTime.Today.AddDays(-3), "third post ever", "", "", comments.FindAll(x => x.ID == 4), new List<Tag> { tags[0] },3),
                new Post(4, "Post 4", "https://Yakir.com", DateTime.Today.AddDays(-3), "forth post ever", "", "", comments.FindAll(x => x.ID == 5), new List<Tag> { tags[1] },1),
                new Post(5, "Post 5", "https://Yakir.com", DateTime.Today.AddDays(-3), "fifth post ever", "", "", comments.FindAll(x => x.ID == 6), tags,2)
           };

            tags[0].Posts = new List<Post> { posts[0], posts[1], posts[2], posts[4] };
            tags[1].Posts = new List<Post> { posts[0], posts[3], posts[4] };
            tags[2].Posts = new List<Post> { posts[1], posts[4] };

            fans.ForEach(f => context.Fans.Add(f));
            context.SaveChanges();
            posts.ForEach(p => context.Posts.Add(p));
            tags.ForEach(t => context.Tags.Add(t));
            comments.ForEach(c => context.Comments.Add(c));
            context.SaveChanges();
        }
    }
}
