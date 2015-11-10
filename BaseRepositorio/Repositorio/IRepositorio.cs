using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BaseRepositorio.ViewModel;

namespace BaseRepositorio.Repositorio
{
    public interface IRepositorio<TModel, TViewModel>
        where TModel : class
        where TViewModel : IViewModel<TModel>
    {
        TViewModel Add(TViewModel model);

        int Update(TViewModel model);

        int Delete(TViewModel model);

        int Delete(Expression<Func<TModel, bool>> expression);

        ICollection<TViewModel> Get();

        ICollection<TViewModel> Get(Expression<Func<TModel, bool>> expression);

        TViewModel Get(params object[] keys);
    }
}