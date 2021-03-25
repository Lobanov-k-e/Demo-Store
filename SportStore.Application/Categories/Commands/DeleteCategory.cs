using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Categories.Commands
{
    public class DeleteCategory : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategory, int>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public DeleteCategoryRequestHandler(IApplicationContext context, IMapper mapper)             
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteCategory request)
        {
            var category = await _context.Categories.Where(c=>c.Id == request.Id).SingleOrDefaultAsync();
            _ = category ?? throw new ArgumentNullException();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(new System.Threading.CancellationToken());
            return category.Id;
        }
    }

    public class DeleteCategoryValidator : AbstractValidator<DeleteCategory>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}
