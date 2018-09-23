using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route( "employee" )]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesData _employeesData;
        public EmployeeController( IEmployeesData employeesData )
        {
            _employeesData = employeesData;
        }

        public IActionResult Index()
        {
            return View( _employeesData.GetAll() );
        }
        [Route( "{id}" )]
        public IActionResult Details( int Id )
        {
            // Получаем сотрудника по Id
            var employee = _employeesData.GetById( Id );

            if ( ReferenceEquals( employee , null ) )
                return NotFound();

            return View( employee );
        }
        /// <summary>
        /// Добавление или редактирование сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route( "edit/{id?}" )]
        public IActionResult Edit( int? id )
        {
            EmployeeView model;
            if ( id.HasValue )
            {
                model = _employeesData.GetById( id.Value );
                if ( ReferenceEquals( model , null ) )
                    return NotFound(); // возвращаем результат 404 Not Found
            }
            else
            {
                model = new EmployeeView();
            }
            return View( model );
        }
        [HttpPost]
        [Route( "edit/{id?}" )]
        public IActionResult Edit( EmployeeView model )
        {
            if ( !ModelState.IsValid )
                return View( model );

            if ( model.id > 0 )
            {
                var dbItem = _employeesData.GetById( model.id );
                if ( ReferenceEquals( dbItem , null ) )
                    return NotFound(); // возвращаем результат 404 Not Found

                dbItem.FirstName = model.FirstName;
                dbItem.SurName = model.SurName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
                dbItem.DateOfEmploy = dbItem.DateOfEmploy;
                dbItem.Salary = dbItem.Salary;
            }
            else
            {
                _employeesData.AddNew( model );
            }
            _employeesData.Commit();
            return RedirectToAction( nameof( Index ) );
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns></returns>
        [Route( "delete/{id}" )]
        public IActionResult Delete( int id )
        {
            _employeesData.Delete( id );
            return RedirectToAction( nameof( Index ) );
        }
    }
}