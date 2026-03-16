using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController(IRepository<Employee> employeeRepository, IRepository<Role> roleRepository) : ControllerBase
    {

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);

            if (employee is null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description,
                    Id = x.Id
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployeeAsync([FromBody] CreateEmployeeRequest request)
        {
            var allRoles = await roleRepository.GetAllAsync();

            var intersect = allRoles.IntersectBy(request.RoleIds, x => x.Id);

            if (intersect.Count() != request.RoleIds.Count())
            {
                return Problem(type: "BadRequest",
                               title: "Invalid request",
                               detail: "Роли пользователя заданы некорректно",
                               statusCode: StatusCodes.Status400BadRequest);
            }
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Roles = intersect.ToList(),
                AppliedPromocodesCount = request.AppliedPromocodesCount
            };
            await employeeRepository.CreateAsync(employee);
            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        /// Изменить данные сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployeeAsync(Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            if (request is null)
                return Problem(type: "BadRequest",
                               title: "Invalid request",
                               detail: "Некорректный запрос",
                               statusCode: StatusCodes.Status400BadRequest);

            var updatingEmployee = await employeeRepository.GetByIdAsync(id);

            if (updatingEmployee is null)
                return Problem(type: "NotFound",
                               title: "Not found",
                               detail: "Сотрудник не найден",
                               statusCode: StatusCodes.Status404NotFound);

            var allRoles = await roleRepository.GetAllAsync();

            var intersect = allRoles.IntersectBy(request.RoleIds, x => x.Id);

            if (intersect.Count() != request.RoleIds.Count())
            {
                return Problem(type: "BadRequest",
                               title: "Invalid request",
                               detail: "Роли пользователя заданы некорректно",
                               statusCode: StatusCodes.Status400BadRequest);
            }



            updatingEmployee.FirstName = request.FirstName;
            updatingEmployee.LastName = request.SecondName;
            updatingEmployee.Email = request.Email;
            updatingEmployee.Roles = intersect.ToList();
            updatingEmployee.AppliedPromocodesCount = request.AppliedPromocodesCount;

            await employeeRepository.UpdateAsync(updatingEmployee);

            var employeeModel = new EmployeeResponse()
            {
                Id = updatingEmployee.Id,
                Email = updatingEmployee.Email,
                Roles = updatingEmployee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description,
                    Id = x.Id,
                }).ToList(),
                FullName = updatingEmployee.FullName,
                AppliedPromocodesCount = updatingEmployee.AppliedPromocodesCount
            };

            return employeeModel;
        }
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EmployeeShortResponse>> DeleteEmployeeAsync(Guid id)
        {

            var deletingEmployee = await employeeRepository.GetByIdAsync(id);

            if (deletingEmployee is null)
                return Problem(type: "NotFound",
                               title: "Not found",
                               detail: "Сотрудник не найден",
                               statusCode: StatusCodes.Status404NotFound);

            await employeeRepository.DeleteAsync(deletingEmployee);

            var employeeModel = new EmployeeShortResponse()
            {
                Id = deletingEmployee.Id,
                Email = deletingEmployee.Email,

                FullName = deletingEmployee.FullName,

            };

            return employeeModel;
        }
    }
}