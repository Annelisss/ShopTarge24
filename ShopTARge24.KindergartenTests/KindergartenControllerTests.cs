using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopTARge24.Controllers;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto.KindergartenDto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShopTARge24.KindergartenTests
{
    public class KindergartenControllerTests
    {
        // Creates controller with mocked services + real in-memory DbContext
        private KindergartenController GetController(
            Mock<IKindergartenService> kindergartenServiceMock,
            Mock<IFileServices> fileServiceMock)
        {
            // in-memory database for tests
            var options = new DbContextOptionsBuilder<ShopTARge24Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ShopTARge24Context(options);

            return new KindergartenController(
                kindergartenServiceMock.Object,
                fileServiceMock.Object,
                dbContext);
        }


        // 1) GET Create
        [Fact]
        public void Create_Get_ReturnsViewWithModel()
        {
            var serviceMock = new Mock<IKindergartenService>();
            var fileMock = new Mock<IFileServices>();
            var controller = GetController(serviceMock, fileMock);

            var result = controller.Create() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<KindergartenDto>(result!.Model);
        }

        // 2) POST Create – invalid model
        [Fact]
        public async Task Create_Post_Invalid_ReturnsSameView()
        {
            var serviceMock = new Mock<IKindergartenService>();
            var fileMock = new Mock<IFileServices>();
            var controller = GetController(serviceMock, fileMock);

            controller.ModelState.AddModelError("GroupName", "Required");
            var dto = new KindergartenDto();

            var result = await controller.Create(dto, null) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(dto, result!.Model);
            serviceMock.Verify(s => s.Create(It.IsAny<KindergartenDto>()), Times.Never);
        }

        // 3) POST Create – valid model
        [Fact]
        public async Task Create_Post_Valid_RedirectsToIndex()
        {
            var serviceMock = new Mock<IKindergartenService>();
            var fileMock = new Mock<IFileServices>();

            serviceMock
                .Setup(s => s.Create(It.IsAny<KindergartenDto>()))
                .ReturnsAsync(new Kindergartens { Id = Guid.NewGuid() });

            var controller = GetController(serviceMock, fileMock);

            var dto = new KindergartenDto
            {
                GroupName = "Mesimummid",
                ChildrenCount = 10,
                KindergartenName = "TTHK Lasteaed",
                TeacherName = "Anneli"
            };

            var result = await controller.Create(dto, null) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result!.ActionName);
            serviceMock.Verify(s => s.Create(It.IsAny<KindergartenDto>()), Times.Once);
        }

        // 4) Details – item not found
        [Fact]
        public async Task Details_NotFound_ReturnsNotFoundResult()
        {
            var serviceMock = new Mock<IKindergartenService>();
            var fileMock = new Mock<IFileServices>();

            serviceMock
                .Setup(s => s.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Kindergartens?)null);

            var controller = GetController(serviceMock, fileMock);

            var result = await controller.Details(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        // 5) Details – item exists
        [Fact]
        public async Task Details_Valid_ReturnsViewWithModel()
        {
            var serviceMock = new Mock<IKindergartenService>();
            var fileMock = new Mock<IFileServices>();
            var id = Guid.NewGuid();

            serviceMock
                .Setup(s => s.GetAsync(id))
                .ReturnsAsync(new Kindergartens { Id = id });

            var controller = GetController(serviceMock, fileMock);

            var result = await controller.Details(id) as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<Kindergartens>(result!.Model);
        }

        // 6) POST DeleteConfirmed
        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            var serviceMock = new Mock<IKindergartenService>();
            var fileMock = new Mock<IFileServices>();

            serviceMock
                .Setup(s => s.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(true);

            var controller = GetController(serviceMock, fileMock);
            var id = Guid.NewGuid();

            var result = await controller.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result!.ActionName);
            serviceMock.Verify(s => s.Delete(id), Times.Once);
        }
    }
}
