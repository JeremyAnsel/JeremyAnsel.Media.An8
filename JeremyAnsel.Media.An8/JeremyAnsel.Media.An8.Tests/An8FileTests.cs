// <copyright file="An8FileTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8File"/>.
    /// </summary>
    public class An8FileTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewFile()
        {
            var file = new An8File();

            Assert.NotNull(file.Header);
            Assert.Equal("0.85beta", file.Header.Version);
            Assert.Equal("2003.09.21", file.Header.Build);
            Assert.Null(file.Description);
            Assert.NotNull(file.Environment);
            Assert.Equal(30, file.Environment.Framerate);
            Assert.True(file.Environment.IsPlaybackFramerateLimited);
            Assert.True(file.Environment.IsAutoGridEnabled);
            Assert.Equal(10.0f, file.Environment.ModelingGridSpacing);
            Assert.Equal(50.0f, file.Environment.SceneEditorGridSpacing);
            Assert.Equal(50.0f, file.Environment.GroundFloorGridSize);
            Assert.NotNull(file.Textures);
            Assert.NotNull(file.Materials);
            Assert.NotNull(file.Objects);
            Assert.NotNull(file.Figures);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var file = new An8File();

            file.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var file = new An8File();

            file.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseHeader()
        {
            var file = new An8File();

            file.Parse(
@"
header {
  version { ""abc"" }
}
");

            Assert.Equal("abc", file.Header.Version);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDescription()
        {
            var file = new An8File();

            file.Parse(
@"
description { ""abc"" }
");

            Assert.Equal("abc", file.Description);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEnvironment()
        {
            var file = new An8File();

            file.Parse(
@"
environment {
  framerate { 2 }
}
");

            Assert.Equal(2, file.Environment.Framerate);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTexture()
        {
            var file = new An8File();

            file.Parse(
@"
texture { }
");

            Assert.Single(file.Textures);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterial()
        {
            var file = new An8File();

            file.Parse(
@"
material { }
");

            Assert.Single(file.Materials);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseObject()
        {
            var file = new An8File();

            file.Parse(
@"
object { }
");

            Assert.Single(file.Objects);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFigure()
        {
            var file = new An8File();

            file.Parse(
@"
figure { }
");

            Assert.Single(file.Figures);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var file = new An8File();

            var text = file.GenerateText();

            string expected =
@"header {
  version { ""0.85beta"" }
  build { ""2003.09.21"" }
}
environment {
  grid { 1 10.000000 50.000000 50.000000 }
  framerate { 30 }
  limitplayback { }
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
        public void GenerateTextDescription()
        {
            var file = new An8File();
            file.Description = "abc";

            var text = file.GenerateText();

            string expected =
@"header {
  version { ""0.85beta"" }
  build { ""2003.09.21"" }
}
description { ""abc"" }
environment {
  grid { 1 10.000000 50.000000 50.000000 }
  framerate { 30 }
  limitplayback { }
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
        public void GenerateTextTexture()
        {
            var file = new An8File();
            file.Textures.Add(new An8Texture());

            var text = file.GenerateText();

            string expected =
@"header {
  version { ""0.85beta"" }
  build { ""2003.09.21"" }
}
environment {
  grid { 1 10.000000 50.000000 50.000000 }
  framerate { 30 }
  limitplayback { }
}
texture { """"
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
        public void GenerateTextMaterial()
        {
            var file = new An8File();
            file.Materials.Add(new An8Material());

            var text = file.GenerateText();

            string expected =
@"header {
  version { ""0.85beta"" }
  build { ""2003.09.21"" }
}
environment {
  grid { 1 10.000000 50.000000 50.000000 }
  framerate { 30 }
  limitplayback { }
}
material { """"
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
        public void GenerateTextObject()
        {
            var file = new An8File();
            file.Objects.Add(new An8Object());

            var text = file.GenerateText();

            string expected =
@"header {
  version { ""0.85beta"" }
  build { ""2003.09.21"" }
}
environment {
  grid { 1 10.000000 50.000000 50.000000 }
  framerate { 30 }
  limitplayback { }
}
object { """"
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
        public void GenerateTextFigure()
        {
            var file = new An8File();
            file.Figures.Add(new An8Figure());

            var text = file.GenerateText();

            string expected =
@"header {
  version { ""0.85beta"" }
  build { ""2003.09.21"" }
}
environment {
  grid { 1 10.000000 50.000000 50.000000 }
  framerate { 30 }
  limitplayback { }
}
figure { """"
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
