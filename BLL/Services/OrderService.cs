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
    internal class OrderService : BaseService, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public async Task AddPerformerAsync(OrderDTO projectDTO, UserDTO performer)
        {
            var searchedProject = await _unitOfWork.OrderRepository
                .GetByIdAsync(projectDTO.Id);

            if (searchedProject == null)
                throw new NotFoundException(searchedProject.Title);

            var searchedPerformer = await _unitOfWork.UserRepository
                    .GetByIdAsync(performer.Id);

            if (searchedPerformer == null)
                throw new NotFoundException(searchedPerformer.Name);

            searchedProject.Users.Add(searchedPerformer);
            searchedPerformer.Order = searchedProject;

            await _unitOfWork.OrderRepository.UpdateAsync(searchedProject);
            await _unitOfWork.UserRepository.UpdateAsync(searchedPerformer);
        }

        public async Task ChangeDescriptionAsync(OrderDTO projectDTO, string description)
        {
            var searchedProject = await _unitOfWork.OrderRepository
                .GetByIdAsync(projectDTO.Id);

            if (searchedProject == null)
                throw new NotFoundException(searchedProject.Title);

            searchedProject.Description = description;

            await _unitOfWork.OrderRepository.UpdateAsync(searchedProject);
        }

        public async Task ChangeManagerAsync(OrderDTO projectDTO, UserDTO manager)
        {
            var searchedProject = await _unitOfWork.OrderRepository
                .GetByIdAsync(projectDTO.Id);

            if (searchedProject == null)
                throw new NotFoundException(searchedProject.Title);

            var searchedUser = await _unitOfWork.UserRepository
                    .GetByIdAsync(manager.Id);

            if (searchedUser == null)
                throw new NotFoundException(searchedUser.Name);

            if (searchedUser.Role != UserRole.Waiter)
            {
                //TODO: add exception / maybe this check wont be needed
            }

            searchedUser.Order = searchedProject;
            searchedProject.Chef = searchedUser;

            await _unitOfWork.UserRepository.UpdateAsync(searchedUser);
            await _unitOfWork.OrderRepository.UpdateAsync(searchedProject);
        }

        public async Task CreateOrderAsync(OrderDTO projectDTO)
        {
            var mappedProject = _mapper.Map<Order>(projectDTO);

            await _unitOfWork.OrderRepository.AddAsync(mappedProject);
        }

        public async Task DeleteOrderAsync(OrderDTO projectDTO, UserDTO manager)
        {
            var searchedUser = await _unitOfWork.UserRepository
                .GetByIdAsync(manager.Id);

            if (searchedUser == null)
                throw new NotFoundException(searchedUser.Name);

            var searchedProject = await _unitOfWork.OrderRepository
                    .GetByIdAsync(projectDTO.Id);

            if (searchedProject == null)
                throw new NotFoundException(searchedProject.Title);

            if (searchedUser.Role != UserRole.Waiter)
            {
                //exception
            }

            var projects = await _unitOfWork.OrderRepository
                .GetAllAsync();

            //prbbly should change it
            bool isCurrentProjectManager = projects
                .Exists(x => x.Chef.Id == manager.Id);

            if (isCurrentProjectManager)
            {
                await _unitOfWork.OrderRepository.DeleteAsync(searchedProject);
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

            var projects = await _unitOfWork.OrderRepository
                .GetAllAsync();

            var projectsByManager = projects
                .Where(x => x.Chef.Name == name).ToList();

            if (projectsByManager == null || projectsByManager.Count == 0)
                throw new NotFoundException("");

            return _mapper.ProjectTo<OrderDTO>(projectsByManager as IQueryable);
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int id)
        {
            var searcherProject = await _unitOfWork.OrderRepository
                .GetByIdAsync(id);

            if (searcherProject == null)
                throw new NotFoundException(searcherProject.Title);

            return _mapper.Map<OrderDTO>(searcherProject);
        }
    }
}
