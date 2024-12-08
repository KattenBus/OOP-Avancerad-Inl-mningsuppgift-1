using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Queries.Login.Helpers;
using Domain;
using MediatR;

namespace Application.Users.Queries.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LoginUserQueryHandler(IUserRepository userRepository, TokenHelper tokenhelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenhelper;
        }

        public async Task<OperationResult<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.LogInUser(request.LoginUser.UserName, request.LoginUser.Password);

            if (user.isSuccessfull)
            {
                string token = _tokenHelper.GenerateJwtToken(user.Data);

                return OperationResult<string>.Successfull(token);
            }
            else
            {
                return OperationResult<string>.Failure(user.ErrorMessage);
            }

        }
    }
}
