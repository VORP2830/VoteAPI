using VoteAPI.Domain.Entities;
using VoteAPI.Domain.Exceptions;

namespace VoteAPI.Tests.Domain
{
    public class PersonTest
    {
        [Fact(DisplayName = "Valid person creation should succeed")]
        public void ValidPersonCreation()
        {
            // Arrange
            var cpf = "12345678901";

            // Act
            var person = new Person(cpf);

            // Assert
            Assert.Equal(cpf.Trim(), person.CPF);
            Assert.True(person.Active);
        }
        [Fact(DisplayName = "Invalid CPF length should throw VoteAPIException")]
        public void InvalidCPFLengthShouldThrowException()
        {
            // Arrange
            var cpf = "123456";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Person(cpf));
        }

        [Fact(DisplayName = "Non-numeric CPF should throw VoteAPIException")]
        public void NonNumericCPFShouldThrowException()
        {
            // Arrange
            var cpf = "abc456def01";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Person(cpf));
        }

        [Fact(DisplayName = "SetActive should update active status")]
        public void SetActiveShouldUpdateActiveStatus()
        {
            // Arrange
            var cpf = "12345678901";
            var person = new Person(cpf);

            // Act
            person.SetActive(false);

            // Assert
            Assert.False(person.Active);
        }

        [Fact(DisplayName = "Trimmed CPF should be set")]
        public void TrimmedNameAndCPF()
        {
            // Arrange
            var cpf = "  12345678901  ";

            // Act
            var person = new Person(cpf);

            // Assert
            Assert.Equal("12345678901", person.CPF);
        }
    }
}
