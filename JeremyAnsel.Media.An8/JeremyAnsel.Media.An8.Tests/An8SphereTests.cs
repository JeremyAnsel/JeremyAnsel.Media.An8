// <copyright file="An8SphereTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Sphere"/>.
    /// </summary>
    public class An8SphereTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewSphere()
        {
            var sphere = new An8Sphere();

            Assert.Null(sphere.Material);
            Assert.Null(sphere.LongLatDivisions);
            Assert.False(sphere.Geodesic.HasValue);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var sphere = new An8Sphere();

            sphere.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var sphere = new An8Sphere();

            sphere.Parse(
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
            var sphere = new An8Sphere();

            sphere.Parse(
@"
material { }
");

            Assert.NotNull(sphere.Material);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseLongLatDivisions()
        {
            var sphere = new An8Sphere();
            sphere.Geodesic = 0;

            sphere.Parse(
@"
longlat { 2 3 }
");

            Assert.Null(sphere.Geodesic);
            Assert.NotNull(sphere.LongLatDivisions);
            Assert.Equal(2, sphere.LongLatDivisions.VerticalDivisions);
            Assert.Equal(3, sphere.LongLatDivisions.HorizontalDivisions);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseGeodesic()
        {
            var sphere = new An8Sphere();
            sphere.LongLatDivisions = new An8LongLat();

            sphere.Parse(
@"
geodesic { 2 }
");

            Assert.Null(sphere.LongLatDivisions);
            Assert.True(sphere.Geodesic.HasValue);
            Assert.Equal(2, sphere.Geodesic.Value);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var sphere = new An8Sphere();

            var text = sphere.GenerateText();

            string expected =
@"sphere {
  name { """" }
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
            var sphere = new An8Sphere();
            sphere.Material = new An8Material();

            var text = sphere.GenerateText();

            string expected =
@"sphere {
  name { """" }
  material { """"
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
        public void GenerateTextLongLatDivisions()
        {
            var sphere = new An8Sphere();
            sphere.LongLatDivisions = new An8LongLat
            {
                VerticalDivisions = 2,
                HorizontalDivisions = 3
            };

            var text = sphere.GenerateText();

            string expected =
@"sphere {
  name { """" }
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
        public void GenerateTextGeodesic()
        {
            var sphere = new An8Sphere();
            sphere.Geodesic = 2;

            var text = sphere.GenerateText();

            string expected =
@"sphere {
  name { """" }
  geodesic { 2 }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
