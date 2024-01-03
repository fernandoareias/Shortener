using System;
using Encurtador.API.Models.ValueObjects;

namespace Encurtador.API.Testes.Models.ValueObjects
{
    public class EmailTests
    {

        [Fact(DisplayName = "Should create a e-mail")]
        public void ShouldCreateAEmail()
            => Assert.NotNull(new Email("fernandoareias@hotmail.com"));

        [Fact(DisplayName = "Should throw exception when e-mail is empty.")]
        public void ShouldThrowExceptionWhenEmailIsEmpty()
            => Assert.Throws<ArgumentException>(() => new Email(string.Empty));

        [Fact(DisplayName = "Should throw exception when e-mail is null.")]
        public void ShouldThrowExceptionWhenEmailIsNull()
            => Assert.Throws<ArgumentException>(() => new Email(null));

        [Fact(DisplayName = "Should throw exception when e-mail is white space.")]
        public void ShouldThrowExceptionWhenEmailIsWhiteSpace()
            => Assert.Throws<ArgumentException>(() => new Email(" "));

        [Fact(DisplayName = "Should return true when e-mail is invalid.")]
        public void ShouldReturnTrueWhenEmailIsInvalid()
           => Assert.True(new Email("fareias@gmail.com").IsValid());

        [Fact(DisplayName = "Should throw exception when e-mail is invalid.")]
        public void ShouldThrowExceptionWhenEmailIsInvalid()
           => Assert.Throws<ArgumentException>(() => new Email("fareias.com"));
    }
}

