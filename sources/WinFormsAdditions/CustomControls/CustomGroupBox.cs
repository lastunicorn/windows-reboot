// Windows Reboot
// Copyright (C) 2009-2015 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DustInTheWind.WinFormsAdditions.CustomControls
{
    [ToolboxItem(true)]
    public partial class CustomGroupBox : GroupBox
    {
        private Font titleFont;

        [Category("Appearance")]
        public Font TitleFont
        {
            get => titleFont;
            set
            {
                titleFont = value;
                Invalidate();
            }
        }

        private Color titleColor;

        [Category("Appearance"), DefaultValue(typeof(Color), "Black")]
        public Color TitleColor
        {
            get => titleColor;
            set
            {
                titleColor = value;
                invalidPaintResources = true;
                Invalidate();
            }
        }

        private HorizontalAlignment titleAlignment;

        [Category("Appearance"), DefaultValue(typeof(HorizontalAlignment), "Left")]
        public HorizontalAlignment TitleAlignment
        {
            get => titleAlignment;
            set
            {
                titleAlignment = value;
                Invalidate();
            }
        }

        private Color borderColor;

        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ActiveBorder")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                invalidPaintResources = true;
                Invalidate();
            }
        }

        private int cornerRadius;

        [Category("Appearance"), DefaultValue(5)]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = value;
                Invalidate();
            }
        }

        private Padding titlePadding;

        [Category("Layout"), DefaultValue(typeof(Padding), "3, 0, 3, 0")]
        public Padding TitlePadding
        {
            get => titlePadding;
            set
            {
                titlePadding = value;
                Invalidate();
            }
        }

        private Padding titleMargin;

        [Category("Layout"), DefaultValue(typeof(Padding), "2, 0, 2, 0")]
        public Padding TitleMargin
        {
            get => titleMargin;
            set
            {
                titleMargin = value;
                Invalidate();
            }
        }

        public CustomGroupBox()
        {
            InitializeComponent();
            AdditionalInitialization();
        }

        public CustomGroupBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            AdditionalInitialization();
        }

        private void AdditionalInitialization()
        {
            titleFont = (Font)Font.Clone();
            titleColor = ForeColor;
            titleAlignment = HorizontalAlignment.Left;
            borderColor = SystemColors.ActiveBorder;
            cornerRadius = 5;
            titlePadding = new Padding(3, 0, 3, 0);
            titleMargin = new Padding(2, 0, 2, 0);

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        }

        private SizeF titleSize = SizeF.Empty;

        protected override void OnTextChanged(EventArgs e)
        {
            using (Graphics g = CreateGraphics())
                titleSize = g.MeasureString(Text, titleFont);

            base.OnTextChanged(e);
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                if (Text.Length != 0)
                    return base.DisplayRectangle;

                int x = cornerRadius + Padding.Left;
                int y = cornerRadius + Padding.Top;
                int width = Width - 2 * cornerRadius - Padding.Left - Padding.Right;
                int height = Height - 2 * cornerRadius - Padding.Top - Padding.Bottom;

                return new Rectangle(x, y, width, height);
            }
        }

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);

            invalidPaintResources = true;
            Invalidate();
        }

        #region Paint Resources

        private Pen borderPen;
        private Brush titleBrush;
        private volatile bool invalidPaintResources = true;

        private void CreatePaintResources()
        {
            titleBrush = new SolidBrush(titleColor);
            borderPen = new Pen(borderColor);

            invalidPaintResources = false;
        }

        private void CleanUpPaintResources()
        {
            if (titleBrush != null)
                titleBrush.Dispose();

            if (borderPen != null)
                borderPen.Dispose();

            invalidPaintResources = true;
        }

        #endregion

        #region Layout Rectangles

        private volatile bool invalidLayoutRectangles = true;

        private Rectangle cornerTopLeftRectangle = Rectangle.Empty;
        private Rectangle cornerTopRightRectangle = Rectangle.Empty;
        private Rectangle cornerBottomRightRectangle = Rectangle.Empty;
        private Rectangle cornerBottomLeftRectangle = Rectangle.Empty;

        private void CreateLayoutRectangles()
        {
            ScrollableControl s = new ScrollableControl();
        }

        #endregion

        #region Paint

        private Size Ceiling(SizeF size)
        {
            return new Size((int)Math.Ceiling(size.Width), (int)Math.Ceiling(size.Height));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (invalidPaintResources)
                CreatePaintResources();

            StringFormat titleStringFormat = new StringFormat();
            titleStringFormat.Trimming = StringTrimming.EllipsisCharacter;
            titleStringFormat.FormatFlags = StringFormatFlags.NoWrap;

            Size cornerSize = new Size(cornerRadius, cornerRadius);

            // The corners
            Rectangle cornerTopLeftRectangle = new Rectangle(new Point(0, 0), cornerSize);
            Rectangle cornerTopRightRectangle = new Rectangle(new Point(Width - cornerSize.Width - 1, 0), cornerSize);
            Rectangle cornerBottomRightRectangle = new Rectangle(new Point(Width - cornerSize.Width - 1, Height - cornerSize.Height - 1), cornerSize);
            Rectangle cornerBottomLeftRectangle = new Rectangle(new Point(0, Height - cornerSize.Height - 1), cornerSize);

            // Measure the title.
            int titleTextMaxWidth = cornerTopRightRectangle.Left - titleMargin.Right - titleMargin.Left - cornerTopLeftRectangle.Right;
            Size titleTextSize = Ceiling(g.MeasureString(Text, titleFont, titleTextMaxWidth, titleStringFormat));

            // Correct the corners position.
            int borderTopMargin = titleMargin.Top + titleTextSize.Height / 2;
            cornerTopLeftRectangle.Offset(0, borderTopMargin);
            cornerTopRightRectangle.Offset(0, borderTopMargin);

            RectangleF titleRectangle;
            //Rectangle titleTextRectangle;

            //Rectangle borderTopLeftRectangle = new Rectangle(cornerTopLeftRectangle.Left, cornerTopLeftRectangle.Top, (cornerTopRightRectangle.Right) - (cornerTopLeftRectangle.Left), cornerTopLeftRectangle.Height);
            //Rectangle borderTopRightRectangle = new Rectangle();
            //Rectangle borderRightTopRectangle;
            //Rectangle borderRightBottomRectangle;
            //Rectangle borderBottomRightRectangle;
            //Rectangle borderBottomLeftRectangle;
            //Rectangle borderLeftBottomRectangle;
            //Rectangle borderLeftTopRectangle;

            switch (titleAlignment)
            {
                case HorizontalAlignment.Center:
                    titleRectangle = new RectangleF(
                        cornerTopLeftRectangle.Right + (cornerTopRightRectangle.Left - cornerTopLeftRectangle.Right) / 2 - (titlePadding.Left + titleTextSize.Width + titlePadding.Right) / 2,
                        titleMargin.Top,
                        titleTextSize.Width + titlePadding.Left + titlePadding.Right,
                        titleTextSize.Height + titlePadding.Top + +titlePadding.Bottom);
                    break;

                default:
                case HorizontalAlignment.Left:
                    titleRectangle = new RectangleF(
                        cornerTopLeftRectangle.Right + titleMargin.Left,
                        titleMargin.Top,
                        titleTextSize.Width + titlePadding.Left + titlePadding.Right,
                        titleTextSize.Height + titlePadding.Top + +titlePadding.Bottom);
                    break;

                case HorizontalAlignment.Right:
                    titleRectangle = new RectangleF(
                        cornerTopRightRectangle.Left - titleMargin.Right - titlePadding.Right - titleTextSize.Width - titlePadding.Left,
                        titleMargin.Top,
                        titleTextSize.Width + titlePadding.Left + titlePadding.Right,
                        titleTextSize.Height + titlePadding.Top + titlePadding.Bottom);
                    break;
            }

            // Paint the corners.
            if (cornerRadius > 0)
            {
                // Top Left Corner
                g.DrawArc(borderPen, new Rectangle(cornerTopLeftRectangle.X, cornerTopLeftRectangle.Y, cornerTopLeftRectangle.Width * 2, cornerTopLeftRectangle.Height * 2), 180, 90);

                // Top Right Corner
                g.DrawArc(borderPen, new Rectangle(cornerTopRightRectangle.X - cornerTopRightRectangle.Width, cornerTopRightRectangle.Y, cornerTopRightRectangle.Width * 2, cornerTopRightRectangle.Height * 2), 270, 90);

                // Bottom Right Corner
                g.DrawArc(borderPen, new Rectangle(cornerBottomRightRectangle.X - cornerBottomRightRectangle.Width, cornerBottomRightRectangle.Y - cornerBottomRightRectangle.Height, cornerBottomRightRectangle.Width * 2, cornerBottomRightRectangle.Height * 2), 0, 90);

                // Bottom Left Corner
                g.DrawArc(borderPen, new Rectangle(cornerBottomLeftRectangle.X, cornerBottomLeftRectangle.Y - cornerBottomLeftRectangle.Height, cornerBottomLeftRectangle.Width * 2, cornerBottomLeftRectangle.Height * 2), 90, 90);
            }

            // Top line
            if (Text.Length > 0)
            {
                if (titleRectangle.Left - cornerTopLeftRectangle.Right > 0)
                {
                    g.DrawLine(borderPen, cornerTopLeftRectangle.Right, cornerTopLeftRectangle.Top, titleRectangle.Left, cornerTopRightRectangle.Top);
                }
                if (cornerTopRightRectangle.Left - titleRectangle.Right > 0)
                {
                    g.DrawLine(borderPen, titleRectangle.Right, cornerTopRightRectangle.Top, cornerTopRightRectangle.Left - 1, cornerTopRightRectangle.Top);
                }
            }
            else
            {
                g.DrawLine(borderPen, cornerTopLeftRectangle.Right, cornerTopLeftRectangle.Top, cornerTopRightRectangle.Left - 1, cornerTopRightRectangle.Top);
            }

            // Right line
            g.DrawLine(borderPen, cornerTopRightRectangle.Right, cornerTopRightRectangle.Bottom, cornerBottomRightRectangle.Right, cornerBottomRightRectangle.Top - 1);

            // Bottom line
            g.DrawLine(borderPen, cornerBottomRightRectangle.Left - 1, cornerBottomRightRectangle.Bottom, cornerBottomLeftRectangle.Right + 1, cornerBottomLeftRectangle.Bottom);

            // Left line
            g.DrawLine(borderPen, cornerBottomLeftRectangle.Left, cornerBottomLeftRectangle.Top - 1, cornerTopLeftRectangle.Left, cornerTopLeftRectangle.Bottom + 1);


            // Create the title text rectangle.
            RectangleF titleTextRectangle = new RectangleF(
                titleRectangle.X + titlePadding.Left,
                titleRectangle.Y + titlePadding.Top,
                titleRectangle.Width - titlePadding.Left - titlePadding.Right,
                titleRectangle.Height - titlePadding.Top - titlePadding.Bottom);

            // Paint the title.
            g.DrawString(Text, titleFont, titleBrush, titleTextRectangle, titleStringFormat);
        }

        #endregion
    }
}