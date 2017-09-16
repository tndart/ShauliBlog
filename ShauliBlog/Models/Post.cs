using System;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ShauliBlog.Models {
    public class Post {

        public Post() {
        }

        public Post(int ID, string title, string author, string authorSiteUrl, DateTime publishedDate, 
                    string content, string imageUrl, string videoUrl, ICollection<Comment> comments) {
            this.ID = ID;
            this.Title = title;
            this.Author = author;
            this.AuthorSiteUrl = authorSiteUrl;
            this.PublishedDate = publishedDate;
            this.Content = content;
            this.ImageUrl = imageUrl;
            this.VideoUrl = videoUrl;
           // this.Comments = comments;
        }

        #region Props

        public int ID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorSiteUrl { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        /*[NotMapped]
        public Image Image {
            get {
                if (ImageUrl.ToString().ToLower().Contains("http")) {
                    throw new Exception("Cannot get image from url, must be a local path");
                }
                return Image.FromFile(ImageUrl);
            }
        }
        */
        public string VideoUrl { get; set; }

       // public virtual ICollection<Comment> Comments { get; set; }


        #endregion

    }
}