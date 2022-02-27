using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Book name is required.")]
		[StringLength(255, ErrorMessage = "Book name cannot be longer than 255 characters.")]
		public string Name { get; set; }
		[Display(Name ="Author name")]
		[Required(ErrorMessage = "Author name is required.")]
		public string AuthorName { get; set; }
		[Required(ErrorMessage = "Genre is required.")]
		public Genre Genre { get; set; }
		public byte GenreId { get; set; }
		[Display(Name="Date added")]
		public DateTime DateAdded { get; set; }
		[Display(Name="Release date")]
		[Required(ErrorMessage = "Release date is required.")]
		public DateTime ReleaseDate { get; set; }
		[Display(Name="Number in stock")]
		[Required(ErrorMessage = "Number in stock is required.")]
		[Range(1, 20, ErrorMessage = "Number in stock should be between 1 and 20.")]
		public int NumberInStock { get; set; }
		[Display(Name="Number available")]
		[Required(ErrorMessage = "Number available is required.")]
		public int NumberAvailable { get; set; }
	}
      
}
