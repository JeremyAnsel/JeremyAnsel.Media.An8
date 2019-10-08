// <copyright file="An8MaterialTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Material"/>.
    /// </summary>
    public class An8MaterialTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewMaterial()
        {
            var material = new An8Material();

            Assert.Null(material.Name);
            Assert.Null(material.FrontSurface);
            Assert.Null(material.BackSurface);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var material = new An8Material();

            material.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var material = new An8Material();

            material.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseName()
        {
            var material = new An8Material();

            material.Parse(
@"
""abc""
");

            Assert.Equal("abc", material.Name);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFrontSurface()
        {
            var material = new An8Material();

            material.Parse(
@"
surface { }
");

            Assert.NotNull(material.FrontSurface);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseBackSurface()
        {
            var material = new An8Material();

            material.Parse(
@"
backsurface { }
");

            Assert.NotNull(material.BackSurface);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var material = new An8Material();

            var text = material.GenerateText();

            string expected =
@"material { """"
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
        public void GenerateTextName()
        {
            var material = new An8Material();
            material.Name = "abc";

            var text = material.GenerateText();

            string expected =
@"material { ""abc""
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
        public void GenerateTextFrontSurface()
        {
            var material = new An8Material();
            material.FrontSurface = new An8Surface();

            var text = material.GenerateText();

            string expected =
@"material { """"
  surface {
    alpha { 255 }
    brilliance { 0.000000 }
    phongsize { 0.000000 }
  }
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
        public void GenerateTextBackSurface()
        {
            var material = new An8Material();
            material.BackSurface = new An8Surface();

            var text = material.GenerateText();

            string expected =
@"material { """"
  backsurface {
    alpha { 255 }
    brilliance { 0.000000 }
    phongsize { 0.000000 }
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
