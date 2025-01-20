using Microsoft.EntityFrameworkCore;
using myAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myAPI.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;
        // Using lambda expression
        //public RepositoryBase(RepositoryContext repositoryContext)
        //=> RepositoryContext = repositoryContext;
        //public IQueryable<T> FindAll(bool trackChanges) =>
        //!trackChanges ?
        //RepositoryContext.Set<T>()
        //.AsNoTracking() :
        //RepositoryContext.Set<T>();
        //public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        //bool trackChanges) =>
        //!trackChanges ?
        //RepositoryContext.Set<T>()
        //.Where(expression)
        //.AsNoTracking() :
        //RepositoryContext.Set<T>()
        //.Where(expression);
        //public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        //public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        //public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        // Without lambda expression, longer but easy to read =]]]
        /// <summary>
        /// Initializes the repository with a database context instance.
        /// </summary>
        /// <param name="repositoryContext">The database context for the repository.</param>
        /// <summary>
        /// Khởi tạo repository với một instance của database context.
        /// </summary>
        /// <param name="repositoryContext">Database context cho repository.</param>
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        /// <summary>
        /// Lấy tất cả các thực thể của loại T từ database.
        /// Vô hiệu hóa theo dõi thay đổi nếu trackChanges được đặt là false.
        /// </summary>
        /// <param name="trackChanges">Nếu là false, vô hiệu hóa theo dõi thay đổi để tối ưu hóa cho các trường hợp chỉ đọc.</param>
        /// <returns>IQueryable của tất cả các thực thể loại T.</returns>
        public IQueryable<T> FindAll(bool trackChanges)
        {
            if (!trackChanges)
            {
                return RepositoryContext.Set<T>().AsNoTracking();
            }
            return RepositoryContext.Set<T>();
        }

        /// <summary>
        /// Lấy các thực thể loại T thỏa mãn điều kiện chỉ định.
        /// Vô hiệu hóa theo dõi thay đổi nếu trackChanges được đặt là false.
        /// </summary>
        /// <param name="expression">Điều kiện để lọc các thực thể.</param>
        /// <param name="trackChanges">Nếu là false, vô hiệu hóa theo dõi thay đổi để tối ưu hóa cho các trường hợp chỉ đọc.</param>
        /// <returns>IQueryable của các thực thể loại T thỏa mãn điều kiện chỉ định.</returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            if (!trackChanges)
            {
                return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
            }
            return RepositoryContext.Set<T>().Where(expression);
        }

        /// <summary>
        /// Thêm một thực thể mới của loại T vào context, đánh dấu nó để thêm vào database.
        /// </summary>
        /// <param name="entity">Thực thể cần tạo.</param>
        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// Đánh dấu một thực thể loại T đã tồn tại để cập nhật, các thay đổi sẽ được lưu vào database.
        /// </summary>
        /// <param name="entity">Thực thể cần cập nhật.</param>
        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        /// <summary>
        /// Đánh dấu một thực thể loại T để xóa, nó sẽ được loại bỏ khỏi database.
        /// </summary>
        /// <param name="entity">Thực thể cần xóa.</param>
        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
