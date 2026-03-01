using AutoMapper;
using BCrypt.Net;
using Financas.Service.Profiles;
using Finance.Domain.Interfaces;
using Finance.Domain.Models;
using Finance.Service.EntitiesVO;
using Finance.Service.Interfaces;

namespace Finance.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        protected readonly IMapper mapper;
         
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<UserVO> Create(UserVO userVO)
        {
            var userEntity = mapper.Map<User>(userVO);

            // 1. Verificar se o e-mail já existe
            var existingUser = await _repository.GetByEmail(userEntity.Email);
            if (existingUser != null)
                throw new Exception("Este e-mail já está cadastrado.");

            // 2. Criptografar a senha (Hash)
            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password);

            // 3. Salvar no banco
            _repository.Add(userEntity);
            await _repository.SaveChangesAsync();

            return mapper.Map<UserVO>(userEntity);
        }

        public async Task<UserVO> Authenticate(string email, string password)
        {
            var user = await _repository.GetByEmail(email);

            // Verifica se o usuário existe e se a senha "bate" com o Hash
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            else
            {
                var userVO = mapper.Map<UserVO>(user);
                return userVO;
            }
        }

        public async Task<UserVO> GetById(Guid id)
        {

            var userEntity = await _repository.GetById(id);
            var userVO = mapper.Map<UserVO>(userEntity);

            return userVO;
        }

        public async Task Update(UserVO userVO)
        {
            // 1. Busca o cara que já está no banco (Tracked)
            var userExistente = await _repository.GetById(userVO.IdUser);
            if (userExistente == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            // 2. O AutoMapper "copia" os dados do VO para a entidade que o EF já está cuidando
            mapper.Map(userVO, userExistente);

            // 3. Agora o Update e o Save funcionam sem conflito de memória
            _repository.Update(userExistente);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserVO>> GetAll()
        {
            var user = await _repository.GetAll();
            var usersVO = mapper.Map<IEnumerable<UserVO>>(user);
            return usersVO;
        }

        public async Task Deleted(Guid Id)
        {
            // 1. Busca o usuário para ter certeza que ele existe
            var user = await _repository.GetById(Id);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            else
            {
                // 2. Apenas delega a função para quem sabe deletar
              _repository.StatusDeleted(user);

                // 3. Salva a alteração (MUITO IMPORTANTE!)
              await  _repository.SaveChangesAsync();
            }

        }
    }

}