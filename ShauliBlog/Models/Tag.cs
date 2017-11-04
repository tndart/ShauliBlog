using System.Collections.Generic;

namespace ShauliBlog.Models {
    public class Tag {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}