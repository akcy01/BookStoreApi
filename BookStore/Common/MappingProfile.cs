﻿using AutoMapper;
using BookStore.BookOperations.CreateBooks;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.GenreOperations.GetGenreDetail;
using BookStore.GenreOperations.GetGenres;
using BookStore.Models;
using BookStore.UserOperations.CreateUser;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre,opt => opt.MapFrom(src => src.Genres.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genres.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<CreateUserModel, User>();
        }
    }
}
