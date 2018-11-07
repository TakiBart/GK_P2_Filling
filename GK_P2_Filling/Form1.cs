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

    // #1 Nie usuwać pierwszego wierzchołka - przeglądanie zaczyna się na twardo od 0.
    
    public partial class Form1 : Form
    {
        
        public Dictionary<int,MyPoint> Points = new Dictionary<int, MyPoint>();
        int maxKey = 0;

        int iChosen;
        bool isChosen = false;
        bool isEdgeChosen = false;
        
        bool isBeingMoved = false;
        Point mousePos;
        int r = 10;

        bool isCreating = true;
        bool isAutoNext = false;
        bool isAutoHor = false;
        bool isAutoDoing = false;

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
            //System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            circ.Size = new Size(r, r);
            using (g)
            {
                if (!isCreating)
                {
                    int prev = iChosen == -1 ? 0 : iChosen;
                    int next = Points[prev].INext;
                    CorrectPoint(prev, next, true);
                    while (true)
                    {
                        CorrectPoint(prev, Points[prev].IPrev, false);
                        CorrectPoint(next, Points[next].INext, true);
                        prev = Points[prev].IPrev;
                        next = Points[next].INext;
                        if (Points[prev].INext == next || Points[prev].INext == Points[next].IPrev)
                            break;
                    }
                }
                foreach (KeyValuePair<int, MyPoint> point in Points)
                {
                    if (point.Value.Loc.X < WorkspacePictureBox.Width && point.Value.Loc.Y < WorkspacePictureBox.Height)
                    {
                        if (!isCreating || point.Key != maxKey - 1)
                        {
                            if (isEdgeChosen && point.Key == iChosen)
                                pen.Color = Color.GreenYellow;
                            switch (point.Value.NRest)
                            {
                                case 1:
                                    DrawString("_", new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y + r) / 2), Color.Red);
                                    break;
                                case 2:
                                    DrawString("|", new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X + r) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y) / 2), Color.Red);
                                    break;
                                case 3:
                                    int length = (int)Math.Sqrt((point.Value.Loc.X - Points[point.Value.INext].Loc.X) * (point.Value.Loc.X - Points[point.Value.INext].Loc.X) + (point.Value.Loc.Y - Points[point.Value.INext].Loc.Y) * (point.Value.Loc.Y - Points[point.Value.INext].Loc.Y));
                                    //DrawString(length.ToString(), new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X + r) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y) / 2));
                                    DrawString(((int)point.Value.NLength).ToString(), new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X + r) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y) / 2), Color.Red);
                                    break;
                            }

                            if(isChosen && AutoRelCB.Checked && isAutoDoing)
                            {
                                if (point.Value.INext == iChosen && isAutoNext == false)
                                {
                                    if (isAutoHor)
                                    {
                                        DrawString("_", new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y + r) / 2), Color.Black);
                                    }
                                    else
                                    {
                                        DrawString("|", new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X + r) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y) / 2), Color.Black);
                                    }
                                    //isAutoDoing = false;
                                }
                                if (point.Key == iChosen && isAutoNext)
                                {
                                    if (isAutoHor)
                                    {
                                        DrawString("_", new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y + r) / 2), Color.Black);
                                    }
                                    else
                                    {
                                        DrawString("|", new Point((point.Value.Loc.X + Points[point.Value.INext].Loc.X + r) / 2, (point.Value.Loc.Y + Points[point.Value.INext].Loc.Y) / 2), Color.Black);
                                    }
                                    //isAutoDoing = false;
                                }
                            }
                            //isAutoDoing = false;

                            //g.DrawLine(pen, new Point(point.Value.Loc.X + r / 2, point.Value.Loc.Y + r / 2), new Point(Points[point.Value.INext].Loc.X + r / 2, Points[point.Value.INext].Loc.Y + r / 2));
                            BresenhamLine(point.Value.Loc.X + r / 2, point.Value.Loc.Y + r / 2, Points[point.Value.INext].Loc.X + r / 2, Points[point.Value.INext].Loc.Y + r / 2, pen.Color);
                        }

                        circ.Location = point.Value.Loc;
                        pen.Color = Color.Black;
                        brush.Color = Color.Black;

                        g.DrawEllipse(pen, circ);
                        
                        g.FillEllipse(brush, circ);
                        //brush.Color = p.color;
                        //g.DrawString((Points.IndexOf(p) + 1).ToString(), font, brush, new Point(p.point.X + r / 2 - font.Height / 2, p.point.Y + r / 2 - font.Height / 2));

                    }
                }
                if (!isCreating)
                {
                    circ.Location = Points[0].Loc;
                    g.DrawEllipse(pen, circ);
                    g.FillEllipse(brush, circ);
                }

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
                if (isCreating)
                {
                    if (Points.Count > 1 && AutoRelCB.Checked)
                    {
                        if (Math.Abs(location.Y - Points[maxKey - 1].Loc.Y) < 7 && Points[maxKey - 1].PRest != 1)
                        {
                            Points.Add(maxKey++, new MyPoint(location, maxKey, 0, maxKey - 2, 1));
                            Points[maxKey - 2].NRest = 1;
                        }
                        else if (Math.Abs(location.X - Points[maxKey - 1].Loc.X) < 7 && Points[maxKey-1].PRest != 2)
                        {
                            Points.Add(maxKey++, new MyPoint(location, maxKey, 0, maxKey - 2, 2));
                            Points[maxKey - 2].NRest = 2;
                        }
                        else
                            Points.Add(maxKey++, new MyPoint(location, maxKey, 0, maxKey - 2, 0));
                    }
                    else
                        Points.Add(maxKey++, new MyPoint(location, maxKey, 0, maxKey - 2, 0));
                    //WorkspacePictureBox.Image.Dispose();
                    //WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                    RedrawGraph(g);
                }
                else // isCreating == false
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
                            foreach (KeyValuePair<int, MyPoint> point in Points)
                            {

                                if (IsOnLine(point.Value.Loc, Points[point.Value.INext].Loc, e.Location, 17))
                                {
                                    isChosen = false;
                                    iChosen = point.Key;
                                    switch (Points[iChosen].NRest)
                                    {
                                        case 0:
                                            NoRestRB.Checked = true;
                                            break;
                                        case 1:
                                            HorizontalRB.Checked = true;
                                            break;
                                        case 2:
                                            VerticalRB.Checked = true;
                                            break;
                                        case 3:
                                            ConstLenRB.Checked = true;
                                            break;
                                    }
                                    isEdgeChosen = true;
                                    break;
                                }
                            }
                                if (isEdgeChosen)
                                {
                                    NoRestRB.Enabled = true;
                                    HorizontalRB.Enabled = true;
                                    VerticalRB.Enabled = true;
                                    ConstLenRB.Enabled = true;
                                    AddVerBut.Enabled = true;
                                    DeleteButton.Enabled = false;

                                }
                                else
                                {
                                    isChosen = false;
                                    DeleteButton.Enabled = false;
                                }
                            
                        }
                        WorkspacePictureBox.Refresh();
                        //WorkspacePictureBox.Image.Dispose();
                        //WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                        RedrawGraph(g);
                    }

                    
                    mousePos = e.Location;
                    
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
                if(isCreating)
                {
                    isCreating = false;
                    Points[maxKey - 1].INext = 0;
                    Points[0].IPrev = maxKey - 1;
                    RedrawGraph(g);
                }
            }
        }



        private void SaveButton_Click(object sender, EventArgs e)
        {
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
                            sw.WriteLine("{0},{1},{2},{3},{4},{5},{6}", point.Key, point.Value.Loc.X, point.Value.Loc.Y, point.Value.INext, point.Value.NRest, point.Value.IPrev, point.Value.PRest);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(GlobalStrings.SaveErrorMessageBox+$"\n{ex.Message}", "ERROR");
                    MessageBox.Show("Saving error:\n{ex.Message}", "ERROR");
                }
            }
            save.Dispose();

        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
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
                            Points.Add(Int32.Parse(pts[0]), new MyPoint(new Point(Int32.Parse(pts[1]), Int32.Parse(pts[2])), Int32.Parse(pts[3]), Int32.Parse(pts[4]), Int32.Parse(pts[5]), Int32.Parse(pts[6])));
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
            isCreating = true;
            DeleteButton.Enabled = false;
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if(isChosen)
            {
                DeletePoint();
            }
        }

        private void GraphEditor_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Delete)
            {
                DeletePoint();               
            }
        }

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
            if(e.Button == MouseButtons.Left)
            {
                if (!isCreating)
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

                        if (isChosen && AutoRelCB.Checked && isAutoDoing)
                        {
                            if (isAutoNext == false)
                            {
                                if (isAutoHor)
                                {
                                    if (Points[iChosen].NRest != 1 && Points[Points[iChosen].IPrev].PRest != 1)
                                    {
                                        Points[iChosen].PRest = 1;
                                        Points[Points[iChosen].IPrev].NRest = 1;
                                    }
                                }
                                else
                                {
                                    if (Points[iChosen].NRest != 2 && Points[Points[iChosen].IPrev].PRest != 2)
                                    {
                                        Points[iChosen].PRest = 2;
                                        Points[Points[iChosen].IPrev].NRest = 2;
                                    }
                                }
                                isAutoDoing = false;
                            }
                            if (isAutoNext)
                            {
                                if (isAutoHor)
                                {
                                    if (Points[iChosen].PRest != 1 && Points[Points[iChosen].INext].NRest != 1)
                                    {
                                        Points[iChosen].NRest = 1;
                                        Points[Points[iChosen].INext].PRest = 1;
                                    }
                                }
                                else
                                {
                                    if (Points[iChosen].PRest != 2 && Points[Points[iChosen].INext].NRest != 2)
                                    {
                                        Points[iChosen].NRest = 2;
                                        Points[Points[iChosen].INext].PRest = 2;
                                    }
                                }
                                isAutoDoing = false;
                            }
                        }

                        mousePos = e.Location;
                        WorkspacePictureBox.Image.Dispose();
                        WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                        CorrectPoint(iChosen, Points[iChosen].INext, true);
                        RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
                    }
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
                isAutoDoing = false;

                

                if (AutoRelCB.Checked)
                {
                    if (Math.Abs(nY - Points[Points[iChosen].INext].Loc.Y) < 7)       // Horizontal first
                    {
                        nY = Points[Points[iChosen].INext].Loc.Y;
                        isAutoDoing = true;
                        isAutoNext = true;
                        isAutoHor = true;
                    }
                    else if (Math.Abs(nX - Points[Points[iChosen].INext].Loc.X) < 7)
                    {
                        nX = Points[Points[iChosen].INext].Loc.X;
                        isAutoDoing = true;
                        isAutoNext = true;
                        isAutoHor = false;
                    }

                    if (Math.Abs(nY - Points[Points[iChosen].IPrev].Loc.Y) < 7)
                    {
                        nY = Points[Points[iChosen].INext].Loc.Y;
                        isAutoDoing = true;
                        isAutoNext = false;
                        isAutoHor = true;
                    }
                    else if (Math.Abs(nX - Points[Points[iChosen].IPrev].Loc.X) < 7)
                    {
                        nX = Points[Points[iChosen].INext].Loc.X;
                        isAutoDoing = true;
                        isAutoNext = false;
                        isAutoHor = false;
                    }

                }
                mousePos = e.Location;
                
            }
            
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
            if (isCreating)
            {
                if (Points.Count > 1 && AutoRelCB.Checked)
                {
                    if (Math.Abs(e.Location.Y - r/2 - Points[maxKey - 1].Loc.Y) < 7 && Points[maxKey - 1].PRest != 1)
                    {
                        DrawString("_", e.Location, Color.Black);
                        
                    }
                    else if (Math.Abs(e.Location.X - r/2 - Points[maxKey - 1].Loc.X) < 7 && Points[maxKey - 1].PRest != 2)
                    {
                        DrawString("|", e.Location, Color.Black);
                    }
                }
                
            }
            RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
        }

        private void NoRestRB_CheckedChanged(object sender, EventArgs e) // iChosen set to point before edge
        {
            if (NoRestRB.Checked)
            {
                Points[iChosen].NRest = 0;
                Points[Points[iChosen].INext].PRest = 0;
                WorkspacePictureBox.Image.Dispose();
                WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                using (Graphics g = Graphics.FromImage(WorkspacePictureBox.Image))
                    RedrawGraph(g);
            }
        }

        private void HorizontalRB_CheckedChanged(object sender, EventArgs e)
        {
            // Check previous and following
            if (HorizontalRB.Checked)
            {
                if (Points[iChosen].PRest != 1 && Points[Points[iChosen].INext].NRest != 1)
                {
                    Points[iChosen].NRest = 1;
                    Points[Points[iChosen].INext].PRest = 1;
                    WorkspacePictureBox.Image.Dispose();
                    WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                    using (Graphics g = Graphics.FromImage(WorkspacePictureBox.Image))
                        RedrawGraph(g);
                }
                else
                {
                    MessageBox.Show("There can't be two same restraints next to each other.", "ERROR");
                }
            }
        }

        private void VerticalRB_CheckedChanged(object sender, EventArgs e)
        {
            if (VerticalRB.Checked)
            {
                if (Points[iChosen].PRest != 2 && Points[Points[iChosen].INext].NRest != 2)
                {
                    Points[iChosen].NRest = 2;
                    Points[Points[iChosen].INext].PRest = 2;
                    WorkspacePictureBox.Image.Dispose();
                    WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                    using (Graphics g = Graphics.FromImage(WorkspacePictureBox.Image))
                        RedrawGraph(g);
                }
                else
                {
                    MessageBox.Show("There can't be two same restraints next to each other.", "ERROR");
                }
            }
        }

        private void ConstLenRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ConstLenRB.Checked)
            {
                Points[iChosen].NRest = 3;
                Points[Points[iChosen].INext].PRest = 3;
                double edgeLength = Math.Sqrt((Points[iChosen].Loc.X - Points[Points[iChosen].INext].Loc.X) * (Points[iChosen].Loc.X - Points[Points[iChosen].INext].Loc.X) + (Points[iChosen].Loc.Y - Points[Points[iChosen].INext].Loc.Y) * (Points[iChosen].Loc.Y - Points[Points[iChosen].INext].Loc.Y));

                double length = Prompt.ShowDialog("Wprowadź długość", "Długość krawędzi", edgeLength);
                Points[iChosen].NLength = length > 0 ? length : 50;
                WorkspacePictureBox.Image.Dispose();
                WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                using (Graphics g = Graphics.FromImage(WorkspacePictureBox.Image))
                    RedrawGraph(g);
            }
        }

        private void AddVerBut_Click(object sender, EventArgs e)
        {
            Points.Add(maxKey++, new MyPoint(
                new Point((Points[iChosen].Loc.X + Points[Points[iChosen].INext].Loc.X) / 2,
                          (Points[iChosen].Loc.Y + Points[Points[iChosen].INext].Loc.Y) / 2),
                Points[iChosen].INext,
                0,
                iChosen,
                0));
            Points[Points[iChosen].INext].PRest = 0;
            Points[Points[iChosen].INext].IPrev = maxKey - 1;
            Points[iChosen].NRest = 0;
            Points[iChosen].INext = maxKey - 1;
            using (Graphics g = Graphics.FromImage(WorkspacePictureBox.Image))
            {
                WorkspacePictureBox.Image.Dispose();
                WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                RedrawGraph(g);
            }
        }


        private void DeletePoint()
        {
            if (isChosen)
            {
                if (Points.Count <= 3)
                {
                    MessageBox.Show("Can't delete when there are less than 4 vertices.", "ERROR");
                    return;
                }

                if (iChosen == 0)
                {
                    MessageBox.Show("Can't delete first vertex.", "ERROR");
                    return;
                }

                Points[Points[iChosen].INext].IPrev = Points[iChosen].IPrev;
                Points[Points[iChosen].INext].PRest = 0;
                Points[Points[iChosen].IPrev].INext = Points[iChosen].INext;
                Points[Points[iChosen].IPrev].NRest = 0;

                Points.Remove(iChosen);

                iChosen = -1;
                isChosen = false;
                WorkspacePictureBox.Image.Dispose();
                WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
                RedrawGraph(Graphics.FromImage(WorkspacePictureBox.Image));
                DeleteButton.Enabled = false;
            }
        }

        public void CorrectPoint(int iPrev, int iNext, bool isNext) // iNext will be modified; isNext - which edge from iPrev should we check
        {
            int rest = isNext ? Points[iPrev].NRest : Points[iPrev].PRest;
            double nLength = isNext ? Points[iPrev].NLength : Points[iNext].NLength;
            double length, diff;
            switch (rest)
            {
                case 1: // HORIZONTAL
                    Points[iNext].Loc.Y = Points[iPrev].Loc.Y;
                    break;

                case 2: // VERTICAL
                    Points[iNext].Loc.X = Points[iPrev].Loc.X;
                    break;

                case 3: // CONST. LENGTH
                    length = Math.Sqrt((Points[iPrev].Loc.X - Points[iNext].Loc.X) * (Points[iPrev].Loc.X - Points[iNext].Loc.X) + (Points[iPrev].Loc.Y - Points[iNext].Loc.Y) * (Points[iPrev].Loc.Y - Points[iNext].Loc.Y));
                    while (Math.Abs(diff = (length - nLength)) > 0.9)
                    {
                        if (diff > 0)
                        {
                            Points[iNext].Loc.X += Points[iNext].Loc.X - Points[iPrev].Loc.X > 0 ? -1 : 1;
                            Points[iNext].Loc.Y += Points[iNext].Loc.Y - Points[iPrev].Loc.Y > 0 ? -1 : 1;
                        }
                        else // diff < 0
                        {
                            Points[iNext].Loc.X += Points[iNext].Loc.X - Points[iPrev].Loc.X > 0 ? 1 : -1;
                            Points[iNext].Loc.Y += Points[iNext].Loc.Y - Points[iPrev].Loc.Y > 0 ? 1 : -1;
                        }
                        length = Math.Sqrt((Points[iPrev].Loc.X - Points[iNext].Loc.X) * (Points[iPrev].Loc.X - Points[iNext].Loc.X) + (Points[iPrev].Loc.Y - Points[iNext].Loc.Y) * (Points[iPrev].Loc.Y - Points[iNext].Loc.Y));
                    }
                    break;
                default:

                    break;
            }
        }

        public bool IsOnLine(Point p1, Point p2, Point p, int width = 1)
        {
            bool isOnLine = false;
            using (GraphicsPath path = new GraphicsPath())
            {
                using (var pen = new Pen(Brushes.Black, width))
                {
                    path.AddLine(new Point(p1.X + r / 2, p1.Y + r / 2), new Point(p2.X + r / 2, p2.Y + r / 2));
                    isOnLine = path.IsOutlineVisible(p, pen);
                }
            }
            return isOnLine;
        }

        public void DrawString(string s, Point p, Color color)
        {
            Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
            Font drawFont = new Font("Consolas", 10);
            SolidBrush drawBrush = new SolidBrush(color);
            StringFormat drawFormat = new StringFormat();
            g.DrawString(s, drawFont, drawBrush, p, drawFormat);
            drawFont.Dispose();
            drawBrush.Dispose();
            g.Dispose();
        }

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
        public int NRest { get; set; }      // restraint of edge to next point
        public int IPrev { get; set; }      // index of previous point
        public int PRest { get; set; }      // restraint of edge to previous point
        public double NLength { get; set; }    // constant length of next edge (if NRest == 3)

        public MyPoint()
        {
            Loc = new Point();
            INext = -1;
            NRest = -1;
            IPrev = -1;
            PRest = -1;
            NLength = 0;
        }

        public MyPoint(Point _point, int _iNext, int _nRest, int _iPrev, int _pRest)
        {
            Loc = _point;
            INext = _iNext;
            NRest = _nRest;
            IPrev = _iPrev;
            PRest = _pRest;
            NLength = 50;
        }
    }

    public static class Prompt
    {
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
