using AdilBooks.Data;
using AdilBooks.Interfaces;
using AdilBooks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdilBooks.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorListDto>> ListAuthors()
        {
            var authors = await _context.Authors
                .Select(a => new AuthorListDto
                {
                    AuthorId = a.AuthorId,
                    Name = a.Name
                })
                .ToListAsync();

            return authors;
        }

        public async Task<AuthorDto?> FindAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
            {
                return null;
            }

            return new AuthorDto
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Bio = author.Bio,
                Titles = string.Join(", ", author.Books.Select(b => b.Title))
            };
        }

        public async Task<ServiceResponse> AddAuthor(AuthorDto authorDto)
        {
            ServiceResponse serviceResponse = new();

            try
            {
                var author = new Author
                {
                    Name = authorDto.Name,
                    Bio = authorDto.Bio
                };

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
                serviceResponse.CreatedId = author.AuthorId;
                serviceResponse.Messages.Add("Author added successfully.");
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add($"An error occurred while adding the author: {ex.Message}");
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse> UpdateAuthor(AuthorDto authorDto)
        {
            ServiceResponse serviceResponse = new();

            try
            {
                var author = await _context.Authors.FindAsync(authorDto.AuthorId);
                if (author == null)
                {
                    serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                    serviceResponse.Messages.Add("Author not found.");
                    return serviceResponse;
                }

                author.Name = authorDto.Name;
                author.Bio = authorDto.Bio;

                _context.Entry(author).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
                serviceResponse.Messages.Add("Author updated successfully.");
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add($"An error occurred while updating the author: {ex.Message}");
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse> DeleteAuthor(int id)
        {
            ServiceResponse serviceResponse = new();

            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    serviceResponse.Status = ServiceResponse.ServiceStatus.NotFound;
                    serviceResponse.Messages.Add("Author not found.");
                    return serviceResponse;
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                serviceResponse.Status = ServiceResponse.ServiceStatus.Deleted;
                serviceResponse.Messages.Add("Author deleted successfully.");
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add($"An error occurred while deleting the author: {ex.Message}");
                return serviceResponse;
            }
        }
    }
}
