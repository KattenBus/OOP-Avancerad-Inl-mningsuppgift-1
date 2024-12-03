//using Application.Users.Queries.Login.Helpers;
//using Infrastructure.Database;
//using MediatR;

//namespace Application.Users.Queries.Login
//{
//    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, string>
//    {
//        private readonly FakeDatabase _database;
//        private readonly TokenHelper _tokenHelper;

//        public LoginUserQueryHandler(FakeDatabase database, TokenHelper tokenhelper)
//        {
//            _database = database;
//            _tokenHelper = tokenhelper;
//        }

//        public Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
//        {
//            var user = _database.Users.FirstOrDefault(user => user.UserName == request.LoginUser.UserName && user.Password == request.LoginUser.Password);

//            if (user == null)
//            {
//                throw new UnauthorizedAccessException("Invalid Username or password");
//            }

//            string token = _tokenHelper.GenerateJwtToken(user);

//            return Task.FromResult(token);
//        }
//    }
//}
