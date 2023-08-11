using Microsoft.EntityFrameworkCore;
using Moq;
using Ssl.Certificate.Data;
using Ssl.Certificate.Data.Models;
using Ssl.Certificate.Monitor.Repository;

namespace Ssl.Certificate.Tests
{
    public class ActivityRepositoryTests
    {
        private MonitorDbContext SetUp()
        {
            var inMemoryOption = new DbContextOptionsBuilder<MonitorDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging();
            return new MonitorDbContext(inMemoryOption.Options);
        }


        [Fact]
        public void Test_Get_All()
        {
            var db = SetUp();
            db.Add(new SslControlTable()
            {
                id = 1,
                url = "foo.bar.com",
                SslActivityLog = new List<SslActivityLog>()
                {
                    new SslActivityLog {
                        id = 1,
                        control_id = 1,
                        run_time = DateTime.Now,
                        created_on = DateTime.Now,
                        expires_on = DateTime.Now
                    }
                }
            });
            db.SaveChanges();

            var repository = new SslActivityLogRepository(db);

            var items = repository.GetAll();

            Assert.True(items.Any());
        }

        [Fact]
        public void Test_Insert()
        {
            var db = SetUp();
            db.Add(new SslControlTable()
            {
                id = 1,
                url = "foo.bar.com",
                SslActivityLog = new List<SslActivityLog>()
                {
                    new SslActivityLog {
                        id = 1,
                        control_id = 1,
                        run_time = DateTime.Now,
                        created_on = DateTime.Now,
                        expires_on = DateTime.Now
                    }
                }
            });
            db.SaveChanges();

            var repository = new SslActivityLogRepository(db);

            repository.Insert(new SslActivityLog()
            {
                id = 2,
                control_id = 1,
                run_time = DateTime.Now,
                created_on = DateTime.Now,
                expires_on = DateTime.Now
            });
            repository.Save();
            var item = db.Find<SslActivityLog>(1);
            Assert.NotNull(item);
        }

        [Fact]
        public void Test_Update()
        {
            //Arrange
            var created_on = DateTime.Now;
            var db = SetUp();
            db.Add(new SslControlTable()
            {
                id = 1,
                url = "foo.bar.com",
                SslActivityLog = new List<SslActivityLog>()
                {
                    new SslActivityLog {
                        id = 1,
                        control_id = 1,
                        run_time = DateTime.Now,
                        created_on = created_on,
                        expires_on = DateTime.Now
                    }
                }
            });
            db.SaveChanges();
            db.ChangeTracker.Clear();
            //Act
            var repository = new SslActivityLogRepository(db);

            repository.Update(new SslActivityLog()
            {
                id = 1,
                control_id = 1,
                run_time = DateTime.Now,
                created_on = created_on.AddDays(1),
                expires_on = DateTime.Now
            });
            repository.Save();
            //Assert
            var item = db.Find<SslActivityLog>(1);
            Assert.NotNull(item);
            Assert.True(item.created_on.Equals(created_on.AddDays(1)));

        }

        [Fact]
        public void Test_Delete()
        {
            //Arrange
            var db = SetUp();

            db.Add(new SslControlTable()
            {
                id = 1,
                url = "foo.bar.com",
                SslActivityLog = new List<SslActivityLog>()
                {
                    new SslActivityLog {
                        id = 1,
                        control_id = 1,
                        run_time = DateTime.Now,
                        created_on = DateTime.Now,
                        expires_on = DateTime.Now
                    }
                }
            });
            db.SaveChanges();
            db.ChangeTracker.Clear();
            //Act
            var repository = new SslActivityLogRepository(db);

            repository.Delete(1);
            repository.Save();
            //Assert
            var item = db.Find<SslActivityLog>(2);
            Assert.Null(item);
        }
    }
}