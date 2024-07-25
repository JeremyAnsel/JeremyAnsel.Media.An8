// <copyright file="An8TextureTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Texture"/>.
    /// </summary>
    public class An8TextureTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewTexture()
        {
            var texture = new An8Texture();

            Assert.Null(texture.Name);
            Assert.False(texture.IsInverted);
            Assert.False(texture.IsCubeMap);
            Assert.NotNull(texture.Files);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var texture = new An8Texture();

            texture.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var texture = new An8Texture();

            texture.Parse(
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
            var texture = new An8Texture();

            texture.Parse(
@"
""abc""
");

            Assert.Equal("abc", texture.Name);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseInverted()
        {
            var texture = new An8Texture();

            texture.Parse(
@"
invert { }
");

            Assert.True(texture.IsInverted);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseCubeMap()
        {
            var texture = new An8Texture();

            texture.Parse(
@"
cubemap { }
");

            Assert.True(texture.IsCubeMap);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFile()
        {
            var texture = new An8Texture();

            texture.Parse(
@"
file { ""abc"" }
");

            Assert.Single(texture.Files);
            Assert.Equal("abc", texture.Files[0]);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var texture = new An8Texture();

            var text = texture.GenerateText();

            string expected =
@"texture { """"
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
            var texture = new An8Texture();
            texture.Name = "abc";

            var text = texture.GenerateText();

            string expected =
@"texture { ""abc""
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
        public void GenerateTextInverted()
        {
            var texture = new An8Texture();
            texture.IsInverted = true;

            var text = texture.GenerateText();

            string expected =
@"texture { """"
  invert { }
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
        public void GenerateTextCubeMap()
        {
            var texture = new An8Texture();
            texture.IsCubeMap = true;

            var text = texture.GenerateText();

            string expected =
@"texture { """"
  cubemap { }
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
        public void GenerateTextFile()
        {
            var texture = new An8Texture();
            texture.Files.Add("abc");

            var text = texture.GenerateText();

            string expected =
@"texture { """"
  file { ""abc"" }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
