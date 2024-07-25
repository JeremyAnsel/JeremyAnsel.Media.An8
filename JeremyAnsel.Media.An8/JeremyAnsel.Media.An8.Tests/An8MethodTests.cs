// <copyright file="An8MethodTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Method"/>.
    /// </summary>
    public class An8MethodTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewMethod()
        {
            var method = new An8Method();

            Assert.Equal("modifier", method.Kind);
            Assert.Null(method.Name);
            Assert.NotNull(method.Parameters);
        }

        /// <summary>
        /// Tests the kind property.
        /// </summary>
        [Fact]
        public void PropertyKindThrows()
        {
            Assert.Throws<ArgumentOutOfRangeException>("value", () => new An8Method().Kind = null);
            Assert.Throws<ArgumentOutOfRangeException>("value", () => new An8Method().Kind = string.Empty);
            Assert.Throws<ArgumentOutOfRangeException>("value", () => new An8Method().Kind = " ");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var method = new An8Method();

            method.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var method = new An8Method();

            method.Parse(
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
            var method = new An8Method();

            method.Parse(
@"
abc ""def""
");

            Assert.Equal("abc", method.Kind);
            Assert.Equal("def", method.Name);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseParameter()
        {
            var method = new An8Method();

            method.Parse(
@"
parameter { ""abc"" 2.0 }
");

            Assert.Single(method.Parameters);
            Assert.Equal("abc", method.Parameters.ElementAt(0).Key);
            Assert.Equal(2.0f, method.Parameters.ElementAt(0).Value);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var method = new An8Method();

            var text = method.GenerateText();

            string expected =
@"method { modifier """"
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
            var method = new An8Method();
            method.Kind = "abc";
            method.Name = "def";

            var text = method.GenerateText();

            string expected =
@"method { abc ""def""
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
        public void GenerateTextParameter()
        {
            var method = new An8Method();
            method.Parameters.Add("abc", 2.0f);

            var text = method.GenerateText();

            string expected =
@"method { modifier """"
  parameter { ""abc"" 2.000000 }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
