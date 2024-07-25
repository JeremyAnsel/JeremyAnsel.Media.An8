// <copyright file="An8BezierTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Bezier"/>.
    /// </summary>
    public class An8BezierTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewBezier()
        {
            var bezier = new An8Bezier();

            Assert.False(bezier.IsClosed);
            Assert.NotNull(bezier.Knots);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var bezier = new An8Bezier();

            bezier.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var bezier = new An8Bezier();

            bezier.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseClosed()
        {
            var bezier = new An8Bezier();

            bezier.Parse(
@"
closed { }
");

            Assert.True(bezier.IsClosed);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseKnot()
        {
            var bezier = new An8Bezier();

            bezier.Parse(
@"
knot { }
");

            Assert.Single(bezier.Knots);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var bezier = new An8Bezier();

            var text = bezier.GenerateText();

            string expected =
@"bezier {
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextClosed()
        {
            var bezier = new An8Bezier();
            bezier.IsClosed = true;

            var text = bezier.GenerateText();

            string expected =
@"bezier {
  closed { }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextKnot()
        {
            var bezier = new An8Bezier();
            bezier.Knots.Add(new An8Knot());

            var text = bezier.GenerateText();

            string expected =
@"bezier {
  knot {
    ( 0.000000 0.000000 0.000000 ) ( 0.000000 0.000000 0.000000 ) ( 0.000000 0.000000 0.000000 )
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
