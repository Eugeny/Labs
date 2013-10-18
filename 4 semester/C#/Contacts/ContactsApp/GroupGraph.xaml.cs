using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ContactsLib.Entities;

namespace ContactsApp
{
    public partial class GroupGraph
    {
        public static DependencyProperty ContactsProperty = DependencyProperty.Register("Contacts", typeof (ContactList),
                                                                                        typeof (GroupGraph));

        private readonly DrawingVisual visual = new DrawingVisual();

        public GroupGraph()
        {
            InitializeComponent();
            AddVisualChild(visual);
            AddLogicalChild(visual);
        }

        public ContactList Contacts
        {
            get { return (ContactList) GetValue(ContactsProperty); }
            set { SetValue(ContactsProperty, value); }
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        public void Update()
        {
            const int S = 200;
            const int C = S/2;
            const int R = S/3;

            DrawingContext ctx = visual.RenderOpen();
            ctx.DrawRectangle(Brushes.White, null, new Rect(0, 0, S, S));

            double ang = 0;
            int total = Contacts.Groups.Sum(g => g.Contacts.Count);

            var type = new Typeface(new FontFamily("Segoe UI"), FontStyles.Normal, FontWeights.Normal,
                                    FontStretches.Normal);
            int idx = 0;
            Dictionary<string, int> stats = ContactList.Instance.GetGroupStats();

            foreach (string g in stats.Keys)
            {
                idx++;
                double ap = 1.0*stats[g]/total*Math.PI*2;
                var fig = new PathFigure();
                fig.Segments.Add(new LineSegment(new Point(Math.Cos(ang)*R, Math.Sin(ang)*R), true));
                fig.Segments.Add(new ArcSegment(new Point(Math.Cos(ang + ap)*R, Math.Sin(ang + ap)*R), new Size(R, R),
                                                ap, (ap > Math.PI), SweepDirection.Clockwise,
                                                true));
                var l = new List<PathFigure> {fig};

                var fill =
                    new SolidColorBrush(Color.FromRgb((byte) ((idx*50)%255), (byte) ((idx*50 + 80)%255),
                                                      (byte) ((idx*50 + 160)%255)));
                var stroke = new SolidColorBrush(Color.Multiply(fill.Color, 0.5f));
                ctx.DrawGeometry(fill, new Pen(stroke, 2),
                                 new PathGeometry(l, FillRule.Nonzero, new TranslateTransform(C, C)));
                var tPoint = new Point(C + (float) Math.Cos(ang + ap/2)*R/2, C + (float) Math.Sin(ang + ap/2)*R/2);
                ctx.DrawText(
                    new FormattedText(g, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, type, 12, stroke),
                    tPoint);
                tPoint.X++;
                tPoint.Y--;
                ctx.DrawText(
                    new FormattedText(g, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, type, 12,
                                      Brushes.White), tPoint);
                ang += ap;
            }
            ctx.Close();
        }

        protected override Visual GetVisualChild(int index)
        {
            return visual;
        }
    }
}