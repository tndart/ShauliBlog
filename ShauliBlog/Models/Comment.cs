using System.ComponentModel.DataAnnotations.Schema;

namespace ShauliBlog.Models {
    public class Comment {

        public Comment() {
        }

        public Comment(int iD, string title, string author, string authorSiteUrl, string content, int postID) {
            ID = iD;
            Title = title;
            Author = author;
            AuthorSiteUrl = authorSiteUrl;
            Content = content;
            PostID = postID;
        }

        public int ID { get;  set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorSiteUrl { get; set; }

        public string Content { get; set; }

        [ForeignKey("Post")]
        public int PostID { get; set; }

        public Post Post { get; set; }
    }
}