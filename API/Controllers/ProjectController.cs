using BLL.DTO;
using BLL.Services;
using DAL.Entities;
using DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public ProjectController(IOrderService projectService) =>
            _projectService = projectService;

        readonly IOrderService _projectService;

        [HttpGet]
        [Route("project/{id}")]
        [Authorize(Policy = "RequirePerformerRole")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectService.
                GetOrderByIdAsync(id);

            return Ok(project);
        }

        [HttpPost]
        [Route("project/add")]
        public async Task<IActionResult> AddProjectAsync([FromBody] OrderDTO projectDTO)
        {
            await _projectService.CreateOrderAsync(projectDTO);

            return Ok();
        }
    }
}
