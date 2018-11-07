using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing.Drawing2D;

namespace GK_P2_Filling
{

    public partial class Form1 : Form
    {

        public Dictionary<int, MyPoint> Points = new Dictionary<int, MyPoint>();
        int maxKey = 0;

        int iChosen;
        bool isChosen = false;
        bool isEdgeChosen = false;

        bool isBeingMoved = false;
        Point mousePos;
        int r = 10;
        
        public Form1()
        {
            InitializeComponent();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
            //OpenButton_Click(this, new EventArgs());
            // Default polygon
            //0,141,96,1,1,4,2
            //1,248,135,2,3,0,1
            //2,331,168,3,2,1,3
            //3,175,192,4,1,2,2
            //4,240,132,0,2,3,1

        }

        // TODO - might be useful

        //private void ColorButton_Click(object sender, EventArgs e)
        //{
        //    ColorDialog cd = new ColorDialog();

        //    if (cd.ShowDialog() == DialogResult.OK)
        //    {
        //        ColorPictureBox.BackColor = cd.Color;
        //        if(isChosen)
        //        {
        //            int i = Points.IndexOf(chosen);
        //            Points[i] = chosen;

        //            RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
        //        }
        //    }
        //}

        private void RedrawGraph(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            Rectangle circ = new Rectangle();
            SolidBrush brush = new SolidBrush(Color.Black);

            circ.Size = new Size(r, r);
            using (g)
            {

                int prev = iChosen == -1 ? 0 : iChosen;
                int next = Points[prev].INext;

                
                foreach (KeyValuePair<int, MyPoint> point in Points)
                {
                    if (point.Value.Loc.X < WorkspacePictureBox.Width && point.Value.Loc.Y < WorkspacePictureBox.Height)
                    {
                        if (point.Key != maxKey - 1)
                        {
                            if (isEdgeChosen && point.Key == iChosen)
                                pen.Color = Color.GreenYellow;

                            //g.DrawLine(pen, new Point(point.Value.Loc.X + r / 2, point.Value.Loc.Y + r / 2), new Point(Points[point.Value.INext].Loc.X + r / 2, Points[point.Value.INext].Loc.Y + r / 2));
                            BresenhamLine(point.Value.Loc.X + r / 2, point.Value.Loc.Y + r / 2, Points[point.Value.INext].Loc.X + r / 2, Points[point.Value.INext].Loc.Y + r / 2, pen.Color);
                        }

                        circ.Location = point.Value.Loc;
                        pen.Color = Color.Black;
                        brush.Color = Color.Black;

                        g.DrawEllipse(pen, circ);

                        g.FillEllipse(brush, circ);

                    }
                }
                circ.Location = Points[0].Loc;
                g.DrawEllipse(pen, circ);
                g.FillEllipse(brush, circ);


                if (isChosen)
                {
                    circ.Location = Points[iChosen].Loc;
                    pen.Color = Color.GreenYellow;
                    g.DrawEllipse(pen, circ);
                }
                WorkspacePictureBox.Refresh();
            }
            brush.Dispose();
            pen.Dispose();
        }

        private void WorkspacePictureBox_SizeChanged(object sender, EventArgs e)
        {
            Image nIm = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

            using (Graphics g = Graphics.FromImage(nIm))
            {
                g.DrawImage(WorkspacePictureBox.Image, new Point(0, 0));
                RedrawGraph(g);
            }
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = nIm;
        }


        private void WorkspacePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
            Point location = new Point(e.X - r / 2, e.Y - r / 2);
            if (e.Button == MouseButtons.Left)
            {

                if (Points.Count <= 0)
                    return;

                Rectangle circ = new Rectangle();
                //MyPoint closest = Points[0];
                // #1 ========|v|=========|^|
                int iClosest = 0;
                int closestDist = (int)(Math.Pow(Math.Abs(Points[iClosest].Loc.X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(Points[iClosest].Loc.Y + r / 2 - e.Y), 2));

                circ.Size = new Size(r, r);
                using (g)
                {
                    foreach (KeyValuePair<int, MyPoint> point in Points)
                    {
                        if (Math.Pow(Math.Abs(point.Value.Loc.X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(point.Value.Loc.Y + r / 2 - e.Y), 2) < closestDist)
                        {
                            iClosest = point.Key;
                            closestDist = (int)(Math.Pow(Math.Abs(point.Value.Loc.X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(point.Value.Loc.Y + r / 2 - e.Y), 2));
                        }

                        isEdgeChosen = false;
                        NoRestRB.Enabled = false;
                        HorizontalRB.Enabled = false;
                        VerticalRB.Enabled = false;
                        ConstLenRB.Enabled = false;
                        AddVerBut.Enabled = false;
                    }

                    if (closestDist < 800)
                    {
                        isChosen = true;
                        DeleteButton.Enabled = true;
                        iChosen = iClosest;
                        isBeingMoved = true;
                    }
                    else
                    {

                        // Checking whether we're choosing edge - useful?
                        //
                        //foreach (KeyValuePair<int, MyPoint> point in Points)
                        //{

                        //    if (IsOnLine(point.Value.Loc, Points[point.Value.INext].Loc, e.Location, 17))
                        //    {
                        //        isChosen = false;
                        //        iChosen = point.Key;
                        //        switch (Points[iChosen].NRest)
                        //        {
                        //            case 0:
                        //                NoRestRB.Checked = true;
                        //                break;
                        //            case 1:
                        //                HorizontalRB.Checked = true;
                        //                break;
                        //            case 2:
                        //                VerticalRB.Checked = true;
                        //                break;
                        //            case 3:
                        //                ConstLenRB.Checked = true;
                        //                break;
                        //        }
                        //        isEdgeChosen = true;
                        //        break;
                        //    }
                        //}
                        //    if (isEdgeChosen)
                        //    {
                        //        NoRestRB.Enabled = true;
                        //        HorizontalRB.Enabled = true;
                        //        VerticalRB.Enabled = true;
                        //        ConstLenRB.Enabled = true;
                        //        AddVerBut.Enabled = true;
                        //        DeleteButton.Enabled = false;

                        //    }
                        //    else
                        {
                            isChosen = false;
                            DeleteButton.Enabled = false;
                        }

                    }
                    WorkspacePictureBox.Refresh();
                    RedrawGraph(g);
                }
                mousePos = e.Location;
            }


        }



        private void SaveButton_Click(object sender, EventArgs e)
        {

            // TODO - UPDATE SAVE & OPEN according to structure of triangles

            SaveFileDialog save = new SaveFileDialog
            {
                FileName = "default.graph",
                Filter = "Graph files (*.graph)|*.graph"
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(save.FileName))
                    {
                        foreach (KeyValuePair<int, MyPoint> point in Points)
                            sw.WriteLine("{0},{1},{2},{3},{4}", point.Key, point.Value.Loc.X, point.Value.Loc.Y, point.Value.INext, point.Value.IPrev);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(GlobalStrings.SaveErrorMessageBox+$"\n{ex.Message}", "ERROR");
                    MessageBox.Show("Saving error:\n" + ex.Message, "ERROR");
                }
            }
            save.Dispose();

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {

            // TODO - UPDATE SAVE & OPEN according to structure of triangles

            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Graph files (*.graph)|*.graph"
            };
            if (open.ShowDialog() == DialogResult.OK && File.Exists(open.FileName))
            {
                try
                {
                    string[] pts = new string[7];
                    string word = "";
                    using (StreamReader sr = new StreamReader(open.FileName))
                    {
                        Points.Clear();
                        isChosen = false;
                        isEdgeChosen = false;
                        iChosen = -1;
                        WorkspacePictureBox.Image.Dispose();
                        WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

                        while ((word = sr.ReadLine()) != null)
                        {
                            pts = word.Split(',', '\n');
                            Points.Add(Int32.Parse(pts[0]), new MyPoint(new Point(Int32.Parse(pts[1]), Int32.Parse(pts[2])), Int32.Parse(pts[3]), Int32.Parse(pts[4])));
                            if (maxKey < Int32.Parse(pts[0]))
                                maxKey = Int32.Parse(pts[0]);
                        }
                        maxKey++;
                        WorkspacePictureBox.Image.Dispose();
                        WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                        RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(GlobalStrings.OpenErrorMessageBox+"\n" + ex.Message, "ERROR");
                    MessageBox.Show("Loading error:\n" + ex.Message, "ERROR");
                }
            }
            open.Dispose();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Points.Clear();
            maxKey = 0;
            iChosen = -1;
            isChosen = false;
            isEdgeChosen = false;
            DeleteButton.Enabled = false;
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
        }

        // Translating - probably useful
        //
        //private void PLButton_Click(object sender, EventArgs e)
        //{
        //    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");

        //    ApplyResourceToControl(this, new ComponentResourceManager(typeof(GraphEditor)), new System.Globalization.CultureInfo("pl-PL"));

        //}
        //private void ApplyResourceToControl(Control control, ComponentResourceManager cmp, System.Globalization.CultureInfo cultureInfo)
        //{
        //    foreach (Control child in control.Controls)
        //    {
        //        //Store current position and size of the control
        //        var childSize = child.Size;
        //        var childLoc = child.Location;
        //        //Apply CultureInfo to child control
        //        ApplyResourceToControl(child, cmp, cultureInfo);
        //        //Restore position and size
        //        child.Location = childLoc;
        //        child.Size = childSize;
        //    }
        //    //Do the same with the parent control
        //    var parentSize = control.Size;
        //    var parentLoc = control.Location;
        //    cmp.ApplyResources(control, control.Name, cultureInfo);
        //    control.Location = parentLoc;
        //    control.Size = parentSize;
        //}

        //private void ENButton_Click(object sender, EventArgs e)
        //{
        //    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-GB");

        //    ApplyResourceToControl(this, new ComponentResourceManager(typeof(GraphEditor)), new System.Globalization.CultureInfo("en-GB"));

        //}

        private void WorkspacePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (isBeingMoved && isChosen)
                {
                    isBeingMoved = false;
                    int nX = Points[iChosen].Loc.X;
                    int nY = Points[iChosen].Loc.Y;
                    int i = iChosen;
                    if (Points[iChosen].Loc.X < 0)
                        nX = -r / 2;
                    else if (Points[iChosen].Loc.X > WorkspacePictureBox.Width)
                        nX = WorkspacePictureBox.Width - r / 2;

                    if (Points[iChosen].Loc.Y < 0)
                        nY = -r / 2;
                    else if (Points[iChosen].Loc.Y > WorkspacePictureBox.Height)
                        nY = WorkspacePictureBox.Height - r / 2;

                    Points[iChosen].Loc.X = nX;
                    Points[iChosen].Loc.Y = nY;

                    mousePos = e.Location;
                    WorkspacePictureBox.Image.Dispose();
                    WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

                    RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
                }
                
            }
        }

        private void WorkspacePictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (isBeingMoved && isChosen)
            {
                int i = iChosen;
                int nX = Points[iChosen].Loc.X - mousePos.X + e.X;
                int nY = Points[iChosen].Loc.Y - mousePos.Y + e.Y;
                Points[iChosen].Loc.X = nX;
                Points[iChosen].Loc.Y = nY;
                
                mousePos = e.Location;
            }

            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

            RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
        }


        // Checking whether edge has been chosen - useful?
        //public bool IsOnLine(Point p1, Point p2, Point p, int width = 1)
        //{
        //    bool isOnLine = false;
        //    using (GraphicsPath path = new GraphicsPath())
        //    {
        //        using (var pen = new Pen(Brushes.Black, width))
        //        {
        //            path.AddLine(new Point(p1.X + r / 2, p1.Y + r / 2), new Point(p2.X + r / 2, p2.Y + r / 2));
        //            isOnLine = path.IsOutlineVisible(p, pen);
        //        }
        //    }
        //    return isOnLine;
        //}

        // Drawing string - useful?
        //
        //public void DrawString(string s, Point p, Color color)
        //{
        //    Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
        //    Font drawFont = new Font("Consolas", 10);
        //    SolidBrush drawBrush = new SolidBrush(color);
        //    StringFormat drawFormat = new StringFormat();
        //    g.DrawString(s, drawFont, drawBrush, p, drawFormat);
        //    drawFont.Dispose();
        //    drawBrush.Dispose();
        //    g.Dispose();
        //}

        public void BresenhamLine(int x, int y, int x2, int y2, Color color)
        {
            Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                //putpixel(x, y, color);
                g.FillRectangle(new SolidBrush(color), x, y, 3, 3);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

    }

    public class MyPoint
    {
        public Point Loc;
        public int INext { get; set; }      // index of next point
        public int IPrev { get; set; }      // index of previous point

        public MyPoint()
        {
            Loc = new Point();
            INext = -1;
            IPrev = -1;
        }

        public MyPoint(Point _point, int _iNext, int _iPrev)
        {
            Loc = _point;
            INext = _iNext;
            IPrev = _iPrev;
        }
    }

    public static class Prompt
    {
        // TODO - adjust to show "bubble"
        //
        public static double ShowDialog(string text, string caption, double length)
        {
            Form prompt = new Form();
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 200, Minimum = 1, Maximum = 9999, Value = (decimal)length };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return Decimal.ToDouble(inputBox.Value);
        }
    }
}
