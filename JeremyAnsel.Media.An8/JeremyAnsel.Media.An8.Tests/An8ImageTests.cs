// <copyright file="An8ImageTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Image"/>.
    /// </summary>
    public class An8ImageTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewImage()
        {
            var image = new An8Image();

            Assert.Null(image.FileName);
            Assert.Equal(0, image.Width);
            Assert.Equal(0, image.Height);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var image = new An8Image();

            image.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var image = new An8Image();

            image.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFileName()
        {
            var image = new An8Image();

            image.Parse(
@"
""abc""
");

            Assert.Equal("abc", image.FileName);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseSize()
        {
            var image = new An8Image();

            image.Parse(
@"
size { 2 3 }
");

            Assert.Equal(2, image.Width);
            Assert.Equal(3, image.Height);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var image = new An8Image();

            var text = image.GenerateText();

            string expected =
@"image {
  name { """" }
  """"
  size { 0 0 }
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
        public void GenerateTextFileName()
        {
            var image = new An8Image();
            image.FileName = "abc";

            var text = image.GenerateText();

            string expected =
@"image {
  name { """" }
  ""abc""
  size { 0 0 }
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
        public void GenerateTextSize()
        {
            var image = new An8Image();
            image.Width = 2;
            image.Height = 3;

            var text = image.GenerateText();

            string expected =
@"image {
  name { """" }
  """"
  size { 2 3 }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
