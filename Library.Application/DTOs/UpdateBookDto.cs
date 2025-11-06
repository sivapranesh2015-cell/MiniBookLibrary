using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs
{
    public record UpdateBookDto(string? Title=null,string? Author=null,bool? IsAvailable=null);

}
