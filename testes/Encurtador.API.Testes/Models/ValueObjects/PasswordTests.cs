using System;
using Encurtador.API.Models.ValueObjects;

namespace Encurtador.API.Testes.Models.ValueObjects
{
    public class PasswordTests
    {
        private readonly string PASSWORD = "$WHfeUhgBWjh1";

        [Fact(DisplayName = "Should create a password.")]
        public void ShouldCreateAPassword()
            => Assert.NotNull(new Password("$WHfeUhgBWjh1"));

        [Fact(DisplayName = "Should throw exception when password is empty.")]
        public void ShouldThrowExceptionWhenPasswordIsEmpty()
           => Assert.Throws<ArgumentException>(() => new Password(string.Empty));

        [Fact(DisplayName = "Should throw exception when password is null.")]
        public void ShouldThrowExceptionWhenPasswordIsNull()
           => Assert.Throws<ArgumentException>(() => new Password(null));

        [Fact(DisplayName = "Should return true if password is equals.")]
        public void ShouldReturnTrueIfPasswordIsEquals()
        {
            var password = new Password(PASSWORD);

            Assert.True(password.Compare(PASSWORD));
        }

        [Fact(DisplayName = "Should return false if password is equals.")]
        public void ShouldReturnFalseIfPasswordIsEquals()
        {
            var password = new Password(PASSWORD);

            Assert.False(password.Compare("@1231231231"));
        }


        [Fact(DisplayName = "Should throw exception when password in compare is empty")]
        public void ShouldThrowExceptionWhenPasswordInCompareIsEmpty()
        {
            var password = new Password(PASSWORD);

            Assert.Throws<ArgumentException>(() => password.Compare(string.Empty));
        }

        [Fact(DisplayName = "Should throw exception when password in compare is null")]
        public void ShouldThrowExceptionWhenPasswordInCompareIsNull()
        {
            var password = new Password(PASSWORD);

            Assert.Throws<ArgumentException>(() => password.Compare(null));
        }
    }
}

