/*
    Grant McDade
    Copyright(C) 2017 Grant McDade

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/

using Microsoft.Practices.Unity;
using Moq;
using ProjectEuler.Library;
using ProjectEulerTests.Properties;
using ProjectEulerWorkbench.Problems;
using System;
using System.IO;
using Xunit;

namespace ProjectEulerTests
{
    public class ProjrectEuler
    {
        private string[] _correctAnswers = {
            "", "233168","4613732","6857","906609","232792560","25164150","104743","40824", "31875000", "142913828922", // Answers 1 - 10
            "70600674","76576500","5537376230","837799","137846528820","1366","21124","1074","171","648",               // Answers 11 - 20
            "31626","871198282","4179871","2783915460","4782","983","-59231","669171001","9183","443839",               // Answers 21 - 30
            "73682","45228","100","40730","55","872187","748317","932718654","840","210",                               // Answers 31 - 40
            "7652413","162","16695334890","5482660","1533776805","5777","134043","9110846700","296962999629","997651",  // Answers 41 - 50
            "121313", "142857", "4075", "376","249", "972", "153", "26241", "107359", ""                                // Answers 51 - 60
            };

        private UnityContainer _container = null;

        private IProblem GetProblemInstance(int problemNumber)
        {
            if ( _container == null)
            {
                _container = new UnityContainer();
            }
            var typeName = $"ProjectEulerWorkbench.Problems.Problem{problemNumber:000}, ProjectEulerWorkbench";
            var type = Type.GetType(typeName, false);
            if (type != null)
            {
                _container.RegisterType(type);
                return (IProblem)_container.Resolve(type);
            }

            return null;
        }

        private void AddFileNameMock(string fileName)
        {
            var mock = new Mock<IPathProvider>();
            var settings = new Settings();
            mock.Setup(provider => provider.GetFullyQualifiedPath(fileName)).Returns(Path.Combine(settings.ResourcePath, fileName));

            _container = new UnityContainer();
            _container.RegisterInstance(mock.Object);
        }

        private void ExecuteProblem(int problemNumber)
        {
            var problem = GetProblemInstance(problemNumber);
            if ( problem !=null)
            { 
                string answer = problem.Solve();
                Assert.Equal(answer, _correctAnswers[problemNumber]);
            }

            Assert.NotNull(problem) ;
        }

        [Fact]
        public void Problem001ReturnsCorrectAnswer()
        {
            ExecuteProblem(1);
        }
        [Fact]
        public void Problem002ReturnsCorrectAnswer()
        {
            ExecuteProblem(2);
        }
        [Fact]
        public void Problem003ReturnsCorrectAnswer()
        {
            ExecuteProblem(3);
        }
        [Fact]
        public void Problem004ReturnsCorrectAnswer()
        {
            ExecuteProblem(4);
        }
        [Fact]
        public void Problem005ReturnsCorrectAnswer()
        {
            ExecuteProblem(5);
        }
        [Fact]
        public void Problem006ReturnsCorrectAnswer()
        {
            ExecuteProblem(6);
        }
        [Fact]
        public void Problem007ReturnsCorrectAnswer()
        {
            ExecuteProblem(7);
        }
        [Fact]
        public void Problem008ReturnsCorrectAnswer()
        {
            ExecuteProblem(8);
        }
        [Fact]
        public void Problem009ReturnsCorrectAnswer()
        {
            ExecuteProblem(9);
        }
        [Fact]
        public void Problem010ReturnsCorrectAnswer()
        {
            ExecuteProblem(10);
        }
        [Fact]
        public void Problem011ReturnsCorrectAnswer()
        {
            ExecuteProblem(11);
        }
        [Fact]
        public void Problem012ReturnsCorrectAnswer()
        {
            ExecuteProblem(12);
        }
        [Fact]
        public void Problem013ReturnsCorrectAnswer()
        {
            ExecuteProblem(13);
        }
        [Fact]
        public void Problem014ReturnsCorrectAnswer()
        {
            ExecuteProblem(14);
        }
        [Fact]
        public void Problem015ReturnsCorrectAnswer()
        {
            ExecuteProblem(15);
        }
        [Fact]
        public void Problem016ReturnsCorrectAnswer()
        {
            ExecuteProblem(16);
        }
        [Fact]
        public void Problem017ReturnsCorrectAnswer()
        {
            ExecuteProblem(17);
        }
        [Fact]
        public void Problem018ReturnsCorrectAnswer()
        {
            AddFileNameMock("triangle.txt");
            ExecuteProblem(18);
        }
        [Fact]
        public void Problem019ReturnsCorrectAnswer()
        {
            ExecuteProblem(19);
        }
        [Fact]
        public void Problem020ReturnsCorrectAnswer()
        {
            ExecuteProblem(20);
        }
        [Fact]
        public void Problem021ReturnsCorrectAnswer()
        {
            ExecuteProblem(21);
        }
        [Fact]
        public void Problem022ReturnsCorrectAnswer()
        {
            AddFileNameMock("names.txt");
            ExecuteProblem(22);
        }
        [Fact]
        public void Problem023ReturnsCorrectAnswer()
        {
            ExecuteProblem(23);
        }
        [Fact]
        public void Problem024ReturnsCorrectAnswer()
        {
            ExecuteProblem(24);
        }
        [Fact]
        public void Problem025ReturnsCorrectAnswer()
        {
            ExecuteProblem(25);
        }
        [Fact]
        public void Problem026ReturnsCorrectAnswer()
        {
            ExecuteProblem(26);
        }
        [Fact]
        public void Problem027ReturnsCorrectAnswer()
        {
            ExecuteProblem(27);
        }
        [Fact]
        public void Problem028ReturnsCorrectAnswer()
        {
            ExecuteProblem(28);
        }
        [Fact]
        public void Problem029ReturnsCorrectAnswer()
        {
            ExecuteProblem(29);
        }
        [Fact]
        public void Problem030ReturnsCorrectAnswer()
        {
            ExecuteProblem(30);
        }
        [Fact]
        public void Problem031ReturnsCorrectAnswer()
        {
            ExecuteProblem(31);
        }
        [Fact]
        public void Problem032ReturnsCorrectAnswer()
        {
            ExecuteProblem(32);
        }
        [Fact]
        public void Problem033ReturnsCorrectAnswer()
        {
            ExecuteProblem(33);
        }
        [Fact]
        public void Problem034ReturnsCorrectAnswer()
        {
            ExecuteProblem(34);
        }
        [Fact]
        public void Problem035ReturnsCorrectAnswer()
        {
            ExecuteProblem(35);
        }
        [Fact]
        public void Problem036ReturnsCorrectAnswer()
        {
            ExecuteProblem(36);
        }
        [Fact]
        public void Problem037ReturnsCorrectAnswer()
        {
            ExecuteProblem(37);
        }
        [Fact]
        public void Problem038ReturnsCorrectAnswer()
        {
            ExecuteProblem(38);
        }
        [Fact]
        public void Problem039ReturnsCorrectAnswer()
        {
            ExecuteProblem(39);
        }
        [Fact]
        public void Problem040ReturnsCorrectAnswer()
        {
            ExecuteProblem(40);
        }
        [Fact]
        public void Problem041ReturnsCorrectAnswer()
        {
            ExecuteProblem(41);
        }
        [Fact]
        public void Problem042ReturnsCorrectAnswer()
        {
            AddFileNameMock("words.txt");
            ExecuteProblem(42);
        }
        [Fact]
        public void Problem043ReturnsCorrectAnswer()
        {
            ExecuteProblem(43);
        }
        [Fact]
        public void Problem044ReturnsCorrectAnswer()
        {
            ExecuteProblem(44);
        }
        [Fact]
        public void Problem045ReturnsCorrectAnswer()
        {
            ExecuteProblem(45);
        }
        [Fact]
        public void Problem046ReturnsCorrectAnswer()
        {
            ExecuteProblem(46);
        }
        [Fact]
        public void Problem047ReturnsCorrectAnswer()
        {
            ExecuteProblem(47);
        }
        [Fact]
        public void Problem048ReturnsCorrectAnswer()
        {
            ExecuteProblem(48);
        }
        [Fact]
        public void Problem049ReturnsCorrectAnswer()
        {
            ExecuteProblem(49);
        }
        [Fact]
        public void Problem050ReturnsCorrectAnswer()
        {
            ExecuteProblem(50);
        }
        [Fact]
        public void Problem051ReturnsCorrectAnswer()
        {
            ExecuteProblem(51);
        }
        [Fact]
        public void Problem052ReturnsCorrectAnswer()
        {
            ExecuteProblem(52);
        }
        [Fact]
        public void Problem053ReturnsCorrectAnswer()
        {
            ExecuteProblem(53);
        }
        [Fact]
        public void Problem054ReturnsCorrectAnswer()
        {
            AddFileNameMock("poker.txt");
            ExecuteProblem(54);
        }
        [Fact]
        public void Problem055ReturnsCorrectAnswer()
        {
            ExecuteProblem(55);
        }
        [Fact]
        public void Problem056ReturnsCorrectAnswer()
        {
            ExecuteProblem(56);
        }
        [Fact]
        public void Problem057ReturnsCorrectAnswer()
        {
            ExecuteProblem(57);
        }
        [Fact]
        public void Problem058ReturnsCorrectAnswer()
        {
            ExecuteProblem(58);
        }
        [Fact]
        public void Problem059ReturnsCorrectAnswer()
        {
            AddFileNameMock("cipher1.txt");
            ExecuteProblem(59);
        }

    }
}
