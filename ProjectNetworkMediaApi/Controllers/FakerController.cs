using Azure.Core;
using Bogus;
using DataAcess;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;

namespace ProjectNetworkMediaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakerController : ControllerBase
    {
        // GET: api/<TestController>
        
        public MyDbContext _context;

        public FakerController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {

            //var faker = new Faker<User>();

            //var privacies = new List<Privacy>
            //{
            //    new Privacy
            //    {
            //        Name = "public"
            //    },
            //    new Privacy
            //    {
            //        Name = "private"
            //    }
            //};
            //_context.Privacy.AddRange(privacies);

            //var post1 = new Post
            //{
            //    Title = "Post br 1",
            //    Content = "Opis posta 1",
            //    Privacy = privacies.ElementAt(0)
            //};
            //var postVideo = new Post
            //{
            //    Title = "Post Video",
            //    Content = "Opis posta video",
            //    Privacy = privacies.ElementAt(0)
            //};
            //var postSlika = new Post
            //{
            //    Title = "Post SLika",
            //    Content = "Opis posta slike",
            //    Privacy = privacies.ElementAt(0)
            //};

            //var video = new PhotoVideo
            //{
            //    Path = "neka putanja 1",
            //    Post = postVideo,
            //};
            //var slika = new PhotoVideo
            //{
            //    Path = "neka putanja slike",
            //    Post = postSlika
            //};
            //var userWall1 = new UserWall
            //{
            //    UserId = 1,
            //    Post = post1,
            //    Created = DateTime.Now,
            //};
            //var userWall2 = new UserWall
            //{
            //    UserId = 1,
            //    Post = postVideo,
            //    Created = DateTime.Now,
            //};
            //var userWall3 = new UserWall
            //{
            //    UserId = 1,
            //    Post = postSlika,
            //    Created = DateTime.Now,
            //};
            //_context.Posts.Add(post1);
            //_context.Posts.Add(postSlika);
            //_context.Posts.Add(postVideo);
            //_context.PhotosVideos.Add(video);
            //_context.PhotosVideos.Add(slika);
            //_context.UserWalls.Add(userWall1);
            //_context.UserWalls.Add(userWall2);
            //_context.UserWalls.Add(userWall3);


            
            //var roleIds = _context.Role.Select(x => x.Id).ToList();
            //faker.RuleFor(x => x.FirstName, f => f.Name.FirstName());
            //faker.RuleFor(x => x.LastName, f => f.Name.LastName());
            //faker.RuleFor(x => x.Email, f => f.Internet.Email());
            //faker.RuleFor(x => x.Password, f => BCrypt.Net.BCrypt.HashPassword("sifra1!A@"));
            //faker.RuleFor(x => x.DateOfBirth, f => f.Date.Past());
            //faker.RuleFor(x => x.RoleId, f => f.PickRandom(roleIds));
            //var users = faker.Generate(100);




            //var fakerUseCases = new Faker<UserUseCase>()
            //    .RuleFor(x => x.User, f => f.PickRandom(users))
            //    .RuleFor(x => x.UseCaseId, f => f.Random.Number(1, 30));
            //var useCases = fakerUseCases.Generate(3000);
            

            //var firstUsers = users.Take(50).ToList();
            //var secondUsers = users.Skip(50).Take(50).ToList();
            
            //var fakerFriends = new Faker<Friend>()
            //    .RuleFor(x => x.User, f => f.PickRandom(firstUsers))
            //    .RuleFor(x => x.OneFriend, f => f.PickRandom(secondUsers));
            //var friends = fakerFriends.Generate(100);
            

            //var fakerMessages = new Faker<Message>()
            //    .RuleFor(x => x.Sender, f => f.PickRandom(firstUsers))
            //    .RuleFor(x => x.Reciver, f => f.PickRandom(secondUsers))
            //    .RuleFor(x => x.Content, f => f.Lorem.Text());
            //var messages = fakerMessages.Generate(300);
            


            var usersIds = _context.Users.Select(x => x.Id).ToList();
            var fakerPhoto = new Faker<PhotoVideo>()
                .RuleFor(x => x.Path, f => f.Image.PicsumUrl());
            var photosProfile = fakerPhoto.Generate(300);
            var profilePictures = new Faker<UserProfilePhotos>()
                .RuleFor(x => x.Photo, f => f.PickRandom(photosProfile))
                .RuleFor(x => x.UserId, f => f.PickRandom(usersIds))
                .RuleFor(x => x.Created, f=> DateTime.Now);
            var photoProfileJoinTable = profilePictures.Generate(300);


            //var fakerPosts = new Faker<Post>()
            //    .RuleFor(x => x.Title, f => f.Lorem.Word())
            //    .RuleFor(x => x.Content, f => f.Lorem.Text())
            //    .RuleFor(x => x.Privacy, f => f.PickRandom(privacies));

            //var postsNoPhoto = fakerPosts.Generate(3000);

            //var photoPosts = fakerPosts.Generate(1000);

            //var fakerPhotos = new Faker<PhotoVideo>()
            //    .RuleFor(x => x.Path, f => f.Image.PicsumUrl())
            //    .RuleFor(x => x.Post, f => f.PickRandom(photoPosts));

            //var photos = fakerPhotos.Generate(1000);

            //var fakerUserWallNoPhoto = new Faker<UserWall>()
            //    .RuleFor(x => x.User, f => f.PickRandom(users))
            //    .RuleFor(x => x.Post, f => f.PickRandom(postsNoPhoto))
            //    .RuleFor(x => x.Created, f => DateTime.Now);
            //var userWallNoPhoto = fakerUserWallNoPhoto.Generate(3000);


            //var fakerUserWallPhoto = new Faker<UserWall>()
            //    .RuleFor(x => x.User, f => f.PickRandom(users))
            //    .RuleFor(x => x.Post, f => f.PickRandom(photoPosts))
            //    .RuleFor(x=> x.Created, f=> DateTime.Now);

            //var userWallPhoto = fakerUserWallPhoto.Generate(1000);







            //var fakerComment = new Faker<Comment>()
            //    .RuleFor(x => x.Content, f => f.Lorem.Paragraph())
            //    .RuleFor(x => x.UserId, f => f.PickRandom(usersIds))
            //    .RuleFor(x => x.Post, f => f.PickRandom(postsNoPhoto));

            //var comments = fakerComment.Generate(3000);



            //var likesPost = new Faker<Like>()
            //    .RuleFor(x => x.UserId, f => f.PickRandom(usersIds))
            //    .RuleFor(x => x.Post, f => f.PickRandom(postsNoPhoto));

            //var likesP = likesPost.Generate(30000);

            //var likesPostPhoto = new Faker<Like>()
            //   .RuleFor(x => x.UserId, f => f.PickRandom(usersIds))
            //   .RuleFor(x => x.Post, f => f.PickRandom(photoPosts));

            //var likesPhoto = likesPostPhoto.Generate(10000);

            //var likesComments = new Faker<Like>()
            //    .RuleFor(x => x.UserId, f => f.PickRandom(usersIds))
            //    .RuleFor(x => x.Comment, f => f.PickRandom(comments));

            //var finalLikes = likesComments.Generate(30000);

          



            //var groupsFaker = new Faker<Groupp>()
            //    .RuleFor(x => x.Name, f => f.Commerce.Categories(1).First())
            //    .RuleFor(x => x.Description, f => f.Lorem.Text())
            //    .RuleFor(x => x.Privacy, f => f.PickRandom(privacies));

            //var groups = groupsFaker.Generate(300);
            //var groupsDistinct = groups.Select(x => x.Name).Distinct();

            //var groupsFinal = groupsDistinct.Select(x => groups.First(c => c.Name == x));

            


            //var groupMembers = new Faker<GroupMember>()
            //    .RuleFor(x => x.Group, f => f.PickRandom(groupsFinal))
            //    .RuleFor(x => x.RoleId, f => f.PickRandom(roleIds))
            //    .RuleFor(x => x.User, f => f.PickRandom(users));
            //var members = groupMembers.Generate(1000);
           

            //var fakerPostsG = new Faker<Post>()
            //    .RuleFor(x => x.Title, f => f.Lorem.Word())
            //    .RuleFor(x => x.Content, f => f.Lorem.Text())
            //    .RuleFor(x => x.Privacy, f => f.PickRandom(privacies));

            //var postsG = fakerPostsG.Generate(2000);

          

            //var groupsPost = new Faker<GroupPost>()
            //    .RuleFor(x => x.Post, f => f.PickRandom(postsG))
            //    .RuleFor(x => x.Group, f => f.PickRandom(groupsFinal))
            //    .RuleFor(x => x.Created, f => DateTime.Now)
            //    .RuleFor(x => x.User, f=> f.PickRandom(users));

            //var groupsPostsFinal = groupsPost.Generate(300);



            //_context.Users.AddRange(users);
            //_context.UserUseCase.AddRange(useCases);
            //_context.Friends.AddRange(friends);
            //_context.Messages.AddRange(messages);
            _context.PhotosVideos.AddRange(photosProfile);
            _context.UserProfilePhotos.AddRange(photoProfileJoinTable);
            //_context.Posts.AddRange(postsNoPhoto);
            //_context.Posts.AddRange(photoPosts);
            //_context.PhotosVideos.AddRange(photos);
            //_context.UserWalls.AddRange(userWallNoPhoto);
            //_context.UserWalls.AddRange(userWallPhoto);
            //_context.Comments.AddRange(comments);
            //_context.Likes.AddRange(likesP);
            //_context.Likes.AddRange(likesPhoto);
            //_context.Likes.AddRange(finalLikes);
            //_context.Groups.AddRange(groupsFinal);
            //_context.GroupMembers.AddRange(members);
            //_context.Posts.AddRange(postsG);
            //_context.GroupPost.AddRange(groupsPostsFinal);
            _context.SaveChanges();


            return NoContent();
        }
    }
}
