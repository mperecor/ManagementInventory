using AutoMapper;
using ManagementInventory.Application.Contracts.Persistence;
using ManagementInventory.Application.Exceptions;
using ManagementInventory.Application.Features.Inventory.Command.AddItem;
using ManagementInventory.Application.Features.Inventory.Command.DeleteItemByName.Event;
using ManagementInventory.Application.Features.Inventory.Vm;
using ManagementInventory.Application.Utils;
using ManagementInventory.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ManagementInventory.Application.Features.Inventory.Command.DeleteItemByName
{
    /// <summary>
    /// Handler class that to delete an item by name
    /// </summary>
    public class DeleteItemByNameCommandHandler : IRequestHandler<DeleteItemByNameCommand, ItemCommonObjectVm>
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
        private readonly ILogger<DeleteItemByNameCommandHandler> _logger;

        /// <summary>
        /// Constructor to dependency injection
        /// </summary>
        /// <param name="unitOfWork">Param to inject unitOfWork</param>
        /// <param name="mapper">Param to inject a mapper</param>
        /// <param name="logger">Param to inject logger</param>
        public DeleteItemByNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteItemByNameCommandHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Method to delete an item by name
        /// </summary>
        /// <param name="request">Obecjt type <see cref="DeleteItemByNameCommand"/> to delete an object</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Return an object type <see cref="ItemCommonObjectVm"/> with the data has been deleted</returns>
        /// <exception cref="HttpResponseException"></exception>
        public async Task<ItemCommonObjectVm> Handle(DeleteItemByNameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Entry object {request}");
            var itemInDb = await _unitOfWork.Repository<Item>().GetAsync(x => x.Name == request.Name);

            if (!itemInDb.HasElements())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, new MessageResponseException
                {
                    ErrorCount = 1,
                    Message = "No existe ningún item con ese nombre",
                    Errors = new List<string> { $"No existe ningún item con el nombre {request.Name}" }
                });
            }

            var itemToDelete = itemInDb[0];

            _unitOfWork.Repository<Item>().DeleteEntity(itemToDelete);

            itemToDelete.AddDomainEvent(new ItemRemovedDomainEvent(itemToDelete.Id, itemToDelete.Name, itemToDelete.ExpirationDate, itemToDelete.Type, itemToDelete.Price));

            await _unitOfWork.Complete();

            _logger.LogInformation($"Item has been deleted succesfully {itemToDelete.Id}");

            return _mapper.Map<Item, ItemCommonObjectVm>(itemToDelete);
        }
    }
}