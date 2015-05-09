// <copyright file="An8MeshTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Mesh"/>.
    /// </summary>
    public class An8MeshTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewMesh()
        {
            var mesh = new An8Mesh();

            Assert.Null(mesh.Material);
            Assert.Equal(0.0f, mesh.SmoothAngleThreshold);
            Assert.NotNull(mesh.MaterialList);
            Assert.NotNull(mesh.Points);
            Assert.NotNull(mesh.Normals);
            Assert.NotNull(mesh.Edges);
            Assert.NotNull(mesh.TexCoords);
            Assert.NotNull(mesh.Faces);
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
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
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
            var mesh = new An8Mesh();

            mesh.Parse(
@"
material { }
");

            Assert.NotNull(mesh.Material);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseSmoothAngle()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
smoothangle { 2.0 }
");

            Assert.Equal(2.0f, mesh.SmoothAngleThreshold);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterialListClear()
        {
            var mesh = new An8Mesh();
            mesh.MaterialList.Add(null);

            mesh.Parse(
@"
materiallist {
}
");

            Assert.Equal(0, mesh.MaterialList.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterialList()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
materiallist {
  materialname { ""abc"" }
}
");

            Assert.Equal(1, mesh.MaterialList.Count);
            Assert.Equal("abc", mesh.MaterialList[0]);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParsePointsClear()
        {
            var mesh = new An8Mesh();
            mesh.Points.Add(new An8Point());

            mesh.Parse(
@"
points {
}
");

            Assert.Equal(0, mesh.Points.Count);
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
points {
  ( 2.0 3.0 4.0 )
}
");

            Assert.Equal(1, mesh.Points.Count);
            Assert.Equal(2.0f, mesh.Points[0].X);
            Assert.Equal(3.0f, mesh.Points[0].Y);
            Assert.Equal(4.0f, mesh.Points[0].Z);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNormalsClear()
        {
            var mesh = new An8Mesh();
            mesh.Normals.Add(new An8Point());

            mesh.Parse(
@"
normals {
}
");

            Assert.Equal(0, mesh.Normals.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNormals()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
normals {
  ( 2.0 3.0 4.0 )
}
");

            Assert.Equal(1, mesh.Normals.Count);
            Assert.Equal(2.0f, mesh.Normals[0].X);
            Assert.Equal(3.0f, mesh.Normals[0].Y);
            Assert.Equal(4.0f, mesh.Normals[0].Z);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEdgesClear()
        {
            var mesh = new An8Mesh();
            mesh.Edges.Add(new An8Edge());

            mesh.Parse(
@"
edges {
}
");

            Assert.Equal(0, mesh.Edges.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEdges()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
edges {
  ( 2 3 )
  ( 4 5 6 )
}
");

            Assert.Equal(2, mesh.Edges.Count);
            Assert.Equal(2, mesh.Edges[0].Index1);
            Assert.Equal(3, mesh.Edges[0].Index2);
            Assert.False(mesh.Edges[0].Sharpness.HasValue);
            Assert.Equal(4, mesh.Edges[1].Index1);
            Assert.Equal(5, mesh.Edges[1].Index2);
            Assert.True(mesh.Edges[1].Sharpness.HasValue);
            Assert.Equal(6, mesh.Edges[1].Sharpness.Value);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTexCoordsClear()
        {
            var mesh = new An8Mesh();
            mesh.TexCoords.Add(new An8TexCoord());

            mesh.Parse(
@"
texcoords {
}
");

            Assert.Equal(0, mesh.TexCoords.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTexCoords()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
texcoords {
  ( 2.0 3.0 )
}
");

            Assert.Equal(1, mesh.TexCoords.Count);
            Assert.Equal(2.0f, mesh.TexCoords[0].U);
            Assert.Equal(3.0f, mesh.TexCoords[0].V);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFacesClear()
        {
            var mesh = new An8Mesh();
            mesh.Faces.Add(new An8Face());

            mesh.Parse(
@"
faces {
}
");

            Assert.Equal(0, mesh.Faces.Count);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFaces()
        {
            var mesh = new An8Mesh();

            mesh.Parse(
@"
faces {
  0 0 0 0 ( )
}
");

            Assert.Equal(1, mesh.Faces.Count);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var mesh = new An8Mesh();

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
        public void GenerateTextMaterial()
        {
            var mesh = new An8Mesh();
            mesh.Material = new An8Material();

            var text = mesh.GenerateText();

            string expected =
@"mesh {
  name { """" }
  material { """"
  }
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
        public void GenerateTextSmoothAngle()
        {
            var mesh = new An8Mesh();
            mesh.SmoothAngleThreshold = 2.0f;

            var text = mesh.GenerateText();

            string expected =
@"mesh {
  name { """" }
  smoothangle { 2.000000 }
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
        public void GenerateTextMaterialList()
        {
            var mesh = new An8Mesh();
            mesh.MaterialList.Add("abc");

            var text = mesh.GenerateText();

            string expected =
@"mesh {
  name { """" }
  smoothangle { 0.000000 }
  materiallist {
    materialname { ""abc"" }
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
            var mesh = new An8Mesh();
            mesh.Points.Add(new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f });

            var text = mesh.GenerateText();

            string expected =
@"mesh {
  name { """" }
  smoothangle { 0.000000 }
  materiallist {
  }
  points {
    ( 2.000000 3.000000 4.000000 )
  }
  normals {
  }
  edges {
  }
  texcoords {
  }
  faces {
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
        public void GenerateTextNormals()
        {
            var mesh = new An8Mesh();
            mesh.Normals.Add(new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f });

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
    ( 2.000000 3.000000 4.000000 )
  }
  edges {
  }
  texcoords {
  }
  faces {
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
        public void GenerateTextEdges()
        {
            var mesh = new An8Mesh();
            mesh.Edges.Add(new An8Edge { Index1 = 2, Index2 = 3 });
            mesh.Edges.Add(new An8Edge { Index1 = 4, Index2 = 5, Sharpness = 6 });

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
    ( 2 3 )
    ( 4 5 6 )
  }
  texcoords {
  }
  faces {
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
        public void GenerateTextTexCoords()
        {
            var mesh = new An8Mesh();
            mesh.TexCoords.Add(new An8TexCoord { U = 2.0f, V = 3.0f });

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
    ( 2.000000 3.000000 )
  }
  faces {
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
        public void GenerateTextFaces()
        {
            var mesh = new An8Mesh();
            mesh.Faces.Add(new An8Face());

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
    }
}
