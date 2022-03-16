using Shouldly;
using System.Linq;
using TrisslottSimulator;
using Xunit;

namespace TrissLottSimulator.Tests
{
    public class ScratcherTests
    {
        private readonly Scratcher _sut;

        public ScratcherTests()
        {
            _sut = new Scratcher();
        }

        [Fact]
        public void ShouldReturnSameAmountOfScratchesAsPassedIn()
        {
            var results = _sut.Scratch(100);
            
            results.Values.Sum(x => x).ShouldBe(100);
        }

        [Fact]
        public void NumberOfScratchesShouldBeSameAsSumOfPassedInCount()
        {
            _ =_sut.Scratch(100);
            _ = _sut.Scratch(20);

            _sut.NumberOfScratches.ShouldBe(120);
        }

        [Fact]
        public void AmountSpentShouldBeTicketPriceTimesNumberOfScratches()
        {
            var ticketPrice = 30;
            var numberOfScratches = 100;
            var expected = ticketPrice * numberOfScratches;

            _ = _sut.Scratch(numberOfScratches);

            _sut.AmountSpent.ShouldBe(expected);
        }
    }
}