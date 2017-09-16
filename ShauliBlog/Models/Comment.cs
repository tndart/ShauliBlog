using System.ComponentModel.DataAnnotations.Schema;

namespace ShauliBlog.Models {
    public class Comment {
        public Comment(int iD, string title, string author, string authorSiteUrl, string content, int postRefID) {
            ID = iD;
            Title = title;
            Author = author;
            AuthorSiteUrl = authorSiteUrl;
            Content = content;
            PostRefID = postRefID;
        }

        public int ID { get; private set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorSiteUrl { get; set; }

        public string Content { get; set; }

        [ForeignKey("Post")]
        public int PostRefID { get; set; }

        public Post Post { get; set; }
    }
}