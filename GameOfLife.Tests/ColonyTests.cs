using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using GameOfLife.Model;

namespace GameOfLife.Tests
{

    [TestFixture]
    public class ColonyTests
    {
        private Colony colony;

        [SetUp]
        public void Setup()
        {
            colony = new Colony(3, 3);
            colony.Reset(3, 3);
        }

        [Test]
        public void ComputeNextGeneration_DeadCellWithThreeLiveNeighbors_ShouldRevive()
        {
            // Assert initial state
            Assert.IsFalse(colony.GetCellState(0, 0));
            Assert.IsFalse(colony.GetCellState(0, 1));
            Assert.IsFalse(colony.GetCellState(1, 0));

            colony.SetCellState(0, 0, true); //setting to:
            colony.SetCellState(0, 1, true); //   X X
            colony.SetCellState(1, 0, true); //   X 

            Assert.IsFalse(colony.GetCellState(1, 1)); // Before method

            colony.ComputeNextGeneration();

            Assert.IsTrue(colony.GetCellState(0, 0)); // After
            Assert.IsTrue(colony.GetCellState(0, 1)); // X X
            Assert.IsTrue(colony.GetCellState(1, 0)); // X X
            Assert.IsTrue(colony.GetCellState(1, 1));
        }

        [Test]
        public void ComputeNextGeneration_AliveCellDiesDueToUnderpopulation()
        {

            colony.SetCellState(0, 0, true);

            // Assert initial state
            Assert.IsTrue(colony.GetCellState(0, 0)); // This cell is alive now
            Assert.IsFalse(colony.GetCellState(0, 1));
            Assert.IsFalse(colony.GetCellState(1, 0));
            Assert.IsFalse(colony.GetCellState(1, 1));

            // Update the colony to the next generation
            colony.ComputeNextGeneration();

            // After ComputeNextGeneration, the only alive cell should die.
            Assert.IsFalse(colony.GetCellState(0, 0));
        }

        [Test]
        public void ComputeNextGeneration_AliveCellDiesByOverpopulation()
        {
            // Set initial colony state to form a block
            colony.SetCellState(0, 0, true); // X X X
            colony.SetCellState(0, 1, true); // X X X
            colony.SetCellState(0, 2, true); //     X
            colony.SetCellState(1, 0, true);
            colony.SetCellState(1, 1, true);
            colony.SetCellState(1, 2, true);
            colony.SetCellState(2, 2, true);

            // Check state before ComputeNextGeneration
            Assert.IsTrue(colony.GetCellState(1, 1)); // This cell has 8 neighbors

            // Update the colony to the next generation
            colony.ComputeNextGeneration();

            // After ComputeNextGeneration, the center cell should die
            Assert.IsFalse(colony.GetCellState(1, 1));
        }


        [Test]
        public void ComputeNextGeneration_AliveCellWithTwoOrThreeLiveNeighbors_ShouldSurvive()
        {
            // Case with 2 neighbors
            colony.SetCellState(0, 0, true); // X X
            colony.SetCellState(0, 1, true); // X
            colony.SetCellState(1, 0, true);

            // Check state before ComputeNextGeneration
            Assert.IsTrue(colony.GetCellState(0, 1)); // This cell has 2 neighbors

            colony.ComputeNextGeneration();

            // After ComputeNextGeneration, the cell should still be alive
            Assert.IsTrue(colony.GetCellState(0, 1));

            // Resetting for case with 3 neighbors
            colony.Reset(3, 3);

            colony.SetCellState(0, 0, true); // X X
            colony.SetCellState(0, 1, true); //   X X
            colony.SetCellState(1, 1, true);
            colony.SetCellState(1, 2, true);

            // Check state before ComputeNextGeneration
            Assert.IsTrue(colony.GetCellState(1, 1)); // This cell has 3 neighbors

            colony.ComputeNextGeneration();

            // After ComputeNextGeneration, the cell should still be alive
            Assert.IsTrue(colony.GetCellState(1, 1));
        }
    }
}