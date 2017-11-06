using System;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShauliBlog.Models {
    public class Post {

        private DateTime publishedDate;

        public Post() {
            //this.PublishedDate = DateTime.Now;
        }



        public Post(int ID, string title, string authorSiteUrl, DateTime publishedDate,
                    string content, string imageUrl, string videoUrl, ICollection<Comment> comments, ICollection<Tag> tags,int FanID) {
            this.ID = ID;
            this.Title = title;
            this.AuthorSiteUrl = authorSiteUrl;
            this.PublishedDate = publishedDate;
            this.Content = content;
            this.ImageUrl = imageUrl;
            this.VideoUrl = videoUrl;
            this.Comments = comments;
            this.Tags = tags;
            this.FanID = FanID;

        }

        #region Props
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorSiteUrl { get; set; }

        public DateTime PublishedDate {
            get {
                if (publishedDate != null)
                    return this.publishedDate;
                return DateTime.Now;
            }
            set {
                this.publishedDate = value;
            }
        }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public Image Image {
            get {
                if (ImageUrl.ToString().ToLower().Contains("http")) {
                    throw new Exception("Cannot get image from url, must be a local path");
                }
                return Image.FromFile(ImageUrl);
            }
        }

        public string VideoUrl { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    
        public int FanID { get; set; }
        [ForeignKey("FanID")]
        public virtual Fan Fan { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        [NotMapped]
        public string tagsToString {
            get {
                StringBuilder all = new StringBuilder();
                foreach (var tag in Tags) {
                    all.Append(tag.ID + "-");
                }
                return all.ToString();
            }
        }

        #endregion

    }

    public class PostObject {
        public string Date { get; set; }
    }


}
