using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace Repro
{
    [TestFixture]
    public class ReproClass
    {
        [Test]
        public void ReproTestRunFails()
        {
            var processor = new Mock<IReproInterface>();
            processor.Setup(x => x.ProcessSmth(It.IsAny<IEnumerable<object>>()))
                     .Returns((Task<List<object>> val) => val);
            
            var anyProc = new AnyProcessor(processor.Object);
            
            anyProc.Process();
        }
    }

    public interface IReproInterface
    {
        Task<List<object>> ProcessSmth(IEnumerable<object> input);
    }

    public class AnyProcessor
    {
        private readonly IReproInterface _reproInterface;

        public AnyProcessor(IReproInterface reproInterface)
        {
            _reproInterface = reproInterface;
        }

        public async void Process()
        {
            var list = new List<object> {new object()};
            var @out = await _reproInterface.ProcessSmth(list);
        }
    }
}