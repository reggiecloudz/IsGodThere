using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Category : Entity
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}