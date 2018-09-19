namespace Helpers.Tests
{
    using System;
    using Xunit;

    public class AgeTests
    {
        [Fact]
        public void ValidAge()
        {
            var birthdate = new DateTime(1985, 4, 26);
            var today = new DateTime(2018, 4, 10);

            var age = HelperMethods.CalculateAge(today, birthdate);

            Assert.True(age == 32);
        }

        [Fact]
        public void ValidAge_EdgeLeft()
        {
            var birthdate = new DateTime(1985, 4, 26);
            var today = new DateTime(2018, 4, 25);

            var age = HelperMethods.CalculateAge(today, birthdate);

            Assert.True(age == 32);
        }

        [Fact]
        public void ValidAge_EdgeRight()
        {
            var birthdate = new DateTime(1985, 4, 26);
            var today = new DateTime(2018, 4, 26);

            var age = HelperMethods.CalculateAge(today, birthdate);

            Assert.True(age == 33);
        }

        [Fact]
        public void ValidAge2()
        {
            var birthdate = new DateTime(1985, 4, 26);
            var today = new DateTime(2018, 4, 30);

            var age = HelperMethods.CalculateAge(today, birthdate);

            Assert.True(age == 33);
        }
    }
}