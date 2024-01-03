using System;
using Encurtador.API.Models;

namespace Encurtador.API.Testes.Models
{
    public class ShortenedTests
    {
        [Fact(DisplayName = "Should create shortened.")]
        public void ShouldCreateShortened()
            => Assert.NotNull(new Shortened("https://google.com", new MongoDB.Bson.ObjectId("65837c191499e44a497d4235")));

        [Fact(DisplayName = "Should throw exception when url is empty.")]
        public void ShouldThrowExceptionWhenUrlIsEmpty()
            => Assert.Throws<ArgumentException>(() => new Shortened(string.Empty, new MongoDB.Bson.ObjectId("65837c191499e44a497d4235")));

        [Fact(DisplayName = "Should throw exception when url is null.")]
        public void ShouldThrowExceptionWhenUrlIsNull()
           => Assert.Throws<ArgumentException>(() => new Shortened(null, new MongoDB.Bson.ObjectId("65837c191499e44a497d4235")));

        [Fact(DisplayName = "Should create shortened with burned false.")]
        public void ShouldCreateShortenedWithBunerdFalse()
        {
            var entity = new Shortened("https://google.com", new MongoDB.Bson.ObjectId("65837c191499e44a497d4235"));

            Assert.False(entity.Burned);
        } 

        [Fact(DisplayName = "Should create shortened with burned false.")]
        public void ShouldBurnShortenedWithBunerd()
        {
            var entity = new Shortened("https://google.com", new MongoDB.Bson.ObjectId("65837c191499e44a497d4235"));
            entity.Burn();

            Assert.True(entity.Burned);
        }
         
    }
}

