// <copyright file="An8SubdivisionTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8Subdivision"/>.
    /// </summary>
    public class An8SubdivisionTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewSubdivision()
        {
            var sub = new An8Subdivision();

            Assert.Null(sub.Material);
            Assert.Equal(0.0f, sub.SmoothAngleThreshold);
            Assert.Equal(0, sub.Divisions);
            Assert.Equal(0, sub.RenderDivisions);
            Assert.NotNull(sub.MaterialList);
            Assert.NotNull(sub.Points);
            Assert.NotNull(sub.Normals);
            Assert.NotNull(sub.Edges);
            Assert.NotNull(sub.TexCoords);
            Assert.NotNull(sub.Faces);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var sub = new An8Subdivision();

            sub.Parse(
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
            var sub = new An8Subdivision();

            sub.Parse(
@"
material { }
");

            Assert.NotNull(sub.Material);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseSmoothAngle()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
smoothangle { 2.0 }
");

            Assert.Equal(2.0f, sub.SmoothAngleThreshold);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseDivisions()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
working { 2 }
");

            Assert.Equal(2, sub.Divisions);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseRenderDivisions()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
divisions { 2 }
");

            Assert.Equal(2, sub.RenderDivisions);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterialListClear()
        {
            var sub = new An8Subdivision();
            sub.MaterialList.Add(null);

            sub.Parse(
@"
materiallist {
}
");

            Assert.Empty(sub.MaterialList);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseMaterialList()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
materiallist {
  materialname { ""abc"" }
}
");

            Assert.Single(sub.MaterialList);
            Assert.Equal("abc", sub.MaterialList[0]);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParsePointsClear()
        {
            var sub = new An8Subdivision();
            sub.Points.Add(new An8Point());

            sub.Parse(
@"
points {
}
");

            Assert.Empty(sub.Points);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParsePoints()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
points {
  ( 2.0 3.0 4.0 )
}
");

            Assert.Single(sub.Points);
            Assert.Equal(2.0f, sub.Points[0].X);
            Assert.Equal(3.0f, sub.Points[0].Y);
            Assert.Equal(4.0f, sub.Points[0].Z);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNormalsClear()
        {
            var sub = new An8Subdivision();
            sub.Normals.Add(new An8Point());

            sub.Parse(
@"
normals {
}
");

            Assert.Empty(sub.Normals);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseNormals()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
normals {
  ( 2.0 3.0 4.0 )
}
");

            Assert.Single(sub.Normals);
            Assert.Equal(2.0f, sub.Normals[0].X);
            Assert.Equal(3.0f, sub.Normals[0].Y);
            Assert.Equal(4.0f, sub.Normals[0].Z);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEdgesClear()
        {
            var sub = new An8Subdivision();
            sub.Edges.Add(new An8Edge());

            sub.Parse(
@"
edges {
}
");

            Assert.Empty(sub.Edges);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEdges()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
edges {
  ( 2 3 )
  ( 4 5 6 )
}
");

            Assert.Equal(2, sub.Edges.Count);
            Assert.Equal(2, sub.Edges[0].Index1);
            Assert.Equal(3, sub.Edges[0].Index2);
            Assert.False(sub.Edges[0].Sharpness.HasValue);
            Assert.Equal(4, sub.Edges[1].Index1);
            Assert.Equal(5, sub.Edges[1].Index2);
            Assert.True(sub.Edges[1].Sharpness.HasValue);
            Assert.Equal(6, sub.Edges[1].Sharpness!.Value);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTexCoordsClear()
        {
            var sub = new An8Subdivision();
            sub.TexCoords.Add(new An8TexCoord());

            sub.Parse(
@"
texcoords {
}
");

            Assert.Empty(sub.TexCoords);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseTexCoords()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
texcoords {
  ( 2.0 3.0 )
}
");

            Assert.Single(sub.TexCoords);
            Assert.Equal(2.0f, sub.TexCoords[0].U);
            Assert.Equal(3.0f, sub.TexCoords[0].V);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFacesClear()
        {
            var sub = new An8Subdivision();
            sub.Faces.Add(new An8Face());

            sub.Parse(
@"
faces {
}
");

            Assert.Empty(sub.Faces);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFaces()
        {
            var sub = new An8Subdivision();

            sub.Parse(
@"
faces {
  0 0 0 0 ( )
}
");

            Assert.Single(sub.Faces);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var sub = new An8Subdivision();

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
            var sub = new An8Subdivision();
            sub.Material = new An8Material();

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  material { """"
  }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
            var sub = new An8Subdivision();
            sub.SmoothAngleThreshold = 2.0f;

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 2.000000 }
  working { 0 }
  divisions { 0 }
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
        public void GenerateTextDivisions()
        {
            var sub = new An8Subdivision();
            sub.Divisions = 2;

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 2 }
  divisions { 0 }
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
        public void GenerateTextRenderDivisions()
        {
            var sub = new An8Subdivision();
            sub.RenderDivisions = 2;

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 2 }
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
            var sub = new An8Subdivision();
            sub.MaterialList.Add("abc");

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
            var sub = new An8Subdivision();
            sub.Points.Add(new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f });

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
            var sub = new An8Subdivision();
            sub.Normals.Add(new An8Point { X = 2.0f, Y = 3.0f, Z = 4.0f });

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
            var sub = new An8Subdivision();
            sub.Edges.Add(new An8Edge { Index1 = 2, Index2 = 3 });
            sub.Edges.Add(new An8Edge { Index1 = 4, Index2 = 5, Sharpness = 6 });

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
            var sub = new An8Subdivision();
            sub.TexCoords.Add(new An8TexCoord { U = 2.0f, V = 3.0f });

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
            var sub = new An8Subdivision();
            sub.Faces.Add(new An8Face());

            var text = sub.GenerateText();

            string expected =
@"subdivision {
  name { """" }
  smoothangle { 0.000000 }
  working { 0 }
  divisions { 0 }
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
