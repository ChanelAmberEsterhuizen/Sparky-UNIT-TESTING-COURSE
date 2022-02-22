
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{ 

    public class GradingCalculatorXUnitTests
    {
        private GradingCalculator calc;
        
        public GradingCalculatorXUnitTests() 
        {
            calc = new GradingCalculator();
        }

        [Fact]
        public void Gradecalc_InputScore95Attendance90_getAGrade()
        {
            calc.Score = 95;
            calc.AttendancePercentage = 90;
            string result = calc.GetGrade();
            Assert.Equal("A", result); 
        }

        [Theory]
        [InlineData(95, 55)]//F
        [InlineData(65, 55)]//F
        [InlineData(50, 90)]//F
        public void GetGradeCalc_FScenarios_GetFGrade(int a , int b )
        {

            calc.Score = a;
            calc.AttendancePercentage = b; 
            //Act
            string  result = calc.GetGrade();

            //Assert 
            Assert.Equal("F", result);
        }


        [Theory]
        [InlineData(95, 90,  "A")]//A
        [InlineData(85, 90, "B")]//
        [InlineData(65, 90,  "C")]//
        [InlineData(95, 65,  "B")]//
        [InlineData(95, 55,  "F") ]//F
        [InlineData(65, 55,  "F")]//F
        [InlineData(50, 90, "F" )]//F
        public void GetGradeCalc_AllGradelogicaalScenarios_GradeOutput(int a, int b, string  expectedResult)
        {

            calc.Score = a;
            calc.AttendancePercentage = b;
            var result = calc.GetGrade();
            Assert.Equal(expectedResult, result); 
         
        }
    }
}
