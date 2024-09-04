using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.MovieApi.Domain.Exceptions;
public class GenreNotExistsException : Exception
{
    public Guid Id { get; set; }

    public GenreNotExistsException(Guid id) : base($"Resourse not found with id = {id}.")
    {
        Id = id;
    }
}
