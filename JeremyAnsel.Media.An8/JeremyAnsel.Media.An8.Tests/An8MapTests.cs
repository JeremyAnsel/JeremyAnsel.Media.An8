// <copyright file="An8MapTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Map"/>.
    /// </summary>
    public class An8MapTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewMap()
        {
            var map = new An8Map();

            Assert.Null(map.Kind);
            Assert.Null(map.TextureName);
            Assert.Null(map.TextureParams);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var map = new An8Map();

            map.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var map = new An8Map();

            map.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseKind()
        {
            var map = new An8Map();

            map.Parse(
@"
kind { ""abc"" }
");

            Assert.Equal("abc", map.Kind);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTextureName()
        {
            var map = new An8Map();

            map.Parse(
@"
texturename { ""abc"" }
");

            Assert.Equal("abc", map.TextureName);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTextureParams()
        {
            var map = new An8Map();

            map.Parse(
@"
textureparams { }
");

            Assert.NotNull(map.TextureParams);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var map = new An8Map();

            var text = map.GenerateText();

            string expected =
@"map {
  kind { """" }
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
        public void GenerateTextKind()
        {
            var map = new An8Map();
            map.Kind = "abc";

            var text = map.GenerateText();

            string expected =
@"map {
  kind { ""abc"" }
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
            var map = new An8Map();
            map.TextureName = "abc";

            var text = map.GenerateText();

            string expected =
@"map {
  kind { """" }
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
            var map = new An8Map();
            map.TextureParams = new An8TextureParams();

            var text = map.GenerateText();

            string expected =
@"map {
  kind { """" }
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
