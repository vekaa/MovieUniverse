﻿using MovieUniverse.Data.Models;
using MovieUniverse.WebAPI.Models;

namespace MovieUniverse.WebAPI.Mappers
{
    public interface IGetMovieMapper
    {
        GetMovieDTO MapToDTO(Movie movie);
    }
}