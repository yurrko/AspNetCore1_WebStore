using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.infrastructure.Implementations
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

        public void Commit()
        {

        }

        public void Delete( int id )
        {
            var employee = GetById( id );
            if ( !ReferenceEquals( employee , null ) )
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
