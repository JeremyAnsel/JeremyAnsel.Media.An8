// <copyright file="An8TextComTests.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>
// <license>
// Licensed under the MIT license. See LICENSE.txt
// </license>

namespace JeremyAnsel.Media.An8.Tests
{
    using Xunit;

    /// <summary>
    /// Tests for <see cref="An8TextCom"/>.
    /// </summary>
    public class An8TextComTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Fact]
        public void NewTextCom()
        {
            var textCom = new An8TextCom();

            Assert.Null(textCom.Text);
            Assert.Null(textCom.FontFamily);
            Assert.Equal(0, textCom.FontSize);
            Assert.False(textCom.IsBold);
            Assert.False(textCom.IsItalic);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseEmpty()
        {
            var textCom = new An8TextCom();

            textCom.Parse(
@"
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseOther()
        {
            var textCom = new An8TextCom();

            textCom.Parse(
@"
other { }
");
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseText()
        {
            var textCom = new An8TextCom();

            textCom.Parse(
@"
string { L""abc"" }
");

            Assert.Equal("abc", textCom.Text);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFontFamily()
        {
            var textCom = new An8TextCom();

            textCom.Parse(
@"
typeface { ""abc"" }
");

            Assert.Equal("abc", textCom.FontFamily);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseFontSize()
        {
            var textCom = new An8TextCom();

            textCom.Parse(
@"
size { 2 }
");

            Assert.Equal(2, textCom.FontSize);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseBold()
        {
            var textCom = new An8TextCom();

            textCom.Parse(
@"
bold { }
");

            Assert.True(textCom.IsBold);
        }

        /// <summary>
        /// Tests parsing.
        /// </summary>
        [Fact]
        public void ParseItalic()
        {
            var textCom = new An8TextCom();

            textCom.Parse(
@"
italic { }
");

            Assert.True(textCom.IsItalic);
        }

        /// <summary>
        /// Tests text generating.
        /// </summary>
        [Fact]
        public void GenerateTextEmpty()
        {
            var textCom = new An8TextCom();

            var text = textCom.GenerateText();

            string expected =
@"textcom {
  name { """" }
  string { L"""" }
  typeface { """" }
  size { 0 }
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
        public void GenerateTextText()
        {
            var textCom = new An8TextCom();
            textCom.Text = "abc";

            var text = textCom.GenerateText();

            string expected =
@"textcom {
  name { """" }
  string { L""abc"" }
  typeface { """" }
  size { 0 }
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
        public void GenerateTextFontFamily()
        {
            var textCom = new An8TextCom();
            textCom.FontFamily = "abc";

            var text = textCom.GenerateText();

            string expected =
@"textcom {
  name { """" }
  string { L"""" }
  typeface { ""abc"" }
  size { 0 }
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
        public void GenerateTextFontSize()
        {
            var textCom = new An8TextCom();
            textCom.FontSize = 2;

            var text = textCom.GenerateText();

            string expected =
@"textcom {
  name { """" }
  string { L"""" }
  typeface { """" }
  size { 2 }
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
        public void GenerateTextBold()
        {
            var textCom = new An8TextCom();
            textCom.IsBold = true;

            var text = textCom.GenerateText();

            string expected =
@"textcom {
  name { """" }
  string { L"""" }
  typeface { """" }
  size { 0 }
  bold { }
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
        public void GenerateTextItalic()
        {
            var textCom = new An8TextCom();
            textCom.IsItalic = true;

            var text = textCom.GenerateText();

            string expected =
@"textcom {
  name { """" }
  string { L"""" }
  typeface { """" }
  size { 0 }
  italic { }
}
";

            Assert.Equal(
                expected.Replace("\r\n", "\n"),
                text.Replace("\r\n", "\n"));
        }
    }
}
