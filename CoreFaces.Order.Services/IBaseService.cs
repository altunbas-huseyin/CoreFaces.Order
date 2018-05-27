using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Services
{
    public interface IBaseService<TEntity>
    {
        Guid Save(TEntity tEntity);
        bool Update(TEntity tEntity);
        bool Delete(Guid Id);
        TEntity GetById(Guid id);
    }
}
