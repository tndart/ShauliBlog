using System;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ShauliBlog.Models {
    public class Post {
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
            this.Comments = comments;
        }

        #region Props

        public int ID { get; private set; }

        public string Title { get; private set; }

        public string Author { get; private set; }

        public string AuthorSiteUrl { get; private set; }

        public DateTime PublishedDate { get; private set; }

        public string Content { get; private set; }

        public string ImageUrl { get; private set; }

        [NotMapped]
        public Image Image {
            get {
                if (ImageUrl.ToString().ToLower().Contains("http")) {
                    throw new Exception("Cannot get image from url, must be a local path");
                }
                return Image.FromFile(ImageUrl);
            }
        }

        public string VideoUrl { get; private set; }

        public virtual ICollection<Comment> Comments { get; private set; }


        #endregion

    }
}