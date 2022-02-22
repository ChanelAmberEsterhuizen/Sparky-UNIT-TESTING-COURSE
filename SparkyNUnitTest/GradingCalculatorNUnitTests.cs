using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class GradingCalculatorNUnitTests
    {
        private GradingCalculator calc;
        [SetUp]
        public void Setup()
        {
            calc = new GradingCalculator();
        }

        [Test]
        public void Gradecalc_InputScore95Attendance90_getAGrade()
        {
            calc.Score = 95;
            calc.AttendancePercentage = 90;
            string result = calc.GetGrade();
            Assert.That(result, Is.EqualTo("A")); 
        }
        
        [Test]
        [TestCase(95, 55)]//F
        [TestCase(65, 55)]//F
        [TestCase(50, 90)]//F
        public void GetGradeCalc_FScenarios_GetFGrade(int a , int b )
        {

            calc.Score = a;
            calc.AttendancePercentage = b; 
            //Act
            string  result = calc.GetGrade();

            //Assert 
            Assert.AreEqual("F", result);
        }


        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]//A
        [TestCase(85, 90, ExpectedResult = "B")]//
        [TestCase(65, 90, ExpectedResult = "C")]//
        [TestCase(95, 65, ExpectedResult = "B")]//
        [TestCase(95, 55, ExpectedResult = "F") ]//F
        [TestCase(65, 55, ExpectedResult = "F")]//F
        [TestCase(50, 90, ExpectedResult = "F" )]//F
        public string GetGradeCalc_AllGradelogicaalScenarios_GradeOutput(int a, int b)
        {

            calc.Score = a;
            calc.AttendancePercentage = b;
            //Act
           return calc.GetGrade();
        
        }
    }
}
