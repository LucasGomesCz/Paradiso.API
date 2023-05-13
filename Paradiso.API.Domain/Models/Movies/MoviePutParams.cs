﻿using Microsoft.AspNetCore.Http;

namespace Paradiso.API.Domain.Models.Movies;

public class MoviePutParams
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public bool? HasCopyright { get; set; }
    public string? Description { get; set; }
    public Guid? KindMovieId { get; set; }
    public Guid? GenreId { get; set; }

    public List<Guid>? Cast { get; set; }

    public IFormFile? File { get; set; }
}
