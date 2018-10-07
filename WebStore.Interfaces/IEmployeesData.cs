﻿using System.Collections.Generic;
using WebStore.Domain.Models.Product;

namespace WebStore.Interfaces
{
    public interface IEmployeesData
    {
        /// <summary>
        /// Получение списка сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeView> GetAll();
        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        EmployeeView GetById( int id );
        /// <summary>
        /// Сохранить изменения
        /// </summary>
        void Commit();
        /// <summary>
        /// Добавить нового
        /// </summary>
        /// <param name="model"></param>
        void AddNew( EmployeeView model );
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        void Delete( int id );
    }
}
