using AutoMapper;
using Finance.Domain.Models;
using Finance.Service.EntitiesVO;

namespace Financas.Service.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {

            // SENTIDO: VO -> Entity (Entrada para o Banco/Register)
            // Aqui a senha DEVE ser mapeada para que o banco consiga salvá-la
            CreateMap<UserVO, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUser)); // mapeando o campo 'id' para 'IdDespesa'             


            // SENTIDO: Entity -> VO (Saída para a API/Swagger)
            // Aqui dizemos: "Quando transformar o User do banco em UserVO, IGNORE a senha"
            CreateMap<User, UserVO>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<CategoryVO, Category>().ReverseMap();

            CreateMap<TransactionVO, Transaction>().ReverseMap();




        }
    }
}