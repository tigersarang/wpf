using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HitTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Rectangle> hitResultList = new List<Rectangle>();

        public MainWindow()
        {
            InitializeComponent();
            DrawImages();
        }

        private void DrawImages()
        {
            PathGeometry pg1 = DrawArc(40, 100, new Point(100, 100), 0);
            PathGeometry pg2 = DrawArc(40, 60, new Point(100, 100), 0);

            PathGeometry pg3 = DrawArc(40, 100, new Point(100, 100), 90);
            PathGeometry pg4 = DrawArc(40, 60, new Point(100, 100), 90);

            PathGeometry pg5 = DrawArc(40, 100, new Point(100, 100), 180);
            PathGeometry pg6 = DrawArc(40, 60, new Point(100, 100), 180);

            PathGeometry pg7 = DrawArc(40, 100, new Point(100, 100), 270);
            PathGeometry pg8 = DrawArc(40, 60, new Point(100, 100), 270);

            Rectangle rect1 = new Rectangle();
            rect1.Width = 10;
            rect1.Height = 10;
            rect1.Fill = Brushes.Red;
            Canvas.SetTop(rect1, 100);
            Canvas.SetLeft(rect1, 100);
            canvas1.Children.Add(rect1);
        }

        private HitTestResultBehavior hitTestResultCallback(HitTestResult result)
        {
            GeometryHitTestResult geometryHitTest = (GeometryHitTestResult)result;

            Rectangle visual = result.VisualHit as Rectangle;

            if (visual != null && geometryHitTest.IntersectionDetail == IntersectionDetail.FullyInside)
            {
                hitResultList.Add(visual);
            }

            return HitTestResultBehavior.Continue;
        }

        private PathGeometry DrawArc(int interval, int size, Point center, int angle)
        {
            PathFigure pf1 = new PathFigure();


            switch (angle)
            {
                case 0:
                    pf1.StartPoint = new Point(center.X, center.Y - size);

                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X + size, center.Y),
                        new Size(size, size),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Clockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            new Point(center.X + size - interval, center.Y),
                            true /* IsStroked */ ));


                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X, center.Y - size + interval),
                        new Size(size - interval, size - interval),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Counterclockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            pf1.StartPoint,
                            true /* IsStroked */ ));
                    break;
                case 90:
                    pf1.StartPoint = new Point(center.X + size, center.Y);

                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X, center.Y + size),
                        new Size(size, size),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Clockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            new Point(center.X, center.Y + size - interval),
                            true /* IsStroked */ ));


                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X + size - interval, center.Y),
                        new Size(size - interval, size - interval),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Counterclockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            pf1.StartPoint,
                            true /* IsStroked */ ));
                    break;
                case 180:
                    pf1.StartPoint = new Point(center.X, center.Y + size);

                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X - size, center.Y),
                        new Size(size, size),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Clockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            new Point(center.X - size + interval, center.Y),
                            true /* IsStroked */ ));


                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X, center.Y + size - interval),
                        new Size(size - interval, size - interval),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Counterclockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            pf1.StartPoint,
                            true /* IsStroked */ ));
                    break;
                case 270:
                    pf1.StartPoint = new Point(center.X - size, center.Y);

                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X, center.Y - size),
                        new Size(size, size),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Clockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            new Point(center.X, center.Y - size + interval),
                            true /* IsStroked */ ));


                    pf1.Segments.Add(
                    new ArcSegment(
                        new Point(center.X - size + interval, center.Y),
                        new Size(size - interval, size - interval),
                        0,
                        false, /* IsLargeArc */
                        SweepDirection.Counterclockwise,
                        true /* IsStroked */ ));

                    pf1.Segments.Add(
                        new LineSegment(
                            pf1.StartPoint,
                            true /* IsStroked */ ));
                    break;
            }


            PathGeometry pg1 = new PathGeometry();
            pg1.Figures.Add(pf1);

            Path p1 = new Path();
            p1.Stroke = Brushes.Black;
            p1.StrokeThickness = 2;
            p1.Data = pg1;

            canvas1.Children.Add(p1);
            return pg1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            GeometryGroup ellipses = new GeometryGroup();

            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(100, 100);
            myEllipseGeometry.RadiusX = 20;
            myEllipseGeometry.RadiusY = 20;

            Path p1 = new Path();
            p1.Stroke = Brushes.Black;
            p1.StrokeThickness = 2;
            p1.Data = myEllipseGeometry;

            canvas1.Children.Add(p1);

            VisualTreeHelper.HitTest(canvas1, null, new HitTestResultCallback(hitTestResultCallback),new GeometryHitTestParameters(myEllipseGeometry));
            lblHit.Content = hitResultList.Count;

        }
    }
}
