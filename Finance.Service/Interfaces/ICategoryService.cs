using Finance.Service.EntitiesVO;

namespace Finance.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryVO> Create(CategoryVO categoryVO);
        Task Update(CategoryVO categoryVO);
        Task Delete(Guid id);
        Task<CategoryVO> GetById(Guid id);
        Task<IEnumerable<CategoryVO>> GetByUser(Guid userId);
    }
}