// <copyright file="An8CubeTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Cube"/>.
    /// </summary>
    public class An8CubeTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewCube()
        {
            var cube = new An8Cube();

            Assert.Null(cube.Material);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var cube = new An8Cube();

            cube.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var cube = new An8Cube();

            cube.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterial()
        {
            var cube = new An8Cube();

            cube.Parse(
@"
material { }
");

            Assert.NotNull(cube.Material);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseScale()
        {
            var cube = new An8Cube();

            cube.Parse(
@"
scale { 2.0 3.0 4.0 }
");

            Assert.Equal(2.0f, cube.ScaleX);
            Assert.Equal(3.0f, cube.ScaleY);
            Assert.Equal(4.0f, cube.ScaleZ);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDivisions()
        {
            var cube = new An8Cube();

            cube.Parse(
@"
divisions { 2 3 4 }
");

            Assert.Equal(2, cube.DivisionsX);
            Assert.Equal(3, cube.DivisionsY);
            Assert.Equal(4, cube.DivisionsZ);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var cube = new An8Cube();

            var text = cube.GenerateText();

            string expected =
@"cube {
  name { """" }
  scale { 0.000000 0.000000 0.000000 }
  divisions { 0 0 0 }
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
            var cube = new An8Cube();
            cube.Material = new An8Material();

            var text = cube.GenerateText();

            string expected =
@"cube {
  name { """" }
  material { """"
  }
  scale { 0.000000 0.000000 0.000000 }
  divisions { 0 0 0 }
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
        public void GenerateTextScale()
        {
            var cube = new An8Cube();
            cube.ScaleX = 2.0f;
            cube.ScaleY = 3.0f;
            cube.ScaleZ = 4.0f;

            var text = cube.GenerateText();

            string expected =
@"cube {
  name { """" }
  scale { 2.000000 3.000000 4.000000 }
  divisions { 0 0 0 }
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
        public void GenerateTextDivisions()
        {
            var cube = new An8Cube();
            cube.DivisionsX = 2;
            cube.DivisionsY = 3;
            cube.DivisionsZ = 4;

            var text = cube.GenerateText();

            string expected =
@"cube {
  name { """" }
  scale { 0.000000 0.000000 0.000000 }
  divisions { 2 3 4 }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
