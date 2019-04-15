namespace MovieStore.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using MovieStore.Controllers;
    using MovieStore.Models;

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    [TestClass]
    public class MovieControllerTest
    {
        [TestMethod]
        public void Movie_Index_TestView()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_Create_TestView()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_Create_ModelStateIsValid()
        {
            // Arrange
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            Movie movie = new Movie { MovieId = 1, Title = "Inception", YearRelease = 2012 };

            // Act
            RedirectToRouteResult result = controller.Create(movie) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Movie_Create_ModelStateIsNotValid()
        {
            // Arrange
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);
            controller.ModelState.AddModelError("Movie", "Movie Title is required");

            Movie movie = new Movie { MovieId = -1, Title = "" };

            // Act
            ViewResult result = controller.Create(movie) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_ListOfMovies()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            List<Movie> result = controller.ListOfMovies();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: "Iron Man 1", actual: result[0].Title);
            Assert.AreEqual(expected: "Iron Man 2", actual: result[1].Title);
            Assert.AreEqual(expected: "Iron Man 3", actual: result[2].Title);
        }

        [TestMethod]
        public void Movie_IndexRedirect_Success()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            RedirectToRouteResult result = controller.IndexRedirect(1) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.RouteValues["action"]);
            Assert.AreEqual("HomeController", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Movie_IndexRedirect_BadRequest()
        {
            // Arrange
            MoviesController controller = new MoviesController();

            // Act
            HttpStatusCodeResult result = controller.IndexRedirect(0) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void Movie_ListFromDb()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            ViewResult result = controller.ListFromDb() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_Details_Success()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_Details_IdIsNull()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void Movie_Details_MovieIsNull()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            Movie movie = null;

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(movie);

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            HttpStatusCodeResult result = controller.Details(0) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void Movie_Edit_IdIsNull()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            Nullable<int> id = null;
            HttpStatusCodeResult result = controller.Edit(id) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void Movie_Edit_MovieIsNull()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            Movie movie = null;

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(movie);

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            HttpStatusCodeResult result = controller.Edit(0) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void Movie_Edit_Success()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            ViewResult result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_Edit_ModelStateIsValid()
        {
            // Arrange
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);
            mockContext.Setup(db => db.SetModified(It.IsAny<Movie>()));

            MoviesController controller = new MoviesController(mockContext.Object);

            Movie movie = new Movie { MovieId = 1, Title = "Inception", YearRelease = 2012 };

            // Act
            RedirectToRouteResult result = controller.Edit(movie) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Movie_Edit_ModelStateIsNotValid()
        {
            // Arrange
            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();
            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);
            controller.ModelState.AddModelError("Movie", "Movie Title is required");

            Movie movie = new Movie { MovieId = -1, Title = "" };

            // Act
            ViewResult result = controller.Edit(movie) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_Delete_IdIsNull()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            HttpStatusCodeResult result = controller.Delete(null) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void Movie_Delete_MovieIsNull()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);

            Movie movie = null;

            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(movie);

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            HttpStatusCodeResult result = controller.Delete(0) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }

        [TestMethod]
        public void Movie_Delete_Success()
        {
            // Goal: Query from our own list instead of the database.
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            // Arrange
            MoviesController controller = new MoviesController(mockContext.Object);

            // Act
            ViewResult result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Movie_DeleteConfirmed_IndexRedirect()
        {
            // Arrange
            var list = new List<Movie>
            {
                new Movie() {MovieId = 1, Title = "Superman 1"},
                new Movie() {MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());
            mockSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(list.Provider);
            mockSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(list.ElementType);
            mockSet.Setup(m => m.Find(It.IsAny<Object>())).Returns(list.First());

            mockContext.Setup(db => db.Movies).Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);
            // Act
            RedirectToRouteResult result = controller.DeleteConfirmed(1) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
