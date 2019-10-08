// <copyright file="TokenizerTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="Tokenizer"/>.
    /// </summary>
    public class TokenizerTests
    {
        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNull()
        {
            var header = new An8Header();

            header.Parse(null);
            header.Parse(string.Empty);
            header.Parse(" ");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEscapedBackslash()
        {
            var header = new An8Header();

            header.Parse(@"version { ""\\"" }");

            Assert.Equal(@"\", header.Version);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEscapedDoubleQuote()
        {
            var header = new An8Header();

            header.Parse(@"version { ""\"""" }");

            Assert.Equal(@"""", header.Version);
        }

        /// <summary>
        /// Tests building.
        /// </summary>
        [Fact]
        public void BuildEscapedBackslash()
        {
            var header = new An8Header();
            header.Version = "\\";

            var text = header.GenerateText();

            string expected =
@"header {
  version { ""\\"" }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests building.
        /// </summary>
        [Fact]
        public void BuildEscapedDoubleQuote()
        {
            var header = new An8Header();
            header.Version = "\"";

            var text = header.GenerateText();

            string expected =
@"header {
  version { ""\"""" }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
