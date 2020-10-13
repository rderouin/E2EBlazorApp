using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using AutoMapper;
using BookStore_API.Data;
using BookStore_API.DTOs;

namespace BookStore_API.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorCreateDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();

        }
    }
}
