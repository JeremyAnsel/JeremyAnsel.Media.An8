// <copyright file="An8HeaderTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref=" "/>.
    /// </summary>
    public class An8HeaderTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewHeader()
        {
            var header = new An8Header();

            Assert.Null(header.Version);
            Assert.Null(header.Build);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void Parse()
        {
            var header = new An8Header();

            header.Parse(
@"
version { ""v1"" }
build { ""b1"" }
");

            Assert.Equal("v1", header.Version);
            Assert.Equal("b1", header.Build);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var header = new An8Header();

            header.Parse(
@"
");

            Assert.Null(header.Version);
            Assert.Null(header.Build);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var header = new An8Header();

            header.Parse(
@"
other {}
");

            Assert.Null(header.Version);
            Assert.Null(header.Build);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateText()
        {
            var header = new An8Header
            {
                Version = "v1",
                Build = "b1"
            };

            var text = header.GenerateText();

            string expected =
@"header {
  version { ""v1"" }
  build { ""b1"" }
}
";

            Assert.Equal(expected, text);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var header = new An8Header();

            var text = header.GenerateText();

            string expected =
@"header {
}
";

            Assert.Equal(expected, text);
        }
    }
}
