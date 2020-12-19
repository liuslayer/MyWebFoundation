using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyWebFoundation.DB.Dapper
{
    public interface IBaseService
    {
        IScopeTransaction CreateScopeTransaction();

        T Get<T>(int id) where T : class;
        List<T> GetAllList<T>() where T : class;
        List<T> GetListWithCondition<T>(object whereConditions) where T : class;
        List<T> GetListWithCondition<T>(string conditions) where T : class;
        List<T> GetListPaged<T>(int pageNumber, int rowsPerPage, string conditions, string orderby) where T : class;
        int? Insert<T>(T entityToInsert) where T : class;
        void Insert<T>(IEnumerable<T> entitiesToInsert) where T : class;
        int Update<T>(T entityToUpdate) where T : class;
        void Update<T>(IEnumerable<T> entitiesToUpdate) where T : class;
        int Delete<T>(int Id) where T : class;
        int Delete<T>(T entityToDel) where T : class;
        int DeleteList<T>(object whereConditions) where T : class;
        int DeleteList<T>(string conditions) where T : class;
        int RecordCount<T>(string conditions = "") where T : class;
        Guid GetGuid();
    }
}
