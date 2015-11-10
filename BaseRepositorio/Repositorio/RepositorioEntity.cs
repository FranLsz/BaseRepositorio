using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BaseRepositorio.ViewModel;

namespace BaseRepositorio.Repositorio
{
    public class RepositorioEntity<TModel, TViewModel> : IRepositorio<TModel, TViewModel>
        where TModel : class
        where TViewModel : IViewModel<TModel>, new()
    {

        private DbContext context;

        protected DbSet<TModel> DbSet => context.Set<TModel>();

        public RepositorioEntity(DbContext context)
        {
            this.context = context;
        }

        public virtual TViewModel Add(TViewModel model)
        {
            var m = model.ToBaseDatos();
            DbSet.Add(m);

            try
            {
                context.SaveChanges();

                model.FromBaseDatos(m);

                return model;
            }
            catch (Exception e)
            {
                return default(TViewModel);
            }
        }

        public virtual int Delete(System.Linq.Expressions.Expression<Func<TModel, bool>> expression)
        {
            var data = DbSet.Where(expression);

            DbSet.RemoveRange(data);


            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public virtual int Delete(TViewModel model)
        {
            var data = DbSet.Find(model.GetKeys());

            DbSet.Remove(data);

            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public virtual ICollection<TViewModel> Get()
        {
            var lista = new List<TViewModel>();

            foreach (var model in DbSet)
            {
                TViewModel vm = new TViewModel();
                vm.FromBaseDatos(model);
                lista.Add(vm);
            }

            return lista;
        }

        public virtual TViewModel Get(params object[] keys)
        {
            var data = DbSet.Find(keys);

            if (data == null)
            {
                return default(TViewModel);
            }
            TViewModel vm = new TViewModel();

            vm.FromBaseDatos(data);

            return vm;
        }

        public virtual ICollection<TViewModel> Get(System.Linq.Expressions.Expression<Func<TModel, bool>> expression)
        {
            var data = new List<TViewModel>();
            foreach (var modelo in DbSet.Where(expression))
            {
                TViewModel obj = new TViewModel();

                obj.FromBaseDatos(modelo);

                data.Add(obj);
            }
            return data;
        }

        public virtual int Update(TViewModel model)
        {
            var obj = DbSet.Find(model.GetKeys());

            model.UpdateBaseDatos(obj);

            try
            {
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
