// <copyright file="An8EnvironmentTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Environment"/>.
    /// </summary>
    public class An8EnvironmentTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewEnvironment()
        {
            var env = new An8Environment();
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var env = new An8Environment();

            env.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var env = new An8Environment();

            env.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseGridAuto()
        {
            var env = new An8Environment();

            env.Parse(
@"
grid { 1 }
");

            Assert.True(env.IsAutoGridEnabled);
            Assert.Equal(0.0f, env.ModelingGridSpacing);
            Assert.Equal(0.0f, env.SceneEditorGridSpacing);
            Assert.Equal(0.0f, env.GroundFloorGridSize);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseGridAutoAndManual()
        {
            var env = new An8Environment();

            env.Parse(
@"
grid { 1 2.0 3.0 4.0 }
");

            Assert.True(env.IsAutoGridEnabled);
            Assert.Equal(2.0f, env.ModelingGridSpacing);
            Assert.Equal(3.0f, env.SceneEditorGridSpacing);
            Assert.Equal(4.0f, env.GroundFloorGridSize);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseGridManual()
        {
            var env = new An8Environment();

            env.Parse(
@"
grid { 0 2.0 3.0 4.0 }
");

            Assert.False(env.IsAutoGridEnabled);
            Assert.Equal(2.0f, env.ModelingGridSpacing);
            Assert.Equal(3.0f, env.SceneEditorGridSpacing);
            Assert.Equal(4.0f, env.GroundFloorGridSize);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFramerate()
        {
            var env = new An8Environment();

            env.Parse(
@"
framerate { 2 }
");

            Assert.Equal(2, env.Framerate);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLimitPlayback()
        {
            var env = new An8Environment();

            env.Parse(
@"
limitplayback { }
");

            Assert.True(env.IsPlaybackFramerateLimited);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var env = new An8Environment();

            var text = env.GenerateText();

            string expected =
@"environment {
  grid { 0 0.000000 0.000000 0.000000 }
  framerate { 0 }
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
        public void GenerateTextGridAuto()
        {
            var env = new An8Environment();
            env.IsAutoGridEnabled = true;

            var text = env.GenerateText();

            string expected =
@"environment {
  grid { 1 0.000000 0.000000 0.000000 }
  framerate { 0 }
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
        public void GenerateTextGridManual()
        {
            var env = new An8Environment();
            env.IsAutoGridEnabled = true;
            env.ModelingGridSpacing = 2.0f;
            env.SceneEditorGridSpacing = 3.0f;
            env.GroundFloorGridSize = 4.0f;

            var text = env.GenerateText();

            string expected =
@"environment {
  grid { 1 2.000000 3.000000 4.000000 }
  framerate { 0 }
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
        public void GenerateFramerate()
        {
            var env = new An8Environment();
            env.Framerate = 2;

            var text = env.GenerateText();

            string expected =
@"environment {
  grid { 0 0.000000 0.000000 0.000000 }
  framerate { 2 }
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
        public void GenerateTextLimitPlayback()
        {
            var env = new An8Environment();
            env.IsPlaybackFramerateLimited = true;

            var text = env.GenerateText();

            string expected =
@"environment {
  grid { 0 0.000000 0.000000 0.000000 }
  framerate { 0 }
  limitplayback { }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
