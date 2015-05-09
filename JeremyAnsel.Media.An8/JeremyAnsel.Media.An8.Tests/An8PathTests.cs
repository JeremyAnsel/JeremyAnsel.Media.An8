// <copyright file="An8PathTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Path"/>.
    /// </summary>
    public class An8PathTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewPath()
        {
            var path = new An8Path();

            Assert.False(path.IsExtendable);
            Assert.NotNull(path.Splines);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var path = new An8Path();

            path.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var path = new An8Path();

            path.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseExtendable()
        {
            var path = new An8Path();

            path.Parse(
@"
extendable { }
");

            Assert.True(path.IsExtendable);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseBezier()
        {
            var path = new An8Path();

            path.Parse(
@"
bezier { }
");

            Assert.Equal(1, path.Splines.Count);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var path = new An8Path();

            var text = path.GenerateText();

            string expected =
@"path {
  name { """" }
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
        public void GenerateTextExtendable()
        {
            var path = new An8Path();
            path.IsExtendable = true;

            var text = path.GenerateText();

            string expected =
@"path {
  name { """" }
  extendable { }
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
        public void GenerateTextBezier()
        {
            var path = new An8Path();
            path.Splines.Add(new An8Bezier());

            var text = path.GenerateText();

            string expected =
@"path {
  name { """" }
  bezier {
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
