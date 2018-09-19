namespace Helpers.Tests
{
    using Xunit;

    public class EmailTests
    {
        [Fact]
        public void IsNotValidFormat()
        {
            Assert.False(HelperMethods.IsValidEmail(string.Empty));
            Assert.False(HelperMethods.IsValidEmail("fdsa"));
            Assert.False(HelperMethods.IsValidEmail("fdsa@"));
            Assert.False(HelperMethods.IsValidEmail("fdsa@fdsa"));
            Assert.False(HelperMethods.IsValidEmail("fdsa@fdsa."));
            Assert.False(HelperMethods.IsValidEmail("fdsa@fdsa.c"));
        }

        [Fact]
        public void IsValidFormat()
        {
            Assert.True(HelperMethods.IsValidEmail("someone@somewhere.com"));
            Assert.True(HelperMethods.IsValidEmail("someone@somewhere.co.uk"));
            Assert.True(HelperMethods.IsValidEmail("someone+tag@somewhere.net"));
            Assert.True(HelperMethods.IsValidEmail("futureTLD@somewhere.fooo"));
            Assert.True(HelperMethods.IsValidEmail("futureTLD@somewhere.fooo"));
            Assert.True(HelperMethods.IsValidEmail("development@kaluga.com.br"));
        }
    }
}