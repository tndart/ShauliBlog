using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShauliBlog.Models {
    public class Comment {

        public Comment() {
        }

        public Comment(int iD, string title, string author, string authorSiteUrl, DateTime PublishedDate, string content, int postID) {
            this.ID = iD;
            this.Title = title;
            this.Author = author;
            this.PublishedDate = PublishedDate;
            this.AuthorSiteUrl = authorSiteUrl;
            this.Content = content;
            this.PostID = postID;
        }

        public int ID { get;  set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorSiteUrl { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Content { get; set; }

        [ForeignKey("Post")]
        public int PostID { get; set; }

        public Post Post { get; set; }
    }
}