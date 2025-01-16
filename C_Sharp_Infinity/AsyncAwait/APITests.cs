using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{

    [TestFixture]
    public class APITests
    {
        [Test]
        public async Task FetchDataFromApi_ShouldReturnValidResponse()
        {
            //Arrange
            var apiUrl = "https://jsonplaceholder.typicode.com/posts/1";

            //Act
            var result = await AsyncAwaitNew.FetchDataFromAPI(apiUrl);

            //Assert
           // Assert.IsNotNull(result, "The result should not be null.");


        }
    }
}
