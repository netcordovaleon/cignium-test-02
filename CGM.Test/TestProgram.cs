using System;
using CGM.Resolver;
using Moq;
using NUnit.Framework;

namespace CGM.Test
{
    [TestFixture]
    public class TestProgram
    {

        readonly Mock<IResolverSearchEngine> mock = new Mock<IResolverSearchEngine>();

        public TestProgram() {
            mock.Setup(p => p.SearchEngineResultInString(It.IsAny<string[]>()))
            .Returns((string[] arg) =>
            {
                if (arg.Length < 2)
                    throw new ArgumentException(string.Empty);
                return "YOLO";
            });
        }

        [Test]
        public void Verify_Method_Return_Exception_If_Arguments_Is_Correct()
        {
            IResolverSearchEngine program = mock.Object;
            Assert.Throws<ArgumentException>(() => program.SearchEngineResultInString(new string[] { "java" }));
        }

        [Test]
        public void Verify_Method_Dont_Return_Exception_When_Arguments_Is_Correct()
        {
            IResolverSearchEngine program = mock.Object;
            Assert.DoesNotThrow(() => program.SearchEngineResultInString(new string[] { "java", ".net", "javascript" }));
        }

        [Test]
        public void Verify_Method_Dont_Return_Empty_Answer()
        {
            IResolverSearchEngine program = mock.Object;
            var response = program.SearchEngineResultInString(new string[] { "java", ".net" });
            Assert.AreNotEqual("", response);
        }

        [Test]
        public void Verify_Method_Return_Expected_MoqAnswer ()
        {
            IResolverSearchEngine program = mock.Object;
            var response = program.SearchEngineResultInString(new string[] { "java", ".net" });
            Assert.AreEqual("YOLO", response);
        }
    }
}
