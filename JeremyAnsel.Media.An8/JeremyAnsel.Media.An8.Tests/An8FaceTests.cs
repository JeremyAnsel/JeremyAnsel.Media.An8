// <copyright file="An8FaceTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Face"/>.
    /// </summary>
    public class An8FaceTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewFace()
        {
            var face = new An8Face();

            Assert.False(face.IsBackShown);
            Assert.Equal(0, face.MaterialIndex);
            Assert.Equal(0, face.FlatNormalIndex);
            Assert.Null(face.PointIndexes);
            Assert.Null(face.NormalIndexes);
            Assert.False(face.HasNormalIndexes);
            Assert.Null(face.TexCoordIndexes);
            Assert.False(face.HasTexCoordIndexes);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
faces {
  0 0 0 0 ( )
}
");

            var face = mesh.Faces[0];
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParsePoints()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
faces {
  3 0 4 5 ( ( 10 ) ( 13 ) ( 16 ) )
}
");

            var face = mesh.Faces[0];

            Assert.False(face.IsBackShown);
            Assert.False(face.HasNormalIndexes);
            Assert.False(face.HasTexCoordIndexes);
            Assert.Equal(3, face.PointIndexes.Length);
            Assert.Null(face.NormalIndexes);
            Assert.Null(face.TexCoordIndexes);
            Assert.Equal(4, face.MaterialIndex);
            Assert.Equal(5, face.FlatNormalIndex);
            Assert.Equal(10, face.PointIndexes[0]);
            Assert.Equal(13, face.PointIndexes[1]);
            Assert.Equal(16, face.PointIndexes[2]);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNormalsAndTexCoords()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
faces {
  3 7 4 5 ( ( 10 11 12 ) ( 13 14 15 ) ( 16 17 18 ) )
}
");

            var face = mesh.Faces[0];

            Assert.True(face.IsBackShown);
            Assert.True(face.HasNormalIndexes);
            Assert.True(face.HasTexCoordIndexes);
            Assert.Equal(3, face.PointIndexes.Length);
            Assert.Equal(3, face.NormalIndexes.Length);
            Assert.Equal(3, face.TexCoordIndexes.Length);
            Assert.Equal(4, face.MaterialIndex);
            Assert.Equal(5, face.FlatNormalIndex);
            Assert.Equal(10, face.PointIndexes[0]);
            Assert.Equal(11, face.NormalIndexes[0]);
            Assert.Equal(12, face.TexCoordIndexes[0]);
            Assert.Equal(13, face.PointIndexes[1]);
            Assert.Equal(14, face.NormalIndexes[1]);
            Assert.Equal(15, face.TexCoordIndexes[1]);
            Assert.Equal(16, face.PointIndexes[2]);
            Assert.Equal(17, face.NormalIndexes[2]);
            Assert.Equal(18, face.TexCoordIndexes[2]);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var face = new An8Face();

            var mesh = new An8Mesh();
            mesh.Faces.Add(face);

            var text = mesh.GenerateText();

            string expected =
@"mesh {
  name { """" }
  smoothangle { 0.000000 }
  materiallist {
  }
  points {
  }
  normals {
  }
  edges {
  }
  texcoords {
  }
  faces {
    0 0 0 0 ( )
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
        public void GenerateTextPoints()
        {
            var face = new An8Face();
            face.MaterialIndex = 4;
            face.FlatNormalIndex = 5;
            face.PointIndexes = new int[] { 10, 13, 16 };

            var mesh = new An8Mesh();
            mesh.Faces.Add(face);

            var text = mesh.GenerateText();

            string expected =
@"mesh {
  name { """" }
  smoothangle { 0.000000 }
  materiallist {
  }
  points {
  }
  normals {
  }
  edges {
  }
  texcoords {
  }
  faces {
    3 0 4 5 ( ( 10 ) ( 13 ) ( 16 ) )
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
        public void GenerateTextNormalsAndTexCoords()
        {
            var face = new An8Face();
            face.IsBackShown = true;
            face.MaterialIndex = 4;
            face.FlatNormalIndex = 5;
            face.PointIndexes = new int[] { 10, 13, 16 };
            face.NormalIndexes = new int[] { 11, 14, 17 };
            face.TexCoordIndexes = new int[] { 12, 15, 18 };

            var mesh = new An8Mesh();
            mesh.Faces.Add(face);

            var text = mesh.GenerateText();

            string expected =
@"mesh {
  name { """" }
  smoothangle { 0.000000 }
  materiallist {
  }
  points {
  }
  normals {
  }
  edges {
  }
  texcoords {
  }
  faces {
    3 7 4 5 ( ( 10 11 12 ) ( 13 14 15 ) ( 16 17 18 ) )
  }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
