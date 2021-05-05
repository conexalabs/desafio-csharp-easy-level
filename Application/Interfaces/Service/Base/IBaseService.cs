using Application.Entidades.Base;

namespace Application.Interfaces.Service.Base
{
    public interface IBaseService<TViewModelRequest,TViewModelResponse, TEntity> where TEntity : EntityBase
    {
    }
}