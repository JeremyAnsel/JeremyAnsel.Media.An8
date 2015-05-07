// <copyright file="An8DegreeOfFreedomTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8DegreeOfFreedom"/>.
    /// </summary>
    public class An8DegreeOfFreedomTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewDegreeOfFreedom()
        {
            var dof = new An8DegreeOfFreedom();

            Assert.Null(dof.Axis);
            Assert.False(dof.IsLocked);
            Assert.False(dof.IsUnlimited);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var dof = new An8DegreeOfFreedom();

            dof.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var dof = new An8DegreeOfFreedom();

            dof.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNull()
        {
            var dof = new An8DegreeOfFreedom();

            dof.Parse(
@"
""abc"" 2.0 3.0 4.0
");

            Assert.Equal("abc", dof.Axis);
            Assert.Equal(2.0f, dof.MinimumAngle);
            Assert.Equal(3.0f, dof.DefaultAngle);
            Assert.Equal(4.0f, dof.MaximumAngle);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLocked()
        {
            var dof = new An8DegreeOfFreedom();

            dof.Parse(
@"
locked { }
");

            Assert.True(dof.IsLocked);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseUnlimited()
        {
            var dof = new An8DegreeOfFreedom();

            dof.Parse(
@"
unlimited { }
");

            Assert.True(dof.IsUnlimited);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var dof = new An8DegreeOfFreedom();

            var text = dof.GenerateText();

            string expected =
@"dof { """" 0.000000 0.000000 0.000000 }
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextNull()
        {
            var dof = new An8DegreeOfFreedom();
            dof.Axis = "abc";
            dof.MinimumAngle = 2.0f;
            dof.DefaultAngle = 3.0f;
            dof.MaximumAngle = 4.0f;

            var text = dof.GenerateText();

            string expected =
@"dof { ""abc"" 2.000000 3.000000 4.000000 }
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextLocked()
        {
            var dof = new An8DegreeOfFreedom();
            dof.IsLocked = true;

            var text = dof.GenerateText();

            string expected =
@"dof { """" 0.000000 0.000000 0.000000 locked { } }
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextUnlimited()
        {
            var dof = new An8DegreeOfFreedom();
            dof.IsUnlimited = true;

            var text = dof.GenerateText();

            string expected =
@"dof { """" 0.000000 0.000000 0.000000 unlimited { } }
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
