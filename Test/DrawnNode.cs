﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace Altseed.Test
{
    [TestFixture]
    class DrawnNode
    {
        [Test, Apartment(ApartmentState.STA)]
        public void SpriteNode()
        {
            var tc = new TestCore();
            tc.Init();

            var texture = Texture2D.Load(@"../../Core/TestData/IO/AltseedPink.png");
            Assert.NotNull(texture);

            var node = new SpriteNode();
            node.Src = new RectF(new Vector2F(100, 100), new Vector2F(200, 200));
            node.Texture = texture;
            node.Pivot = new Vector2F(0.5f, 0.5f);
            node.AdjustSize();
            Engine.AddNode(node);

            tc.LoopBody(c =>
            {
                if (Engine.Keyboard.GetKeyState(Keys.Right) == ButtonState.Hold) node.Position += new Vector2F(1.5f, 0);
                if (Engine.Keyboard.GetKeyState(Keys.Left) == ButtonState.Hold) node.Position -= new Vector2F(1.5f, 0);
                if (Engine.Keyboard.GetKeyState(Keys.Down) == ButtonState.Hold) node.Position += new Vector2F(0, 1.5f);
                if (Engine.Keyboard.GetKeyState(Keys.Up) == ButtonState.Hold) node.Position -= new Vector2F(0, 1.5f);
                if (Engine.Keyboard.GetKeyState(Keys.B) == ButtonState.Hold) node.Scale += new Vector2F(0.01f, 0.01f);
                if (Engine.Keyboard.GetKeyState(Keys.S) == ButtonState.Hold) node.Scale -= new Vector2F(0.01f, 0.01f);
                if (Engine.Keyboard.GetKeyState(Keys.R) == ButtonState.Hold) node.Angle += 3;
                if (Engine.Keyboard.GetKeyState(Keys.L) == ButtonState.Hold) node.Angle -= 3;
                if (Engine.Keyboard.GetKeyState(Keys.X) == ButtonState.Hold) node.Src = new RectF(node.Src.X, node.Src.Y, node.Src.Width - 2.0f, node.Src.Height - 2.0f);
                if (Engine.Keyboard.GetKeyState(Keys.Z) == ButtonState.Hold) node.Src = new RectF(node.Src.X, node.Src.Y, node.Src.Width + 2.0f, node.Src.Height + 2.0f);
                if (Engine.Keyboard.GetKeyState(Keys.C) == ButtonState.Hold) node.Src = new RectF(node.Src.X - 2.0f, node.Src.Y - 2.0f, node.Src.Width, node.Src.Height);
                if (Engine.Keyboard.GetKeyState(Keys.V) == ButtonState.Hold) node.Src = new RectF(node.Src.X + 2.0f, node.Src.Y + 2.0f, node.Src.Width, node.Src.Height);
            }
            , null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void TextNode()
        {
            var tc = new TestCore();
            tc.Init();

            var font = Font.LoadDynamicFont("../../Core/TestData/Font/mplus-1m-regular.ttf", 100);
            var font2 = Font.LoadDynamicFont("../../Core/TestData/Font/GenYoMinJP-Bold.ttf", 100);
            Assert.NotNull(font);
            Assert.NotNull(font2);
            var imageFont = Font.CreateImageFont(font);
            imageFont.AddImageGlyph('〇', Texture2D.Load(@"../../Core/TestData/IO/AltseedPink.png"));

            Engine.AddNode(new TextNode() { Font = font, Text = "Hello, world! こんにちは" });
            Engine.AddNode(new TextNode() { Font = font, Text = "色を指定します。", Position = new Vector2F(0.0f, 100.0f), Color = new Color(0, 0, 255) });
            Engine.AddNode(new TextNode() { Font = font, Text = "太さを指定します。", Position = new Vector2F(0.0f, 200.0f), Weight = 1.0f });
            Engine.AddNode(new TextNode() { Font = font, Text = "太さを指定します。", Position = new Vector2F(0.0f, 200.0f), Weight = 1.0f });
            Engine.AddNode(new TextNode() { Font = font2, Text = "𠀋 𡈽 𡌛 𡑮 𡢽 𠮟 𡚴 𡸴 𣇄 𣗄 𣜿 𣝣 𣳾", Position = new Vector2F(0.0f, 300.0f) });
            Engine.AddNode(new TextNode() { Font = imageFont, Text = "Altseed〇Altseed", Position = new Vector2F(0.0f, 500.0f) });
            var rotated = new TextNode() { Font = font, Text = "太さを指定します。", Position = new Vector2F(400.0f, 400.0f) };
            Engine.AddNode(rotated);

            tc.LoopBody(c =>
            {
                rotated.Angle += 1.0f;
            }
            , null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void TextNode2()
        {
            var tc = new TestCore();
            tc.Init();

            var font = Font.LoadDynamicFont("../../Core/TestData/Font/mplus-1m-regular.ttf", 40);
            var font2 = Font.LoadDynamicFont("../../Core/TestData/Font/GenYoMinJP-Bold.ttf", 40);
            Assert.NotNull(font);
            Assert.NotNull(font2);
            var imageFont = Font.CreateImageFont(font);
            imageFont.AddImageGlyph('〇', Texture2D.Load(@"../../Core/TestData/IO/AltseedPink.png"));

            TextNode node = new TextNode() { Font = font2, Text = "Hello, world! こんにちは カーニングあるよ！！", IsEnableKerning = false, Color = new Color(255, 0, 0, 200) };
            Engine.AddNode(node);
            Engine.AddNode(new TextNode() { Font = font2, Text = "Hello, world! こんにちは カーニングないです", Color = new Color(0, 255, 0, 200) });
            Engine.AddNode(new TextNode() { Font = font, Text = node.Size.ToString(), Position = new Vector2F(0.0f, 50.0f) });
            Engine.AddNode(new TextNode() { Font = font, Text = "字間5です。\n行間標準です。", CharacterSpace = 10, Position = new Vector2F(0.0f, 150.0f) });
            Engine.AddNode(new TextNode() { Font = font, Text = "字間10です。\n行間70です。", CharacterSpace = 50, LineGap = 200, Position = new Vector2F(0.0f, 250.0f) });
            tc.Duration = 1000;
            tc.LoopBody(c =>
            {
            }
            , null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void StaticFont()
        {
            var tc = new TestCore();
            tc.Init();

            Assert.IsTrue(Font.GenerateFontFile("../../Core/TestData/Font/mplus-1m-regular.ttf", "test.a2f", 100, "Hello, world! こんにちは"));

            var font = Font.LoadDynamicFont("../../Core/TestData/Font/mplus-1m-regular.ttf", 100);
            var font2 = Font.LoadStaticFont("test.a2f");
            Assert.NotNull(font);
            Assert.NotNull(font2);
            var imageFont = Font.CreateImageFont(font);
            imageFont.AddImageGlyph('〇', Texture2D.Load(@"../../Core/TestData/IO/AltseedPink.png"));

            Engine.AddNode(new TextNode() { Font = font, Text = "Hello, world! こんにちは" });
            Engine.AddNode(new TextNode() { Font = font2, Text = "Hello, world! こんにちは", Position = new Vector2F(0.0f, 100.0f), Color = new Color(0, 0, 255) });

            tc.LoopBody(c => { }, null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void Culling()
        {
            var tc = new TestCore();
            tc.Init();

            var font = Font.LoadDynamicFont("../../Core/TestData/Font/mplus-1m-regular.ttf", 30);
            Assert.NotNull(font);

            var texture = Texture2D.Load(@"../../Core/TestData/IO/AltseedPink.png");
            Assert.NotNull(texture);

            var text = new TextNode() { Font = font, Text = "", ZOrder = 10 };
            Engine.AddNode(text);

            var parent = new Altseed.Node();
            Engine.AddNode(parent);

            tc.LoopBody(c =>
            {
                text.Text = "Drawing Object : " + Engine.CullingSystem.DrawingRenderedCount + " FPS: " + Engine.CurrentFPS.ToString();

                var node = new SpriteNode();
                node.Src = new RectF(new Vector2F(100, 100), new Vector2F(200, 200));
                node.Texture = texture;
                node.Position = new Vector2F(200, -500);
                parent.AddChildNode(node);

                foreach (var item in parent.Children.OfType<SpriteNode>())
                {
                    item.Position += new Vector2F(0, 10);
                }

            }, null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void MassSpriteNode()
        {
            var tc = new TestCore(new Configuration() { WaitVSync = false });
            tc.Init();

            var texture = Texture2D.Load(@"../../Core/TestData/IO/AltseedPink256.png");
            Assert.NotNull(texture);

            var ws = Engine.WindowSize;
            var size = 10;
            for (var x = 0; x < ws.X / size; x++)
            {
                for (var y = 0; y < ws.Y / size; y++)
                {
                    var node = new SpriteNode();
                    node.Texture = texture;
                    node.Src = new RectF(new Vector2F(128 * (x % 2), 128 * (y % 2)), new Vector2F(128, 128));
                    node.Scale = new Vector2F(1, 1) * size / 128f;
                    node.Position = new Vector2F(x, y) * size;
                    Engine.AddNode(node);
                }
            }

            tc.LoopBody(null, null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void PolygonNode_SetVertexesByVector2F()
        {
            var tc = new TestCore(new Configuration() { WaitVSync = false });
            tc.Init();

            var node = new PolygonNode();
            Engine.AddNode(node);

            tc.LoopBody(c =>
            {
                var sin = MathF.Sin(MathHelper.DegreeToRadian(c)) * 50;
                var cos = MathF.Cos(MathHelper.DegreeToRadian(c)) * 50;

                node.SetVertexes(new[] {
                    new Vector2F(100 + cos, 100 - sin),
                    new Vector2F(100 - sin, 100 - cos),
                    new Vector2F(100 - cos, 100 + sin),
                    new Vector2F(100 + sin, 100 + cos),
                }, new Color(255, c % 255, 255, 255));
            }, null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void Pivot()
        {
            var tc = new TestCore();
            tc.Init();

            var font = Font.LoadDynamicFont("../../Core/TestData/Font/mplus-1m-regular.ttf", 100);
            var font2 = Font.LoadDynamicFont("../../Core/TestData/Font/GenYoMinJP-Bold.ttf", 100);
            Assert.NotNull(font);

            var rotated = new TextNode() { Font = font, Text = "中心で回転します", Position = new Vector2F(300.0f, 300.0f), Pivot = new Vector2F(0.5f, 0.5f) };
            rotated.AdjustSize();
            Engine.AddNode(rotated);

            tc.LoopBody(c =>
            {
                rotated.Angle += 1.0f;
            }
            , null);

            tc.End();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void Anchor()
        {
            var tc = new TestCore();
            tc.Init();

            var font = Font.LoadDynamicFont("../../Core/TestData/Font/mplus-1m-regular.ttf", 30);
            Assert.NotNull(font);

            var texture = Texture2D.Load(@"../../Core/TestData/IO/AltseedPink.png");
            Assert.NotNull(texture);

            Vector2F rectSize = texture.Size;
            var parent = new PolygonNode();
            parent.Position = new Vector2F(320, 240);
            //parent.Pivot = new Vector2F(0.5f, 0.5f);
            parent.SetVertexes(new[] {
                        new Vector2F(0, 0),
                        new Vector2F(rectSize.X, 0),
                        new Vector2F(rectSize.X, rectSize.Y),
                        new Vector2F(0, rectSize.Y),
                    }, new Color(255, 255, 255, 255));
            parent.AdjustSize();
            Engine.AddNode(parent);

            var child = new SpriteNode();
            child.Texture = texture;
            child.Position = new Vector2F(120, 200);
            child.Src = new RectF(new Vector2F(), texture.Size);
            child.Pivot = new Vector2F(0.5f, 0.5f);
            child.AnchorMin = new Vector2F(0.2f, 0.0f);
            child.AnchorMax = new Vector2F(0.8f, 1f);
            child.ZOrder = 10;
            child.Mode = DrawMode.Fill;
            child.AdjustSize();
            parent.AddChildNode(child);

            var childText = new TextNode();
            childText.Font = font;
            childText.Color = new Color(0, 0, 0);
            childText.Text = "あいうえお";
            childText.Pivot = new Vector2F(0.5f, 0.5f);
            childText.AnchorMin = new Vector2F(0.5f, 0.5f);
            childText.AnchorMax = new Vector2F(0.5f, 0.5f);
            childText.ZOrder = 15;
            childText.HorizontalAlignment = HorizontalAlignment.Center;
            childText.VerticalAlignment = VerticalAlignment.Center;
            childText.Size = child.Size;
            child.AddChildNode(childText);

            var text = new TextNode() { Font = font, Text = "", ZOrder = 10 };
            Engine.AddNode(text);

            tc.Duration = 10000;
            tc.LoopBody(c =>
            {
                if (Engine.Keyboard.GetKeyState(Keys.Right) == ButtonState.Hold) rectSize.X += 1.5f;
                if (Engine.Keyboard.GetKeyState(Keys.Left) == ButtonState.Hold) rectSize.X -= 1.5f;
                if (Engine.Keyboard.GetKeyState(Keys.Down) == ButtonState.Hold) rectSize.Y += 1.5f;
                if (Engine.Keyboard.GetKeyState(Keys.Up) == ButtonState.Hold) rectSize.Y -= 1.5f;

                if (Engine.Keyboard.GetKeyState(Keys.D) == ButtonState.Hold) parent.Position += new Vector2F(1.5f, 0);
                if (Engine.Keyboard.GetKeyState(Keys.A) == ButtonState.Hold) parent.Position += new Vector2F(-1.5f, 0);
                if (Engine.Keyboard.GetKeyState(Keys.S) == ButtonState.Hold) parent.Position += new Vector2F(0, 1.5f);
                if (Engine.Keyboard.GetKeyState(Keys.W) == ButtonState.Hold) parent.Position += new Vector2F(0, -1.5f);

                parent.SetVertexes(new[] {
                        new Vector2F(0, 0),
                        new Vector2F(rectSize.X, 0),
                        new Vector2F(rectSize.X, rectSize.Y),
                        new Vector2F(0, rectSize.Y),
                    }, new Color(255, 255, 255, 255));
                parent.AdjustSize();

                text.Text = child.Size.ToString();
            }, null);

            tc.End();
        }
    }
}
