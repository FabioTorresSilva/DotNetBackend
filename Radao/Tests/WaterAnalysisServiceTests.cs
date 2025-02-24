/*using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Radao.Models;
using Radao.Services;
using Radao.Data;
using Radao.Enums;
using Radao.Dtos;
using Radao.Exceptions;
using System.Data.Entity;

namespace Radao.Tests
{
    public class WaterAnalysisServiceTests
    {
        private List<Fountain> _fountains;
        private List<WaterAnalysis> _waterAnalyses;
        private Mock<RadaoContext> _mockContext;
        private WaterAnalysisService _service;

        // This constructor acts like the @BeforeEach in JUnit.
        public WaterAnalysisServiceTests()
        {
            // Arrange: Create sample fountain entities (with full data)
            var fountain1 = new Fountain(1, "Fountain A", SusceptibilityIndex.Low, null, true, -43.0, 22.0);
            var fountain2 = new Fountain(2, "Fountain B", SusceptibilityIndex.Moderate, null, true, -44.0, 23.0);
            _fountains = new List<Fountain> { fountain1, fountain2 };

            // Arrange: Create sample water analysis records for the fountains.
            // For example: fountain1 gets two tests, fountain2 gets one test.
            _waterAnalyses = new List<WaterAnalysis>
            {
                new WaterAnalysis { Id = 1, FountainId = 1, RadonConcentration = 50, Fountain = fountain1 },
                new WaterAnalysis { Id = 2, FountainId = 1, RadonConcentration = 75, Fountain = fountain1 },
                new WaterAnalysis { Id = 3, FountainId = 2, RadonConcentration = 200, Fountain = fountain2 }
            };

            // Create mocked DbSet objects (using a helper method, see below)
            var waterAnalysisDbSetMock = DbSetMocking.CreateMockDbSet(_waterAnalyses);
            var fountainDbSetMock = DbSetMocking.CreateMockDbSet(_fountains);

            // Arrange: Setup a fake RadaoContext to return the mock DbSets.
            _mockContext = new Mock<RadaoContext>();
            _mockContext.Setup(c => c.WaterAnalysis).Returns(waterAnalysisDbSetMock.Object);
            _mockContext.Setup(c => c.Fountains).Returns(fountainDbSetMock.Object);

            // Instantiate the service with the mocked context.
            _service = new WaterAnalysisService(_mockContext.Object);
        }

        [Fact]
        public async Task Test_GetFavoriteFountainsAnalysis_ReturnsAggregatedData()
        {
            // Act: Call the aggregated method using the complete fountain list.
            var result = await _service.GetFavoriteFountainsAnalysis(_fountains);

            // Assert: Validate the aggregated results.
            // Expect total tests: 3.
            Assert.Equal(3, result.TotalTests);

            // Lowest radon value should be 50 from fountain1 ("Fountain A").
            Assert.Equal(50, result.LowestRadonValue);
            Assert.Equal("Fountain A", result.LowestRadonFountain);

            // Highest radon value should be 200 from fountain2 ("Fountain B").
            Assert.Equal(200, result.HighestRadonValue);
            Assert.Equal("Fountain B", result.HighestRadonFountain);

            // Drinkable tests: tests with <= 150 radon are drinkable.
            // Here, two out of three tests are drinkable (50 and 75), so expected percentage ≈ 66.67%.
            double expectedPercentage = (2 * 100.0) / 3;
            Assert.Equal(expectedPercentage, result.DrinkablePercentage, 1);
        }

        [Fact]
        public async Task Test_GetFavoriteFountainsAnalysis_WithNoRecords_ThrowsEmptyList()
        {
            // Arrange: Setup the context to return an empty water analyses list.
            var emptyWaterAnalyses = new List<WaterAnalysis>();
            _mockContext.Setup(c => c.WaterAnalysis)
                        .Returns(DbSetMocking.CreateMockDbSet(emptyWaterAnalyses).Object);

            // Act & Assert: Calling the service should throw an EmptyList exception.
            await Assert.ThrowsAsync<EmptyList>(() => _service.GetFavoriteFountainsAnalysis(_fountains));
        }
    }

    // Helper class to create a mock DbSet<T> from an in-memory list.
    public static class DbSetMocking
    {
        public static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            return dbSet;
        }
    }
}
*/