using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, OperationResult<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task <OperationResult<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            var allUsersFromDB = await _userRepository.GetAllUsersList();

            if (allUsersFromDB.isSuccessfull)
            {
                var allUsersResponse = new List<UserDto>();

                foreach (var user in allUsersFromDB.Data)
                {
                    var userDto = new UserDto
                    {
                        UserName = user.UserName,
                        Password = user.Password
                    };
                    allUsersResponse.Add(userDto);
                }
                return OperationResult<List<UserDto>>.Successfull(allUsersResponse);
            }
            else
            {
                return OperationResult<List<UserDto>>.Failure(allUsersFromDB.ErrorMessage);
            }

        }
    }
}
