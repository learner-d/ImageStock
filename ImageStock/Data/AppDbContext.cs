using ImageStock.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace ImageStock.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<PostInfo> Posts { get; set; }
        public DbSet<CommentInfo> CommentInfo { get; set; }

        public UserProfile GetUserProfile(ClaimsPrincipal user)
        {
            if (user != null)
                return Users.FirstOrDefault(u => u.Username == user.Identity.Name); 
            return null;
        }
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {
        }
        private static bool _inited = false;
        public static void SetupDefaults(AppDbContext appDbContext)
        {
            if (_inited) return;

            //appDbContext.Database.EnsureCreated();
            if (appDbContext.DbTableExists("Users") && appDbContext.Users.FirstOrDefault() == null)
            {
                appDbContext.Users.Add(new UserProfile
                {
                    Username = "Admin",
                    PwdHash = "qwerty"
                });
            }

            if (appDbContext.DbTableExists("Posts") && appDbContext.Posts.FirstOrDefault() == null)
            {
                List<CommentInfo> comments = new List<CommentInfo>()
                {
                    new CommentInfo() {/*Username = "real_doer", */Text="Мені подобається"},
                    new CommentInfo() {/*Username = "d_gergel", */Text="Фантастичний вигляд"},
                    new CommentInfo() {/*Username = "roboboy", */Text="Я робот"},
                    new CommentInfo() {/*Username = "dreamer", */Text="Відчув себе, неначе уві сні"},
                    new CommentInfo() {/*Username = "alejandro", */Text="Вітання всім, шановні люди"},
                    new CommentInfo() {/*Username = "entity", */Text="Сенс є, немає сенсу"},
                };

                PostInfo[] posts = new PostInfo[] {
                    new PostInfo{ Title = "Краєвид колообігу",
                        Description = "Гарно з колеса огляду, Аж перехоплює дух. " +
                                        "Романтика,з високого погляду, Неначе пікантний вибух. " +
                                        "Очі,від захвату світяться. Зверху така благодать!.. " +
                                        "Кожному в щастя віриться. Ось воно! Як упіймать? Лоскотно,боязно,весело. " +
                                        "Ейфорія якась нахлинула. Солодко, й неймовірно піднесено, " +
                                        "А ти злякавшись, прилинула. Образить тебе не хочеться. " +
                                        "Годі боятись,розкріпостись. Люба,чого ти горнешся? " +
                                        "Ясно,ти вниз не дивись. Додому скоро повернемося. " +
                                        "Упевнений,все скінчилося.",
                        ImgUrl = "~/img/aestetic_1.jpg" },
                    new PostInfo{ Title = "Сонячна краса", Description = "", ImgUrl = "~/img/aestetic_2.jpg", Comments = comments },
                    new PostInfo{ Title = "Квітуча врода", Description = "", ImgUrl = "~/img/aestetic_3.jpg" },
                    new PostInfo{ Title = "Стиль життя", Description = "", ImgUrl = "~/img/aestetic_4.jpg", Comments = comments },
                    new PostInfo{ Title = "Промінь надії", Description = "", ImgUrl = "~/img/aestetic_5.jpg"},
                    new PostInfo{ Title = "Метушний момент", Description = "", ImgUrl = "~/img/aestetic_6.jpg"},
                    new PostInfo{ Title = "Вірна мить", Description = "", ImgUrl = "~/img/aestetic_7.jpg"}
                };

                appDbContext.Posts.AddRange(posts);
            }

            appDbContext.SaveChanges();
            _inited = true;
        }

        public bool DbTableExists(string tableName, string schema = "dbo")
        {
            var connection = Database.GetDbConnection();

            if (connection.State.Equals(ConnectionState.Closed))
                connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = 
                  @"SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_SCHEMA = @Schema
                    AND TABLE_NAME = @TableName";

                var schemaParam = command.CreateParameter();
                schemaParam.ParameterName = "@Schema";
                schemaParam.Value = schema;
                command.Parameters.Add(schemaParam);

                var tableNameParam = command.CreateParameter();
                tableNameParam.ParameterName = "@TableName";
                tableNameParam.Value = tableName;
                command.Parameters.Add(tableNameParam);

                return command.ExecuteScalar() != null;
            }
        }
    }
}
