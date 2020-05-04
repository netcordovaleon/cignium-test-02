using CGM.Entities;
using CGM.Resolver;
using CGM.Utils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGM.Test
{
    [TestFixture]
    public class TestResolverEngine
    {
        private IList<SearchEngineResponse> responseSearchEngine = null;
        readonly Mock<IResolverSearchEngine> mock = new Mock<IResolverSearchEngine>();

        public TestResolverEngine()
        {
            mock.Setup(p => p.GetWinnerByEngine(It.IsAny<List<SearchEngineResponse>>(), It.IsAny<string>()))
            .Returns((List<SearchEngineResponse> Response, string Engine) =>
            {
                return Response.Where(x => x.Engine == Engine).OrderByDescending(x => x.Total).FirstOrDefault();
            });
            mock.Setup(p => p.GetTotalResultByEngineAndLanguage(It.IsAny<List<SearchEngineResponse>>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns((List<SearchEngineResponse> Response, string Engine, string Lang) =>
            {
                return Response.FirstOrDefault(c => c.Engine == Engine && c.Name == Lang).Total;
            });
            mock.Setup(p => p.GetWinner(It.IsAny<List<SearchEngineResponse>>()))
            .Returns((List<SearchEngineResponse> Response) =>
            {
                var groupResponseByLanguages = Response.GroupBy(item => item.Name);
                return groupResponseByLanguages.Select(x => new SearchEngineResponse() { Name = x.Key, Total = x.Sum(y => y.Total) }).OrderByDescending(z => z.Total).First();
            });
        }

        [Test]
        public void Verify_That_Language_Net_Is_Winner_With_Mock_Data() {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var response = program.GetWinner(responseSearchEngine);
            Assert.AreEqual("net", response.Name);
        }

        [Test]
        public void Verify_That_Language_Java_Dont_Win_With_Mock_Data()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var response = program.GetWinner(responseSearchEngine);
            Assert.AreNotEqual("java", response.Name);
        }

        [Test]
        public void Verify_Total_Register_With_Net_Language()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var response = program.GetWinner(responseSearchEngine);
            Assert.AreEqual(76000, response.Total);
        }

        [Test]
        public void Verify_That_Net_Win_In_Google()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var response = program.GetWinnerByEngine(responseSearchEngine, Constants.Google.ENGINE);
            Assert.AreEqual("net", response.Name);
        }

        [Test]
        public void Verify_That_JavaScript_Win_In_Bing()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var response = program.GetWinnerByEngine(responseSearchEngine, Constants.Bing.ENGINE);
            Assert.AreEqual("javascript", response.Name);
        }

        [Test]
        public void Verify_Total_Search_Net_Language_In_Google()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var totalRegister = program.GetTotalResultByEngineAndLanguage(responseSearchEngine, Constants.Google.ENGINE, "net");
            Assert.AreEqual(50000, totalRegister);
        }

        [Test]
        public void Verify_Total_Search_JavaScript_Language_In_Google()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var totalRegister = program.GetTotalResultByEngineAndLanguage(responseSearchEngine, Constants.Google.ENGINE, "javascript");
            Assert.AreEqual(2500, totalRegister);
        }

        [Test]
        public void Verify_Total_Search_Java_Language_In_Google()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var totalRegister = program.GetTotalResultByEngineAndLanguage(responseSearchEngine, Constants.Google.ENGINE, "java");
            Assert.AreEqual(1000, totalRegister);
        }

        [Test]
        public void Verify_Total_Search_Net_Language_In_Bing()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var totalRegister = program.GetTotalResultByEngineAndLanguage(responseSearchEngine, Constants.Bing.ENGINE, "net");
            Assert.AreEqual(26000, totalRegister);
        }

        [Test]
        public void Verify_Total_Search_JavaScript_Language_In_Bing()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var totalRegister = program.GetTotalResultByEngineAndLanguage(responseSearchEngine, Constants.Bing.ENGINE, "javascript");
            Assert.AreEqual(26001, totalRegister);
        }

        [Test]
        public void Verify_Total_Search_Java_Language_In_Bing()
        {
            IResolverSearchEngine program = mock.Object;
            responseSearchEngine = DataResolverEngine.GetInstance().Data();
            var totalRegister = program.GetTotalResultByEngineAndLanguage(responseSearchEngine, Constants.Bing.ENGINE, "java");
            Assert.AreEqual(500, totalRegister);
        }
    }
}
