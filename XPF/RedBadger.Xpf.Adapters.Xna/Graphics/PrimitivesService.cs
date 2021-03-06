#region License
/* The MIT License
 *
 * Copyright (c) 2011 Red Badger Consulting
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
*/
#endregion

namespace RedBadger.Xpf.Adapters.Xna.Graphics
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using RedBadger.Xpf.Graphics;

    /// <summary>
    ///     Provides primitives that XPF requires to render correctly.
    /// </summary>
    public class PrimitivesService : IPrimitivesService
    {
        private readonly GraphicsDevice graphicsDevice;

        /// <summary>
        ///     Initializes a new instance of the <see cref = "PrimitivesService">PrimitivesService</see>.
        /// </summary>
        /// <param name = "graphicsDevice">An XNA <see cref = "GraphicsDevice">GraphicsDevice</see> that can be used to generate primitives.</param>
        public PrimitivesService(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            this.SinglePixel = new Texture2DAdapter(CreateSinglePixel(this.graphicsDevice, 1, 1, Color.White));
        }

        public ITexture SinglePixel { get; private set; }

        private static Texture2D CreateSinglePixel(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            // create the rectangle texture without colors
            var texture = new Texture2D(graphicsDevice, width, height, false, SurfaceFormat.Color);

            // Create a color array for the pixels
            var colors = new Color[width * height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = new Color(color.ToVector3());
            }

            // Set the color data for the texture
            texture.SetData(colors);

            return texture;
        }
    }
}
