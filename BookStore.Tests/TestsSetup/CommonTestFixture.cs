using AutoMapper;
using BookStore.Common;
using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.TestsSetup
{
    public class CommonTestFixture
    {
        public ApplicationDbContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();
            _context.AddBooks();
            _context.AddGenres();
            _context.SaveChanges();

            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
