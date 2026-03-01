using AutoMapper;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Service.EntitiesVO;
using Finance.Service.Interfaces;

namespace Finance.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryVO> Create(CategoryVO categoryVO)
        {
            var entity = _mapper.Map<Category>(categoryVO);

            _repository.Add(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<CategoryVO>(entity);
        }

        public async Task Update(CategoryVO categoryVO)
        {
            // Buscamos a categoria existente para garantir que o EF a rastreie
            var existingCategory = await _repository.GetById(categoryVO.Id);

            if (existingCategory == null)
                throw new Exception("Categoria não encontrada.");

            // Mapeamos as mudanças do VO para a Entidade existente
            _mapper.Map(categoryVO, existingCategory);

            _repository.Update(existingCategory);
            await _repository.SaveChangesAsync();
        }

        public async Task<CategoryVO> GetById(Guid id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<CategoryVO>(entity);
        }

        public async Task<IEnumerable<CategoryVO>> GetByUser(Guid userId)
        {
            // Usamos o método específico que criamos no CategoryRepository
            var list = await _repository.GetByUser(userId);
            return _mapper.Map<IEnumerable<CategoryVO>>(list);
        }

        public async Task Delete(Guid id)
        {
            var entity = await _repository.GetById(id);
            if (entity == null) throw new Exception("Categoria não encontrada.");

            // Soft Delete: Apenas marca como deletado no banco
            _repository.StatusDeleted(entity);
            await _repository.SaveChangesAsync();
        }
    }
}