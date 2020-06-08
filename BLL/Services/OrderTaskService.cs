using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Services.Base;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class OrderTaskService : BaseService, IOrderTaskService
    {
        public OrderTaskService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper) { }

        public async Task AssignPerformerAsync(OrderTaskDTO projectTaskDTO, UserDTO performer)
        {
            var searchedTask = await _unitOfWork.OrderTaskRepository
                .GetByIdAsync(projectTaskDTO.Id);

            if (searchedTask == null)
                throw new NotFoundException(searchedTask.Title);

            var searchedUser = await _unitOfWork.UserRepository
                .GetByIdAsync(performer.Id);

            if (searchedUser == null)
                throw new NotFoundException(searchedUser.Name);

            searchedTask.Performer = searchedUser;
            searchedUser.Tasks.Add(searchedTask);

            await _unitOfWork.OrderTaskRepository.
                UpdateAsync(searchedTask);

            await _unitOfWork.UserRepository.
                UpdateAsync(searchedUser);
        }

        public async Task CreateTaskAsync(OrderTaskDTO projectTaskDTO)
        {
            var taskExist = await _unitOfWork.OrderTaskRepository
                .GetByCriteriaAsync(x => x.Title == projectTaskDTO.Title) != null;

            if (taskExist) throw new Exception("already exists");//gotta change

            var mappedTask = _mapper.Map<OrderTask>(projectTaskDTO);

            await _unitOfWork.OrderTaskRepository
                .AddAsync(mappedTask);
        }

        public async Task ChangeDesriptionAsync(OrderTaskDTO orderTaskDTO, string description)
        {
            var searchedTask = await _unitOfWork.OrderTaskRepository
                .GetByIdAsync(orderTaskDTO.Id);

            if (searchedTask == null)
                throw new NotFoundException(searchedTask.Title);

            searchedTask.Description = description;

            await _unitOfWork.OrderTaskRepository
                .UpdateAsync(searchedTask);
        }


    }
}
