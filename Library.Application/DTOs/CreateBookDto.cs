using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public record CreateBookDto
    {
        [Required(ErrorMessage = "Title is Required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = default!;
        [Required(ErrorMessage ="Author is required")]
        [StringLength(80)]
        public string Author { get; set; } = default!;
        public bool IsAvailable { get; set; } = true;
       

    } 

}
