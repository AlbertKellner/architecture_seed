namespace Helpers.Tests
{
    using Xunit;

    public class CpfTests
    {
        [Fact]
        public void IsNotValidFormat()
        {
            Assert.False(HelperMethods.IsValidCpf(string.Empty));
            Assert.False(HelperMethods.IsValidCpf("123"));
            Assert.False(HelperMethods.IsValidCpf("123asd"));
            Assert.False(HelperMethods.IsValidCpf("asd"));
        }

        [Fact]
        public void IsValidFormat()
        {
            Assert.True(HelperMethods.IsValidCpf("81869757300"));
            Assert.True(HelperMethods.IsValidCpf("02259637361"));
            Assert.True(HelperMethods.IsValidCpf("84881410334"));
            Assert.True(HelperMethods.IsValidCpf("16658174296"));
            Assert.True(HelperMethods.IsValidCpf("90523156200"));

            Assert.True(HelperMethods.IsValidCpf("818.697.573-00"));
            Assert.True(HelperMethods.IsValidCpf("022.596.373-61"));
            Assert.True(HelperMethods.IsValidCpf("848.814.103-34"));
            Assert.True(HelperMethods.IsValidCpf("166.581.742-96"));
            Assert.True(HelperMethods.IsValidCpf("905.231.562-00"));
        }
    }
}