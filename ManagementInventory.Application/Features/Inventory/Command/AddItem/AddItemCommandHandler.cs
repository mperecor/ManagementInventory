using AutoMapper;
using ManagementInventory.Application.Contracts.Persistence;
using ManagementInventory.Application.Exceptions;
using ManagementInventory.Application.Features.Inventory.Command.AddItem.Event;
using ManagementInventory.Application.Features.Inventory.Vm;
using ManagementInventory.Application.Utils;
using ManagementInventory.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ManagementInventory.Application.Features.Inventory.Command.AddItem
{
    /// <summary>
    /// Handler class that insert a new item
    /// </summary>
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, ItemCommonObjectVm>
    {
        /// <summary>
        /// UnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Property using to mapper objects
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Property to do log
        /// </summary>
        private readonly ILogger<AddItemCommandHandler> _logger;

        /// <summary>
        /// Constructor to dependency injection
        /// </summary>
        /// <param name="unitOfWork">Param to inject unitOfWork</param>
        /// <param name="mapper">Param to inject a mapper</param>
        /// <param name="logger">Param to inject logger</param>
        public AddItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddItemCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Method to insert a new item
        /// </summary>
        /// <param name="request">Obecjt type <see cref="AddItemCommand"/> to insert a new object</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Return an object type <see cref="ItemCommonObjectVm"/> with the result of operation</returns>
        /// <exception cref="HttpResponseException"></exception>
        public async Task<ItemCommonObjectVm> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entry object {request}");
            var itemInDb = await _unitOfWork.Repository<Item>().GetAsync(x => x.Name == request.Name, includes: null);

            if (itemInDb.HasElements())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new MessageResponseException
                {
                    ErrorCount = 1,
                    Message = "Ya existe un item con ese nombre",
                    Errors = new List<string> { $"El item con nombre {request.Name} ya existe" }
                });
            }

            var itemToInsert = _mapper.Map<AddItemCommand, Item>(request);

            _unitOfWork.Repository<Item>().AddEntity(itemToInsert);

            if (itemToInsert.ExpirationDate.HasValue)
            {
                itemToInsert.AddDomainEvent(new ItemExpiredDomainEvent(itemToInsert.Id));
            }

            await _unitOfWork.Complete();

            _logger.LogInformation($"Item has been inserted succesfully {itemToInsert.Id}");

            return _mapper.Map<Item, ItemCommonObjectVm>(itemToInsert);
        }
    }
}