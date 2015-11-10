
namespace BaseRepositorio.ViewModel
{
    public interface IViewModel<TModel> where TModel : class
    {

        TModel ToBaseDatos();

        void FromBaseDatos(TModel model);

        void UpdateBaseDatos(TModel model);

        object[] GetKeys();
    }
}