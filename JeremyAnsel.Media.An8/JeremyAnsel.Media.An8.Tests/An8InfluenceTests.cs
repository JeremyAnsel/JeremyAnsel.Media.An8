// <copyright file="An8InfluenceTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Influence"/>.
    /// </summary>
    public class An8InfluenceTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewInfluence()
        {
            var influence = new An8Influence();
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var influence = new An8Influence();

            influence.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var influence = new An8Influence();

            influence.Parse(
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
            var influence = new An8Influence();

            influence.Parse(
@"
2.0 3.0 4.0 5.0 6.0 7.0
");

            Assert.Equal(2.0f, influence.LowerEndCenter);
            Assert.Equal(3.0f, influence.LowerEndInnerRadius);
            Assert.Equal(4.0f, influence.LowerEndOuterRadius);
            Assert.Equal(5.0f, influence.UpperEndCenter);
            Assert.Equal(6.0f, influence.UpperEndInnerRadius);
            Assert.Equal(7.0f, influence.UpperEndOuterRadius);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var influence = new An8Influence();

            var text = influence.GenerateText();

            string expected =
@"influence { 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 }
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
            var influence = new An8Influence();
            influence.LowerEndCenter = 2.0f;
            influence.LowerEndInnerRadius = 3.0f;
            influence.LowerEndOuterRadius = 4.0f;
            influence.UpperEndCenter = 5.0f;
            influence.UpperEndInnerRadius = 6.0f;
            influence.UpperEndOuterRadius = 7.0f;

            var text = influence.GenerateText();

            string expected =
@"influence { 2.000000 3.000000 4.000000 5.000000 6.000000 7.000000 }
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
