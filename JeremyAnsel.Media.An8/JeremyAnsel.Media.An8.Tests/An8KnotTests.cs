// <copyright file="An8KnotTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Knot"/>.
    /// </summary>
    public class An8KnotTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewKnot()
        {
            var knot = new An8Knot();

            Assert.Null(knot.Location);
            Assert.Null(knot.ForwardDirection);
            Assert.Null(knot.ReverseDirection);
            Assert.False(knot.SegmentsCount.HasValue);
            Assert.False(knot.IsCorner);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var knot = new An8Knot();

            knot.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var knot = new An8Knot();

            knot.Parse(
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
            var knot = new An8Knot();

            knot.Parse(
@"
( 2.0 3.0 4.0 ) ( 5.0 6.0 7.0 ) ( 8.0 9.0 10.0 )
");

            Assert.NotNull(knot.Location);
            Assert.Equal(2.0f, knot.Location.X);
            Assert.Equal(3.0f, knot.Location.Y);
            Assert.Equal(4.0f, knot.Location.Z);
            Assert.NotNull(knot.ForwardDirection);
            Assert.Equal(5.0f, knot.ForwardDirection.X);
            Assert.Equal(6.0f, knot.ForwardDirection.Y);
            Assert.Equal(7.0f, knot.ForwardDirection.Z);
            Assert.NotNull(knot.ReverseDirection);
            Assert.Equal(8.0f, knot.ReverseDirection.X);
            Assert.Equal(9.0f, knot.ReverseDirection.Y);
            Assert.Equal(10.0f, knot.ReverseDirection.Z);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNullWithSegmentsCount()
        {
            var knot = new An8Knot();

            knot.Parse(
@"
( 2.0 3.0 4.0 ) ( 5.0 6.0 7.0 ) ( 8.0 9.0 10.0 ) 11
");

            Assert.NotNull(knot.Location);
            Assert.Equal(2.0f, knot.Location.X);
            Assert.Equal(3.0f, knot.Location.Y);
            Assert.Equal(4.0f, knot.Location.Z);
            Assert.NotNull(knot.ForwardDirection);
            Assert.Equal(5.0f, knot.ForwardDirection.X);
            Assert.Equal(6.0f, knot.ForwardDirection.Y);
            Assert.Equal(7.0f, knot.ForwardDirection.Z);
            Assert.NotNull(knot.ReverseDirection);
            Assert.Equal(8.0f, knot.ReverseDirection.X);
            Assert.Equal(9.0f, knot.ReverseDirection.Y);
            Assert.Equal(10.0f, knot.ReverseDirection.Z);
            Assert.True(knot.SegmentsCount.HasValue);
            Assert.Equal(11, knot.SegmentsCount.Value);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseCorner()
        {
            var knot = new An8Knot();

            knot.Parse(
@"
corner { }
");

            Assert.True(knot.IsCorner);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var knot = new An8Knot();

            var text = knot.GenerateText();

            string expected =
@"knot {
  ( 0.000000 0.000000 0.000000 ) ( 0.000000 0.000000 0.000000 ) ( 0.000000 0.000000 0.000000 )
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
        public void GenerateTextNull()
        {
            var knot = new An8Knot();
            knot.Location = new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f };
            knot.ForwardDirection = new An8Point { X = 5.0f, Y = 6.0f, Z = 7.0f };
            knot.ReverseDirection = new An8Point { X = 8.0f, Y = 9.0f, Z = 10.0f };

            var text = knot.GenerateText();

            string expected =
@"knot {
  ( 2.000000 3.000000 4.000000 ) ( 5.000000 6.000000 7.000000 ) ( 8.000000 9.000000 10.000000 )
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
        public void GenerateTextNullWithSegmentsCount()
        {
            var knot = new An8Knot();
            knot.Location = new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f };
            knot.ForwardDirection = new An8Point { X = 5.0f, Y = 6.0f, Z = 7.0f };
            knot.ReverseDirection = new An8Point { X = 8.0f, Y = 9.0f, Z = 10.0f };
            knot.SegmentsCount = 11;

            var text = knot.GenerateText();

            string expected =
@"knot {
  ( 2.000000 3.000000 4.000000 ) ( 5.000000 6.000000 7.000000 ) ( 8.000000 9.000000 10.000000 ) 11
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
        public void GenerateTextCorner()
        {
            var knot = new An8Knot();
            knot.IsCorner = true;

            var text = knot.GenerateText();

            string expected =
@"knot {
  ( 0.000000 0.000000 0.000000 ) ( 0.000000 0.000000 0.000000 ) ( 0.000000 0.000000 0.000000 )
  corner { }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
