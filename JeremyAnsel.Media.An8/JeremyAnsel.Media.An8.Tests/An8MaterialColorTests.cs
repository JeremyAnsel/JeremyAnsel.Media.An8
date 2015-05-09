// <copyright file="An8MaterialColorTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8MaterialColor"/>.
    /// </summary>
    public class An8MaterialColorTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewMap()
        {
            var color = new An8MaterialColor();

            Assert.Equal<byte>(255, color.Red);
            Assert.Equal<byte>(255, color.Green);
            Assert.Equal<byte>(255, color.Blue);
            Assert.Equal(1.0f, color.WeightingFactor);
            Assert.Null(color.TextureName);
            Assert.Null(color.TextureParams);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var color = new An8MaterialColor();

            color.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var color = new An8MaterialColor();

            color.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseColor()
        {
            var color = new An8MaterialColor();

            color.Parse(
@"
rgb { 2 3 4 }
");

            Assert.Equal<byte>(2, color.Red);
            Assert.Equal<byte>(3, color.Green);
            Assert.Equal<byte>(4, color.Blue);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFactor()
        {
            var color = new An8MaterialColor();

            color.Parse(
@"
factor { 2.0 }
");

            Assert.Equal(2.0f, color.WeightingFactor);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTextureName()
        {
            var color = new An8MaterialColor();

            color.Parse(
@"
texturename { ""abc"" }
");

            Assert.Equal("abc", color.TextureName);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTextureParams()
        {
            var color = new An8MaterialColor();

            color.Parse(
@"
textureparams { }
");

            Assert.NotNull(color.TextureParams);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var color = new An8MaterialColor();

            var text = color.GenerateText();

            string expected =
@"ambiant {
  rgb { 255 255 255 }
  factor { 1.000000 }
  texturename { """" }
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
        public void GenerateTextColor()
        {
            var color = new An8MaterialColor();
            color.Red = 2;
            color.Green = 3;
            color.Blue = 4;

            var text = color.GenerateText();

            string expected =
@"ambiant {
  rgb { 2 3 4 }
  factor { 1.000000 }
  texturename { """" }
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
        public void GenerateTextFactor()
        {
            var color = new An8MaterialColor();
            color.WeightingFactor = 2.0f;

            var text = color.GenerateText();

            string expected =
@"ambiant {
  rgb { 255 255 255 }
  factor { 2.000000 }
  texturename { """" }
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
        public void GenerateTextTextureName()
        {
            var color = new An8MaterialColor();
            color.TextureName = "abc";

            var text = color.GenerateText();

            string expected =
@"ambiant {
  rgb { 255 255 255 }
  factor { 1.000000 }
  texturename { ""abc"" }
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
        public void GenerateTextTextureParams()
        {
            var color = new An8MaterialColor();
            color.TextureParams = new An8TextureParams();

            var text = color.GenerateText();

            string expected =
@"ambiant {
  rgb { 255 255 255 }
  factor { 1.000000 }
  texturename { """" }
  textureparams {
    blendmode { decal }
    alphamode { none }
    percent { 100 }
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
