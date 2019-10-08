// <copyright file="An8TextureParamsTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8TextureParams"/>.
    /// </summary>
    public class An8TextureParamsTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewTextureParams()
        {
            var texParams = new An8TextureParams();

            Assert.Equal(An8BlendMode.Decal, texParams.BlendMode);
            Assert.Equal(An8AlphaMode.None, texParams.AlphaMode);
            Assert.Equal(100, texParams.Percent);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var texParams = new An8TextureParams();

            texParams.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var texParams = new An8TextureParams();

            texParams.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        /// <param name="mode">The enumeration value.</param>
        /// <param name="value">The string value</param>
        [Theory]
        [InlineData(An8BlendMode.Decal, "abc")]
        [InlineData(An8BlendMode.Decal, "decal")]
        [InlineData(An8BlendMode.Darken, "darken")]
        [InlineData(An8BlendMode.Lighten, "lighten")]
        public void ParseBlendMode(An8BlendMode mode, string value)
        {
            var texParams = new An8TextureParams();

            texParams.Parse(
@"
blendmode { " + value + @" }
");

            Assert.Equal(mode, texParams.BlendMode);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        /// <param name="mode">The enumeration value.</param>
        /// <param name="value">The string value</param>
        [Theory]
        [InlineData(An8AlphaMode.None, "abc")]
        [InlineData(An8AlphaMode.None, "none")]
        [InlineData(An8AlphaMode.Layer, "layer")]
        [InlineData(An8AlphaMode.Final, "final")]
        public void ParseAlphaMode(An8AlphaMode mode, string value)
        {
            var texParams = new An8TextureParams();

            texParams.Parse(
@"
alphamode { " + value + @" }
");

            Assert.Equal(mode, texParams.AlphaMode);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParsePercent()
        {
            var texParams = new An8TextureParams();

            texParams.Parse(
@"
percent { 2 }
");

            Assert.Equal(2, texParams.Percent);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var texParams = new An8TextureParams();

            var text = texParams.GenerateText();

            string expected =
@"textureparams {
  blendmode { decal }
  alphamode { none }
  percent { 100 }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        /// <param name="value">The string value</param>
        /// <param name="mode">The enumeration value.</param>
        [Theory]
        [InlineData("decal", An8BlendMode.Decal)]
        [InlineData("darken", An8BlendMode.Darken)]
        [InlineData("lighten", An8BlendMode.Lighten)]
        public void GenerateTextBlendMode(string value, An8BlendMode mode)
        {
            var texParams = new An8TextureParams();
            texParams.BlendMode = mode;

            var text = texParams.GenerateText();

            string expected =
@"textureparams {
  blendmode { " + value + @" }
  alphamode { none }
  percent { 100 }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        /// <param name="value">The string value</param>
        /// <param name="mode">The enumeration value.</param>
        [Theory]
        [InlineData("none", An8AlphaMode.None)]
        [InlineData("layer", An8AlphaMode.Layer)]
        [InlineData("final", An8AlphaMode.Final)]
        public void GenerateTextAlphaMode(string value, An8AlphaMode mode)
        {
            var texParams = new An8TextureParams();
            texParams.AlphaMode = mode;

            var text = texParams.GenerateText();

            string expected =
@"textureparams {
  blendmode { decal }
  alphamode { " + value + @" }
  percent { 100 }
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
        public void GenerateTextPercent()
        {
            var texParams = new An8TextureParams();
            texParams.Percent = 2;

            var text = texParams.GenerateText();

            string expected =
@"textureparams {
  blendmode { decal }
  alphamode { none }
  percent { 2 }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
