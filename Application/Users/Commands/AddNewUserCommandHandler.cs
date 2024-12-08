using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Domain;
using MediatR;

namespace Application.Users.Commands
{
    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, OperationResult<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public AddNewUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<UserDto>> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
        {
            var newUserToRegisterInDB = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password,
            };

            var result = await _userRepository.RegisterUser(newUserToRegisterInDB);

            if (result.isSuccessfull)
            {
                var newUserDtoResponse = new UserDto
                {
                    UserName = newUserToRegisterInDB.UserName,
                    Password = newUserToRegisterInDB.Password,
                };

                return OperationResult<UserDto>.Successfull(newUserDtoResponse);
            }
            else
            {
                return OperationResult<UserDto>.Failure(result.ErrorMessage);
            }
        }
    }
}
