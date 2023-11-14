using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Exceptions;

namespace VoteAPI.Tests.Domain
{
    public class VoteTest
    {
        [Fact(DisplayName = "Valid vote creation should succeed")]
        public void ValidVoteCreation()
        {
            // Arrange
            var personId = 1;
            var scheduleId = 2;
            var voteOption = "S";

            // Act
            var vote = new Vote(personId, scheduleId, voteOption);

            // Assert
            Assert.Equal(personId, vote.PersonId);
            Assert.Equal(scheduleId, vote.ScheduleId);
            Assert.Equal(voteOption, vote.VoteOption);
            Assert.True(vote.Active);
        }

        [Fact(DisplayName = "Invalid person ID should throw VoteAPIException")]
        public void InvalidPersonIdShouldThrowException()
        {
            // Arrange
            var personId = 0;
            var scheduleId = 2;
            var voteOption = "S";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Vote(personId, scheduleId, voteOption));
        }

        [Fact(DisplayName = "Invalid schedule ID should throw VoteAPIException")]
        public void InvalidScheduleIdShouldThrowException()
        {
            // Arrange
            var personId = 1;
            var scheduleId = 0;
            var voteOption = "S";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Vote(personId, scheduleId, voteOption));
        }

        [Fact(DisplayName = "Empty vote option should throw VoteAPIException")]
        public void EmptyVoteOptionShouldThrowException()
        {
            // Arrange
            var personId = 1;
            var scheduleId = 2;
            var voteOption = "";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Vote(personId, scheduleId, voteOption));
        }

        [Fact(DisplayName = "Invalid vote option length should throw VoteAPIException")]
        public void InvalidVoteOptionLengthShouldThrowException()
        {
            // Arrange
            var personId = 1;
            var scheduleId = 2;
            var voteOption = "Invalid";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Vote(personId, scheduleId, voteOption));
        }

        [Fact(DisplayName = "Invalid vote option should throw VoteAPIException")]
        public void InvalidVoteOptionShouldThrowException()
        {
            // Arrange
            var personId = 1;
            var scheduleId = 2;
            var voteOption = "A";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Vote(personId, scheduleId, voteOption));
        }

        [Fact(DisplayName = "SetActive should update active status")]
        public void SetActiveShouldUpdateActiveStatus()
        {
            // Arrange
            var personId = 1;
            var scheduleId = 2;
            var voteOption = "S";
            var vote = new Vote(personId, scheduleId, voteOption);

            // Act
            vote.SetActive(false);

            // Assert
            Assert.False(vote.Active);
        }

        [Fact(DisplayName = "Valid vote option 'N' should succeed")]
        public void ValidVoteOptionNoShouldSucceed()
        {
            // Arrange
            var personId = 1;
            var scheduleId = 2;
            var voteOption = "N";

            // Act
            var vote = new Vote(personId, scheduleId, voteOption);

            // Assert
            Assert.Equal(voteOption, vote.VoteOption);
        }
    }
}
