﻿
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public Book(Guid id, string title, string description) 
        { 
            Id = id;
            Title = title;
            Description = description;
        }
        public Book() { }
    }
}
