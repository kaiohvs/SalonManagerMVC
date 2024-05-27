using AutoMapper;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces;

namespace SalonManager.Services.Services.Users
{
    public interface IUserServices
    {
        Task<UserResponse> CreateUser(UserRequest request);
        Task<IEnumerable<UserResponse>> GetAll();
        Task<UserResponse> GetById(int? id);
        Task<UserResponse> UpdateUser(int id, UserRequest request);
        Task<bool> Delete(int id);
    }

    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserServices(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserResponse> CreateUser(UserRequest request)
        {
            try
            {
                var createUser = _mapper.Map<User>(request);

                if (createUser == null)
                    return null;

                await _repository.Save(createUser);
                return _mapper.Map<UserResponse>(createUser);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            try
            {
                var allUser = await _repository.FindAll();

                if (allUser != null)
                {
                    return _mapper.Map<IEnumerable<UserResponse>>(allUser);
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> GetById(int? id)
        {
            try
            {
                var byId = await _repository.FindById(id);
                if (byId != null)
                    return _mapper.Map<UserResponse>(byId);

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UserResponse> UpdateUser(int id, UserRequest request)
        {
            try
            {
                var byId = await _repository.FindById(id);

                if (byId != null)
                {
                    // Mapeia as novas propriedades para a entidade existente
                    _mapper.Map(request, byId);

                    await _repository.Save(byId);

                    return _mapper.Map<UserResponse>(byId);

                }
                else
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _repository.RemoverCustomer(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
