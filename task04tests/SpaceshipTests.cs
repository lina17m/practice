using Xunit;
using task04;

namespace task04tests
{
    public class SpaceshipTests
    {

        [Fact]
        public void Cruiser_ShouldHaveCorrectStats()
        {
            ISpaceship cruiser = new Cruiser();
            Assert.Equal(50, cruiser.Speed);
            Assert.Equal(100, cruiser.FirePower);
        }

        [Fact]
        public void Fighter_ShouldBeFasterThanCruiser()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.Speed > cruiser.Speed);
        }


        [Fact]
        public void Fighter_MoveForward_ShouldIncreaseEngineFatigue()
        {
            var fighter = new Fighter();
            fighter.MoveForward();
            
            Assert.Equal(10, fighter.EngineFatigue);
        }

        [Fact]
        public void Fighter_Rotate_ShouldHandleOver360Degrees()
        {
            var fighter = new Fighter();
            fighter.Rotate(400);
            
            Assert.Equal(40, fighter.CurrentAngle);
        }

        [Fact]
        public void Fighter_Fire_ShouldIncreaseHeatLevel()
        {
            var fighter = new Fighter();
            fighter.Fire();
            
            Assert.Equal(10, fighter.HeatLevel);
        }

        [Fact]
        public void Cruiser_MoveForward_ShouldDecreaseFuel()
        {
            var cruiser = new Cruiser();
            int initialFuel = cruiser.Fuel;
            cruiser.MoveForward();
            
            Assert.Equal(initialFuel - 50, cruiser.Fuel);
        }

        [Fact]
        public void Cruiser_Rotate_ShouldBeLimitedTo45Degrees()
        {
            var cruiser = new Cruiser();
            cruiser.Rotate(90); 
            
            Assert.Equal(45, cruiser.CurrentAngle);
        }

        [Fact]
        public void Cruiser_Fire_ShouldDecreaseRocketCount()
        {
            var cruiser = new Cruiser();
            int initialRockets = cruiser.RocketCount;
            cruiser.Fire();
            
            Assert.Equal(initialRockets - 1, cruiser.RocketCount);
        }
    }
}
