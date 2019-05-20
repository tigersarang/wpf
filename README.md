# wpf
wpf study.

These days I need a HitTest program which is circle sector with empty in the middle of it and there are some rectangles.

There is a rectangle In the middle of the circle and the hit count of the circle is one.
I referenced lots of good articles and thanks for them.
 
This sample is so simple that I will explain it brifly.
Please see the source.
Thank you.
 
This is DrawImagtes, that is drawing sector circle and one rectangle.


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
        
        
And DrawArc function is as follow.


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
              .........................
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
        
        And HitTestResultCallback is as follow.
        
        
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
