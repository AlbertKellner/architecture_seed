namespace Helpers.Tests
{
    using Xunit;

    public class CnpjTests
    {
        [Fact]
        public void IsNotValidFormat()
        {
            Assert.False(HelperMethods.IsValidCnpj(string.Empty));
            Assert.False(HelperMethods.IsValidCnpj("123"));
            Assert.False(HelperMethods.IsValidCnpj("123asd"));
            Assert.False(HelperMethods.IsValidCnpj("asd"));
        }

        [Fact]
        public void IsValidFormat()
        {
            Assert.True(HelperMethods.IsValidCnpj("41987483000147"));
            Assert.True(HelperMethods.IsValidCnpj("34381528000170"));
            Assert.True(HelperMethods.IsValidCnpj("16696381000125"));
            Assert.True(HelperMethods.IsValidCnpj("94541778000151"));
            Assert.True(HelperMethods.IsValidCnpj("04467871000170"));

            Assert.True(HelperMethods.IsValidCnpj("41.987.483/0001-47"));
            Assert.True(HelperMethods.IsValidCnpj("34.381.528/0001-70"));
            Assert.True(HelperMethods.IsValidCnpj("16.696.381/0001-25"));
            Assert.True(HelperMethods.IsValidCnpj("94.541.778/0001-51"));
            Assert.True(HelperMethods.IsValidCnpj("04.467.871/0001-70"));
        }
    }
}