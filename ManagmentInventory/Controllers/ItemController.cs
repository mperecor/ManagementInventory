using ManagementInventory.Application.Features.Inventory.Command.AddItem;
using ManagementInventory.Application.Features.Inventory.Command.DeleteItemByName;
using ManagementInventory.Application.Features.Inventory.Vm;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ManagementInventory.API.Controllers
{
    /// <summary>
    /// Class controller to item
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Constructor to inject dependency
        /// </summary>
        /// <param name="mediator"></param>
        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method to add a new item
        /// </summary>
        /// <param name="request">Object type <see cref="AddItemCommand"/> with data of item to insert</param>
        /// <returns>Return an object type <see cref="ItemCommonObjectVm"/> with data of item that has been inserted</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ItemCommonObjectVm), (int)HttpStatusCode.OK)]
        public async Task<ItemCommonObjectVm> AddItem([FromBody]AddItemCommand request)
        {
            return await _mediator.Send(request);
        }

        /// <summary>
        /// Method to delete a item by name
        /// </summary>
        /// <param name="name">Name of item to delete</param>
        /// <returns>>Return an object type <see cref="ItemCommonObjectVm"/> with data of item that has been deleted</returns>
        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(ItemCommonObjectVm), (int)HttpStatusCode.OK)]
        public async Task<ItemCommonObjectVm> DeleteItemByName([FromRoute] string name)
        {
            return await _mediator.Send(new DeleteItemByNameCommand(name));
        }
    }
}