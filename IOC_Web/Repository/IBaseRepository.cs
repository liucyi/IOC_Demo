using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace IOC_Web.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> exp);
        //T GetById(Guid gid);
        T GetEntity(Expression<Func<T, bool>> exp);
        int GetCount(Expression<Func<T, bool>> exp = null);
        IEnumerable<T> GetList(Expression<Func<T, bool>> exp = null);
        IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending = false);
        IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, Expression<Func<T, TKey>> thenBy, bool thenByAscending = false);
        IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount);
        IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount, Expression<Func<T, TKey>> thenBy = null, bool thenByAscending = false);

        IEnumerable<T> GetListMany(List<Expression<Func<T, bool>>> exp = null);
        IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending = false);
        IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, Expression<Func<T, TKey>> thenBy, bool thenByAscending = false);
        IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount);
        IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount, Expression<Func<T, TKey>> thenBy, bool thenByAscending);

        #region sql
        int ExecuteSql(string sql, params SqlParameter[] parameters);
        IEnumerable SqlQuery(Type type, string sql, params SqlParameter[] parameters);
        IEnumerable<T> SqlQuery(string sql, params SqlParameter[] parameters);
        DataTable AdoSqlQueryDataTable(Database db, string sql, params SqlParameter[] parameters);
        DataSet AdoSqlQueryDataSet(Database db, string sql, params SqlParameter[] parameters);
        SqlParameter AddSqlParameter(string parameterName, object value, SqlDbType sqlDbType);
        #endregion
    }
}
