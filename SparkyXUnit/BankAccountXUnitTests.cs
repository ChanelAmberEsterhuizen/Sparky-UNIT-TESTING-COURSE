using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
   
    public class BankAccountXUnitTests
    {
        private BankAccount account; 
       
        public BankAccountXUnitTests()
        {
            
        }
        //[Test]
        //public void BankDepositLogFaker_Add100_ReturnTrue()
        //{
        //    BankAccount bankAccount = new(new LogFaker());
        //    var result = bankAccount.Deposit(100);
        //    Assert.IsTrue(result);
        //    Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
                
        //}
        [Fact]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));
            BankAccount bankAccount = new(logMock.Object);
            var result = bankAccount.Deposit(100);
            Assert.True(result);
            Assert.Equal(100, bankAccount.GetBalance());

        }
        [Theory]
        [InlineData(200, 100)]
        public void BankWwithdraw_Withdraw100WWith200Balance_ReturnsTrue(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>(); 
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWWithdrawal(It.Is<int>(x => x>0))).Returns(true);
            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.True(result); 
        }

        [Theory]
        [InlineData(200, 300)]
        public void BankWwithdraw_Withdraw100WWith200Balance_ReturnsFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogBalanceAfterWWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            //logMock.Setup(u => u.LogBalanceAfterWWithdrawal(It.Is<int>(x => x < 0))).Returns(false);
            logMock.Setup(u => u.LogBalanceAfterWWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);
            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.False(result);
        }

        [Fact]
    
        public void BankLogDummy_LogMockStringOutPutStr_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello"; 
            logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.True(logMock.Object.LogWithOutputResult("Ben", out result)); 
            Assert.Equal(desiredOutput, result); 
        }

        [Fact]

        public void BankLogDummy_LogRefChecker_ReturnsTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new();
            Customer customerNotUsed = new();
            logMock.Setup(u => u.LogWithRefObj(ref customer )).Returns(true);
            Assert.False(logMock.Object.LogWithRefObj(ref customerNotUsed));
            Assert.True(logMock.Object.LogWithRefObj(ref customer));
        }

        [Fact]

        public void BankLogDummy_SetAndGetLogTypeSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties(); 
            logMock.Setup(u => u.Logseverity).Returns(10);
            logMock.Setup(u => u.LogType).Returns("warning");
            logMock.Object.Logseverity = 100; 
            Assert.Equal(100, logMock.Object.Logseverity);
            Assert.Equal("warning", logMock.Object.LogType);

            //callbacks 
            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback((string str) => logTemp += str);
            logMock.Object.LogToDb("Ben");
            Assert.Equal("Hello, Ben", logTemp);

            //callbacks 
            int counter  = 5 ;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>()))
                .Returns(true).Callback(() => counter++);
            logMock.Object.LogToDb("Ben");
            logMock.Object.LogToDb("Ben");
            Assert.Equal(7, counter);

        }

        [Fact]
        public void BankLogDummy_VerifyExamle()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(100);
            Assert.Equal(100, bankAccount.GetBalance());
            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);
            logMock.VerifySet(u => u.Logseverity = 101, Times.Once);
            logMock.VerifyGet(u => u.Logseverity , Times.Once);
        }
    }
}
