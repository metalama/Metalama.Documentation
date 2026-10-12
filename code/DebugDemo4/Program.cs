using Metalama.Documentation.QuickStart;

namespace DebugDemo4
{
    [NotifyPropertyChanged]
    internal partial class MovingVertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double DX { get; set; }
        public double DY { get; set; }
        public double Velocity => Math.Sqrt((this.DX * this.DX) + (this.DY * this.DY));
        public virtual void ApplyTime(double time)
        {
            this.X += this.DX * time;
            this.Y += this.DY * time;
        }
    }
    internal class MovingVertex3D : MovingVertex
    {
        public double Z { get; set; }
        public double DZ { get; set; }

        public override void ApplyTime(double time)
        {
            base.ApplyTime(time);
            this.Z += this.DZ * time;
        }
    }

    internal class Program
    {
        private static void Main()
        {
            var car = new MovingVertex { X = 5, Y = 3, DX = 0.1, DY = 0.3 };
            car.PropertyChanged += (_, args) => Console.WriteLine($"{args.PropertyName} has changed");
            car.ApplyTime(1.2);

            Console.WriteLine(new String('-', 10));

            var car3 = new MovingVertex3D { X = 5, Y = 3, Z = 4, DX = 0.1, DY = 0.3, DZ = 0.4 };
            car3.PropertyChanged += (_, args) => Console.WriteLine($"{args.PropertyName} has changed");
            car3.ApplyTime(1.2);
        }
    }
}