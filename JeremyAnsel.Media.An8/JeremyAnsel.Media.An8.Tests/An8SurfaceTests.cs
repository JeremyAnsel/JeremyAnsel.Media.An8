// <copyright file="An8SurfaceTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8SurfaceTests"/>.
    /// </summary>
    public class An8SurfaceTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewSurface()
        {
            var surface = new An8Surface();

            Assert.Null(surface.Ambiant);
            Assert.Null(surface.Diffuse);
            Assert.Null(surface.Specular);
            Assert.Null(surface.Emissive);
            Assert.Equal(255, surface.Alpha);
            Assert.NotNull(surface.Maps);
            Assert.False(surface.IsAmbiantDiffuseLocked);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseAmbiant()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
ambiant { }
");

            Assert.NotNull(surface.Ambiant);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDiffuse()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
diffuse { }
");

            Assert.NotNull(surface.Diffuse);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseSpecular()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
specular { }
");

            Assert.NotNull(surface.Specular);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmissive()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
emissive { }
");

            Assert.NotNull(surface.Emissive);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseAlpha()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
alpha { 2 }
");

            Assert.Equal(2, surface.Alpha);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseBrilliance()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
brilliance { 2.0 }
");

            Assert.Equal(2.0f, surface.Brilliance);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParsePhongSize()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
phongsize { 2.0 }
");

            Assert.Equal(2.0f, surface.PhongRoughness);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMap()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
map { }
");

            Assert.Equal(1, surface.Maps.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLockAmbiantDiffuse()
        {
            var surface = new An8Surface();

            surface.Parse(
@"
lockambdiff { }
");

            Assert.True(surface.IsAmbiantDiffuseLocked);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var surface = new An8Surface();

            var text = surface.GenerateText();

            string expected =
@"surface {
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
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
        public void GenerateTextAmbiant()
        {
            var surface = new An8Surface();
            surface.Ambiant = new An8MaterialColor();

            var text = surface.GenerateText();

            string expected =
@"surface {
  ambiant {
    rgb { 255 255 255 }
    factor { 1.000000 }
    texturename { """" }
  }
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
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
        public void GenerateTextDiffuse()
        {
            var surface = new An8Surface();
            surface.Diffuse = new An8MaterialColor();

            var text = surface.GenerateText();

            string expected =
@"surface {
  diffuse {
    rgb { 255 255 255 }
    factor { 1.000000 }
    texturename { """" }
  }
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
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
        public void GenerateTextSpecular()
        {
            var surface = new An8Surface();
            surface.Specular = new An8MaterialColor();

            var text = surface.GenerateText();

            string expected =
@"surface {
  specular {
    rgb { 255 255 255 }
    factor { 1.000000 }
    texturename { """" }
  }
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
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
        public void GenerateTextEmissive()
        {
            var surface = new An8Surface();
            surface.Emissive = new An8MaterialColor();

            var text = surface.GenerateText();

            string expected =
@"surface {
  emissive {
    rgb { 255 255 255 }
    factor { 1.000000 }
    texturename { """" }
  }
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
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
        public void GenerateTextAlpha()
        {
            var surface = new An8Surface();
            surface.Alpha = 2;

            var text = surface.GenerateText();

            string expected =
@"surface {
  alpha { 2 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
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
        public void GenerateTextBrilliance()
        {
            var surface = new An8Surface();
            surface.Brilliance = 2.0f;

            var text = surface.GenerateText();

            string expected =
@"surface {
  alpha { 255 }
  brilliance { 2.000000 }
  phongsize { 0.000000 }
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
        public void GenerateTextPhongSize()
        {
            var surface = new An8Surface();
            surface.PhongRoughness = 2.0f;

            var text = surface.GenerateText();

            string expected =
@"surface {
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 2.000000 }
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
        public void GenerateTextMap()
        {
            var surface = new An8Surface();
            surface.Maps.Add(new An8Map());

            var text = surface.GenerateText();

            string expected =
@"surface {
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
  map {
    kind { """" }
    texturename { """" }
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
        public void GenerateTextLockAmbiantDiffuse()
        {
            var surface = new An8Surface();
            surface.IsAmbiantDiffuseLocked = true;

            var text = surface.GenerateText();

            string expected =
@"surface {
  alpha { 255 }
  brilliance { 0.000000 }
  phongsize { 0.000000 }
  lockambdiff { }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
