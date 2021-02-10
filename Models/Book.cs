using System.ComponentModel.DataAnnotations;

namespace POILibrary.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ID_User { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }


        [Required]
        [Range(1, 10000)]
        [Display(Name = "Pages")]
        public int Pages { get; set; }


        [Required]
        [EnumDataType(typeof(BookState))]
        [Display(Name = "State")]
        public BookState State { get; set; }


        [Required]
        [StringLength(100)]
        [Display(Name = "Author")]
        public string Author { get; set; }


        [Range(0, 2021)]
        [Display(Name = "Year Published")]
        public int YearPublished { get; set; }


        [Range(0, 2021)]
        [Display(Name = "Year Readed")]
        public int YearReaded { get; set; }

        public override bool Equals(object obj)
        {
            var book = obj as Book;

            if (book != null)
            {
                return this.Title == book.Title;
            }

            return false;
        }

        public override int GetHashCode() => base.GetHashCode();
        public object ToObject()
        {
            return new { Title = this.Title, Pages = this.Pages, State = this.State };
        }
    }
}