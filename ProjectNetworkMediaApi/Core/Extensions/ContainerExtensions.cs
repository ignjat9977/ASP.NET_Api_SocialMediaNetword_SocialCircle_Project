using Application.Commands;
using Application.Queries;
using Implementation.Commands;
using Implementation.Queries;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectNetworkMediaApi.Core.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProjectNetworkMediaApi.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<CreatePostValidator>();
            services.AddTransient<UpdatePostValidator>();
            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<CreateGroupValidator>();
            services.AddTransient<CreatePostOrVideoInGroupValidator>();
            services.AddTransient<CreateFriendValidator>();
        }
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddTransient<ICreatePostCommand, EFCreatePostCommand>();
            services.AddTransient<ICreateUserCommand, EFCreateUserCommand>();
            services.AddTransient<IDeletePostCommand, EFDeletePostCommand>();
            services.AddTransient<IUpdatePostCommand, EFUpdatePostCommand>();
            services.AddTransient<ICreateCommentCommand, EFCreateCommentCommand>();
            services.AddTransient<ICreateFriendCommand, EFCreateFriendCommand>();
            services.AddTransient<IDeleteFriendCommand, EFDeleteFriendCommand>();
            services.AddTransient<ICreateLikePostCommand, EFCreateLikePostCommand>();
            services.AddTransient<ICreateLikeCommentCommand, EFCreateLikeCommentCommand>();
            services.AddTransient<ICreatePhotoOrVideoCommand, EFCreatePhotoOrVideoCommand>();
            services.AddTransient<ICreateGroupCommand, EFCreateGroupCommand>();
            services.AddTransient<IUpdateGroupCommand, EFUpdateGroupCommand>();
            services.AddTransient<IDeleteGroupCommand, EFDeleteGroupCommand>();
            services.AddTransient<ICreatePostGroupPostCommand, EFCreatePostGroupPostCommand>();
            services.AddTransient<ICreateProfileImageCommand, EFCreateProfileImageCommand>();
        }
        public static void AddQueries(this IServiceCollection services)
        {
            services.AddTransient<ISearchPostUserWallQuery, EFSearchPostUserWallQuery>();
            services.AddTransient<ISearchPostInGroupQuery, EFSearchPostInGroupQuery>();
            services.AddTransient<IGetFriendsAndFriendsOfFriendsQuery, EFGetFriendsAndFriendsOfFriendsQuery>();
            services.AddTransient<IGetUserInfoQuery, EFGetUserInfoQuery>();
            services.AddTransient<IGetGroupInfoQuery, EFGetGroupInfoQuery>();
            services.AddTransient<ISearchGroupsQuery, EFSearchGroupsQuery>();
            services.AddTransient<ISearchUsersQuery, EFSearchUsersQuery>();
            services.AddTransient<ISearchInboxQuery, EFSearchInboxQuery>();
            services.AddTransient<ISearchInboxPartsQuery, EFSearchInboxPartsQuery>();
            services.AddTransient<IGetSpecificInboxPartQuery, EFGetSpecificInboxPartQuery>();
            services.AddTransient<ISearchAuditLogQuery, EFSearchAuditLogQuery>();
            services.AddTransient<IGetAllPostsOfUserFriendsAndGroupsQuery, EFGetAllPostsOfUserFriendsAndGroupsQuery>();
        }
        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                cfg.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        //Token dohvatamo iz Authorization header-a

                        var header = context.Request.Headers["Authorization"];

                        var token = header.ToString().Split("Bearer ")[1];

                        var handler = new JwtSecurityTokenHandler();

                        var tokenObj = handler.ReadJwtToken(token);

                        string jti = tokenObj.Claims.FirstOrDefault(x => x.Type == "jti").Value;


                        //ITokenStorage

                        ITokenStorage storage = context.HttpContext.RequestServices.GetService<ITokenStorage>();

                        bool isValid = storage.TokenExists(jti);

                        if (!isValid)
                        {
                            context.Fail("Token is not valid.");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
