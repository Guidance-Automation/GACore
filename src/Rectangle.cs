using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace GACore;

/// <summary>
/// Describes the width, height, and location of a rectangle.
/// </summary>
public struct Rectangle
{
    internal double _x;

    internal double _y;

    internal double _width;

    internal double _height;

    private static readonly Rectangle _empty = CreateEmptyRect();

    /// <summary>
    /// Gets a special value that represents a rectangle with no position or area.
    /// </summary>
    /// <returns>The empty rectangle, which has X and Y property values of System.Double.PositiveInfinity,
    /// and has Width and Height property values of System.Double.NegativeInfinity.</returns>
    public static Rectangle Empty => _empty;

    /// <summary>
    /// Gets a value that indicates whether the rectangle is the Empty rectangle.
    /// </summary>
    /// <returns>True if the rectangle is the Empty rectangle; otherwise false</returns>
    public readonly bool IsEmpty => _width < 0.0;

    /// <summary>
    /// Gets or sets the position of the top-left corner of the rectangle.
    /// </summary>
    /// <returns>The position of the top-left corner of the rectangle. The default is (0, 0).</returns>
    /// <exception cref="InvalidOperationException">Location is set on an Empty rectangle. </exception>
    public Vector128<double> Location
    {
        readonly get
        {
            return Vector128.Create(_x, _y);
        }
        set
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot Modify Empty Rectangle");
            }

            _x = value[0];
            _y = value[1];
        }
    }

    /// <summary>
    /// Gets or sets the width and height of the rectangle.
    /// </summary>
    /// <returns>A Vector128 structure that specifies the width and height of the rectangle.</returns>
    /// <exception cref="InvalidOperationException">Size is set on an Empty rectangle. </exception>
    public Vector128<double> Size
    {
        readonly get
        {
            if (IsEmpty)
            {
                return Vector128.Create(double.NegativeInfinity, double.NegativeInfinity);
            }

            return Vector128.Create(_width, _height);
        }
        set
        {
            if (value[0] == double.NegativeInfinity && value[1] == double.NegativeInfinity)
            {
                this = _empty;
                return;
            }

            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot Modify Empty Rectangle");
            }

            _width = value[0];
            _height = value[1];
        }
    }

    /// <summary>
    /// Gets or sets the x-axis value of the left side of the rectangle.
    /// </summary>
    /// <returns>The x-axis value of the left side of the rectangle.</returns>
    /// <exception cref="InvalidOperationException">X is set on an Empty rectangle. </exception>
    public double X
    {
        readonly get
        {
            return _x;
        }
        set
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot Modify Empty Rectangle");
            }

            _x = value;
        }
    }

    /// <summary>
    /// Gets or sets the y-axis value of the top of the rectangle.
    /// </summary>
    /// <returns>The y-axis value of the top of the rectangle.</returns>
    /// <exception cref="InvalidOperationException">Y is set on an Empty rectangle. </exception>
    public double Y
    {
        readonly get
        {
            return _y;
        }
        set
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot Modify Empty Rectangle");
            }

            _y = value;
        }
    }

    /// <summary>
    /// Gets or sets the width of the rectangle.
    /// </summary>
    /// <returns>A positive number that represents the width of the rectangle. The default is 0.</returns>
    /// <exception cref="ArgumentException">Width is set to a negative value.</exception>
    /// <exception cref="InvalidOperationException">Width is set on an Empty rectangle.</exception>
    public double Width
    {
        readonly get
        {
            return _width;
        }
        set
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot Modify Empty Rectangle");
            }

            if (value < 0.0)
            {
                throw new ArgumentException("Width Cannot Be Negative");
            }

            _width = value;
        }
    }

    /// <summary>
    /// Gets or sets the height of the rectangle.
    /// </summary>
    /// <returns>A positive number that represents the height of the rectangle. The default is 0.</returns>
    /// <exception cref="ArgumentException">Height is set to a negative value.</exception>
    /// <exception cref="InvalidOperationException">Height is set on an Empty rectangle.</exception>
    public double Height
    {
        readonly get
        {
            return _height;
        }
        set
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Cannot Modify Empty Rectangle");
            }

            if (value < 0.0)
            {
                throw new ArgumentException("Height Cannot Be Negative");
            }

            _height = value;
        }
    }

    /// <summary>
    /// Gets the x-axis value of the left side of the rectangle.
    /// </summary>
    /// <returns>The x-axis value of the left side of the rectangle.</returns>
    public readonly double Left => _x;

    /// <summary>
    /// Gets the y-axis value of the top of the rectangle.
    /// </summary>
    /// <returns>The y-axis value of the top of the rectangle.</returns>
    public readonly double Top => _y;

    /// <summary>
    /// Gets the x-axis value of the right side of the rectangle.
    /// </summary>
    /// <returns>The x-axis value of the right side of the rectangle.</returns>
    public readonly double Right
    {
        get
        {
            if (IsEmpty)
            {
                return double.NegativeInfinity;
            }

            return _x + _width;
        }
    }

    /// <summary>
    /// Gets the y-axis value of the bottom of the rectangle.
    /// </summary>
    /// <returns>The y-axis value of the bottom of the rectangle. If the rectangle is empty, the value is System.Double.NegativeInfinity .</returns>
    public readonly double Bottom
    {
        get
        {
            if (IsEmpty)
            {
                return double.NegativeInfinity;
            }

            return _y + _height;
        }
    }

    /// <summary>
    /// Gets the position of the top-left corner of the rectangle.
    /// </summary>
    public readonly Vector128<double> TopLeft => Vector128.Create(Left, Top);

    /// <summary>
    /// Gets the position of the top-right corner of the rectangle.
    /// </summary>
    public readonly Vector128<double> TopRight => Vector128.Create(Right, Top);

    /// <summary>
    /// Gets the position of the bottom-left corner of the rectangle
    /// </summary>
    public readonly Vector128<double> BottomLeft => Vector128.Create(Left, Bottom);

    /// <summary>
    /// Gets the position of the bottom-right corner of the rectangle.
    /// </summary>
    public readonly Vector128<double> BottomRight => Vector128.Create(Right, Bottom);

    /// <summary>
    /// Compares two rectangles for exact equality.
    /// </summary>
    /// <param name="rect1">The first rectangle to compare.</param>
    /// <param name="rect2">The second rectangle to compare.</param>
    /// <returns>True if the rectangles have the same Location and Size values; otherwise, false.</returns>
    public static bool operator ==(Rectangle rect1, Rectangle rect2)
    {
        if (rect1.X == rect2.X && rect1.Y == rect2.Y && rect1.Width == rect2.Width)
        {
            return rect1.Height == rect2.Height;
        }

        return false;
    }

    /// <summary>
    /// Compares two rectangles for inequality.
    /// </summary>
    /// <param name="rect1">The first rectangle to compare.</param>
    /// <param name="rect2">The second rectangle to compare.</param>
    /// <returns>True if the rectangles do not have the same Location and Size values; otherwise, false.</returns>
    public static bool operator !=(Rectangle rect1, Rectangle rect2)
    {
        return !(rect1 == rect2);
    }

    /// <summary>
    /// Indicates whether the specified rectangles are equal.
    /// </summary>
    /// <param name="rect1">The first rectangle to compare.</param>
    /// <param name="rect2">The second rectangle to compare.</param>
    /// <returns>True if the rectangles have the same Location and Size values; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Equals(Rectangle rect1, Rectangle rect2)
    {
        if (rect1.IsEmpty)
        {
            return rect2.IsEmpty;
        }

        if (rect1.X.Equals(rect2.X) && rect1.Y.Equals(rect2.Y) && rect1.Width.Equals(rect2.Width))
        {
            return rect1.Height.Equals(rect2.Height);
        }

        return false;
    }

    /// <summary>
    /// Indicates whether the specified object is equal to the current rectangle.
    /// </summary>
    /// <param name="o">The object to compare to the current rectangle.</param>
    /// <returns>True if o is a Rectangle and has the same Location and Size values as the current rectangle; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly bool Equals(object? o)
    {
        if (o == null || o is not Rectangle rect)
        {
            return false;
        }

        return Equals(this, rect);
    }

    /// <summary>
    /// Indicates whether the specified rectangle is equal to the current rectangle.
    /// </summary>
    /// <param name="value">The rectangle to compare to the current rectangle.</param>
    /// <returns>True if the specified rectangle has the same Location and Size values as the current rectangle; otherwise, false.</returns>
    public readonly bool Equals(Rectangle value) => Equals(this, value);

    /// <summary>
    /// Creates a hash code for the rectangle.
    /// </summary>
    /// <returns>A hash code for the current System.Windows.Rect structure.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly int GetHashCode()
    {
        if (IsEmpty)
        {
            return 0;
        }

        return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
    }

    /// <summary>
    /// Initializes a new instance of the Rectangle structure that has the specified x-coordinate, y-coordinate, width, and height.
    /// </summary>
    /// <param name="x">The x-coordinate of the top-left corner of the rectangle.</param>
    /// <param name="y">The y-coordinate of the top-left corner of the rectangle.</param>
    /// <param name="width">The width of the rectangle.</param>
    /// <param name="height">The height of the rectangle.</param>
    /// <exception cref="ArgumentException">Width is a negative value.-or- height is a negative value.</exception>
    public Rectangle(double x, double y, double width, double height)
    {
        if (width < 0.0 || height < 0.0)
        {
            throw new ArgumentException("Width And Height Cannot Be Negative");
        }

        _x = x;
        _y = y;
        _width = width;
        _height = height;
    }

    /// <summary>
    /// When CreateMode is TwoPoints, initializes a new instance of the Rectangle structure that is exactly large enough to contain the two specified points.
    /// When CreateMode is PointAndSize initializes a new instance of the Rectangle structure that has the specified top-left corner location and the specified width and height.
    /// </summary>
    /// <param name="point1">TwoPoints : The first point that the new rectangle must contain PointAndSize : A point that specifies the location of the top-left corner of the rectangle.</param>
    /// <param name="point2">TwoPoints : The second point that the new rectangle must contain. PointAndSize : A Vector128 structure that specifies the width and height of the rectangle.</param>
    /// <param name="createMode">Switch between creating a Rectangle based on two points, or a rectangle based on the left pointand a size.</param>
    public Rectangle(Vector128<double> point1, Vector128<double> point2, CreateMode createMode)
    {
        switch(createMode)
        {
            case CreateMode.PointAndSize:
                {
                    if (point2 == Vector128.Create(double.PositiveInfinity, double.PositiveInfinity))
                    {
                        this = Empty;
                        return;
                    }

                    _x = point1[0];
                    _y = point1[1];
                    _width = point2[0];
                    _height = point2[1];
                    break;
                }
            case CreateMode.TwoPoints:
                {
                    _x = Math.Min(point1[0], point2[0]);
                    _y = Math.Min(point1[1], point2[1]);
                    _width = Math.Max(Math.Max(point1[0], point2[0]) - _x, 0.0);
                    _height = Math.Max(Math.Max(point1[1], point2[1]) - _y, 0.0);
                    break;
                }
        }
    }

    /// <summary>
    /// Initializes a new instance of the Rectangle structure that is of the 
    /// specified size and is located at (0,0).
    /// </summary>
    /// <param name="size">A Vector128 structure that specifies the width and height of the rectangle.</param>
    public Rectangle(Vector128<double> size)
    {
        if (size[0] == double.NegativeInfinity && size[1] == double.NegativeInfinity)
        {
            this = _empty;
            return;
        }

        _x = (_y = 0.0);
        _width = size[0];
        _height = size[1];
    }

    /// <summary>
    /// Indicates whether the rectangle contains the specified point.
    /// </summary>
    /// <param name="point">The point to check.</param>
    /// <returns>True if the rectangle contains the specified point; otherwise, false.</returns>
    public readonly bool Contains(Vector128<double> point) => Contains(point[0], point[1]);

    /// <summary>
    /// Indicates whether the rectangle contains the specified x-coordinate and y-coordinate.
    /// </summary>
    /// <param name="x">The x-coordinate of the point to check.</param>
    /// <param name="y">The y-coordinate of the point to check.</param>
    /// <returns>True if (x, y) is contained by the rectangle; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Contains(double x, double y)
    {
        if (IsEmpty)
        {
            return false;
        }

        return ContainsInternal(x, y);
    }

    /// <summary>
    /// Indicates whether the rectangle contains the specified rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to check.</param>
    /// <returns>True if rect is entirely contained by the rectangle; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Contains(Rectangle rect)
    {
        if (IsEmpty || rect.IsEmpty)
        {
            return false;
        }

        if (_x <= rect._x && _y <= rect._y && _x + _width >= rect._x + rect._width)
        {
            return _y + _height >= rect._y + rect._height;
        }

        return false;
    }

    /// <summary>
    /// Indicates whether the specified rectangle intersects with the current rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to check.</param>
    /// <returns>True if the specified rectangle intersects with the current rectangle; otherwise, false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool IntersectsWith(Rectangle rect)
    {
        if (IsEmpty || rect.IsEmpty)
        {
            return false;
        }

        if (rect.Left <= Right && rect.Right >= Left && rect.Top <= Bottom)
        {
            return rect.Bottom >= Top;
        }

        return false;
    }

    /// <summary>
    /// Finds the intersection of the current rectangle and the specified rectangle, and stores the result as the current rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to intersect with the current rectangle.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Intersect(Rectangle rect)
    {
        if (!IntersectsWith(rect))
        {
            this = Empty;
            return;
        }

        double num = Math.Max(Left, rect.Left);
        double num2 = Math.Max(Top, rect.Top);
        _width = Math.Max(Math.Min(Right, rect.Right) - num, 0.0);
        _height = Math.Max(Math.Min(Bottom, rect.Bottom) - num2, 0.0);
        _x = num;
        _y = num2;
    }

    /// <summary>
    /// Returns the intersection of the specified rectangles.
    /// </summary>
    /// <param name="rect1">The first rectangle to compare.</param>
    /// <param name="rect2">The second rectangle to compare.</param>
    /// <returns>The intersection of the two rectangles, or System.Windows.Rect.Empty if no intersection exists.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle Intersect(Rectangle rect1, Rectangle rect2)
    {
        rect1.Intersect(rect2);
        return rect1;
    }

    /// <summary>
    /// Expands the current rectangle exactly enough to contain the specified rectangle.
    /// </summary>
    /// <param name="rect">The rectangle to include.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Union(Rectangle rect)
    {
        if (IsEmpty)
        {
            this = rect;
        }
        else if (!rect.IsEmpty)
        {
            double num = Math.Min(Left, rect.Left);
            double num2 = Math.Min(Top, rect.Top);
            if (rect.Width == double.PositiveInfinity || Width == double.PositiveInfinity)
            {
                _width = double.PositiveInfinity;
            }
            else
            {
                double num3 = Math.Max(Right, rect.Right);
                _width = Math.Max(num3 - num, 0.0);
            }

            if (rect.Height == double.PositiveInfinity || Height == double.PositiveInfinity)
            {
                _height = double.PositiveInfinity;
            }
            else
            {
                double num4 = Math.Max(Bottom, rect.Bottom);
                _height = Math.Max(num4 - num2, 0.0);
            }

            _x = num;
            _y = num2;
        }
    }

    /// <summary>
    /// Creates a rectangle that is exactly large enough to contain the two specified rectangles.
    /// </summary>
    /// <param name="rect1">The first rectangle to include.</param>
    /// <param name="rect2">The second rectangle to include.</param>
    /// <returns>The resulting rectangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle Union(Rectangle rect1, Rectangle rect2)
    {
        rect1.Union(rect2);
        return rect1;
    }

    /// <summary>
    /// Expands the current rectangle exactly enough to contain the specified point.
    /// </summary>
    /// <param name="point">The point to include.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Union(Vector128<double> point)
    {
        Union(new Rectangle(point, point, CreateMode.TwoPoints));
    }

    /// <summary>
    /// Creates a rectangle that is exactly large enough to include the specified rectangle and the specified point.
    /// </summary>
    /// <param name="rect">The rectangle to include.</param>
    /// <param name="point">The point to include.</param>
    /// <returns>A rectangle that is exactly large enough to contain the specified rectangle and the specified point.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle Union(Rectangle rect, Vector128<double> point)
    {
        rect.Union(new Rectangle(point, point, CreateMode.TwoPoints));
        return rect;
    }

    /// <summary>
    /// Moves the rectangle by the specified vector.
    /// </summary>
    /// <param name="offsetVector">A vector that specifies the horizontal and vertical amounts to move the rectangle.</param>
    /// <returns>The resulting rectangle.</returns>
    /// <exception cref="InvalidOperationException">Rectange is Empty</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Offset(Vector128<double> offsetVector)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Cannot Call Method");
        }

        _x += offsetVector[0];
        _y += offsetVector[1];
    }

    /// <summary>
    /// Moves the rectangle by the specified horizontal and vertical amounts.
    /// </summary>
    /// <param name="offsetX">The amount to move the rectangle horizontally.</param>
    /// <param name="offsetY">The amount to move the rectangle vertically.</param>
    /// <returns>The resulting rectangle.</returns>
    /// <exception cref="InvalidOperationException">Rectange is Empty</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Offset(double offsetX, double offsetY)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Cannot Call Method");
        }

        _x += offsetX;
        _y += offsetY;
    }

    /// <summary>
    /// Returns a rectangle that is offset from the specified rectangle by using the specified vector.
    /// </summary>
    /// <param name="rect">The rectangle to move.</param>
    /// <param name="offsetVector">A vector that specifies the horizontal and vertical offsets for the new rectangle.</param>
    /// <returns>The resulting rectangle.</returns>
    /// <exception cref="InvalidOperationException">Rectange is Empty</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle Offset(Rectangle rect, Vector128<double> offsetVector)
    {
        rect.Offset(offsetVector[0], offsetVector[1]);
        return rect;
    }

    /// <summary>
    /// Returns a rectangle that is offset from the specified rectangle by using the specified horizontal and vertical amounts.
    /// </summary>
    /// <param name="rect">The rectangle to move.</param>
    /// <param name="offsetX">The horizontal offset for the new rectangle.</param>
    /// <param name="offsetY">The vertical offset for the new rectangle.</param>
    /// <returns>The resulting rectangle.</returns>
    /// <exception cref="InvalidOperationException">Rectange is Empty</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle Offset(Rectangle rect, double offsetX, double offsetY)
    {
        rect.Offset(offsetX, offsetY);
        return rect;
    }

    /// <summary>
    /// Expands the rectangle by using the specified System.Windows.Size, in all directions.
    /// </summary>
    /// <param name="size">Specifies the amount to expand the rectangle. The Vector128 structure's [0] property specifies the amount 
    /// to increase the rectangle's Left and Right properties. The Vector128 structure's [1] property specifies the amount to increase
    /// the rectangle's Top and Bottom properties.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Inflate(Vector128<double> size)
    {
        Inflate(size[0], size[1]);
    }

    /// <summary>
    /// Expands or shrinks the rectangle by using the specified width and height amounts, in all directions.
    /// </summary>
    /// <param name="width">The amount by which to expand or shrink the left and right sides of the rectangle.</param>
    /// <param name="height">The amount by which to expand or shrink the top and bottom sides of the rectangle.</param>
    /// <exception cref="InvalidOperationException">This method is called on an Empty rectangle.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Inflate(double width, double height)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Cannot Call Method");
        }

        _x -= width;
        _y -= height;
        _width += width;
        _width += width;
        _height += height;
        _height += height;
        if (!(_width >= 0.0) || !(_height >= 0.0))
        {
            this = _empty;
        }
    }

    /// <summary>
    /// Returns the rectangle that results from expanding the specified rectangle by the specified Vector128, in all directions.
    /// </summary>
    /// <param name="rect">The System.Windows.Rect structure to modify.</param>
    /// <param name="size">Specifies the amount to expand the rectangle. The Vector128 structure's [0] property specifies the amount to increase
    /// the rectangle's Left and Right properties. The Vector128 structure's [1] property specifies the amount to increase the rectangle's Top 
    /// and Bottom properties.</param>
    /// <returns>The resulting rectangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle Inflate(Rectangle rect, Vector128<double> size)
    {
        rect.Inflate(size[0], size[1]);
        return rect;
    }

    /// <summary>
    /// Creates a rectangle that results from expanding or shrinking the specified rectangle by the specified width and height amounts, in all directions.
    /// </summary>
    /// <param name="rect">The Rectangle structure to modify.</param>
    /// <param name="width">The amount by which to expand or shrink the left and right sides of the rectangle.</param>
    /// <param name="height">The amount by which to expand or shrink the top and bottom sides of the rectangle.</param>
    /// <returns>The resulting rectangle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle Inflate(Rectangle rect, double width, double height)
    {
        rect.Inflate(width, height);
        return rect;
    }

    /// <summary>
    /// Multiplies the size of the current rectangle by the specified x and y values.
    /// </summary>
    /// <param name="scaleX">The scale factor in the x-direction.</param>
    /// <param name="scaleY">The scale factor in the y-direction.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Scale(double scaleX, double scaleY)
    {
        if (!IsEmpty)
        {
            _x *= scaleX;
            _y *= scaleY;
            _width *= scaleX;
            _height *= scaleY;
            if (scaleX < 0.0)
            {
                _x += _width;
                _width *= -1.0;
            }

            if (scaleY < 0.0)
            {
                _y += _height;
                _height *= -1.0;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly bool ContainsInternal(double x, double y)
    {
        if (x >= _x && x - _width <= _x && y >= _y)
        {
            return y - _height <= _y;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Rectangle CreateEmptyRect()
    {
        Rectangle result = default;
        result._x = double.PositiveInfinity;
        result._y = double.PositiveInfinity;
        result._width = double.NegativeInfinity;
        result._height = double.NegativeInfinity;
        return result;
    }

    public readonly Vector128<double> Center() => Vector128.Create(Math.Min(Left, Right) + (Width / 2), Math.Min(Bottom, Top) + (Height / 2));
}