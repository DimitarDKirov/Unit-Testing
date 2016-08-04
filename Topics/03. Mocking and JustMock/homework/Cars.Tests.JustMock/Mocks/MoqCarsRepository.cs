namespace Cars.Tests.JustMock.Mocks
{
    using Cars.Contracts;
    using Cars.Models;
    using Moq;
    using System.Linq;

    public class MoqCarsRepository : CarRepositoryMock, ICarsRepositoryMock
    {
        protected override void ArrangeCarsRepositoryMock()
        {
            //existing mocks
            var mockedCarsRepository = new Mock<ICarsRepository>();
            mockedCarsRepository.Setup(r => r.Add(It.IsAny<Car>())).Verifiable();
            mockedCarsRepository.Setup(r => r.All()).Returns(this.FakeCarCollection);
            mockedCarsRepository.Setup(r => r.Search(It.IsAny<string>())).Returns(this.FakeCarCollection.Where(c => c.Make == "BMW").ToList());
            mockedCarsRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(this.FakeCarCollection.First());

            //new mocks
            mockedCarsRepository.Setup(r => r.Remove(It.IsAny<Car>())).Verifiable();
            mockedCarsRepository.Setup(r => r.SortedByMake()).Returns(this.FakeCarCollection.OrderBy(c => c.Make).ToList());
            mockedCarsRepository.Setup(r => r.SortedByYear()).Returns(this.FakeCarCollection.OrderBy(c => c.Year).ToList());
            mockedCarsRepository.Setup(r => r.GetById(int.MaxValue)).Returns(() => null);
            this.CarsData = mockedCarsRepository.Object;
        }
    }
}
