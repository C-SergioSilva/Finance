using AutoMapper;
using Finance.Domain.Models;
using Finance.Service.EntitiesVO;

namespace Financas.Service.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {

            // Mapeando de DespesaVO (Value Object) para Despesa (Entidade)
            CreateMap<UserVO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUser)); // mapeando o campo 'id' para 'IdDespesa'

            // Mapeamento reverso (se necessário)
            CreateMap<User, UserVO>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom( src => src.Id));
            
            CreateMap<CategoryVO, Category>().ReverseMap();

            CreateMap<TransactionVO, Transaction>().ReverseMap();




        }
    }
}