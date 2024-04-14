//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using TransactionsAPI.Application.Common.Interfaces;
//using TransactionsAPI.Application.Common.Security;
//using TransactionsAPI.Domain.Enums;
//using MediatR;
//using Microsoft.EntityFrameworkCore;

//namespace TransactionsAPI.Application.Transactions.Queries.GetTransactions;

//[Authorize]
//public record GetTransactionsQuery : IRequest<TodosVm>;

//public class GetTodosQueryHandler : IRequestHandler<GetTransactionsQuery, TodosVm>
//{
//    private readonly IApplicationDbContext _context;
//    private readonly IMapper _mapper;

//    public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }

//    public async Task<TodosVm> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
//    {
//        return new TodosVm
//        {
//            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
//                .Cast<PriorityLevel>()
//                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
//                .ToList(),

//            Lists = await _context.TodoLists
//                .AsNoTracking()
//                .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
//                .OrderBy(t => t.Title)
//                .ToListAsync(cancellationToken)
//        };
//    }
//}
