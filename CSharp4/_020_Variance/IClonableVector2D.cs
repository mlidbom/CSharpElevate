namespace CSharp4._020_Variance
{
        public interface IVector2D<out TCoordinate>
        {
            TCoordinate X { get; }
            TCoordinate Y { get; }
        }

        public interface IClonableVector2D<TCoordinate, out TPointType> : IVector2D<TCoordinate> 
            where TPointType : IClonableVector2D<TCoordinate, TPointType>
        {
            TPointType CloneAt(TCoordinate x, TCoordinate y);
        }

        public interface IPixelOffset : IClonableVector2D<int, IPixelOffset>
        {
        }

    
        public static class ClonableVector2DExtensions
        {            
            public static T Scale<T>(this T me, double factor) where T : IClonableVector2D<int, T>
            {
                return me.CloneAt((int)(me.X * factor), (int)(me.Y * factor));
            }

            public static T Add<T>(this T me, T movement) where T : IClonableVector2D<int, T>
            {
                return me.CloneAt(me.X + movement.X, me.Y + movement.Y);
            }

            public static T Subtract<T>(this T me, T movement) where T : IClonableVector2D<int, T>
            {
                return me.CloneAt(me.X - movement.X, me.Y - movement.Y);
            }
        }
}