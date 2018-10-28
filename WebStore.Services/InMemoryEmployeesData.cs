using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.ViewModel.Product;
using WebStore.Interfaces;

namespace WebStore.Services
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List <EmployeeView> _employees = new List <EmployeeView>
        {
            new EmployeeView
            {
                id = 1,
                FirstName = "Иван" ,
                SurName = "Иванов" ,
                Patronymic = "Иванович" ,
                Age = 22,
                DateOfEmploy = new DateTime(2018, 1, 1),
                Salary = 60000,
            },
            new EmployeeView
            {
                id = 2,
                FirstName = "Владислав" ,
                SurName = "Петров" ,
                Patronymic = "Иванович" ,
                Age = 35,
                DateOfEmploy = new DateTime(2016,2,2),
                Salary = 665544.33,

            }
        };

        public void AddNew( EmployeeView model )
        {
            model.id = _employees.Max( e => e.id ) + 1;
            _employees.Add( model );
        }

        public EmployeeView UpdateEmployee( int id, EmployeeView entity )
        {
            if ( entity == null )
                throw new ArgumentNullException( nameof( entity ) );

            var employee = _employees.FirstOrDefault( e => e.id.Equals( id ) );
            if ( employee == null )
                throw new InvalidOperationException( "Employee not exits" );

            employee.Age = entity.Age;
            employee.FirstName = entity.FirstName;
            employee.Patronymic = entity.Patronymic;
            employee.SurName = entity.SurName;
            employee.DateOfEmploy = entity.DateOfEmploy;
            employee.Salary = entity.Salary;

            return employee;
        }

        public void Delete( int id )
        {
            var employee = GetById( id );
            if ( !ReferenceEquals( employee, null ) )
            {
                _employees.Remove( employee );
            }
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById( int id )
        {
            return _employees.FirstOrDefault( i => i.id == id );
        }
    }
}
