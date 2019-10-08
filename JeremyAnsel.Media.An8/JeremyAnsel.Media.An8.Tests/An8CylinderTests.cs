// <copyright file="An8CylinderTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Cylinder"/>.
    /// </summary>
    public class An8CylinderTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewCylinder()
        {
            var cylinder = new An8Cylinder();

            Assert.Null(cylinder.Material);
            Assert.Null(cylinder.LongLatDivisions);
            Assert.False(cylinder.IsStartCapped);
            Assert.False(cylinder.IsEndCapped);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
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
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
material { }
");

            Assert.NotNull(cylinder.Material);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLength()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
length { 2.0 }
");

            Assert.Equal(2.0f, cylinder.Length);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDiameter()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
diameter { 2.0 }
");

            Assert.Equal(2.0f, cylinder.Diameter);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTopDiameter()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
topdiameter { 2.0 }
");

            Assert.Equal(2.0f, cylinder.TopDiameter);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLongLatDivisions()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
longlat { 2 3 }
");

            Assert.NotNull(cylinder.LongLatDivisions);
            Assert.Equal(2, cylinder.LongLatDivisions.VerticalDivisions);
            Assert.Equal(3, cylinder.LongLatDivisions.HorizontalDivisions);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseCapStart()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
capstart { }
");

            Assert.True(cylinder.IsStartCapped);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseCapEnd()
        {
            var cylinder = new An8Cylinder();

            cylinder.Parse(
@"
capend { }
");

            Assert.True(cylinder.IsEndCapped);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var cylinder = new An8Cylinder();

            var text = cylinder.GenerateText();

            string expected =
@"cylinder {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  topdiameter { 0.000000 }
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
            var cylinder = new An8Cylinder();
            cylinder.Material = new An8Material();

            var text = cylinder.GenerateText();

            string expected =
@"cylinder {
  name { """" }
  material { """"
  }
  length { 0.000000 }
  diameter { 0.000000 }
  topdiameter { 0.000000 }
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
        public void GenerateTextLongLatDivisions()
        {
            var cylinder = new An8Cylinder();
            cylinder.LongLatDivisions = new An8LongLat
            {
                VerticalDivisions = 2,
                HorizontalDivisions = 3
            };

            var text = cylinder.GenerateText();

            string expected =
@"cylinder {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  topdiameter { 0.000000 }
  longlat { 2 3 }
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
        public void GenerateTextCapStart()
        {
            var cylinder = new An8Cylinder();
            cylinder.IsStartCapped = true;

            var text = cylinder.GenerateText();

            string expected =
@"cylinder {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  topdiameter { 0.000000 }
  capstart { }
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
        public void GenerateTextCapEnd()
        {
            var cylinder = new An8Cylinder();
            cylinder.IsEndCapped = true;

            var text = cylinder.GenerateText();

            string expected =
@"cylinder {
  name { """" }
  length { 0.000000 }
  diameter { 0.000000 }
  topdiameter { 0.000000 }
  capend { }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
