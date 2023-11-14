using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Exceptions;

namespace VoteAPI.Tests.Domain
{
    public class ScheduleTest
    {
        [Fact(DisplayName = "Valid schedule creation should succeed")]
        public void ValidScheduleCreation()
        {
            // Arrange
            var description = "Important Meeting";
            var startingDate = DateTime.Now.AddHours(1);
            var finishingDate = DateTime.Now.AddHours(2);

            // Act
            var schedule = new Schedule(description, startingDate, finishingDate);

            // Assert
            Assert.Equal(description.Trim(), schedule.Description);
            Assert.Equal(startingDate, schedule.StartingDate);
            Assert.Equal(finishingDate, schedule.FinishingDate);
            Assert.True(schedule.Active);
        }

        [Fact(DisplayName = "Empty description should throw VoteAPIException")]
        public void EmptyDescriptionShouldThrowException()
        {
            // Arrange
            var description = "";
            var startingDate = DateTime.Now.AddHours(1);
            var finishingDate = DateTime.Now.AddHours(2);

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Schedule(description, startingDate, finishingDate));
        }

        [Fact(DisplayName = "Short description should throw VoteAPIException")]
        public void ShortDescriptionShouldThrowException()
        {
            // Arrange
            var description = "Abc";
            var startingDate = DateTime.Now.AddHours(1);
            var finishingDate = DateTime.Now.AddHours(2);

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Schedule(description, startingDate, finishingDate));
        }

        [Fact(DisplayName = "Starting date in the past should throw VoteAPIException")]
        public void StartingDateInPastShouldThrowException()
        {
            // Arrange
            var description = "Important Meeting";
            var startingDate = DateTime.Now.AddHours(-1);
            var finishingDate = DateTime.Now.AddHours(2);

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Schedule(description, startingDate, finishingDate));
        }

        [Fact(DisplayName = "Finishing date in the past should throw VoteAPIException")]
        public void FinishingDateInPastShouldThrowException()
        {
            // Arrange
            var description = "Important Meeting";
            var startingDate = DateTime.Now.AddHours(1);
            var finishingDate = DateTime.Now.AddHours(-1);

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Schedule(description, startingDate, finishingDate));
        }

        [Fact(DisplayName = "Finishing date before starting date should throw VoteAPIException")]
        public void FinishingDateBeforeStartingDateShouldThrowException()
        {
            // Arrange
            var description = "Important Meeting";
            var startingDate = DateTime.Now.AddHours(2);
            var finishingDate = DateTime.Now.AddHours(1);

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Schedule(description, startingDate, finishingDate));
        }
        [Fact(DisplayName = "SetActive should update active status")]
        public void SetActiveShouldUpdateActiveStatus()
        {
            // Arrange
            var description = "Important Meeting";
            var startingDate = DateTime.Now.AddHours(1);
            var finishingDate = DateTime.Now.AddHours(2);
            var schedule = new Schedule(description, startingDate, finishingDate);

            // Act
            schedule.SetActive(false);

            // Assert
            Assert.False(schedule.Active);
        }

        [Fact(DisplayName = "Trimmed description should be set")]
        public void TrimmedDescription()
        {
            // Arrange
            var description = "  Important Meeting  ";
            var startingDate = DateTime.Now.AddHours(1);
            var finishingDate = DateTime.Now.AddHours(2);

            // Act
            var schedule = new Schedule(description, startingDate, finishingDate);

            // Assert
            Assert.Equal("Important Meeting", schedule.Description);
        }
    }
}
