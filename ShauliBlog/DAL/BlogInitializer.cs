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
                new Fan(1, "Liron", "Elbaz", new DateTime(1992,5,9), Gender.Male, 9, "liron", "123456", false, "Israel"),
                new Fan(2, "Yakir", "Nadav", new DateTime(1992,8,23), Gender.Male, 5, "yakir", "123456", false, "Israel"),
                new Fan(3, "Shem-tova", "Yerhi", new DateTime(1992,8,2), Gender.Female, 7, "admin", "admin", true, "France"),
                new Fan(4, "Vered", "Gotliv", new DateTime(1980,6,8), Gender.Female, 19, "vered", "123456", true, "USA")
            };

            var comments = new List<Comment> {
                new Comment(1, "Comment 1", "Liron", "https://Liron.com", DateTime.Today, "first comment ever", 1),
                new Comment(2, "Comment 2", "Yakir", "https://Yakir.com", DateTime.Today, "Second comment ever", 1),
                new Comment(3, "Comment 3", "Shemtova", "https://Shemtova.com", DateTime.Today, "Third comment ever", 2),
                new Comment(4, "Comment 4", "Yakir", "https://Yakir.com", DateTime.Today, "Fourth comment ever", 3),
                new Comment(5, "Comment 5", "Liron", "https://Liron.com", DateTime.Today, "fifth comment ever", 4),
                new Comment(6, "Comment 6", "Shemtova", "https://Shemtova.com", DateTime.Today, "sixth comment ever", 5),
                new Comment(7, "Comment 7", "Vered", "https://Vered.com", DateTime.Today, "7 comment ever", 6),
                new Comment(8, "Comment 8", "Vered", "https://Vered.com", DateTime.Today, "8 comment ever", 6),
                new Comment(9, "Comment 9", "Yakir", "https://Yakir.com", DateTime.Today, "9 comment ever", 7)

            };

            var posts = new List<Post> {
                new Post(1, "Post 1", 1, "https://Shemtova.com", DateTime.Today, "First post ever", "images/flower.png", "KhzGSHNhnbI", comments.FindAll(x => x.ID < 3), new List<Tag> { tags[0], tags[1] }),
                new Post(2, "Post 2", 2, "https://Yakir.com", DateTime.Today.AddDays(-2), "Second post ever", "", "", comments.FindAll(x => x.ID == 3), new List<Tag> { tags[0], tags[2] }),
                new Post(3, "Post 3", 3, "https://Vered.com", DateTime.Today.AddDays(-3), "third post ever", "", "", comments.FindAll(x => x.ID == 4), new List<Tag> { tags[0] }),
                new Post(4, "Post 4", 2, "https://Yakir.com", DateTime.Today.AddDays(-3), "forth post ever", "", "", comments.FindAll(x => x.ID == 5), new List<Tag> { tags[1] }),
                new Post(5, "Post 5", 3, "https://Yakir.com", DateTime.Today.AddDays(-8), "fifth post ever", "", "", comments.FindAll(x => x.ID == 6), new List<Tag> { tags[0], tags[2] }),
                new Post(6, "Post 6", 4, "https://Vered.com", DateTime.Today.AddDays(-9), "6 post ever", "", "", comments.FindAll(x => x.ID == 7 || x.ID == 8), tags),
                new Post(7, "Post 7", 1, "https://Shemtova.com", DateTime.Today.AddDays(-29), "7 post ever", "", "", comments.FindAll(x => x.ID == 9), new List<Tag> { tags[2] })
            };


            tags[0].Posts = new List<Post> { posts[0], posts[1], posts[2], posts[4], posts[5] };
            tags[1].Posts = new List<Post> { posts[0], posts[3], posts[4] };
            tags[2].Posts = new List<Post> { posts[1], posts[4], posts[5], posts[6] };

            fans.ForEach(f => context.Fans.Add(f));
            context.SaveChanges();
            posts.ForEach(p => context.Posts.Add(p));
            tags.ForEach(t => context.Tags.Add(t));
            comments.ForEach(c => context.Comments.Add(c));
            context.SaveChanges();
        }
    }
}
