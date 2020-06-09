using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Services.Base;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Entities.Enums;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    //TODO: add XML annotations to non-trivial methods
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public async Task AddPerformerAsync(OrderDTO orderDTO, UserDTO performer)
        {
            var searchedOrder = await _unitOfWork.OrderRepository
                .GetByIdAsync(orderDTO.Id);

            if (searchedOrder == null)
                throw new NotFoundException(searchedOrder.Title);

            var searchedPerformer = await _unitOfWork.UserRepository
                    .GetByIdAsync(performer.Id);

            if (searchedPerformer == null)
                throw new NotFoundException(searchedPerformer.Name);

            searchedOrder.Users.Add(searchedPerformer);
            searchedPerformer.Order = searchedOrder;

            await _unitOfWork.OrderRepository.UpdateAsync(searchedOrder);
            await _unitOfWork.UserRepository.UpdateAsync(searchedPerformer);
        }

        public async Task ChangeDescriptionAsync(OrderDTO orderDTO, string description)
        {
            var searchedOrder = await _unitOfWork.OrderRepository
                .GetByIdAsync(orderDTO.Id);

            if (searchedOrder == null)
                throw new NotFoundException(searchedOrder.Title);

            searchedOrder.Description = description;

            await _unitOfWork.OrderRepository.UpdateAsync(searchedOrder);
        }

        public async Task ChangeManagerAsync(OrderDTO orderDTO, UserDTO manager)
        {
            var searchedOrder = await _unitOfWork.OrderRepository
                .GetByIdAsync(orderDTO.Id);

            if (searchedOrder == null)
                throw new NotFoundException(searchedOrder.Title);

            var searchedUser = await _unitOfWork.UserRepository
                    .GetByIdAsync(manager.Id);

            if (searchedUser == null)
                throw new NotFoundException(searchedUser.Name);


            searchedUser.Order = searchedOrder;
            searchedOrder.OrderChef = searchedUser;

            await _unitOfWork.UserRepository.UpdateAsync(searchedUser);
            await _unitOfWork.OrderRepository.UpdateAsync(searchedOrder);
        }

        public async Task CreateOrderAsync(OrderDTO orderDTO)
        {
            var mappedOrder = _mapper.Map<Order>(orderDTO);

            await _unitOfWork.OrderRepository.AddAsync(mappedOrder);
        }

        public async Task DeleteOrderAsync(OrderDTO orderDTO, UserDTO manager)
        {
            var searchedUser = await _unitOfWork.UserRepository
                .GetByIdAsync(manager.Id);

            if (searchedUser == null)
                throw new NotFoundException(searchedUser.Name);

            var searchedOrder = await _unitOfWork.OrderRepository
                    .GetByIdAsync(orderDTO.Id);

            if (searchedOrder == null)
                throw new NotFoundException(searchedOrder.Title);

            if (searchedUser.Role != UserRole.Chef)
            {
                //exception
            }

            var orders = await _unitOfWork.OrderRepository
                .GetAllAsync();

            //prbbly should change it
            bool isCurrentOrderManager = orders
                .Exists(x => x.OrderChef.Id == manager.Id);

            if (isCurrentOrderManager)
            {
                await _unitOfWork.OrderRepository.DeleteAsync(searchedOrder);
            }
            else
            {
                //exception or result
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetByManagerAsync(string name)
        {
            var searchedUser = await _unitOfWork.UserRepository
                .GetByCriteriaAsync(x => x.Name == name);

            if (searchedUser == null)
                throw new NotFoundException(searchedUser.Name);

            var orders = await _unitOfWork.OrderRepository
                .GetAllAsync();

            var ordersByManager = orders
                .Where(x => x.OrderChef.Name == name).ToList();

            if (ordersByManager == null || ordersByManager.Count == 0)
                throw new NotFoundException("");

            return _mapper.ProjectTo<OrderDTO>(ordersByManager as IQueryable);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var searchedOrder = await _unitOfWork.OrderRepository
                .GetByIdAsync(id);

            if (searchedOrder == null)
                throw new NotFoundException(searchedOrder.Title);

            return _mapper.Map<OrderDTO>(searchedOrder);
        }
    }
}
