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
            var name = "John Doe";
            var cpf = "12345678901";

            // Act
            var person = new Person(name, cpf);

            // Assert
            Assert.Equal(name.Trim(), person.Name);
            Assert.Equal(cpf.Trim(), person.CPF);
            Assert.True(person.Active);
        }

        [Fact(DisplayName = "Empty name should throw VoteAPIException")]
        public void EmptyNameShouldThrowException()
        {
            // Arrange
            var name = "";
            var cpf = "12345678901";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Person(name, cpf));
        }

        [Fact(DisplayName = "Short name should throw VoteAPIException")]
        public void ShortNameShouldThrowException()
        {
            // Arrange
            var name = "Ab";
            var cpf = "12345678901";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Person(name, cpf));
        }

        [Fact(DisplayName = "Invalid CPF length should throw VoteAPIException")]
        public void InvalidCPFLengthShouldThrowException()
        {
            // Arrange
            var name = "John Doe";
            var cpf = "123456";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Person(name, cpf));
        }

        [Fact(DisplayName = "Non-numeric CPF should throw VoteAPIException")]
        public void NonNumericCPFShouldThrowException()
        {
            // Arrange
            var name = "John Doe";
            var cpf = "abc456def01";

            // Act & Assert
            Assert.Throws<VoteAPIException>(() => new Person(name, cpf));
        }

        [Fact(DisplayName = "SetActive should update active status")]
        public void SetActiveShouldUpdateActiveStatus()
        {
            // Arrange
            var name = "John Doe";
            var cpf = "12345678901";
            var person = new Person(name, cpf);

            // Act
            person.SetActive(false);

            // Assert
            Assert.False(person.Active);
        }

        [Fact(DisplayName = "Trimmed name and CPF should be set")]
        public void TrimmedNameAndCPF()
        {
            // Arrange
            var name = "  John Doe  ";
            var cpf = "  12345678901  ";

            // Act
            var person = new Person(name, cpf);

            // Assert
            Assert.Equal("John Doe", person.Name);
            Assert.Equal("12345678901", person.CPF);
        }
    }
}
