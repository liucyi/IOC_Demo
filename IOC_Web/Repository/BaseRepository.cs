using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using IOC_Web.Entity;
using EntityFramework.Extensions;

namespace IOC_Web.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        protected IOC_DbContext context =new IOC_DbContext() ;
        protected DbSet<T> dbSet;

        //public BaseRepository()
        //{
        //    this.context = new IOC_DbContext(context.Configuration.);//  EntityConnectionString.ServiceConnStr;
        //    this.dbSet = context.Set<T>();
        //}

        //public BaseRepository(IOC_DbContext context)
        //{
        //    this.context = context;
        //    this.dbSet = context.Set<T>();
        //}

        public virtual void Insert(T entity)
        {
           
            context.Set<T>().Add(entity);
            SaveChanges();
        }

        public virtual void Update(T entity)
        {
            var entry = this.context.Entry(entity);
            //todo:如果状态没有任何更改，会报错
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> exp)
        {
            context.Set<T>().Where(exp).Delete();
        }

        //public virtual T GetById(Guid gid)
        //{
        //    return dbSet.Find(gid);
        //}
        /// <summary>
        /// 查找单个
        /// </summary>
        public virtual T GetEntity(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }
        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        public virtual int GetCount(Expression<Func<T, bool>> exp = null)
        {
            
            return Filter(exp).Count();
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }

        public virtual IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending = false)
        {
            IQueryable<T> query = context.Set<T>();// dbSet;
            if (exp != null)
            {
                query = query.Where(exp);
            }
            if (orderBy == null)
            {
                throw new NullReferenceException("orderBy不能为空");
            }
            if (orderByAscending)
            {
                query = query.OrderBy(orderBy);
            }
            else
            {
                query = query.OrderByDescending(orderBy);
            }
            return query;
        }

        public virtual IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, Expression<Func<T, TKey>> thenBy, bool thenByAscending = false)
        {
            throw new NotImplementedException();
            //IQueryable<T> query = dbSet;
            //if (exp != null)
            //{
            //    query = query.Where(exp);
            //}
            //if (orderBy == null)
            //{
            //    throw new NullReferenceException("orderBy不能为空");
            //}
            //if (thenBy == null)
            //{
            //    throw new NullReferenceException("thenBy不能为空");
            //}
            //if (orderByAscending)
            //{
            //    if (thenByAscending)
            //    {
            //        query = query.OrderBy(orderBy).ThenBy(thenBy);
            //    }
            //    else
            //    {
            //        query = query.OrderBy(orderBy).ThenByDescending(thenBy);
            //    }
            //}
            //else
            //{
            //    if (thenByAscending)
            //    {
            //        query = query.OrderByDescending(orderBy).ThenBy(thenBy);
            //    }
            //    else
            //    {
            //        query = query.OrderByDescending(orderBy).ThenByDescending(thenBy);
            //    }
            //}
            //return query;
        }

        public virtual IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount)
        {
            IQueryable<T> query = dbSet;
            if (exp != null)
            {
                query = query.Where(exp);
            }
            totalCount = query.Count();

            if (orderBy == null)
            {
                throw new NullReferenceException("orderBy不能为空");
            }
            if (orderByAscending)
            {
                return query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                return query.OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public virtual IEnumerable<T> GetList<TKey>(Expression<Func<T, bool>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount, Expression<Func<T, TKey>> thenBy = null, bool thenByAscending = false)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetListMany(List<Expression<Func<T, bool>>> exp = null)
        {
            IQueryable<T> query = dbSet;
            if (exp != null)
            {
                foreach (var item in exp)
                {
                    query = query.Where(item);
                }
            }
            return query;
        }

        public virtual IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending = false)
        {
            if (orderBy == null)
            {
                throw new NullReferenceException("orderBy不能为空");
            }
            IQueryable<T> query = dbSet;
            if (exp != null)
            {
                foreach (var item in exp)
                {
                    query = query.Where(item);
                }
            }
            if (orderByAscending)
            {
                return query.OrderBy(orderBy);
            }
            return query.OrderByDescending(orderBy);
        }

        public virtual IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, Expression<Func<T, TKey>> thenBy, bool thenByAscending = false)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount)
        {
            if (orderBy == null)
            {
                throw new NullReferenceException("orderBy不能为空");
            }
            IQueryable<T> query = dbSet;
            foreach (var item in exp)
            {
                query = query.Where(item);
            }
            totalCount = query.Count();
            if (orderByAscending)
            {
                return query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return query.OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public virtual IEnumerable<T> GetListMany<TKey>(List<Expression<Func<T, bool>>> exp, Expression<Func<T, TKey>> orderBy, bool orderByAscending, int pageIndex, int pageSize, out int totalCount, Expression<Func<T, TKey>> thenBy, bool thenByAscending)
        {
            throw new NotImplementedException();
        }

        #region sql
        public virtual int ExecuteSql(string sql, params SqlParameter[] parameters)
        {
            return context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public virtual System.Collections.IEnumerable SqlQuery(Type type, string sql, params SqlParameter[] parameters)
        {
            return context.Database.SqlQuery(type, sql, parameters);
        }

        public virtual IEnumerable<T> SqlQuery(string sql, params SqlParameter[] parameters)
        {
            return context.Database.SqlQuery<T>(sql, parameters);
        }

        public DataTable AdoSqlQueryDataTable(Database db, string sql, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =db.Connection.ConnectionString;//  EntityConnectionString.ServiceConnStr;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            if (parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public DataSet AdoSqlQueryDataSet(Database db, string sql, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = db.Connection.ConnectionString;//  EntityConnectionString.ServiceConnStr;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            if (parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public SqlParameter AddSqlParameter(string parameterName, object value, SqlDbType sqlDbType)
        {
            SqlParameter sqlParameter = new SqlParameter();

            sqlParameter.SqlDbType = sqlDbType;
            if (value == null)
            {
                sqlParameter.Value = DBNull.Value;
            }
            else
            {
                sqlParameter.Value = value;
            }
            sqlParameter.ParameterName = parameterName;
            return sqlParameter;
        }
        #endregion

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        #region dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion dispose

        private IQueryable<T> Filter(Expression<Func<T, bool>> exp)
        {
            var dbSet = context.Set<T>().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }
    }
}