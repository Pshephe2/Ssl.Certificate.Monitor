using Microsoft.EntityFrameworkCore;
using Moq;
using Ssl.Certificate.Data;
using Ssl.Certificate.Data.Models;
using Ssl.Certificate.Monitor.Repository;

namespace Ssl.Certificate.Tests
{
    public class ControlRepositoryTests
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
                url = "foo.bar.com"
            });
            db.SaveChanges();

            var repository = new ControlRepository(db);

            var items = repository.GetAll();

            Assert.True(items.Any());
        }

        [Fact]
        public void Test_Insert()
        {
            var db = SetUp();

            var repository = new ControlRepository(db);

            repository.Insert(new SslControlTable()
            {
                id = 1,
                url = "foo.bar.com"
            });
            repository.Save();
            var item = db.Find<SslControlTable>(1);
            Assert.NotNull(item);
        }

        [Fact]
        public void Test_Update()
        {
            //Arrange
            var db = SetUp();

            db.Add(new SslControlTable()
            {
                id = 2,
                url = "foo.bar.com"
            });
            db.SaveChanges();
            db.ChangeTracker.Clear();
            //Act
            var repository = new ControlRepository(db);

            repository.Update(new SslControlTable() 
            { 
                id = 2, 
                url = "bar.foo.net" 
            });
            repository.Save();
            //Assert
            var item = db.Find< SslControlTable>(2);
            Assert.NotNull(item);
            Assert.True(item.url.Equals("bar.foo.net"));

        }

        [Fact]
        public void Test_Delete ()
        {
            //Arrange
            var db = SetUp();

            db.Add(new SslControlTable()
            {
                id = 2,
                url = "foo.bar.com"
            });
            db.SaveChanges();
            db.ChangeTracker.Clear();
            //Act
            var repository = new ControlRepository(db);

            repository.Delete(2);
            repository.Save();
            //Assert
            var item = db.Find<SslControlTable>(2);
            Assert.Null(item);
        }
    }
}